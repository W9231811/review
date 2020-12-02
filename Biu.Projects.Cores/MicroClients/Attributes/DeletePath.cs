using System;

namespace Biu.Projects.Cores.Proxy.Attributes
{
    /// <summary>
    /// Delete请求特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DeletePath : Attribute
    {
        // 请求路径
        public string Path { get; }
        public DeletePath(string Path)
        {
            this.Path = Path;
        }
    }
}
