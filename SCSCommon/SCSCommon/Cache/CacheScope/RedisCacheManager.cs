using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SCSCommon.Cache.ExpireCacheScope
{
    //Not Implment yet
    public class RedisCacheManager: ICacheExpireScope
    {

        private IDatabase dbContext;
        public   void Add(string key, object data, TimeSpan? expireTime=null)
        {
            if (data == null) return;

            dbContext.StringSet(key, Serialize(data), (expireTime ?? TimeSpan.MaxValue));
        }

        protected virtual byte[] Serialize(object item)
        {
            var jsonString = JsonConvert.SerializeObject(item);
            return Encoding.UTF8.GetBytes(jsonString);
        }

        protected virtual T DeSerialize<T>(byte[] jsonBtye)
        {

            if (jsonBtye == null)
                return default(T);

            var jsonString = Encoding.UTF8.GetString(jsonBtye);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public T Get<T>(string key)
        {
            var itemByte = dbContext.StringGet(key);
            if (itemByte.HasValue)
            {
                return DeSerialize<T>(itemByte);
            }
            return default(T);
        }

        public  bool ContainKey(string key)
        {

            return dbContext.KeyExists(key);
        }

        public  void Clear()
        {
            
        }

        public  void Remove(string key)
        {
           
        }

        public void RemoveByPattern(string parttern)
        {
            throw new NotImplementedException();
        }


        public  void Dispose()
        {
           
        }
    }
}
