using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.BE;
using WebApi.BL;
using WebApi.Common;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [SwaggerTag("Работа с авторизацией")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBL _authBL;

        public AuthController(IAuthBL authBL)
        {
            _authBL = authBL;
        }

        [HttpPost("login")]
        [SwaggerOperation(Summary = "Авторизация по логину и паролю")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Успешная авторизация", Type = typeof(AuthLoginResponseModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Неверные логин или пароль", Type = typeof(HttpErrorDescription))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "Внутренняя ошибка сервера", Type = typeof(HttpErrorDescription))]
        public async Task<ActionResult<AuthLoginResponseModel>> Login([FromForm] AuthLoginRequestModel model)
        {
            var loginInfo = await _authBL.GetLoginInfoByLoginPasswordAsync(model.Login, model.Password);

            if (!loginInfo.IsSuccess)
            {
                throw new HttpException(StatusCodes.Status400BadRequest, 
                    "Логин или пароль указаны не верно");
            }

            var token = await _authBL.GenerateJwtTokenByLoginInfoAsync(loginInfo);
            var response = new AuthLoginResponseModel
            {
                AccessToken= token,
                UserName = loginInfo.FullName,
                Email = loginInfo.Email
            };

            return response;
        }
    }
}