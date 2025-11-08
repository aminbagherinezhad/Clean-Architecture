using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Domain.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; } = default!; // Email, SMS
        public string Title { get; set; } = default!;
        public string Message { get; set; } = default!;
        public DateTime? SentAt { get; set; }
        public string Status { get; set; } = "Pending";
    }
}
