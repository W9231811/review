using Microsoft.Extensions.DependencyInjection;
using Biu.Projects.Cores.Cluster.Extentions;
using Biu.Projects.Cores.HttpClientPolly;
using Biu.Projects.Cores.Middleware.options;
using Biu.Projects.Cores.Middleware.support;
using Biu.Projects.Cores.Middleware.transports;
using Biu.Projects.Cores.Middleware.Urls;
using System;
using Biu.Projects.Cores.Registry.Extensions;
using Biu.Projects.Cores.DynamicMiddleware.Urls;

namespace Biu.Projects.Cores.Middleware.Extentions
{
    /// <summary>
    ///  中台ServiceCollection扩展方法
    /// </summary>
    public static class DynamicMiddlewareServiceCollectionExtensions
    {
        /// <summary>
        /// 添加动态中台
        /// </summary>
        /// <typeparam name="IMiddleService"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDynamicMiddleware<TMiddleService, TMiddleImplementation>(this IServiceCollection services)
            where TMiddleService : class
            where TMiddleImplementation : class, TMiddleService
        {
            AddDynamicMiddleware<TMiddleService, TMiddleImplementation>(services, options => {});
            return services;
        }

        /// <summary>
        /// 添加动态中台
        /// </summary>
        /// <typeparam name="IMiddleService"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDynamicMiddleware<TMiddleService, TMiddleImplementation>(this IServiceCollection services, Action<DynamicMiddlewareOptions> options)
            where TMiddleService : class
            where TMiddleImplementation : class, TMiddleService
        {
            DynamicMiddlewareOptions dynamicMiddlewareOptions = new DynamicMiddlewareOptions();
            options(dynamicMiddlewareOptions);

            // 1、注册服务发现
            services.AddServiceDiscovery(dynamicMiddlewareOptions.serviceDiscoveryOptions);

            // 2、注册负载均衡
            services.AddLoadBalance(dynamicMiddlewareOptions.loadBalanceOptions);

            // 3、添加中台服务
            services.AddMiddleware(dynamicMiddlewareOptions.middlewareOptions);

            // 4、注册动态中台Url服务
            services.AddSingleton<IDynamicMiddleUrl, DefaultDynamicMiddleUrl>();

            // 5、注册动态服务
            services.AddSingleton<TMiddleService, TMiddleImplementation>();
            return services;
        }
    }
}
