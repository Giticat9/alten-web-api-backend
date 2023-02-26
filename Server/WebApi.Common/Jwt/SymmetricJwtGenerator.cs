using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebApi.Common.Jwt
{
    /// <summary>
    /// Генератор JWT с симметричным шифрованием
    /// </summary>
    public class SymmetricJwtGenerator : IJwtGenerator
    {
        private readonly ISymmetricSigningKey _signingKey;
        private readonly JwtConfiguration _jwtConfig;

        public SymmetricJwtGenerator(ISymmetricSigningKey signingKey, IConfiguration config)
        {
            _signingKey = signingKey;
            _jwtConfig = config.GetJwtConfiguration();
        }

        /// <inheritdoc cref="IJwtGenerator.GenerateTokenString(Claim[])"/>
        public string GenerateTokenString(Claim[] claims)
        {
            var token = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtConfig.ExpirationMinutes),
                signingCredentials: SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private SigningCredentials SigningCredentials => new(_signingKey.GetKey(), _signingKey.SigningAlgorithm);
    }
}
