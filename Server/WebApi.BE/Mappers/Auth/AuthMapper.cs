using System.Data;
using System.Data.SqlClient;

namespace WebApi.BE.Mappers
{
    public class AuthMapper : IAuthMapper
    {
        ///<inheritdoc cref="IAuthMapper" />
        public LoginInfoModel MapReaderToLoginInfoModel(SqlDataReader reader)
        {
            return new()
            {
                UserExternalGuid = reader.GetGuid("external_guid"),
                FullName = (string)reader.GetValue("full_name"),
                Login =  (string)reader.GetValue("login"),
                Email = !reader.IsDBNull("email") ? (string)reader.GetValue("email") : string.Empty,
                IsSuccess = true,
            };
        }
    }
}
