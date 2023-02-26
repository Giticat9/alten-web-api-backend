using System.Data;
using System.Data.SqlClient;
using WebApi.BE.Account;
using WebApi.BE.Mappers;
using WebApi.Common;
using WebApi.Common.Helpers;

namespace WebApi.DAL
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly IWebApiDatabase _webApiDatabase;
        private readonly IAccountsMapper _accountsMapper;

        public AccountsRepository(IWebApiDatabase webApiDatabase, IAccountsMapper accountsMapper)
        {
            _webApiDatabase = webApiDatabase;
            _accountsMapper = accountsMapper;
        }

        public async Task<List<AccountInfoModel>> GetAll(string? term)
        {
            try
            {
                using var connection = await _webApiDatabase.UseWebApiDatabaseAsync();
                using var command = connection.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[spAccountsGetAll]";
                command.Parameters.AddWithValue("@term", (term ?? string.Empty).Trim());

                using var reader = await command.ExecuteReaderAsync();

                var result = new List<AccountInfoModel>();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        var mappedAccount = _accountsMapper.MapReaderToAccountInfoModel(reader);

                        result.Add(mappedAccount);
                    }

                    if (await reader.NextResultAsync())
                    {
                        var sectionRelationAccountList = new List<AccountInfoSectionRelationAccount>();

                        while (await reader.ReadAsync())
                        {
                            var mappedRelation = _accountsMapper.MapReaderToAccountInfoSectionRelationAccountModel(reader);
                            sectionRelationAccountList.Add(mappedRelation);
                        }

                        result = result
                            .Select(account => new AccountInfoModel
                            { 
                                Guid = account.Guid,
                                FirstName = account.FirstName,
                                LastName = account.LastName,
                                MiddleName = account.MiddleName,
                                Position = account.Position,
                                Email = account.Email,
                                Login = account.Login,
                                CreatedAt = account.CreatedAt,
                                Permissions = account.Permissions,
                                SectionAccessList = sectionRelationAccountList
                                    .FindAll(relation => relation.AccountGuid == account.Guid)
                                    .Select(relation => new AccountInfoSectionAccessModel { Guid = relation.Guid, Description = relation.Description })
                                    .ToList()
                            })
                            .ToList();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка получения всех учетных записей", ex);
            }
        }

        public async Task<AccountInfoModel> GetByGuid(Guid guid)
        {
            try
            {
                using var connection = await _webApiDatabase.UseWebApiDatabaseAsync();
                using var command = connection.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[spAccountsGetByGuid]";
                command.Parameters.AddWithValue("@guid", guid);

                using var reader = await command.ExecuteReaderAsync();

                var accountInfo = new AccountInfoModel();

                if (reader.HasRows)
                {
                    if (await reader.ReadAsync())
                    {
                        var mapperdAccountInfo = _accountsMapper.MapReaderToAccountInfoModel(reader);
                        accountInfo = mapperdAccountInfo;
                    }

                    if (await reader.NextResultAsync())
                    {
                        accountInfo.SectionAccessList = new List<AccountInfoSectionAccessModel>();

                        while (await reader.ReadAsync())
                        {
                            var mappedRelation = _accountsMapper.MapReaderToAccountInfoSectionAccessModel(reader);
                            accountInfo.SectionAccessList.Add(mappedRelation);
                        }
                    }
                }

                return accountInfo;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ошибка получения учетной записи по гуиду. Guid: {0}", guid), ex);
            }
        }

        public async Task<bool> AddOrUpdate(AccountInfoRequestModel model)
        {
            try
            {
                using var connection = await _webApiDatabase.UseWebApiDatabaseAsync();
                using var command = connection.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[spAccountAddOrUpdate]";

                var sqlAccountDataModel = new SQLAccountDataModel
                {
                    Guid = model.Guid,
                    Login = model.Login,
                    Password = model.Password,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    Position = model.Position,
                    Email = model.Email,
                    Permission = model.Permission,
                };

                var sqlAccountDataTable 
                    = DataBaseHelpers.ConvertModelToDataTable(sqlAccountDataModel);
                var sqlAccountDataParameter = new SqlParameter
                {
                    ParameterName = "@account_data",
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "[dbo].[AccountModelType]",
                    Value = sqlAccountDataTable
                };

                command.Parameters.Add(sqlAccountDataParameter);

                var sqlAccountAccessRightsTable 
                    = DataBaseHelpers.ConvertArrayToDataTable((model.SectionAccessList ?? new List<Guid>()).Select(guid => guid));
                var sqlAccountAccessRightsParameter = new SqlParameter
                {
                    ParameterName = "@account_sections_access",
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "[dbo].[ArrayOfGuid]",
                    Value = sqlAccountAccessRightsTable
                };

                command.Parameters.Add(sqlAccountAccessRightsParameter);


                await command.ExecuteNonQueryAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка добавления новой учетной записи", ex);
            }
        }

        public async Task<Guid> DeleteByGuid(Guid guid)
        {
            try
            {
                using var connection = await _webApiDatabase.UseWebApiDatabaseAsync();
                using var command = connection.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[spAccountsDeleteByGuid]";
                command.Parameters.AddWithValue("@guid", guid);

                await command.ExecuteNonQueryAsync();

                return guid;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ошибка удаления учетной записи по гуиду. Guid: {0}", guid), ex);
            }
        }

        public async Task<int> GetAccountsCountAll()
        {
            try
            {
                using var connetion = await _webApiDatabase.UseWebApiDatabaseAsync();
                using var command = connetion.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[spAccountsGetCountAll]";

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    return (int)reader.GetValue("count_all");
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка получения количества учетных записей", ex);
            }
        }

        public async Task<bool> CheckAccountExists(string? login, string? email)
        {
            try
            {
                using var connection = await _webApiDatabase.UseWebApiDatabaseAsync();
                using var command = connection.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[spAccountsCheckExists]";
                command.Parameters.AddRange(new SqlParameter[]
                {
                    new SqlParameter("@login", login),
                    new SqlParameter("@email", email)
                });

                var result = await command.ExecuteScalarAsync();
                return result != null && DBNull.Value != result && (bool)result;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при проверки существования учетной записи", ex);
            }
        }
    }
}
