using System.Security.Claims;
using WebApi.BE;
using WebApi.Common.Jwt;
using WebApi.DAL;

namespace WebApi.BL
{
    public class AuthBL : IAuthBL
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJwtGenerator _jwtGenerator;

        public AuthBL(IAuthRepository authRepository, IJwtGenerator jwtGenerator)
        {
            _authRepository = authRepository;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<LoginInfoModel> GetLoginInfoByLoginPasswordAsync(string login, string password)
        {
            try
            {
                return await _authRepository.GetLoginInfoByLoginPasswordAsync(login, password);
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> GenerateJwtTokenByLoginInfoAsync(LoginInfoModel loginInfo)
        {
            return await Task.Run(() =>
            {
                var claims = new Claim[]
                {
                    new Claim(AuthUserAccountClaimNames.UserAccountGuid, loginInfo.UserExternalGuid.ToString()),
                    new Claim(AuthUserAccountClaimNames.UserAccountFullName, loginInfo.FullName.ToString()),
                    new Claim(AuthUserAccountClaimNames.UserAccountLogin, loginInfo.Login.ToString()),
                    new Claim(AuthUserAccountClaimNames.UserAccountEmail, loginInfo.Email ?? string.Empty),
                };

                var token = _jwtGenerator.GenerateTokenString(claims);

                return token;
            });
        }
    }
}
