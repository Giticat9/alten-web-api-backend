using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Common.Jwt;

namespace WebApi.Common.Jwt
{
    /// <summary>
    /// Расширения для настройки и подключения JWT
    /// </summary>
    public static class JwtExtensions
    {
        private const string AuthScheme = "JwtBearer";

        /// <summary>
        /// Добавление зависимостей для работы с JWT с симметричной подписью
        /// </summary>
        /// <param name="services">Контейнер зависимостей приложения</param>
        public static IServiceCollection AddSymmetricJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var jwtConfiguration = config.GetJwtConfiguration();
            var symmetricKey = new SymmetricJwtSigningKey(jwtConfiguration);

            services.AddSingleton<ISymmetricSigningKey>(symmetricKey);
            services.AddTransient<IJwtGenerator, SymmetricJwtGenerator>();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = AuthScheme;
                    options.DefaultChallengeScheme = AuthScheme;
                })
                .AddJwtBearer(AuthScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = symmetricKey.GetKey(),

                        ValidateIssuer = true,
                        ValidIssuer = jwtConfiguration.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwtConfiguration.Audience,

                        ValidateLifetime = jwtConfiguration.ValidateLifetime
                    };
                });

            return services;
        }

        public static JwtConfiguration GetJwtConfiguration(this IConfiguration config)
        {
            var section = config.GetSection("JWTTokenSettings");

            return new()
            {
                Issuer = section.GetValue<string>("Issuer") ?? "",
                Audience = section.GetValue<string>("Audience") ?? "",
                ExpirationMinutes = section.GetValue<int>("ExpirationMinutes"),
                ValidateLifetime = section.GetValue<bool>("ValidateLifetime"),
                SymmetricSecurityKey = section.GetValue<string>("SymmetricSecurityKey") ?? ""
            };
        }
    }
}
