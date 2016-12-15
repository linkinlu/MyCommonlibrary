using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SCSCommon.Clone
{
    public class JsonDeepClone
    {
        public static T DeepClone<T>(T obj)
        {
            if (obj == null) return default(T);

            var ser = new JavaScriptSerializer();
            var objJson = ser.Serialize(obj);
            return ser.Deserialize<T>(objJson);
        }
    }
}
