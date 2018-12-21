using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace SOA.Web.ClientTest
{
    [TestClass]
    public class WSTestClass
    {
        [TestInitialize]
        public void Initialize()
        {
            Trace.WriteLine($"{nameof(WSTestClass)} start...");
        }

        [TestMethod]
        public void HelloWorldTest()
        {
            using (MyWebService.MyWebServiceSoapClient client = new MyWebService.MyWebServiceSoapClient())
            {
                string helloString = client.HelloWorld();
                Assert.AreEqual(helloString, "Hello World");

                MyWebService.CustomerSoapHeader customerSoapHeader = new MyWebService.CustomerSoapHeader
                {
                    Name = "Jack"
                };

                string hiString = client.SayHi(customerSoapHeader);
                Assert.AreEqual(hiString, "Jack say hi");
            }
        }
    }
}