using APIChecksWithRestSharp.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiChecks
{
    public static class Helpers
    {
        public static RestRequest GetAllTodoItemsRequest()
        {
            var request = new RestRequest();
            return request;
        }

        public static RestRequest GetSingleTodoItemRequest(long id)
        {
            var request = new RestRequest($"{id}");
            request.AddUrlSegment("id", id);
            return request;
        }

        public static RestRequest PostTodoItemRequest(TodoItem item)
        {
            var request = new RestRequest();
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(item);
            request.AddHeader("CanAccess", "true");
            return request;
        }

        public static RestRequest PutTodoItemRequest(long id, TodoItem item)
        {
            var request = new RestRequest($"{id}");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(item);
            request.AddHeader("CanAccess", "true");
            request.AddUrlSegment("id", id);
            return request;
        }

        public static RestRequest DeleteTodoItemRequest(long id)
        {
            var request = new RestRequest($"{id}");
            request.AddHeader("CanAccess", "true");
            request.AddUrlSegment("id", id);
            return request;
        }

        public static TodoItem GetTestTodoItem(string name = "mow the lawn", bool isComplete = false, DateTime dateDue = default(DateTime))
        {
            if (dateDue == default(DateTime))
            {
                dateDue = new DateTime(2035, 12, 31);
            }
            return new TodoItem
            {
                Name = name,
                DateDue = dateDue,
                IsComplete = isComplete
            };
        }
    }
}
