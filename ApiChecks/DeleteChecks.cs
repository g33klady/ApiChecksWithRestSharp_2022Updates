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
    public class DeleteChecks : ApiChecksBase
    {
        private TodoItem testItem;

        [SetUp]
        public async Task TestDataSetup()
        {
            TodoItem item = Helpers.GetTestTodoItem(name: $"DeleteChecks item {new DateTime().Ticks}");
            //var request = Helpers.PostTodoItemRequest(item);

            //Act
            RestResponse<TodoItem> response = await _client.ExecutePostAsync<TodoItem>(Helpers.PostTodoItemRequest(item));
            testItem = response.Data;
        }

        [Test]
        public async Task VerifyDeleteWithValidIdReturns204()
        {
            //Arrange
            //var request = Helpers.DeleteTodoItemRequest(testItem.Id);

            //Act
            RestResponse response = await _client.DeleteAsync(Helpers.DeleteTodoItemRequest(testItem.Id));

            //Assert
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode, $"Delete todo item with id {testItem.Id} should have returned a NoContent response; instead it returned {response.StatusCode}");
        }

        //TODO: DELETE check w/out a token - may need to modify Helper method or can we remove the header in the check?
        //TODO: DELETE performance check - use Stopwatch
    }
}
