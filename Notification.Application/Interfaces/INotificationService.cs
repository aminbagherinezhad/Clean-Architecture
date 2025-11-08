using Notification.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Application.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationDto> SendAsync(SendNotificationRequest request, CancellationToken cancellationToken = default);
        Task<IEnumerable<NotificationDto>> GetHistoryAsync(CancellationToken cancellationToken = default);
        Task SetConfigAsync(string defaultType, CancellationToken cancellationToken = default);
    }
}
