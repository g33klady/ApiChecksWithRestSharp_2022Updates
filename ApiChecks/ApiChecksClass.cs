using APIChecksWithRestSharp.Models;
using NUnit.Framework;
using RestSharp;
using System.Collections;
using System.Net;

namespace ApiChecks
{
    [TestFixture]
    public class ApiChecksClass : ApiChecksBase
    {
        [Test]
        public async Task VerifyGetAllTodoItemsReturns200()
        {
            //Arrange
            //var request = Helpers.GetAllTodoItemsRequest();

            //Act
            RestResponse response = await _client.ExecuteGetAsync(Helpers.GetAllTodoItemsRequest());

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"GET all todo items did not return a success status code; it returned {response.StatusCode}");
        }

        [Test]
        public async Task VerifyGetTodoItemWithId1ReturnsId1()
        {
            //Arrange
            var expectedId = 1;
            //var request = Helpers.GetSingleTodoItemRequest(expectedId);

            //Act
            RestResponse<TodoItem> response = await _client.ExecuteGetAsync<TodoItem>(Helpers.GetSingleTodoItemRequest(expectedId));

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"GET todo item w/ id {expectedId} did not return a success status code; it returned {response.StatusCode}");

            Assert.AreEqual(expectedId, response.Data.Id, $"GET todo item w/ id {expectedId} did not return item with id {expectedId}, it returned {response.Data.Id}");

            StringAssert.AreEqualIgnoringCase("Walk the dog", response.Data.Name, $"Actual name should have been 'Walk the dog' but was {response.Data.Name}");
        }
        
        //TODO: GET all performance check; use Stopwatch

        

        [Test, TestCaseSource(typeof(TestDataClass), "PutTestData")]
        public async Task<string> VerifyPut(TodoItem item)
        {
            //Arrange
            //var request = Helpers.PutTodoItemRequest(1, item);

            //Act
            RestResponse response = await _client.ExecutePutAsync(Helpers.PutTodoItemRequest(1, item));

            //Assert
            return response.StatusCode.ToString();
        }

        //TODO: PUT performance check; use Stopwatch
        //TODO: PUT security checks w/ xss and sql injection - maybe add to parameterization?

        //TODO: lifecycle check (GET -> POST -> GET -> PUT -> GET -> DELETE -> GET)
    }

    public class TestDataClass
    {
        public static IEnumerable PutTestData
        {
            get
            {
                yield return new TestCaseData(Helpers.GetTestTodoItem()).Returns("NoContent").SetName("happy path");
                yield return new TestCaseData(Helpers.GetTestTodoItem(name: "")).Returns("BadRequest").SetName("blank name");
                yield return new TestCaseData(Helpers.GetTestTodoItem(name: null)).Returns("BadRequest").SetName("missing name field");
            }
        }
    }
}