using System.ComponentModel;

namespace WebApi.BE
{
    /// <summary>
    /// Права доступа учетной записи
    /// </summary>
    [Serializable]
    [Flags]
    public enum AccountPermissions
    {
        /// <summary>
        /// Без прав
        /// </summary>
        [Description("Без прав")]
        None = 0,

        /// <summary>
        /// Права администатора
        /// </summary>
        [Description("Администратор")]
        Administrator = 1,

        /// <summary>
        /// Просмотр учетных записей
        /// </summary>
        [Description("Просмотр учетных записей")]
        AccountsView = 2,

        /// <summary>
        /// Создание учетных записей
        /// </summary>
        [Description("Создание учетных записей")]
        AccountsCreate = 4,

        /// <summary>
        /// Удаление учетных записей
        /// </summary>
        [Description("Удаление учетных записей")]
        AccountsDetele = 8,

        /// <summary>
        /// Изменение учетных записей
        /// </summary>
        [Description("Изменение учетных записей")]
        AccountsEdit = 16,

        /// <summary>
        /// Полный доступ к учетным записям
        /// </summary>
        [Description("Полный доступ к учетным записям")]
        AccountsFullAccess = 32,
    }
}
