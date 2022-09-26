using APIChecksWithRestSharp.Models;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiTestsProject
{
    [TestFixture]
    public class PutTests : ApiTestsBase
    {
        public long _testItemId;

        [SetUp]
        public async Task SetUp()
        {
            var testingItem = ApiCallsAndHelpers.GetTestTodoItem();
            RestResponse<TodoItem> response = await _client.ExecutePostAsync<TodoItem>(ApiCallsAndHelpers.PostTodoItemRequest(testingItem));
            _testItemId = response.Data.Id;
        }


        [TearDown]
        public async Task TearDown()
        {
            RestResponse response = await _client.DeleteAsync(ApiCallsAndHelpers.DeleteTodoItemRequest(_testItemId));
        }


        [Test]
        public async Task VerifyPutWithAllValidValuesReturns204()
        {
            //arrange
            TodoItem expectedItem = ApiCallsAndHelpers.GetTestTodoItem(name: "mow the lawn again please");

            var request = ApiCallsAndHelpers.PutTodoItemRequest(expectedItem, _testItemId);

            //act
            RestResponse response = await _client.ExecutePutAsync(request);

            //assert
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode, $"PUT expected No Content response, but was {response.StatusCode}");
        }
    }
}
