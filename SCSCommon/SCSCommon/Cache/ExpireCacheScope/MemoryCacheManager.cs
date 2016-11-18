using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace SCSCommon.Cache.ExpireCacheScope
{
    public class MemoryCacheManager : BaseCacheExpireManager
    {
        private ConcurrentDictionary<string, List<string>> tagContainer = new ConcurrentDictionary<string, List<string>>();
        protected ObjectCache InternalCache
        {
            get { return MemoryCache.Default; }
        }

        public override void Add(string key, object data, TimeSpan expireTime,IEnumerable<String> tags = null)
        {
            if (data == null)
                return;

            if (!InternalCache.Contains(key))
            {
                InternalCache.Add(new CacheItem(key, data), new CacheItemPolicy() { AbsoluteExpiration = DateTime.Now + expireTime });
                if (tags != null && tags.Any())
                {
                    tags.ToList().ForEach(t =>
                    {
                        if (!tagContainer.ContainsKey(t))
                        {
                            //first add
                            var keys = new List<string>() {key};
                            tagContainer.TryAdd(t, keys);
                        }
                        else
                        {
                            if (!tagContainer[t].Contains(key))
                            {
                                tagContainer[t].Add(key);
                            }
                        }

                    });
                }
            }
        }

        public override T Get<T>(string key)
        {
            return InternalCache.Contains(key) ? (T)InternalCache.Get(key) : default(T);
        }

        public override bool ContainKey(string key)
        {
            return InternalCache.Contains(key);
        }

        public override void Clear()
        {
            foreach (var item in InternalCache)
            {
                this.Remove(item.Key);
            }

        }

       
        public override void Remove(string key)
        {
            InternalCache.Remove(key);

            foreach (var item in tagContainer)
            {
                //when tag container has only one cache key, it should also remove the tag
                if (item.Value.Contains(key) && item.Value.Count == 1)
                {
                    List<string> result;
                    tagContainer.TryRemove(item.Key, out result);
                }
               
            }

        }




        public override void RemoveByTags(IEnumerable<string> tags)
        {
            tags.ToList().ForEach(t =>
            {
                if (tagContainer.ContainsKey(t))
                {
                    List<string> result;
                    if (tagContainer.TryRemove(t, out result))
                    {
                        if (result != null && result.Any())
                        {
                            result.ForEach(key => InternalCache.Remove(key));
                        }
                    }
                }
            });
        }

        public override void Dispose()
        {
            tagContainer = null;
            
        }
    }
}
