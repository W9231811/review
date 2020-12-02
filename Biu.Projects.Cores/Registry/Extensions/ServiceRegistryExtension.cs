using Biu.Projects.Cores.Registry.Consul;
using Biu.Projects.Cores.Registry.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biu.Projects.Cores.Registry.Extensions
{
  public static  class ServiceRegistryExtension
    {
        public static IServiceCollection AddServiceDiscovery(this IServiceCollection services)
        {
            AddServiceRegistry(services,options=> { });
            return services;
        }
        public static IServiceCollection AddServiceRegistry(this IServiceCollection services, Action<ServiceRegistryOptions> options)
        {
            //1.配置到ioc
            services.Configure<ServiceRegistryOptions>(options);
            //注册consul
            services.AddSingleton<IServiceRegistry, ConsulRegistry>();
            return services;
        }
    }
}
