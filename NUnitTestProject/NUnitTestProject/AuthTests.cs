

using NUnit.Framework;
using System.Net.Http;
using System;
using System.Net;
using RestSharp;
using RestSharp.Serialization.Json;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace KirillProject
{
    [TestFixture]
    public class AuthTests
    {
        private RestClient client;
        private RestRequest request;
        [SetUp]
        public void SetUp(){
             client = new RestClient("https://testapp20200313053149.azurewebsites.net/");
             request = new RestRequest("api/user/register", Method.POST);

        }
        [Test]
        public void ServerCheck_Running_NoState()
        {     
            request = new RestRequest("api/home/testregister", Method.GET); 
            IRestResponse response = client.Execute(request);
            Assert.That((int)response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void RegisterUser_Fails_NoEmailProvided()
        {
            
            request.AddJsonBody(new {  Username="TotallyUnique",Password = "123456" });
            IRestResponse restResponse = client.Execute(request);
            string response = restResponse.Content;
            var jObject = JObject.Parse(restResponse.Content);
            dynamic city = jObject.GetValue("errors").ToString();
            Assert.That(city.Contains("The Email field is required"), Is.EqualTo(true));

        }
        [Test]
        public void RegisterUser_Fails_NoUsernameProvided()
        {
            
            request.AddJsonBody(new { Email = "TotallyUnique@test.com", Password = "123456" });
            IRestResponse restResponse = client.Execute(request);
            string response = restResponse.Content;
            var jObject = JObject.Parse(restResponse.Content);
            dynamic city = jObject.GetValue("errors").ToString();
            Assert.That(city.Contains("The Username field is required."), Is.EqualTo(true));
        }
        [Test]
        public void RegisterUser_Fails_NoPasswordProvided()
        {
            
            request.AddJsonBody(new { Email = "TotallyUnique@test.com", Username="Unique username" });
            IRestResponse restResponse = client.Execute(request);
            string response = restResponse.Content;
            var jObject = JObject.Parse(restResponse.Content);
            dynamic city = jObject.GetValue("errors").ToString();
            Assert.That(city.Contains("The Password field is required."), Is.EqualTo(true));
        }
        
    }
}