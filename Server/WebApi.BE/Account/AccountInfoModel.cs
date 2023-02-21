namespace WebApi.BE.Account
{
    /// <summary>
    /// Модель, описывающая информацию об учетной записи
    /// </summary>
    public class AccountInfoModel
    {
        public Guid Guid { get; set; } = Guid.Empty;
        public string Login { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<AccountInfoSectionAccessModel> SectionAccessList { get; set; } = new List<AccountInfoSectionAccessModel>();
        public AccountInfoPermissionsModel Permissions { get; set; } = new AccountInfoPermissionsModel();
    }

    /// <summary>
    /// Модель запроса для учетной записи
    /// </summary>
    public class AccountInfoRequestModel : AccountInfoModel
    {
        public int Permission { get; set; }

        public AccountInfoRequestModel()
        {
            if (Permissions.IsNoneAccess)
                Permission = (int)AccountPermissions.None;

            if (!Permissions.IsNoneAccess && Permissions.IsAdministrator)
                Permission = (int)AccountPermissions.Administrator;

            if (!Permissions.IsNoneAccess && !Permissions.IsAdministrator && Permissions.IsAccountView)
                Permission += (int)AccountPermissions.AccountsView;

            if (!Permissions.IsNoneAccess && !Permissions.IsAdministrator && Permissions.IsAccountCreate)
                Permission += (int)AccountPermissions.AccountsCreate;

            if (!Permissions.IsNoneAccess && !Permissions.IsAdministrator && Permissions.IsAccountDelete)
                Permission += (int)AccountPermissions.AccountsDetele;

            if (!Permissions.IsNoneAccess && !Permissions.IsAdministrator && Permissions.IsAccountEdit)
                Permission += (int)AccountPermissions.AccountsEdit;

            if (!Permissions.IsNoneAccess && !Permissions.IsAdministrator && Permissions.IsAccountFullAccess)
                Permission += (int)AccountPermissions.AccountsFullAccess;
        }

    }

    /// <summary>
    /// Модель, описывающая права доступа учетной записи
    /// </summary>
    public class AccountInfoPermissionsModel
    {
        public bool IsNoneAccess { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsAccountView { get; set; }
        public bool IsAccountCreate { get; set; }
        public bool IsAccountDelete { get; set;}
        public bool IsAccountEdit { get; set;}
        public bool IsAccountFullAccess { get; set; }
    }

    /// <summary>
    /// Модель, описывающая поля доступа к разделу
    /// </summary>
    public class AccountInfoSectionAccessModel
    {
        public Guid Guid { get; set; } = Guid.Empty;
        public string Description { get; set; } = string.Empty;
    }

    /// <summary>
    /// Модель, описывающая модель поля связи разделов и учетных записей
    /// </summary>
    public class AccountInfoSectionRelationAccount : AccountInfoSectionAccessModel
    {
        public Guid AccountGuid { get; set; } = Guid.Empty;
    }
}
