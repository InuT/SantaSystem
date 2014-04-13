using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Caching;

namespace CacheService
{
    class MemoryCacheGenerator
    {
        private readonly MemoryCache memoryCache;

        private static readonly string MEMORY_CACHE_NAME = "SantaCache";

        public MemoryCacheGenerator(string pollingInterval, string cacheMemoryLimitMegabytes, string physicalMemoryLimitPercentage)
        {
            var parameters = new NameValueCollection();

            parameters.Add("pollingInterval", pollingInterval);

            parameters.Add("cacheMemoryLimitMegabytes", cacheMemoryLimitMegabytes);

            parameters.Add("physicalMemoryLimitPercentage", physicalMemoryLimitPercentage);

            memoryCache = new MemoryCache(MEMORY_CACHE_NAME, parameters);
        }

        public MemoryCache MemoryCache
        { 
            get { return memoryCache; } 
        }
    }
}
