using APIChecksWithRestSharp.Models;
using NUnit.Framework;
using RestSharp;
using System.Net;

namespace ApiChecks
{
    [TestFixture]
    public class ApiChecksClass
    {
        [Test]
        public async Task VerifyGetAllTodoItemsReturns200()
        {
            //Arrange
            var client = new RestClient("https://localhost:44367/api/Todo");
            var request = new RestRequest();

            //Act
            RestResponse response = await client.ExecuteGetAsync(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"GET all todo items did not return a success status code; it returned {response.StatusCode}");
        }

        [Test]
        public async Task VerifyGetTodoItemWithId1ReturnsId1()
        {
            //Arrange
            var expectedId = 1;
            var client = new RestClient($"https://localhost:44367/api/Todo/{expectedId}");
            var request = new RestRequest();

            //Act
            RestResponse<TodoItem> response = await client.ExecuteGetAsync<TodoItem>(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"GET todo item w/ id {expectedId} did not return a success status code; it returned {response.StatusCode}");

            Assert.AreEqual(expectedId, response.Data.Id, $"GET todo item w/ id {expectedId} did not return item with id {expectedId}, it returned {response.Data.Id}");

            StringAssert.AreEqualIgnoringCase("Walk the dog", response.Data.Name, $"Actual name should have been 'Walk the dog' but was {response.Data.Name}");
        }
    }
}