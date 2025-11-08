using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Application.DTOs
{
    public record SendNotificationRequest(int UserId, string Title, string Message, string? Type);
}
