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

    }
}
