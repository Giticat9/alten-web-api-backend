using WebApi.DAL;
using WebApi.Common;
using WebApi.BE;

namespace WebApi.BL;

public class UsersBL : IUsersBL
{
    private readonly IUsersRepository _usersRepository;
    private readonly IWebApiDatabase _webApiDatabase;
    
    public UsersBL(IUsersRepository usersRepository, IWebApiDatabase webApiDatabase)
    {
        _usersRepository = usersRepository;
        _webApiDatabase = webApiDatabase;
    }

    public async Task<List<UserResponse>> GetAllAsync(string? search)
    {
        try
        {
            return await _usersRepository.GetAllUsersAsync(search);
        }
        catch
        {
            throw;
        }
    }

    public async Task<UserResponse?> GetByIdAsync(long id)
    {
        try 
        {
            return await _usersRepository.GetUserByIdAsync(id);
        }
        catch
        {
            throw;
        }
    }

    public async Task<Guid> AddOrUpdateAsync(UserModel model, long? id)
    {
        try 
        {
            return await _usersRepository.AddOrUpdateUserAsync(model, id);
        }
        catch
        {
            throw;
        }
    }

    public async Task<long> DeleteAsync(long id)
    {
        try
        {
            return await _usersRepository.DeleteUserAsync(id);
        }
        catch
        {
            throw;
        }
    }
}