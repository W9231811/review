using Biu.Projects.Cores.Registry.Options;
using Consul;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biu.Projects.Cores.Registry.Consul
{
    /// <summary>
    /// consul服务注册实现
    /// </summary>
    public class ConsulRegistry : IServiceRegistry
    {
        //服务注册的参数
        public readonly ServiceRegistryOptions serviceRegistryOptions;
        public ConsulRegistry(IOptions<ServiceRegistryOptions> options)
        {
            this.serviceRegistryOptions = options.Value;
        }
        /// <summary>
        /// 服务注册
        /// </summary>
        public void Register()
        {
            //1 创建consul客户端连接
            var consulClient = new ConsulClient(configuration =>
              {
                  configuration.Address = new Uri(serviceRegistryOptions.RegistryAddress);
              });
            //2 获取服务地址
            var uri = new Uri(serviceRegistryOptions.ServiceAddress);
            //3 创建consul服务注册对象
            var registration = new AgentServiceRegistration()
            {
                ID = string.IsNullOrEmpty(serviceRegistryOptions.ServiceId) ? Guid.NewGuid().ToString() : serviceRegistryOptions.ServiceId,
                Name = serviceRegistryOptions.ServiceName,
                Address = uri.Host,
                Port = uri.Port,
                Tags = serviceRegistryOptions.ServiceTags,
                Check = new AgentServiceCheck
                {
                    Timeout = TimeSpan.FromSeconds(10),
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    HTTP = $"{uri.Scheme}://{uri.Host}:{uri.Port}{serviceRegistryOptions.HealthCheckAddress}",
                    Interval = TimeSpan.FromSeconds(10),
                }
            };
            //4 注册服务
            consulClient.Agent.ServiceRegister(registration).Wait();
            Console.WriteLine($"服务注册成功:{serviceRegistryOptions.ServiceAddress}");
            //5  关闭连接
            consulClient.Dispose();
        }
        public void Deregister()
        {
            var consulClient = new ConsulClient(configuration =>
              {
                  configuration.Address = new Uri(serviceRegistryOptions.RegistryAddress);
              });
            consulClient.Agent.ServiceDeregister(serviceRegistryOptions.ServiceId).Wait();
            Console.WriteLine($"服务注销成功:{serviceRegistryOptions.ServiceAddress}");
            consulClient.Dispose();
        }

     
    }
}
