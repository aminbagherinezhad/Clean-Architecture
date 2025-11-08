using Microsoft.Extensions.Logging;
using Notification.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Infrastructure.NotificationSenders
{

    public class EmailNotificationSender : INotificationSender
    {
        public string Type => "Email";
        private readonly ILogger<EmailNotificationSender> _logger;
        public EmailNotificationSender(ILogger<EmailNotificationSender> logger) => _logger = logger;

        public Task SendAsync(Notification.Domain.Entities.Notification notification, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("[EMAIL] User={UserId} Title={Title} Message={Message}", notification.UserId, notification.Title, notification.Message);
            Console.WriteLine($"[EMAIL] User {notification.UserId} : {notification.Title} - {notification.Message}");
            return Task.CompletedTask;
        }
    }
}
