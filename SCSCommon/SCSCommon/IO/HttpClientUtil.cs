using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using SCSCommon.Serialization;

namespace SCSCommon.IO
{
    public static class HttpClientUtil
    {
        private static readonly HttpClient client = null;
        #region contructor
        static HttpClientUtil()
        {
            // Default setup for the single http client

            var handler = new HttpClientHandler();
            handler.UseCookies = false;
         //   handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            client = new HttpClient(handler);
            client.Timeout = TimeSpan.FromHours(1);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion

       

   
        public static async Task<HttpResponseMessage> SendDataAsync(Uri uri, HttpContent content, AuthenticationHeaderValue authentication, HttpMethod method)
        {
            using (var request = new HttpRequestMessage(method, uri))
            {
                request.Headers.Authorization = authentication;
                request.Content = content;
                var response = await SendRequestAsync(request).ConfigureAwait(false);
                return response;
            }
        }
        public static async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request)
        {
           
            var response = await client.SendAsync(request).ConfigureAwait(false);
            return response;
        }

        public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(string hostUri, string requestUri, T data, AuthenticationHeaderValue authentication = null, Dictionary<string, string> headerInfos = null)
        {
            Uri uri = null;
            Uri.TryCreate(new Uri(hostUri), requestUri, out uri);
            var response = await PostAsJsonAsync(uri, data, authentication, headerInfos).ConfigureAwait(false);
            return response;
        }

        public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T data, AuthenticationHeaderValue authentication = null, Dictionary<string, string> headerInfos = null)
        {
            var response = await PostAsJsonAsync(new Uri(requestUri), data, authentication, headerInfos).ConfigureAwait(false);
            return response;
        }

        public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(Uri uri, T data, AuthenticationHeaderValue authentication = null, Dictionary<string, string> headerInfos = null)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, uri))
            {
                request.Headers.Authorization = authentication;
                request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                if (headerInfos != null && headerInfos.Any())
                {
                    foreach (var info in headerInfos)
                    {
                        request.Headers.Add(info.Key, info.Value);
                    }
                }
                var response = await SendRequestAsync(request).ConfigureAwait(false);
                return response;
            }
        }

        public static async Task<V> PostAsJsonAsync<T, V>(string hostUri, string requestUri, T data, AuthenticationHeaderValue authentication = null, Dictionary<string, string> headerInfos = null, Action<HttpResponseMessage> requestFailAction = null)
        {
            Uri uri = null;
            Uri.TryCreate(new Uri(hostUri), requestUri, out uri);
            var response = await PostAsJsonAsync<T, V>(uri, data, authentication, headerInfos, requestFailAction).ConfigureAwait(false);
            return response;
        }

        public static async Task<V> PostAsJsonAsync<T, V>(string requestUri, T data, AuthenticationHeaderValue authentication = null, Dictionary<string, string> headerInfos = null, Action<HttpResponseMessage> requestFailAction = null)
        {
            var response = await PostAsJsonAsync<T, V>(new Uri(requestUri), data, authentication, headerInfos, requestFailAction).ConfigureAwait(false);
            return response;
        }

        public static async Task<V> PostAsJsonAsync<T, V>(Uri uri, T data, AuthenticationHeaderValue authentication = null, Dictionary<string, string> headerInfos = null, Action<HttpResponseMessage> requestFailAction = null)
        {
            var response = await PostAsJsonAsync(uri, data, authentication, headerInfos).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return resultString.ToObject<V>();
            }
            else
            {
                requestFailAction?.Invoke(response);
            }
            return default(V);
        }

        public static async Task<T> GetDataAsync<T>(string hostUri, string requestUri, AuthenticationHeaderValue authentication, Dictionary<string, string> headerInfos = null, Action<HttpResponseMessage> requestFailAction = null)
        {
            Uri uri = null;
            Uri.TryCreate(new Uri(hostUri), requestUri, out uri);
            var response = await GetDataAsync<T>(uri, authentication, headerInfos, requestFailAction).ConfigureAwait(false);
            return response;
        }

        public static async Task<T> GetDataAsync<T>(string requestUri, AuthenticationHeaderValue authentication, Dictionary<string, string> headerInfos = null, Action<HttpResponseMessage> requestFailAction = null)
        {
            var response = await GetDataAsync<T>(new Uri(requestUri), authentication, headerInfos, requestFailAction).ConfigureAwait(false);
            return response;
        }

        public static async Task<T> GetDataAsync<T>(Uri uri, AuthenticationHeaderValue authentication, Dictionary<string, string> headerInfos = null, Action<HttpResponseMessage> requestFailAction = null)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                request.Headers.Authorization = authentication;
                if (headerInfos != null && headerInfos.Any())
                {
                    foreach (var info in headerInfos)
                    {
                        request.Headers.Add(info.Key, info.Value);
                    }
                }
                var response = await SendRequestAsync(request).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var data =  await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return data.ToObject<T>();
                }
                else
                {
                    requestFailAction?.Invoke(response);
                    return default(T);
                }
            }
        }

  
     
    }
}
