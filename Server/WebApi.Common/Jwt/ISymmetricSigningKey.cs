namespace WebApi.Common.Jwt
{
    /// <summary>
    /// Симметричный ключ для подписи JWT
    /// </summary>
    public interface ISymmetricSigningKey : IJwtSigningEncodingKey, IJwtSigningDecodingKey
    {

    }
}
