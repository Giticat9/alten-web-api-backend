using System.Data;
using System.Data.SqlClient;
using WebApi.Common;
using WebApi.BE;
using WebApi.BE.Mappers;
using WebApi.Common.Helpers;

namespace WebApi.DAL;
public class UsersRepository : IUsersRepository
{
    private readonly IWebApiDatabase _webApiDatabase;
    private readonly IUsersMappers _usersMappers;
    private readonly IDataBaseHelpers _dataBaseHelpers;

    public UsersRepository(
        IWebApiDatabase webApiDatabase, 
        IUsersMappers usersMappers,
        IDataBaseHelpers dataBaseHelpers
    )
    {
        _webApiDatabase = webApiDatabase;
        _usersMappers = usersMappers;
        _dataBaseHelpers = dataBaseHelpers;
    }

    public async Task<List<UserResponse>> GetAllUsersAsync(string? search)
    {
        try
        {
            using (var connection = await _webApiDatabase.UseWebApiDatabaseAsync())
            using (var command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[spUsersGetAll]";
                command.Parameters.AddWithValue("@search", (search != null || string.IsNullOrEmpty(search)) ? search : DBNull.Value);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    var users = new List<UserResponse>();
                    
                    if (reader.HasRows)
                    {
                        while(await reader.ReadAsync())
                        {
                            var mappedUser = _usersMappers.MapReaderToUserResponseModel(reader);   
                            users.Add(mappedUser);
                        }

                        return users;
                    }
                    else 
                    {
                        return users;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка получения списка пользователей", ex);
        }
    }

    public async Task<UserResponse?> GetUserByIdAsync(long id)
    {
        try 
        {
            using (var connection = await _webApiDatabase.UseWebApiDatabaseAsync())
            using (var command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[spUsersGetById]";
                command.Parameters.Add(new SqlParameter("@id", id));

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (!reader.HasRows) 
                    {
                        return null;
                    }

                    await reader.ReadAsync();
                    var mappedUser = _usersMappers.MapReaderToUserResponseModel(reader);

                    return mappedUser;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка получения пользователя по id", ex);
        }
    }

    public async Task<Guid> AddOrUpdateUserAsync(UserModel model, long? id)
    {
        try
        {
            using (var connection = await _webApiDatabase.UseWebApiDatabaseAsync())
            using (var command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[spUsersAddOrUpdate]";
                command.Parameters.AddWithValue("@id", id != null ? id : DBNull.Value);

                var userTable = _dataBaseHelpers.ConvertModelToDataTable<UserModel>(model);
                command.Parameters.Add(new SqlParameter 
                {
                    ParameterName = "@user",
                    Value = userTable,
                    SqlDbType = SqlDbType.Structured
                });

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return reader.GetGuid("guid");
                    }

                    return Guid.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка при создании/редактировании пользователя", ex);
        }
    }

    public async Task<long> DeleteUserAsync(long id)
    {
        try
        {
            using (var connection = await _webApiDatabase.UseWebApiDatabaseAsync())
            using (var command = connection.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[spUsersDeleteById]";
                command.Parameters.Add(new SqlParameter("@id", id));

                await command.ExecuteNonQueryAsync();

                return id;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(string.Format("Ошибка удаления пользователя с id = {0}", id), ex);
        }
    }
}