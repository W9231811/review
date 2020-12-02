using System;
using System.Collections.Generic;
using System.Text;

namespace Biu.Projects.Cores.Registry.Options
{
   public class ServiceRegistryOptions
    {
        public ServiceRegistryOptions()
        {

        }
        //服务ID
        public string ServiceId { get; set; }
        //服务名称
        public string ServiceName { get; set; }
        //服务地址 http:127.0.0.1:5001
        public string ServiceAddress { get; set; }
        //服务标签
        public string[] ServiceTags { get; set; }
        //服务注册地址
        public string RegistryAddress { get; set; }
        //服务健康检查地址
        public string HealthCheckAddress { get; set; }

    }
}
