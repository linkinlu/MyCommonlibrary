using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SCSCommon.Web
{
    public static class WebUtils
    {
        /// <summary>
        /// Determines whether [is ajax request].
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        ///   <c>true</c> if [is ajax request] [the specified request]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAjaxRequest(this HttpRequestMessage request)
        {
            IEnumerable<string> headers;
            if (request.Headers.TryGetValues("X-Requested-With", out headers))
            {
                return headers != null && headers.Any() && headers.FirstOrDefault().ToLowerInvariant() == "xmlhttprequest";
            }
            return false;
        }

        /// <summary>
        /// Gets the name of the host.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns></returns>
        public static string GetHostName(this string ipAddress)
        {
            return Dns.GetHostEntry(ipAddress).HostName;
        }

        /// <summary>
        /// Gets the name of the host.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns></returns>
        public static string GetHostName(this IPAddress ipAddress)
        {
            return Dns.GetHostEntry(ipAddress).HostName;
        }

        /// <summary>
        /// Users the ip address.
        /// </summary>
        /// <param name="Request">The request.</param>
        /// <returns></returns>
        //public static IPAddress UserIPAddress(this HttpRequestBase Request)
        //{
        //    IPAddress Address = null;
        //    if (!IPAddress.TryParse(Request.UserHostAddress, out Address))
        //        Address = null;
        //    return Address;
        //}

        /// <summary>
        /// Users the ip address.
        /// </summary>
        /// <param name="Request">The request.</param>
        /// <returns></returns>
        public static IPAddress UserIPAddress(this HttpRequest Request)
        {
            IPAddress Address = null;
            return Request.UserIPAddress();
        }

        /// <summary>
        /// URLs the encode.
        /// </summary>
        /// <param name="Input">The input.</param>
        /// <returns></returns>
        public static string URLEncode(this string Input)
        {
            if (string.IsNullOrEmpty(Input))
                return "";
            return HttpUtility.UrlEncode(Input);
        }

        /// <summary>
        /// To the query string.
        /// </summary>
        /// <param name="Input">The input.</param>
        /// <returns></returns>
        public static string ToQueryString(this IDictionary<string, string> Input)
        {
            if (Input.Count <= 0)
                return "";
            var Builder = new StringBuilder();
            Builder.Append("?");
            string Splitter = "";
            foreach (string Key in Input.Keys)
            {
                Builder.Append(Splitter).AppendFormat("{0}={1}", Key.URLEncode(), Input[Key].URLEncode());
                Splitter = "&";
            }
            return Builder.ToString();
        }

        public static string GetImageContentType(string extension)
        {
            if (string.IsNullOrEmpty(extension))
                return null;

            extension = extension.Trim();
            if (extension[0] != '.')
                extension = "." + extension;

            extension = extension.ToLower();

            string suffix = null;

            switch (extension)
            {
                case ".jpeg":
                case ".jpg":
                case ".jpe":
                case ".jfif":
                    suffix = "jpeg";
                    break;

                case ".gif":
                    suffix = "gif";
                    break;

                case ".png":
                    suffix = "png";
                    break;

                case ".ico":
                    suffix = "x-icon";
                    break;

                case ".tiff":
                case ".tif":
                    suffix = "tiff";
                    break;

                case ".fax":
                    suffix = "fax";
                    break;

                case ".net":
                    suffix = "pnetvue";
                    break;

                case ".rp":
                    suffix = "vnd.rn-realpix";
                    break;

                case ".wbmp":
                    suffix = "vnd.wap.wbmp";
                    break;
            }

            return suffix == null ? null : "image/" + suffix;
        }
    }
}
