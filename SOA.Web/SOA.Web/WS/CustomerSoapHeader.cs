using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

namespace SOA.Web.WS
{
    public class CustomerSoapHeader : SoapHeader
    {
        public CustomerSoapHeader()
        {
        }

        public string Name { get; set; }
        public string Password { get; set; }

        public CustomerSoapHeader(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}