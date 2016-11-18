using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSCommon.Cache.ExpireCacheScope
{
    public class HttpContextCacheManager : BaseCacheExpireManager
    {
        public override void Add(string key, object data, TimeSpan expireTime, IEnumerable<string> Tags)
        {
            throw new NotImplementedException();
        }

        public override void Clear()
        {
            throw new NotImplementedException();
        }

        public override bool ContainKey(string key)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public override T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public override void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public override void RemoveByTags(IEnumerable<string> tags)
        {
            throw new NotImplementedException();
        }
    }
}
