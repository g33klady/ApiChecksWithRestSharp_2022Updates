using APIChecksWithRestSharp.Models;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiChecks
{
    [TestFixture]
    public class DeleteChecks
    {
        private TodoItem testItem;

        [SetUp]
        public async Task TestDataSetup()
        {
            TodoItem item = new TodoItem
            {
                Name = $"DeleteChecks item {new DateTime().Ticks}",
                DateDue = new DateTime(2035, 12, 31),
                IsComplete = false
            };
            var client = new RestClient("https://localhost:44367/api/Todo");
            var request = new RestRequest();

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(item);
            request.AddHeader("CanAccess", "true");

            //Act
            RestResponse<TodoItem> response = await client.ExecutePostAsync<TodoItem>(request);
            testItem = response.Data;
        }

        [Test]
        public async Task VerifyDeleteWithValidIdReturns204()
        {
            //Arrange
            var client = new RestClient($"https://localhost:44367/api/Todo/{testItem.Id}");
            var request = new RestRequest();
            request.AddHeader("CanAccess", "true");

            //Act
            RestResponse response = await client.DeleteAsync(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode, $"Delete todo item with id {testItem.Id} should have returned a NoContent response; instead it returned {response.StatusCode}");
        }
    }
}
