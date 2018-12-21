using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SOA.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SOA.Web.Controllers
{
    public class ClientController : ApiController
    {
        private List<Client> _clientList = new List<Client>
        {
            new Client{ Id=1,Name="Marry",Address="USA"},
            new Client{ Id=2,Name="Tomson",Address="China"},
            new Client{Id=3,Name="Kang",Address="Japan"}
        };

        #region GET

        [HttpGet]
        public List<Client> Get()
        {
            return _clientList;
        }

        [HttpGet]
        public Client GetClientById(int id)
        {
            string idstr = HttpContext.Current.Request.QueryString["id"];
            var client = _clientList.FirstOrDefault(x => x.Id == id);
            if (client == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return client;
        }

        [HttpGet]
        public Client GetClientByIdName(int id, string name)
        {
            string idStr = HttpContext.Current.Request.QueryString["id"];
            string nameStr = HttpContext.Current.Request.QueryString["name"];

            var client = _clientList.FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
            if (client == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return client;
        }

        [HttpGet]
        public Client GetClientByModel(Client client)
        {
            string idStr = HttpContext.Current.Request.QueryString["id"];
            string nameStr = HttpContext.Current.Request.QueryString["name"];
            string addressStr = HttpContext.Current.Request.QueryString["address"];
            return client;// Get方式直接传递实体是null
        }

        [HttpGet]
        public Client GetClientByModelUri([FromUri]Client client)
        {
            string idStr = HttpContext.Current.Request.QueryString["id"];
            string nameStr = HttpContext.Current.Request.QueryString["name"];
            string addressStr = HttpContext.Current.Request.QueryString["address"];
            return client;// Get方式直接传递实体是null 需要FromUri特性
        }

        [HttpGet]
        public Client GetClientByModelSerialize(string clientString)
        {
            string idStr = HttpContext.Current.Request.QueryString["id"];
            string nameStr = HttpContext.Current.Request.QueryString["name"];
            string addressStr = HttpContext.Current.Request.QueryString["address"];

            var client = JsonConvert.DeserializeObject<Client>(clientString);
            return client;
        }

        //[HttpGet]
        public Client GetClientWithoutHttpGetAttribute(string clientString)
        {
            string idStr = HttpContext.Current.Request.QueryString["id"];
            string nameStr = HttpContext.Current.Request.QueryString["name"];
            string addressStr = HttpContext.Current.Request.QueryString["address"];

            var client = JsonConvert.DeserializeObject<Client>(clientString);
            return client;
        }

        /// <summary>
        /// 方法名以Get开头，WebApi会自动默认这个请求就是get请求，而如果以其他名称开头而又不标注方法的请求方式，那么这个时候服务器虽然找到了这个方法，但是由于请求方式不确定，所以直接返回给你405——方法不被允许的错误。
        /// </summary>
        /// <param name="clientString"></param>
        /// <returns></returns>
        public Client NoGetClientWithoutHttpGetAttribute(string clientString)
        {
            string idStr = HttpContext.Current.Request.QueryString["id"];
            string nameStr = HttpContext.Current.Request.QueryString["name"];
            string addressStr = HttpContext.Current.Request.QueryString["address"];

            var client = JsonConvert.DeserializeObject<Client>(clientString);
            return client;
        }

        #endregion GET

        #region POST

        [HttpPost]
        public Client PostNoKey([FromBody]int id)
        {
            string idStr = HttpContext.Current.Request.Form["id"];
            string nameStr = HttpContext.Current.Request.Form["name"];
            string addressStr = HttpContext.Current.Request.Form["address"];
            var client = _clientList.FirstOrDefault(x => x.Id == id);
            if (client == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return client;
        }

        /// <summary>
        /// POST 方式传递单个值，json数据不要key，这样后台才能获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public Client PostWithfKey([FromBody]int id)
        {
            string idStr = HttpContext.Current.Request.Form["id"];
            string nameStr = HttpContext.Current.Request.Form["name"];
            string addressStr = HttpContext.Current.Request.Form["address"];
            var client = _clientList.FirstOrDefault(x => x.Id == id);
            if (client == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return client;
        }

        [HttpPost]
        public Client PostModel(Client client)
        {
            string idStr = HttpContext.Current.Request.Form["id"];
            string nameStr = HttpContext.Current.Request.Form["name"];
            string addressStr = HttpContext.Current.Request.Form["address"];
            var clientStr = ControllerContext.Request.Content.ReadAsStringAsync().Result;
            return client;
        }

        [HttpPost]
        public Client PostModelSerialize(Client client)
        {
            string idStr = HttpContext.Current.Request.Form["id"];
            string nameStr = HttpContext.Current.Request.Form["name"];
            string addressStr = HttpContext.Current.Request.Form["address"];
            var clientStr = ControllerContext.Request.Content.ReadAsStringAsync().Result;
            return client;
        }

        [HttpPost]
        public string PostObjectModel(JObject clientObject)
        {
            string idStr = HttpContext.Current.Request.Form["id"];
            string nameStr = HttpContext.Current.Request.Form["name"];
            string addressStr = HttpContext.Current.Request.Form["address"];
            var clientStr = ControllerContext.Request.Content.ReadAsStringAsync().Result;
            dynamic json = clientObject;
            JObject client = json.Client;
            string info = json.Info;
            var clientResult = client.ToObject<Client>();

            return $"Id:{clientResult.Id},Name:{clientResult.Name},info:{info}";
        }

        #endregion POST
    }
}