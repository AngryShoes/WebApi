﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace SOA.Web.WS
{
    /// <summary>
    /// MyWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。
    // [System.Web.Script.Services.ScriptService]
    public class MyWebService : System.Web.Services.WebService
    {
        public CustomerSoapHeader customerSoapHeader = new CustomerSoapHeader();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [SoapHeader(nameof(customerSoapHeader))]
        [WebMethod]
        public string SayHi()
        {
            if (customerSoapHeader.Name.Equals("Jack"))
            {
                return "Jack say hi";
            }
            return "Hi";
        }
    }
}