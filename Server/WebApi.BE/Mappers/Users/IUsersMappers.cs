using System.Data.SqlClient;

namespace WebApi.BE.Mappers;

public interface IUsersMappers
{
    UserResponse MapReaderToUserResponseModel(SqlDataReader reader);
    UserModel MapReaderToUserModel(SqlDataReader reader);
}