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
    public class PostChecks : ApiChecksBase
    {
        private TodoItem testItem;

        [TearDown]
        public async Task TestDataCleanUp()
        {
            RestResponse response = await _client.DeleteAsync(Helpers.DeleteTodoItemRequest(testItem.Id));
            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                Console.WriteLine($"Unable to delete {testItem.Id} - {response.StatusCode}");
            }
            //this could easily be a database cleanup script call
        }

        [Test]
        public async Task VerifyPostWithAllValidValuesReturns201()
        {
            //Arrange
            TodoItem expectedItem = Helpers.GetTestTodoItem();
            //var request = Helpers.PostTodoItemRequest(expectedItem);

            //Act
            RestResponse<TodoItem> response = await _client.ExecutePostAsync<TodoItem>(Helpers.PostTodoItemRequest(expectedItem));
            testItem = response.Data;

            //Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode, $"Post new todo item should have returned a Created status code; instead it returned {response.StatusCode}");
        }

        //TODO: parameterize checks for invalid name, missing name, invalid datedue; include security checks - xss, sql injection
        //TODO: POST performance check; use Stopwatch
    }
}
