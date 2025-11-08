using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Interfaces;
using Notification.Application.Services;
using Notification.Infrastructure.Data;
using Notification.Infrastructure.NotificationSenders;
using Notification.Infrastructure.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Infrastructure.Extensions
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // Repositories
            services.AddScoped<INotificationRepository, NotificationRepository>();

            // Senders
            services.AddScoped<INotificationSender, EmailNotificationSender>();
            services.AddScoped<INotificationSender, SmsNotificationSender>();

            return services;
        }
    }

}