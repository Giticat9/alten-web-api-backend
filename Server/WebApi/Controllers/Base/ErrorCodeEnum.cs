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
        UserPasswordInvalid = 2,

        /// <summary>
        /// Недостаточно учетных записей для удаления
        /// </summary>
        FewAccountsToDelete = 3,

        /// <summary>
        /// Нет учетных записец с правами на создание новых 
        /// или изменение существующих учетных записец
        /// </summary>
        NoAccountsWithRights = 4,
    }
}