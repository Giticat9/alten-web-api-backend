namespace WebApi.Common.Jwt
{
    /// <summary>
    /// Ключ для создания подписи JWT (приватный)
    /// </summary>
    public interface IJwtSigningEncodingKey : IJwtSigningKeyGenerator
    {
        /// <summary>
        /// Алгоритм подписания JWT
        /// </summary>
        string SigningAlgorithm { get; }
    }
}
