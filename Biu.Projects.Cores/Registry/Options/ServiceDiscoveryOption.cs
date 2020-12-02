using System;
using System.Collections.Generic;
using System.Text;

namespace Biu.Projects.Cores.Registry.Options
{
  public  class ServiceDiscoveryOption
    {
        public ServiceDiscoveryOption()
        {
            this.DiscoveryAddress = "http://localhost:8500";
        }
        public string DiscoveryAddress { get; set; }
    }
}
