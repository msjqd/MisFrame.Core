using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisFrame.Core.WebApi.Cache
{
    /// <summary>
    /// 内存缓存
    /// </summary>
    public class MemoryCaching : ICaching
    {
        // 这里使用框架自带内存缓存
        IMemoryCache Cache;

        // 这里要完成注入，需要在service中加入 
        // startup -> ConfigureServices -> services.AddMemoryCache();
        public MemoryCaching(IMemoryCache cache)
        {
            this.Cache = cache;
        }

        public object Get(string cacheKey)
        {
            return this.Cache.Get(cacheKey);
        }

        public void Set(string cacheKey, object cacheValue)
        {
            this.Cache.Set(cacheKey, cacheValue, TimeSpan.FromSeconds(7000));
        }
    }
}
