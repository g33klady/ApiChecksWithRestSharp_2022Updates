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
    public class PostTests : ApiTestsBase
    {
        public long _testItemId;

        [TearDown]
        public async Task TearDown()
        {
            RestResponse response = await _client.DeleteAsync(ApiCallsAndHelpers.DeleteTodoItemRequest(_testItemId));
        }


        [Test]
        public async Task VerifyPostWithAllValidValuesReturns201()
        {
            //arrange
            TodoItem expectedItem = ApiCallsAndHelpers.GetTestTodoItem();
            var request = ApiCallsAndHelpers.PostTodoItemRequest(expectedItem);

            //act
            RestResponse<TodoItem> response = await _client.ExecutePostAsync<TodoItem>(request);
            _testItemId = response.Data.Id;
            
            //assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode, $"POST expected Created response, but was {response.StatusCode}");
        }
    }
}
