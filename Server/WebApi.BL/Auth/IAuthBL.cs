using WebApi.BE;

namespace WebApi.BL
{
    public interface IAuthBL
    {
        Task<LoginInfoModel> GetLoginInfoByLoginPasswordAsync(string login, string password);
        Task<string> GenerateJwtTokenByLoginInfoAsync(LoginInfoModel loginInfo);
    }
}
