using Microsoft.Extensions.DependencyInjection;

namespace UserManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            service.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            return service;
        }
    }
}
