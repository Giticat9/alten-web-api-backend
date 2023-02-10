using System.Net;
using WebApi.Base.Exception;

namespace WebApi.Base;

public class StatusCodeModel
{
    public HttpStatusCode StatusCode { get; set; }
    public ErrorCode ErrorCode { get; set; }
}