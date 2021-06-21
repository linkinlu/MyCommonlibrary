using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace SCSCommon.Cache.ExpireCacheScope
{
    public class MemoryCacheManager : ICacheExpireScope
    {
        //private ConcurrentDictionary<string,string> tagContainer = new ConcurrentDictionary<string, string>();
        protected ObjectCache InternalCache => MemoryCache.Default;

        public  void Add(string key, object data, TimeSpan? expireTime=null)
        {
            if (data == null)
                return;

            if (!InternalCache.Contains(key))
            {
                InternalCache.Add(new CacheItem(key, data),
                    new CacheItemPolicy()
                    {
                        AbsoluteExpiration = DateTime.Now + (expireTime ?? TimeSpan.MaxValue)
                    });
            }
        }

        public  T Get<T>(string key)
        {
            return InternalCache.Contains(key) ? (T)InternalCache.Get(key) : default(T);
        }

        public  bool ContainKey(string key)
        {
            return InternalCache.Contains(key);
        }

        public  void Clear()
        {
            foreach (var item in InternalCache)
            {
                this.Remove(item.Key);
            }

        }

       
        public void Remove(string key)
        {
            InternalCache.Remove(key);
        }

        public void RemoveByPattern(string parttern)
        {
            if (!string.IsNullOrEmpty(parttern))
            {
                this.RemovebyPattern(parttern, InternalCache.Select(x => x.Key));
            }
        }


        public void Dispose()
        {
          
            
        }
    }
}
