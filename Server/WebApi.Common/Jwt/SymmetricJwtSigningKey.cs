using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Common.Jwt;

namespace WebApi.Common.Jwt
{
    /// <summary>
    /// Реализация работы с симметричным ключом шифрования токенов
    /// </summary>
    public class SymmetricJwtSigningKey : ISymmetricSigningKey
    {
        private readonly SymmetricSecurityKey _securityKey;
        private readonly JwtConfiguration _configuration;

        public SymmetricJwtSigningKey(JwtConfiguration configuration)
        {
            _configuration = configuration;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.SymmetricSecurityKey));
        }

        /// <inheritdoc cref="IJwtSigningEncodingKey.SigningAlgorithm" />
        public string SigningAlgorithm { get; } = SecurityAlgorithms.HmacSha256;

        /// <inheritdoc cref="IJwtSigningEncodingKey.GetKey" />
        public SecurityKey GetKey() => _securityKey;
    }
}
