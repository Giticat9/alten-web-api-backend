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
        private readonly IUsersBL _usersBL;
        
        public AuthController(IUsersBL usersBL)
        {
            _usersBL = usersBL;
        }
        
        [HttpPost("login")]
        public async Task<BaseApiModel> Login([FromForm] AuthLoginRequestModel model)
        {
            var searchUserModel = new UserSearchModel
            {
                Login = model.Login
            };

            var user = await _usersBL.GetUserBySearchAsync(searchUserModel);

            if (user == null)
            {
                return DataApiModel.Error("Пользователь не найден", ErrorCode.UserNotExists);
            }

            var isPasswordValid = false;
            
            return DataApiModel.Ok(new {
                isExists = true
            });
        }
    }
}