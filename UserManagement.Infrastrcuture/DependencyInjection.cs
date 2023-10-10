using Microsoft.EntityFrameworkCore;
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
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IServiceBusQueueService, ServiceBusQueueService>();
            services.AddDbContext<UserContext>(o => o.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName)));
            return services;
        }
    }
}
