using System.Net;

namespace WebApi.Base
{
    public class StatusCodeModel
    {
        public HttpStatusCode StatusCode { get; set; }

        public ErrorCode ErrorCode { get; set; }
    }
}