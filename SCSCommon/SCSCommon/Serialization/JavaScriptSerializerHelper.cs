using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SCSCommon.Serialization
{
    public class JavaScriptSerializerHelper
    {
        public static string Serialize<T>(T entity)
        {
            if (entity == null) return string.Empty;

            return new JavaScriptSerializer().Serialize(entity);
        }

        public static T DeSerialize<T>(string jsonString)
        {
            try
            {
                return new JavaScriptSerializer().Deserialize<T>(jsonString);
            }
            catch 
            {
                return default(T);
            }

        }

    }
}
