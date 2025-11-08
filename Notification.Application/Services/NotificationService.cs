using Microsoft.Extensions.Logging;
using Notification.Application.DTOs;
using Notification.Application.Interfaces;
using Notification.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Application.Services
{

    public class NotificationService : INotificationService
    {
        private readonly IEnumerable<INotificationSender> _senders;
        private readonly INotificationRepository _repository;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(IEnumerable<INotificationSender> senders, INotificationRepository repository, ILogger<NotificationService> logger)
        {
            _senders = senders;
            _repository = repository;
            _logger = logger;
        }

        private INotificationSender? ResolveSender(string type)
            => _senders.FirstOrDefault(s => s.Type.Equals(type, StringComparison.OrdinalIgnoreCase));

        public async Task<NotificationDto> SendAsync(SendNotificationRequest request, CancellationToken cancellationToken = default)
        {
            var notification = new Notification.Domain.Entities.Notification
            {
                UserId = request.UserId,
                Title = request.Title,
                Message = request.Message,
                Status = "Pending"
            };

            var config = await _repository.GetConfigAsync(cancellationToken);
            var typeToUse = request.Type ?? config?.DefaultType ?? "Email";
            var sender = ResolveSender(typeToUse) ?? throw new InvalidOperationException($"No sender for type {typeToUse}");

            notification.Type = typeToUse;

            await _repository.AddAsync(notification, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            try
            {
                _logger.LogInformation("Sending notification Type={Type} User={UserId}", typeToUse, request.UserId);
                await sender.SendAsync(notification, cancellationToken);

                notification.Status = "Sent";
                notification.SentAt = DateTime.UtcNow;
                _logger.LogInformation("Sent notification Id={Id}", notification.Id);
            }
            catch (Exception ex)
            {
                notification.Status = "Failed";
                _logger.LogError(ex, "Error sending notification");
            }
            finally
            {
                await _repository.SaveChangesAsync(cancellationToken);
            }

            return new NotificationDto(notification.Id, notification.UserId, notification.Type, notification.Title, notification.Message, notification.Status, notification.SentAt);
        }

        public async Task<IEnumerable<NotificationDto>> GetHistoryAsync(CancellationToken cancellationToken = default)
        {
            var list = await _repository.GetAllAsync(cancellationToken);
            return list.Select(n => new NotificationDto(n.Id, n.UserId, n.Type, n.Title, n.Message, n.Status, n.SentAt));
        }

        public async Task SetConfigAsync(string defaultType, CancellationToken cancellationToken = default)
        {
            var config = new NotificationConfig { DefaultType = defaultType };
            await _repository.SetConfigAsync(config, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }
}
