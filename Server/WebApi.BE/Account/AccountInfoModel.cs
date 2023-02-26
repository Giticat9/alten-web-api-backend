using System.ComponentModel.DataAnnotations;

namespace WebApi.BE.Account
{
    /// <summary>
    /// Модель, описывающая краткую информацию об учетной записи
    /// </summary>
    public class AccountBrieflyInfoModel
    {
        public string Login { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = null;
        public string? Position { get; set; } = null;
        public string? Email { get; set; } = null;
    }

    /// <summary>
    /// Модель, описывающая информацию об учетной записи
    /// </summary>
    public class AccountInfoModel : AccountBrieflyInfoModel
    {
        public Guid? Guid { get; set; } = null;
        public DateTime? CreatedAt { get; set; }
        public List<AccountInfoSectionAccessModel> SectionAccessList { get; set; } = new List<AccountInfoSectionAccessModel>();
        public AccountInfoPermissionsModel Permissions { get; set; } = new AccountInfoPermissionsModel();
    }

    /// <summary>
    /// Модель запроса для учетной записи
    /// </summary>
    public class AccountInfoRequestModel : AccountInfoModel, IValidatableObject
    {
        public string? Password { get; set; } = string.Empty;
        public new List<Guid>? SectionAccessList { get; set; }
        public int Permission
        {
            get
            {
                var result = 0;

                if (Permissions.IsNoneAccess)
                    result = (int)AccountPermissions.None;

                if (!Permissions.IsNoneAccess && Permissions.IsAdministrator)
                    result = (int)AccountPermissions.Administrator;

                if (!Permissions.IsNoneAccess && !Permissions.IsAdministrator && Permissions.IsAccountView)
                    result += (int)AccountPermissions.AccountsView;

                if (!Permissions.IsNoneAccess && !Permissions.IsAdministrator && Permissions.IsAccountCreate)
                    result += (int)AccountPermissions.AccountsCreate;

                if (!Permissions.IsNoneAccess && !Permissions.IsAdministrator && Permissions.IsAccountDelete)
                    result += (int)AccountPermissions.AccountsDetele;

                if (!Permissions.IsNoneAccess && !Permissions.IsAdministrator && Permissions.IsAccountEdit)
                    result += (int)AccountPermissions.AccountsEdit;

                if (!Permissions.IsNoneAccess && !Permissions.IsAdministrator && Permissions.IsAccountFullAccess)
                    result += (int)AccountPermissions.AccountsFullAccess;

                return result;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validateResult = new List<ValidationResult>();

            if (string.IsNullOrEmpty(Login))
                validateResult.Add(new ValidationResult("Логин обязателен для заполнения"));
            else if (Login.Length < 6)
                validateResult.Add(new ValidationResult("Логин должен состоянить как минимум из 6 символов"));

            if (!Guid.HasValue && (string.IsNullOrEmpty(Password) || Password.Length < 6))
                validateResult.Add(new ValidationResult("Пароль обязателен для заполнения и должен состоять как минимум из 6 символов"));

            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
                validateResult.Add(new ValidationResult("Имя и фамилия пользователя обязательны для заполнения"));

            return validateResult;
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

    /// <summary>
    /// Модель, описывающая поля для добавления/изменения учетной записи в SQL
    /// </summary>
    public class SQLAccountDataModel
    {
        public Guid? Guid { get; set; }
        public string Login { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = string.Empty;
        public string? Position { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public int Permission { get; set; }
    }
}
