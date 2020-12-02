using Biu.Projects.Cores.Cluster.Options;
using Biu.Projects.Cores.HttpClientPolic;
using Biu.Projects.Cores.Registry.Options;
using System;

namespace Biu.Projects.Cores.Middleware.options
{
    /// <summary>
    /// 中台配置选项
    /// </summary>
    public class MiddlewareOptions
    {
        public MiddlewareOptions()
        {
            this.HttpClientName = "Micro";
            pollyHttpClientOptions = options => { };
        }

        /// <summary>
        /// polly熔断降级选项
        /// </summary>
        public Action<PollyHttpClientOptions> pollyHttpClientOptions { get; }

        /// <summary>
        /// 客户端名称
        /// </summary>
        public string HttpClientName { set; get; }

    }
}
