using System.Collections.Generic;
using System.Net;
using Nover.DB.WebView2.Infrastructure;


namespace Nover.DB.WebView2.Network
{
    /// <summary>
    /// The EdgeLite response.
    /// </summary>
    public class ActionResponse : IActionResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResponse"/> class.
        /// </summary>
        /// <param name="requestId">
        /// The request id.
        /// </param>
        public ActionResponse()
        {
            Headers = new Dictionary<string, string[]>
            {
                { ResponseConstants.Header_AccessControlAllowOrigin,      new string[] { "*" } },
                { ResponseConstants.Header_CacheControl,                  new string[] { "private" } },
                { ResponseConstants.Header_AccessControlAllowMethods,     new string[] { "*" } },
                { ResponseConstants.Header_AccessControlAllowHeaders,     new string[] { ResponseConstants.Header_ContentType } },
                { ResponseConstants.Header_ContentType,                   new string[] { ResponseConstants.Header_ContentTypeValue } }
            };

            StatusCode = HttpStatusCode.OK;
            ReasonPhrase = ResponseConstants.StatusOKText;
        }

        public HttpStatusCode StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public object Content { get; set; }
        public bool HasRouteResponse { get; set; }
        public IDictionary<string, string[]> Headers { get; }
    }
}
