using WebApi.BE;

namespace WebApi.BL
{
    public interface IUsersBL
    {
        Task<List<UserResponse>> GetAllAsync(string? search);
        Task<UserResponse?> GetByIdAsync(long id);
        Task<Guid> AddOrUpdateAsync(UserModel model, long? id);
        Task<long> DeleteAsync(long id);
    }
}