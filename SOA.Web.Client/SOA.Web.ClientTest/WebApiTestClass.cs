using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace SOA.Web.ClientTest
{
    [TestClass]
    public class WebApiTestClass
    {
        [TestMethod]
        public void TestGetMethod()
        {
            // Arrange
            string urlGetClientById = "http://localhost:8090/api/client/GetClientById?id=1";
            string expectedResult = "{\"Id\":1,\"Name\":\"Marry\",\"Address\":\"USA\"}";
            // Act
            var getWebRequestResult = GetWebRequest(urlGetClientById);
            // Assert
            Assert.AreEqual(getWebRequestResult, expectedResult);
        }

        [TestMethod]
        public void TestPostMethod()
        {
            // Arrange
            string urlPostObjectModel = "http://localhost:8090/api/client/PostObjectModel";
            //string urlPostModelSerialize = " http://localhost:8090/api/client/PostModelSerialize";
            string expectedResult = "\"Id:4,Name:Jerry,info:Parse Model\"";
            // Act
            var getWebRequestResult = PostWebRequest(urlPostObjectModel);
            // Assert
            Assert.AreEqual(getWebRequestResult, expectedResult);
        }

        private string GetWebRequest(string url)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Timeout = 6000;
            string responseResult = string.Empty;
            using (HttpWebResponse response = webRequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseResult = streamReader.ReadToEnd();
                }
            }
            return responseResult;
        }

        private string PostWebRequest(string url)
        {
            var client = new
            {
                Id = 4,
                Name = "Jerry",
                Address = "Boston"
            };
            var parseObject = new
            {
                Client = client,
                Info = "Parse Model"
            };
            var postData = JsonHelper.ObjectToString(parseObject);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Timeout = 6000 * 10;
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.118 Safari/537.36";
            httpWebRequest.Method = "POST";
            byte[] postBytes = Encoding.UTF8.GetBytes(postData);
            httpWebRequest.ContentLength = postBytes.Length;
            Stream stream = httpWebRequest.GetRequestStream();
            stream.Write(postBytes, 0, postBytes.Length);
            stream.Close();

            string result = string.Empty;
            using (HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse)
            {
                if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = httpWebResponse.GetResponseStream();
                    StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                    result = streamReader.ReadToEnd();
                }
            }
            return result;
        }
    }
}