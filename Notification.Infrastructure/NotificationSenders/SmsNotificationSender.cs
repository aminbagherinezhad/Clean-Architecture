using Microsoft.Extensions.Logging;
using Notification.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Infrastructure.NotificationSenders
{
    public class SmsNotificationSender : INotificationSender
    {
        public string Type => "SMS";
        private readonly ILogger<SmsNotificationSender> _logger;
        public SmsNotificationSender(ILogger<SmsNotificationSender> logger) => _logger = logger;

        public Task SendAsync(Notification.Domain.Entities.Notification notification, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("[SMS] User={UserId} Message={Message}", notification.UserId, notification.Message);
            Console.WriteLine($"[SMS] User {notification.UserId} : {notification.Message}");
            return Task.CompletedTask;
        }
    }
}
