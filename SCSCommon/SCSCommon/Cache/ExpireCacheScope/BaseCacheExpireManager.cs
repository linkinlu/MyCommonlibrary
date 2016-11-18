using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSCommon.Cache.ExpireCacheScope
{
    public abstract class BaseCacheExpireManager : ICacheExpireScope
    {


        public abstract void Add(string key, object data, TimeSpan expireTime, IEnumerable<string> Tags = null);

        public abstract T Get<T>(string key);

        public abstract bool ContainKey(string key);

        public abstract void Clear();

        public abstract void Remove(string key);

        public abstract void RemoveByTags(IEnumerable<string> tags);

        public abstract void Dispose();

    }
}
