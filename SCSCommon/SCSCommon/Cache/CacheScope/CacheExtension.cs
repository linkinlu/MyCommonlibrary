using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SCSCommon.Cache.ExpireCacheScope
{
    public static class CacheExtension
    {
        internal static void RemovebyPattern(this ICacheExpireScope scope, string pattern, IEnumerable<string> keys)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            keys.Where(p => regex.IsMatch(p.ToString())).ToList().ForEach(scope.Remove);
        }

        /// <summary>
        /// 根据key找到value  如果找不到value 就回调acquire的值并且缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scope"></param>
        /// <param name="key"></param>
        /// <param name="expiretime"></param>
        /// <param name="acquire"></param>
        public static T Get<T>(this ICacheExpireScope scope, string key,  Func<T> acquire , TimeSpan? expiretime = null)
        {
            if (scope.ContainKey(key) && scope.Get<T>(key) != null)
            {
                return scope.Get<T>(key);
            }

            var cacheItem = acquire();
            if (cacheItem != null)
            {
                scope.Add(key, cacheItem, expiretime.HasValue ? expiretime.Value : TimeSpan.MaxValue);
            }
            return cacheItem;
        }

    }
}
