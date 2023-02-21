using System.Data;
using System.Data.SqlClient;
using WebApi.BE.Account;
using WebApi.Common.Helpers;

namespace WebApi.BE.Mappers
{
    public class AccountsMapper : IAccountsMapper
    {
        public AccountInfoModel MapReaderToAccountInfoModel(SqlDataReader reader)
        {
            var accessRight = (int)reader.GetValue("permission");

            return new AccountInfoModel
            {
                Guid = reader.GetGuid("guid"),
                FirstName = !reader.IsDBNull("first_name") ? (string)reader.GetValue("first_name") : "",
                LastName = !reader.IsDBNull("last_name") ? (string)reader.GetValue("last_name") : "",
                MiddleName = !reader.IsDBNull("middle_name") ? (string)reader.GetValue("middle_name") : "",
                Position = !reader.IsDBNull("position") ? (string)reader.GetValue("position") : "",
                Email = !reader.IsDBNull("email") ? (string)reader.GetValue("email") : "",
                Login = !reader.IsDBNull("login") ? (string)reader.GetValue("login") : "",
                CreatedAt = reader.GetDateTime("created_at"),
                Permissions = new AccountInfoPermissionsModel
                {
                    IsNoneAccess = accessRight != 1 && FlagsHelper.IsSet((AccountPermissions)accessRight, AccountPermissions.None),
                    IsAdministrator = accessRight == 1 || FlagsHelper.IsSet((AccountPermissions)accessRight, AccountPermissions.Administrator),
                    IsAccountView = accessRight == 1
                        || FlagsHelper.IsSet((AccountPermissions)accessRight, AccountPermissions.AccountsFullAccess)
                        || FlagsHelper.IsSet((AccountPermissions)accessRight, AccountPermissions.AccountsView),
                    IsAccountCreate = accessRight == 1
                        || FlagsHelper.IsSet((AccountPermissions)accessRight, AccountPermissions.AccountsFullAccess)
                        || FlagsHelper.IsSet((AccountPermissions)accessRight, AccountPermissions.AccountsCreate),
                    IsAccountDelete = accessRight == 1
                        || FlagsHelper.IsSet((AccountPermissions)accessRight, AccountPermissions.AccountsFullAccess)
                        || FlagsHelper.IsSet((AccountPermissions)accessRight, AccountPermissions.AccountsDetele),
                    IsAccountEdit = accessRight == 1
                        || FlagsHelper.IsSet((AccountPermissions)accessRight, AccountPermissions.AccountsFullAccess)
                        || FlagsHelper.IsSet((AccountPermissions)accessRight, AccountPermissions.AccountsEdit),
                    IsAccountFullAccess = accessRight == 1 || FlagsHelper.IsSet((AccountPermissions)accessRight, AccountPermissions.AccountsFullAccess),
                }
            };
        }

        public AccountInfoSectionAccessModel MapReaderToAccountInfoSectionAccessModel(SqlDataReader reader)
        {
            return new AccountInfoSectionAccessModel
            {
                Guid = !reader.IsDBNull("section_guid") ? reader.GetGuid("section_guid") : Guid.Empty,
                Description = !reader.IsDBNull("description") ? (string)reader.GetValue("description") : ""
            };
        }

        public AccountInfoSectionRelationAccount MapReaderToAccountInfoSectionRelationAccountModel(SqlDataReader reader)
        {
            return new AccountInfoSectionRelationAccount
            {
                Guid = !reader.IsDBNull("section_guid") ? reader.GetGuid("section_guid") : Guid.Empty,
                AccountGuid = !reader.IsDBNull("account_guid") ? reader.GetGuid("account_guid") : Guid.Empty,
                Description = !reader.IsDBNull("description") ? (string)reader.GetValue("description") : ""
            };
        }
    }
}
