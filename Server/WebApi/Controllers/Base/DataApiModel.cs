using System.Net;
using WebApi.Base.Exception;

namespace WebApi.Base;

public class DataApiModel<T> : BaseApiModel
{
    public T? Data { get; set; }

    public string? Csrf { get; set; }
}

public class DataApiModel : DataApiModel<object>
{
    public static BaseApiModel Ok()
    {
        return Ok(new { StatusCode = HttpStatusCode.OK });
    }

    public static DataApiModel<T> Ok<T>(T data)
    {
        return Ok(data, null);
    }

    public static DataApiModel<T> Ok<T>(T data, string? csrf)
    {
        return new DataApiModel<T>
        {
            Data = data,
            IsSuccess = true,
            Csrf = csrf
        };
    }

    public static DataApiModel<T> Ok<T>(T data, string message, string csrf)
    {
        return new DataApiModel<T>
        {
            Data = data,
            Message = message,
            IsSuccess = true,
            Csrf = csrf
        };
    }

    public static RequestStatusModel Error(string message, ErrorCode errorCode)
    {
        return new RequestStatusModel
        {
            Data = new StatusCodeModel
            {
                ErrorCode = errorCode
            },
            Message = message,
            IsSuccess = false
        };
    }
}