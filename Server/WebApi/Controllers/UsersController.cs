using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Base;
using WebApi.BE;
using WebApi.BL;

namespace WebApi.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUsersBL _usersBL;
    
    public UsersController(IUsersBL usersBL)
    {
        _usersBL = usersBL;
    }

    [HttpGet("")]
    public async Task<DataApiModel<List<UserResponse>>> Get([FromQuery] string? search) 
    {
        var users = await _usersBL.GetAllAsync(search);
        
        return DataApiModel.Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<UserResponse?> GetById(long id)
    {
        var user = await _usersBL.GetByIdAsync(id);

        return user;
    }

    [Authorize]
    [HttpPost("addOrUpdate")]
    public async Task<Guid> AddOrUpdate([FromBody] UserModel model, [FromQuery] long? id = null)
    {
        var userGuid = await _usersBL.AddOrUpdateAsync(model, id);

        return userGuid;
    }

    [Authorize]
    [HttpDelete("delete")]
    public async Task<long> DeleteById([FromQuery] long id)
    {
        var userId = await _usersBL.DeleteAsync(id);

        return userId;
    }
}