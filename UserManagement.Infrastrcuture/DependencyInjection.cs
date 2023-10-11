using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Interfaces;
using UserManagement.Infrastrcuture.ExternalService;
using UserManagement.Infrastrcuture.Repositories;

namespace UserManagement.Infrastrcuture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IServiceBusQueueService, ServiceBusQueueService>();
            services.AddDbContext<UserContext>(o => o.UseSqlServer(config.GetConnectionString("Default"), b => b.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName)));
            services.AddAzureClients(b => b.AddServiceBusClient(config.GetSection("Azure:ServiceBusConnection").Value));
            return services;
        }
    }
}
