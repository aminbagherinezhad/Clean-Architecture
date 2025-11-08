using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Application.DTOs
{
    public record NotificationDto(int Id, int UserId, string Type, string Title, string Message, string Status, DateTime? SentAt);
}
