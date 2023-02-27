using System.Data.SqlClient;

namespace WebApi.BE.Mappers
{
    public interface IAuthMapper
    {
        /// <summary>
        /// Преобразует объект <see cref="SqlDataReader"/> в модель <see cref="LoginInfoModel"/>
        /// </summary>
        LoginInfoModel MapReaderToLoginInfoModel(SqlDataReader reader);
    }
}
