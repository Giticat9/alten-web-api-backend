using System.Data;
using System.Data.SqlClient;

namespace WebApi.BE.Mappers;

public class UsersMappers : IUsersMappers
{
    public UserModel MapReaderToUserModel(SqlDataReader reader)
    {   
        return new UserModel
        {
            Guid =  (Guid)reader.GetGuid("guid"),
            LastName = (string)reader.GetValue("lastName"),
            FirstName = (string)reader.GetValue("firstName"),
            MiddleName = !reader.IsDBNull("middlename") ? (string)reader.GetValue("middleName") : null, 
            Email = !reader.IsDBNull("email") ? (string)reader.GetValue("email") : null,
            Login = (string)reader.GetValue("login"),
            Password = (string)reader.GetValue("password"),
        };
    }

    public UserResponse MapReaderToUserResponseModel(SqlDataReader reader)
    {
        return new UserResponse
        {
            Id = (int)reader.GetValue("id"),
            Guid =  (Guid)reader.GetGuid("guid"),
            LastName = (string)reader.GetValue("lastName"),
            FirstName = (string)reader.GetValue("firstName"),
            MiddleName = !reader.IsDBNull("middlename") ? (string)reader.GetValue("middleName") : null, 
            Email = !reader.IsDBNull("email") ? (string)reader.GetValue("email") : null,
            Login = (string)reader.GetValue("login"),
            CreatedAt = reader.GetDateTime("created_at")
        };
    }
}