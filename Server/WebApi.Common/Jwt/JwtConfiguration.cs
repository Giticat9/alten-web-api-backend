namespace WebApi.Common.Jwt
{
    /// <summary>
    /// Конфигурация для работы с JWT
    /// </summary>
    public class JwtConfiguration
    {
        /// <summary>
        /// Издатель токена
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Потребитель токена
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// Время жизни токена в минутах
        /// </summary>
        public int ExpirationMinutes { get; set; }

        /// <summary>
        /// Строка для создания симметричного ключа шифрования токена JWT
        /// </summary>
        public string SymmetricSecurityKey { get; set; } = string.Empty;

        /// <summary>
        /// Определяет необходимость валидации времени жизни токена
        /// </summary>
        public bool ValidateLifetime { get; set; } = true;
    }
}
