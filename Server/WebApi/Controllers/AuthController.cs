using Microsoft.AspNetCore.Mvc;
using WebApi.Base;
using WebApi.Base.Exception;
using WebApi.BE;

namespace WebApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : BaseApiController
{
    [HttpPost("login")]
    public async Task<BaseApiModel> Login()
    {
        return DataApiModel.Ok(HttpContext.Session);
    }
}