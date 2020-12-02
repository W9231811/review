using Biu.Projects.Cores.Registry.Consul;
using Biu.Projects.Cores.Registry.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biu.Projects.Cores.Registry.Extensions
{
 public static   class ServiceDiscoveryExtensions
    {
        public static IServiceCollection AddServiceDiscovery(this IServiceCollection services)
        {
            AddServiceDiscovery(services, options => { });
            return services;
        }
        public static IServiceCollection AddServiceDiscovery(this IServiceCollection services,
            Action<ServiceDiscoveryOption> options)
        {
            services.Configure<ServiceDiscoveryOption>(options);
            services.AddSingleton<IServiceDiscovery, ConsulDiscovery>();
            return services;
        }
    }
}
