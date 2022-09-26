using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTestsProject
{
    [SetUpFixture]
    public class ApiTestsBase
    {
        public static string _baseUrl;
        public static RestClient _client;

        [OneTimeSetUp]
        public void SetUp()
        {
            _baseUrl = "https://localhost:44367/api/Todo";
            _client = new RestClient(_baseUrl);
        }
    }
}
