using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSCommon.Cache.ExpireCacheScope
{
    /// <summary>
    /// 有过期机制的缓存
    /// </summary>
    interface ICacheExpireScope : IDisposable
    {



        void Add(string key, object data, TimeSpan? expireTime = null);

        T Get<T>(string key);

        bool ContainKey(string key);

        void Clear();

        void Remove(string key);

        void RemoveByPattern(string parttern);

    }
}
