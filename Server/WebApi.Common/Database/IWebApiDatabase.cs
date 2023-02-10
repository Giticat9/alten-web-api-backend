using System.Data.SqlClient;

namespace WebApi.Common;

public interface IWebApiDatabase
{
    Task<SqlConnection> UseWebApiDatabaseAsync();
}