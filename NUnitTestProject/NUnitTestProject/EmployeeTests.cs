using NUnit.Framework;
using System.Net.Http;
using System;
using System.Net;
using RestSharp;
using RestSharp.Serialization.Json;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Linq;
using Domain;
using Moq;
using System.Collections.Generic;
using KirillProject.Controllers;
using Application.User;
using Persistence;
using System.Threading;
using FluentAssertions;

///I Know Mocking must be used, but I'm working on this,
///Currently all tests are on real examples
namespace NUnitTestProject
{
    [TestFixture]
    class EmployeeTests
    {
        private RestClient client;
        private RestRequest request;
        [SetUp]
        public void SetUp()
        {
            client = new RestClient("https://testapp20200313053149.azurewebsites.net/");
        }
        [Test]
        public void GetUsers_Success_NoState()
        {
            request = new RestRequest("api/home/", Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.That((int)response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void DeleteUser_Failed_NotAuthenticated()
        {
            Application.User.Register.Command listOfUsers = new Application.User.Register.Command
            {
                FirstName = "First",
                LastName = "Last",
                Age = 54,
                Bio = "Bio",
            };

            Mock<DataContext> mockDataContext = new Mock<DataContext>();

            var registerCommand = new Application.User.Register.Handler(mockDataContext.Object);
            CancellationToken cancellationToken = new CancellationToken();
            registerCommand.Handle(listOfUsers,  cancellationToken);
            registerCommand.Should().NotBeNull();
           
            

        }
    }
}
