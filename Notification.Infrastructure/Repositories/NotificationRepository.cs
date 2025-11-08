using Microsoft.EntityFrameworkCore;
using Notification.Application.Interfaces;
using Notification.Domain.Entities;
using Notification.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Infrastructure.Repositories
{

    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _db;
        public NotificationRepository(AppDbContext db) => _db = db;

        public async Task AddAsync(Notification.Domain.Entities.Notification notification, CancellationToken cancellationToken = default)
        {
            await _db.Notifications.AddAsync(notification, cancellationToken);
        }

        public async Task<IEnumerable<Notification.Domain.Entities.Notification>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Notifications.AsNoTracking().OrderByDescending(n => n.SentAt).ToListAsync(cancellationToken);
        }

        public async Task<NotificationConfig?> GetConfigAsync(CancellationToken cancellationToken = default)
        {
            return await _db.NotificationConfigs.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task SetConfigAsync(NotificationConfig config, CancellationToken cancellationToken = default)
        {
            // ساده: پاک کن و اضافه کن
            var all = _db.NotificationConfigs;
            _db.NotificationConfigs.RemoveRange(all);
            await _db.NotificationConfigs.AddAsync(config, cancellationToken);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
            => _db.SaveChangesAsync(cancellationToken);
    }
}
