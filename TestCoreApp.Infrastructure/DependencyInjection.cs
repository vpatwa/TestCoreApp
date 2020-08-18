using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TestCoreApp.Domain.Persistence;
using TestCoreApp.Infrastructure.Domain;
using TestCoreApp.Infrastructure.Persistence;

namespace CRM.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ISqlConnectionFactory>(sp => new SqlConnectionFactory(configuration.GetConnectionString("TestCoreConnectionString")));

            var assemblies = typeof(RateChartRepository).Assembly;
            foreach (Type type in assemblies.GetTypes().Where(a => a.Name.EndsWith("Repository") && !a.IsInterface))
            {
                var interfaceType = type.GetInterface(string.Format("I{0}", type.Name));
                services.Add(new ServiceDescriptor(interfaceType, type, ServiceLifetime.Transient));
            }
            return services;
        }
    }
}
