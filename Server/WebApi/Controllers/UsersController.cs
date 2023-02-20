using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Base;
using WebApi.BE;
using WebApi.BL;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersBL _usersBL;
        
        public UsersController(IUsersBL usersBL)
        {
            _usersBL = usersBL;
        } 

        [Authorize]
        [HttpGet("")]
        public async Task<DataApiModel<List<UserResponse>>> Get([FromQuery] string? search) 
        {
            var users = await _usersBL.GetAllAsync(search);

            return DataApiModel.Ok<List<UserResponse>>(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<DataApiModel<UserResponse>> GetById(long id)
        {
            var user = await _usersBL.GetByIdAsync(id);

            return DataApiModel.Ok<UserResponse>(user ?? new UserResponse());
        }

        [Authorize]
        [HttpPost("addOrUpdate")]
        public async Task<DataApiModel<Guid>> AddOrUpdate([FromBody] UserModel model, [FromQuery] long? id = null)
        {
            var userGuid = await _usersBL.AddOrUpdateAsync(model, id);

            return DataApiModel.Ok<Guid>(userGuid);
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<DataApiModel<long>> DeleteById([FromQuery] long id)
        {
            var userId = await _usersBL.DeleteAsync(id);

            return DataApiModel.Ok<long>(userId);
        }
    }
}