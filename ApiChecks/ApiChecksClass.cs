using APIChecksWithRestSharp.Models;
using NUnit.Framework;
using RestSharp;
using System.Collections;
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

        [Test]
        public async Task VerifyPostWithAllValidValuesReturns201()
        {
            //Arrange
            TodoItem expectedItem = new TodoItem
            {
                Name = "mow the lawn",
                DateDue = new DateTime(2035, 12, 31),
                IsComplete = false
            };
            var client = new RestClient("https://localhost:44367/api/Todo");
            var request = new RestRequest();

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(expectedItem);
            request.AddHeader("CanAccess", "true");

            //Act
            RestResponse response = await client.ExecutePostAsync(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode, $"Post new todo item should have returned a Created status code; instead it returned {response.StatusCode}");
        }

        [Test, TestCaseSource(typeof(TestDataClass), "PutTestData")]
        public async Task<string> VerifyPut(TodoItem item)
        {
            //Arrange
            var client = new RestClient("https://localhost:44367/api/Todo/1");
            var request = new RestRequest();

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(item);
            request.AddHeader("CanAccess", "true");

            //Act
            RestResponse response = await client.ExecutePutAsync(request);

            //Assert
            return response.StatusCode.ToString();
        }
    }

    public class TestDataClass
    {
        public static IEnumerable PutTestData
        {
            get
            {
                yield return new TestCaseData(new TodoItem
                {
                    Name = "mow the neighbor's lawn",
                    DateDue = new DateTime(2035, 12, 31),
                    IsComplete = false
                }).Returns("NoContent").SetName("happy path");
                yield return new TestCaseData(new TodoItem
                {
                    Name = "",
                    DateDue = new DateTime(2035, 12, 31),
                    IsComplete = false
                }).Returns("BadRequest").SetName("blank name");
                yield return new TestCaseData(new TodoItem
                {
                    DateDue = new DateTime(2035, 12, 31),
                    IsComplete = false
                }).Returns("BadRequest").SetName("missing name field");
            }
        }
    }
}