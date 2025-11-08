using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Application.Interfaces
{
    public interface INotificationSender
    {
        string Type { get; }
        Task SendAsync(Notification.Domain.Entities.Notification notification, CancellationToken cancellationToken = default);
    }
}
