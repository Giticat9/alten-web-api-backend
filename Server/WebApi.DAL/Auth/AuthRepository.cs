using System.Data.SqlClient;
using WebApi.BE;
using WebApi.BE.Mappers;
using WebApi.Common;

namespace WebApi.DAL.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IWebApiDatabase _webApiDatabase;
        private readonly IAuthMapper _authMapper;

        public AuthRepository(IWebApiDatabase webApiDatabase, IAuthMapper authMapper)
        {
            _webApiDatabase = webApiDatabase;
            _authMapper = authMapper;
        }

        public async Task<LoginInfoModel> GetLoginInfoByLoginPasswordAsync(string login, string password)
        {
            try
            {
                using var connection = await _webApiDatabase.UseWebApiDatabaseAsync();
                using var command = connection.CreateCommand();

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "[dbo].[spLoginInfoByLoginPasswordSelect]";
                command.Parameters.AddRange(new SqlParameter[]
                {
                    new SqlParameter("@login", login),
                    new SqlParameter("@password", password)
                });

                using var reader = await command.ExecuteReaderAsync();

                if (!reader.HasRows)
                {
                    return new LoginInfoModel { IsSuccess = false };
                }

                if (await reader.ReadAsync())
                {
                    var mappedLoginInfo = _authMapper.MapReaderToLoginInfoModel(reader);
                    return mappedLoginInfo;
                }

                return new LoginInfoModel { IsSuccess = false };
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка получения данных об учетной записи", ex);
            }
        }
    }
}
