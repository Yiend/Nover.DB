using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;


namespace Nover.DB.WebView2.Network
{
    public class ActionRequest : IActionRequest
    {
        public string RequestId { get; set; }
        public string RoutePath { get; set; }
        public object Content { get; set; }
        public IDictionary<string, IList<object>> Parameters { get; set; }
        public IDictionary<string, string[]> Headers { get; set; }

        public ActionRequest(string requestID = null)
        {
            RequestId = requestID ?? Guid.NewGuid().ToString();
        }

        public static IActionRequest CreateRequest(IRequest request)
        {
            var actionRequest = CreateRequest(request.Url, request.Content);
            actionRequest.Headers = request.Headers;
            return actionRequest;
        }

        public static IActionRequest CreateRequest(string url, object content)
        {
            var request = new ActionRequest();
            request.RoutePath = url;
            request.Parameters = new Dictionary<string, IList<object>>();
            request.Content = content;

            try
            {
                var pathAndQuery = new PathAndQuery();
                pathAndQuery.Parse(url);
                request.RoutePath = pathAndQuery.Path;
                if (pathAndQuery.QueryParameters != null && pathAndQuery.QueryParameters.Any())
                {
                    foreach (var item in pathAndQuery.QueryParameters)
                    {
                        if (!request.Parameters.ContainsKey(item.Key))
                        {
                            var values = item.Value.Split(',');
                            request.Parameters.Add(item.Key, values);
                        }
                    }
                }
            }
            catch { }

            return request;
        }

        public static IActionRequest CreateRequest(string requestString, string requestJson)
        {
            var actionRequest = new ActionRequest();
            actionRequest.Parameters = new Dictionary<string, IList<object>>();

            try
            {
                request req = CreateRequestData(requestString, requestJson);
                actionRequest.RequestId = req.id;
                actionRequest.Content = req.content;

                var pathAndQuery = new PathAndQuery();
                pathAndQuery.Parse(req.url);
                actionRequest.RoutePath = pathAndQuery.Path;
                if (pathAndQuery.QueryParameters != null && pathAndQuery.QueryParameters.Any())
                {
                    foreach (var item in pathAndQuery.QueryParameters)
                    {
                        if (!actionRequest.Parameters.ContainsKey(item.Key))
                        {
                            var values = item.Value.Split(',');
                            actionRequest.Parameters.Add(item.Key, values);
                        }
                    }
                }
            }
            catch { }

            return actionRequest;
        }

        private static request CreateRequestData(string requestString, string requestJsonString)
        {
            try
            {
                var requestData = ConvertJsonToRequest(requestString);
                if (requestData != null)
                {
                    return requestData;
                }
            }
            catch { }

            try
            {
                var requestData = ConvertJsonToRequest(requestJsonString);
                if (requestData != null)
                {
                    return requestData;
                }
            }
            catch { }

            try
            {
                var uri = PathAndQuery.CreateUri(requestString);
                if (uri != null)
                {
                    return new request { url = requestString };
                }

                uri = PathAndQuery.CreateUri(requestJsonString);
                if (uri != null)
                {
                    return new request { url = requestJsonString };
                }
            }
            catch { }

            return null;
        }

        public static request ConvertJsonToRequest(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            return JsonSerializer.Deserialize<request>(json);
        }

        public class request
        {
            public request()
            {
                id = Guid.NewGuid().ToString();
            }

            public string url { get; set; }
            public object content { get; set; }
            public string id { get; set; }
        }
    }
}