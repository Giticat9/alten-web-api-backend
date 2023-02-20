using WebApi.BE;

namespace WebApi.DAL;

public interface IUsersRepository
{
    Task<List<UserResponse>> GetAllUsersAsync(string? search);
    Task<UserResponse?> GetUserBySearchAsync(UserSearchModel model);
    Task<Guid> AddOrUpdateUserAsync(UserModel model, long? id);
    Task<long> DeleteUserAsync(long id);
}