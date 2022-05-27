using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
///using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace SCSCommon.Clone
{
    public class JsonDeepClone
    {
        public static T DeepClone<T>(T obj)
        {
            if (obj == null) return default(T);

            var ser = JsonConvert.SerializeObject(obj);
            
            return JsonConvert.DeserializeObject<T>(ser);
        }
    }
}
