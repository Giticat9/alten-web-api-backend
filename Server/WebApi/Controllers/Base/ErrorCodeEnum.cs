namespace WebApi.Base
{
    /// <summary>
    /// Перечисление ошибок запросов
    /// </summary>
    public enum ErrorCode : int
    {
        /// <summary>
        /// Пользователь отсутствует
        /// </summary>
        UserNotExists = 1,

        /// <summary>
        /// Неверный пароль пользователя
        /// </summary>
        UserPasswordInvalid = 2
    }
}