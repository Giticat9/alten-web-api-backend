using System.Security.Claims;

namespace WebApi.Common.Jwt
{
    /// <summary>
    /// Генератор JWT
    /// </summary>
    public interface IJwtGenerator
    {
        /// <summary>
        /// Генерирует зашифрованную строку с JWT
        /// </summary>
        /// <remarks>
        /// Использует для получения настроек класс <see cref="JwtConfiguration"/>
        /// </remarks>
        /// <param name="claims">Утверждения для генерации токена</param>
        string GenerateTokenString(Claim[] claims);
    }
}
