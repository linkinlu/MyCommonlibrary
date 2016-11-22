using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace SCSCommon.Cache.ExpireCacheScope
{
    public class HttpContextCacheManager : ICacheExpireScope
    {

        private List<string> keys = new List<string>();
        public  System.Web.Caching.Cache Cache
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    throw new Exception("Cache should be in HttpModule");
                }
                return HttpContext.Current.Cache;
            }
        }

        public  void Clear()
        {
            foreach (string key in Cache)
            {
                Remove(key);
            }
            keys.Clear();
        }

        public  bool ContainKey(string key)
        {
            return keys.Contains(key);
        }

        public  void Dispose()
        {
            keys = null;
        }

        public  void Add(string key, object data, TimeSpan? expireTime=null)
        {
            if (!keys.Contains(key))
            {
                Cache.Add(key, data, null, System.Web.Caching.Cache.NoAbsoluteExpiration
                    , (expireTime ?? TimeSpan.MaxValue), CacheItemPriority.Normal, null);
            }
        }

        public  T Get<T>(string key)
        {
            return (T) Cache.Get(key);
        }

        public  void Remove(string key)
        {
            Cache.Remove(key);
            keys.Remove(key);
        }

        public void RemoveByPattern(string parttern)
        {
            this.RemovebyPattern(parttern, keys);
        }
    }
}
