using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Consul;
using SCSCommon.Extension;

namespace SCSCommon.Consul
{
    public class ConsulUtils
    {

        /// <summary>
        /// 同名的服務會覆蓋 設置id為uuid
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static async Task<bool> RegisterService(string id  ,string serviceName, string ip)
        {
            return await RegisterService(new AgentServiceRegistration() { ID = id , Address = ip, Name = serviceName});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reg"></param>
        /// <returns></returns>
        public static async Task<bool> RegisterService(AgentServiceRegistration reg)
        {
            if (reg == null)
            {
                throw new ArgumentNullException();
            }

            var client = new ConsulClient();
            var res = await client.Agent.ServiceRegister(reg);
            return res.StatusCode == HttpStatusCode.OK;
        }

        public static async Task<bool> DeregisterService(string serviceName,bool isDelelteAll = false)
        {
            var serviceNames = await Find(serviceName);
            foreach (var service in serviceNames)
            {
                await DeregisterService(service.ServiceID);
            }

            return true;

        }

        public static async Task<bool> DeregisterService(string serviceId)
        {
            var client = new ConsulClient();

            var res = await client.Agent.ServiceDeregister(serviceId);
            return res.StatusCode == HttpStatusCode.OK;
        }

        /// <summary>
        /// 刪除所有該服務的地址
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static async Task<bool> DeregisterServices(string serviceName)
        {
            var client = new ConsulClient();
            var allServices =await Find(serviceName);
            if (allServices.HasData())
            {
                var tasks = allServices.ToList().Select(c => client.Agent.ServiceDeregister(c.ServiceID));
                var data =await Task.WhenAll(tasks);
                return data.All(c => c.StatusCode == HttpStatusCode.OK);
                //allServices.ForEachAsync(async c => { await client.Agent.ServiceDeregister(c.ServiceID); });
            }

            return true;
        }


        public static async Task<CatalogService[]> Find(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName) )
            {
                throw new ArgumentNullException(serviceName);
            }

            var client = new ConsulClient();
            var res = await client.Catalog.Service(serviceName);
            if (res.StatusCode == HttpStatusCode.OK)
            {
                return res.Response;
            }

            return null;
        }

        public static async Task<CatalogService> FindFirst(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(serviceName);
            }

            var client = new ConsulClient();
            var res = await client.Catalog.Service(serviceName);
            if (res.StatusCode == HttpStatusCode.OK)
            {
                return res.Response.HasData() ? res.Response.First() : null;
            }
            return null;
        }


        public static async Task<Dictionary<string,string[]>> GetAllRegisterService()
        {
            
            var client = new ConsulClient();
            var res = await client.Catalog.Services();
            if (res.StatusCode == HttpStatusCode.OK)
            {
                return res.Response;
            }

            return null;
        }
    }
}
