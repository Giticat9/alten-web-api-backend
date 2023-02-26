using Microsoft.IdentityModel.Tokens;

namespace WebApi.Common.Jwt
{
    /// <summary>
    /// Общий интерфейс для генераторов ключей подписи JWT
    /// </summary>
    public interface IJwtSigningKeyGenerator
    {
        /// <summary>
        /// Генерирует ключ для шифрования JWT
        /// </summary>
        SecurityKey GetKey();
    }
}
