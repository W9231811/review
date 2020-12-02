using Biu.Projects.Cores.Registry.Options;
using Consul;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Biu.Projects.Cores.Registry.Consul
{
    public class ConsulDiscovery : AbstractServiceDiscovery
    {
        public ConsulDiscovery(IOptions<ServiceDiscoveryOption> options):base(options)
        {

        }
        protected override CatalogService[] RemoteDiscovery(string serviceName)
        {
            //1 创建consul客户端连接
            var consulClient = new ConsulClient(configuration =>
              {
                  configuration.Address = new Uri(serviceDiscoveryOption.DiscoveryAddress);
              });
            //2 根据服务名称查询
            var queryResult = consulClient.Catalog.Service(serviceName).Result;
            //3 判断请求是否失败
            if(queryResult.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new Exception($"consul连接失败:{queryResult.StatusCode}");
            }
            return queryResult.Response;
        }
    }
}
