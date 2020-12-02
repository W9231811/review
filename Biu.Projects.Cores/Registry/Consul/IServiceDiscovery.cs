using System;
using System.Collections.Generic;
using System.Text;

namespace Biu.Projects.Cores.Registry.Consul
{
    public interface IServiceDiscovery
    {
        List<ServiceNode> Discovery(string serviceName);
    }
}
