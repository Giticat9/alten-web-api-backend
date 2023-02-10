using System.ComponentModel.DataAnnotations;

namespace WebApi.BE;

///<summary>
/// Модель запроса на авторизацию пользователя
///</summary>
public class AuthLoginRequestModel : IValidatableObject
{
    [Required(ErrorMessage = "Логин обязателен для заполнения")]
    [DataType(DataType.Text)]
    public string Login { get; set; } = "";

    [Required(ErrorMessage = "Пароль обязателен для заполнения")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Модель ответа при успешной авторизации пользователя
/// </summary>
public class AuthLoginResponseModel
{
    public string AccessToken { get; set; } = "";
    public string UserName { get; set; } = "";
}