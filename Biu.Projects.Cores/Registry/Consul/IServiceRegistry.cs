using System;
using System.Collections.Generic;
using System.Text;

namespace Biu.Projects.Cores.Registry.Consul
{
   public interface IServiceRegistry
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        void Register();
        /// <summary>
        /// 撤销服务
        /// </summary>
        void Deregister();
    }
}
