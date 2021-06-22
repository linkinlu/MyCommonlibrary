using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SCSCommon.Serialization
{
    public static class SerializerHelper
    {
        public static T JObjectTo<T>(this object source)
        where T : class
        {
            return source == null ? null : JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));
        }

        public static string ToJson(this object source)
        {
            return source == null ? null : JsonConvert.SerializeObject(source, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        public static string ToXml<T>(T source)
        {
            return source == null ? null : XmlHelper.Serialize(source);
        }

        public static T ToObject<T>(this string source)
           // where T : class, new()
        {
            if (source == null)
                return default(T);

            if (string.IsNullOrWhiteSpace(source))
                return default(T);

            var first = source.First(w => !char.IsWhiteSpace(w));

            const char lt = '<';
            const char b = '{';
            const char sb = '[';

            switch (first)
            {
                case lt:
                    return XmlHelper.Deserialize<T>(source);

                case b:
                case sb:
                    return JsonConvert.DeserializeObject<T>(source);

                default:
                    throw new Exception(string.Format("Unsupported format. \"{0}\"", first));
            }
        }

    
    }
}
