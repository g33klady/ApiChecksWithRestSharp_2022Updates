using APIChecksWithRestSharp.Models;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ApiTestsProject
{
    [TestFixture]
    public class ApiTests : ApiTestsBase
    {
        [Test]
        public async Task VerifyGetAllTodoItemsReturns200()
        {
            //Arrange
            var request = ApiCallsAndHelpers.GetAllTodoItemsRequest();

            //Act
            RestResponse response = await _client.ExecuteGetAsync(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"GET all todo items did not return a success status code; it returned {response.StatusCode}");
        }

        [Test]
        public async Task VerifyGetTodoItemWithId1ReturnsId1()
        {
            //arrange
            var expectedId = 1;
            var request = ApiCallsAndHelpers.GetSingleTodoItemRequest(expectedId);

            //act
            RestResponse<TodoItem> response = await _client.ExecuteGetAsync<TodoItem>(request);

            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"GET todo item {expectedId} did not return a success status code; it returned {response.StatusCode}");

            Assert.AreEqual(expectedId, response.Data.Id, $"Should be {expectedId} but was {response.Data.Id}");
        }

        [Test]
        public async Task DeleteValidItemReturns204()
        {
            //arrange
            var itemToDelete = 2;
            var request = ApiCallsAndHelpers.DeleteTodoItemRequest(itemToDelete);

            //act
            RestResponse response = await _client.DeleteAsync(request);

            //assert
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode, $"expected No Content, was {response.StatusCode}");
        }
    }
}