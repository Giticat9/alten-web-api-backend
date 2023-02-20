using WebApi.BE;

namespace WebApi.BL
{
    public interface IUsersBL
    {
        Task<List<UserResponse>> GetAllAsync(string? search);
        Task<UserResponse?> GetUserBySearchAsync(UserSearchModel model);
        Task<Guid> AddOrUpdateAsync(UserModel model, long? id);
        Task<long> DeleteAsync(long id);
    }
}