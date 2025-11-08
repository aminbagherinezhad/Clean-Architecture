using Notification.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Application.Interfaces
{
    public interface INotificationRepository
    {
        Task AddAsync(Notification.Domain.Entities.Notification notification, CancellationToken cancellationToken = default);
        Task<IEnumerable<Notification.Domain.Entities.Notification>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<NotificationConfig?> GetConfigAsync(CancellationToken cancellationToken = default);
        Task SetConfigAsync(NotificationConfig config, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
