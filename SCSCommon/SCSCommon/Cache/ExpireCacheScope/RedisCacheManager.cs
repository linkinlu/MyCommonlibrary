using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSCommon.Cache.ExpireCacheScope
{
    public class RedisCacheManager//: ICacheExpireScope
    {
        public   void Add(string key, object data, TimeSpan expireTime)
        {
            
        }

        //public  T Get<T>(string key)
        //{
        //    return null;
        //}

        public  bool ContainKey(string key)
        {
            return false;
        }

        public  void Clear()
        {
           
        }

        public  void Remove(string key)
        {
           
        }

   

        public  void Dispose()
        {
           
        }
    }
}
