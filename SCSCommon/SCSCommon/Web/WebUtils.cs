using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SCSCommon.Web
{
    public static class WebUtils
    {
        public static bool IsAjaxRequest(this HttpRequestMessage request)
        {
            IEnumerable<string> headers;
            if (request.Headers.TryGetValues("X-Requested-With", out headers))
            {
                return headers != null && headers.Any() && headers.FirstOrDefault().ToLowerInvariant() == "xmlhttprequest";
            }
            return false;
        }
    }
}
