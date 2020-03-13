

using NUnit.Framework;
using System.Net.Http;
using System;

namespace KirillProject
{
    [TestFixture]
    public class RestSharpDemo
    {
        private HttpClient client;
        [SetUp]
        public void SetUp(){
             client = new HttpClient();
             client.BaseAddress = new Uri("https://localhost:5001");
             
        }
        [Test]
        public void TestMethod1()
        {
            

            Assert.AreEqual(client.GetAsync("home/testregister").Result,"PostCheck");
        }
    }
}