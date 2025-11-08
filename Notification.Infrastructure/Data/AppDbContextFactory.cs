using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // ⚠️ این Connection String باید با connectionString در appsettings.json یکی باشه
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=NotificationDb;Username=postgres;Password=amin061bvb");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
