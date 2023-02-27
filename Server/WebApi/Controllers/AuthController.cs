using Microsoft.AspNetCore.Mvc;
using WebApi.Base;
using WebApi.BE;
using WebApi.BL;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBL _authBL;

        public AuthController(IAuthBL authBL)
        {
            _authBL = authBL;
        }

        [HttpPost("login")]
        public async Task<BaseApiModel> Login([FromForm] AuthLoginRequestModel model)
        {
            var loginInfo = await _authBL.GetLoginInfoByLoginPasswordAsync(model.Login, model.Password);

            if (!loginInfo.IsSuccess)
            {
                return DataApiModel.Error("Логин или пароль указаны не верно", ErrorCode.LoginInfoDoesNotExists);
            }

            var token = await _authBL.GenerateJwtTokenByLoginInfoAsync(loginInfo);
            var response = new AuthLoginResponseModel
            {
                AccessToken= token,
                UserName = loginInfo.FullName,
                Email = loginInfo.Email
            };

            return DataApiModel.Ok(response);
        }
    }
}