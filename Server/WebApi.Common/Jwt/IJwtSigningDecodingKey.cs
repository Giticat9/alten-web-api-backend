namespace WebApi.Common.Jwt
{
    /// <summary>
    /// Ключ для создания подписи JWT (публичный)
    /// </summary>
    public interface IJwtSigningDecodingKey : IJwtSigningKeyGenerator
    {

    }
}
