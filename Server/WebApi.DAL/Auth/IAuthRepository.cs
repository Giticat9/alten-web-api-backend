using WebApi.BE;

namespace WebApi.DAL
{
    public interface IAuthRepository
    {
        Task<LoginInfoModel> GetLoginInfoByLoginPasswordAsync(string login, string password);
    }
}
