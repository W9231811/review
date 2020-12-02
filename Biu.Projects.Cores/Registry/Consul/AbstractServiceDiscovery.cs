using Biu.Projects.Cores.Registry.Options;
using Consul;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Biu.Projects.Cores.Registry.Consul
{
    /// <summary>
    /// 缓存服务发现
    /// </summary>
    public abstract class AbstractServiceDiscovery : IServiceDiscovery
    {
        private readonly Dictionary<string, List<ServiceNode>> CacheConsulResult = new Dictionary<string, List<ServiceNode>>();
        protected readonly ServiceDiscoveryOption serviceDiscoveryOption;
        public AbstractServiceDiscovery(IOptions<ServiceDiscoveryOption> options)
        {
            this.serviceDiscoveryOption = options.Value;
            //1 创建consul客户端连接
            var consulClient = new ConsulClient(configuration =>
              {
                  configuration.Address = new Uri(serviceDiscoveryOption.DiscoveryAddress);
              });
            //2 consul先查询服务
            var queryResult = consulClient.Catalog.Services().Result;
            if(!queryResult.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new Exception($"consul连接失败:{queryResult.StatusCode}");
            }
            //3 获取服务下的所有实例
            foreach (var item in queryResult.Response)
            {
                QueryResult<CatalogService[]> result = consulClient.Catalog.Service(item.Key).Result;
                var list = new List<ServiceNode>();
                foreach (var service in result.Response)
                {
                    list.Add(new ServiceNode { Url = service.ServiceAddress + ":" + service.ServicePort });
                }
                CacheConsulResult.Add(item.Key, list);
            }
        }
        public List<ServiceNode> Discovery(string serviceName)
        {
           //1 从缓存中查询consul结果
           if(CacheConsulResult.ContainsKey(serviceName))
            {
                return CacheConsulResult[serviceName];
            }
           else
            {
                //2 从远程服务器获取
                CatalogService[] queryResult = RemoteDiscovery(serviceName);
                var list = new List<ServiceNode>();
                foreach (var service in queryResult)
                {
                    list.Add(new ServiceNode { Url=service.ServiceAddress+":"+service.ServicePort});
                }
                //3 将结果添加到缓存
                CacheConsulResult.Add(serviceName, list);
                return list;
            }
        }
        protected abstract CatalogService[] RemoteDiscovery(string service);
    }
}
