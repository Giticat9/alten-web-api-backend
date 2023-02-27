using System.ComponentModel.DataAnnotations;

namespace WebApi.BE;

///<summary>
/// Модель запроса на авторизацию пользователя
///</summary>
public class AuthLoginRequestModel
{
    [Required(ErrorMessage = "Логин обязателен для заполнения")]
    [DataType(DataType.Text)]
    public string Login { get; set; } = "";

    [Required(ErrorMessage = "Пароль обязателен для заполнения")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";
}

/// <summary>
/// Модель ответа при успешной авторизации пользователя
/// </summary>
public class AuthLoginResponseModel
{
    public string AccessToken { get; set; } = "";
    public string UserName { get; set; } = "";
    public string? Email { get; set; } = null;
}

/// <summary>
/// Список утверждений для генерации токена на основе данных об учетной записи
/// </summary>
public class AuthUserAccountClaimNames
{
    public const string UserAccountLogin = "UserAccountLogin";
    public const string UserAccountEmail = "UserAccountEmail";
    public const string UserAccountFullName = "UserAccountFullName";
    public const string UserAccountGuid = "UserAccountGuid";
}

/// <summary>
/// Модель, описывающая информацию о логине в учетную запись
/// </summary>
public class LoginInfoModel
{
    public Guid UserExternalGuid { get; set; } = Guid.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Флаг, показывающий, успешно ли получена информация об учетной записи
    /// </summary>
    public bool IsSuccess { get; set; } = false;
}