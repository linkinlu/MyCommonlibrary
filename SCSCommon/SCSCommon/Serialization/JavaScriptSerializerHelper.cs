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
        public static string GetJsonString<T>(T entity)
        {
            if (entity == null) return string.Empty;

            return new JavaScriptSerializer().Serialize(entity);
        }


    }
}
