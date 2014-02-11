using System;
using System.Collections;
using System.Runtime.Caching;

namespace CacheService
{
    class CacheService
    {
        private readonly MemoryCache memoryCache;

        private readonly CacheItemPolicy cacheItemPolicy;

        private CacheEntryRemovedCallback callback;
        
        public CacheService(string pollingInterval, string limitMegabytes, string limitPercentage, double storingPeriod)
        {
            memoryCache = new MemoryCacheGenerator(pollingInterval, limitMegabytes, limitPercentage).MemoryCache;

            callback = new CacheEntryRemovedCallback(this.LoggingRemovedCallback);

            cacheItemPolicy = new CacheItemPolicy();
            
            cacheItemPolicy.RemovedCallback = callback; 
            
            cacheItemPolicy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(storingPeriod);
        }

        public void AddCache(string cacheKey, Object item, CacheItemPolicy cacheItemPolicy)
        {
            if(!CheckCache(cacheKey))
            {
                memoryCache.Add(cacheKey, item, cacheItemPolicy);
            }
        }

        public object GetCache(string cacheKey)
        {
            if(CheckCache(cacheKey))
            {
                return memoryCache.Get(cacheKey);
            }

            return null;
        }

        public void RemoveCach(string cacheKey)
        {
            if(memoryCache.Contains(cacheKey))
            {
                memoryCache.Remove(cacheKey);
            }
        }

        private void LoggingRemovedCallback(CacheEntryRemovedArguments arguments)
        {
            String log = String.Concat("Reason: ", arguments.RemovedReason.ToString(), 
                " | Key: ", arguments.CacheItem.Key, " | Value: ", arguments.CacheItem.Value.ToString()); 
        }

        private Boolean CheckCache(string cacheKey)
        {
            if (memoryCache.Contains(cacheKey))
            {
                return false;
            }

            return true;
        }
    }
}
