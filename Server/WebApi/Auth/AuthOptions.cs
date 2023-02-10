using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApi;

public static class AuthOptions
{
    public static string ISSUER_TOKEN
    {
        get => "ALTEN_WEB_SERVER_DG6VolCg8VCm8OOqhO5K2V5k5gsUp2CT";
    }

    public static string AUDIENCE_TOKEN
    {
        get => "ALTEN_WEB_CLIENT_3rDZFyJUBlzmi0CYlIAgsY8Ymw3Y9Smg";
    }

    private static string KEY = "i4}>ntW-3M_a5v}RWrQJ>}O%sT'f1_4:axu={XAkU^o<3V/+#D9r{g~MJ4`+nq>";

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
        => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}