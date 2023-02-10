using System.Data;
using System.Data.SqlClient;
using WebApi.Config;

namespace WebApi.Common;

public class WebApiDatabase : IWebApiDatabase
{
    private readonly IConfigManager _configManager;
    
    public WebApiDatabase(IConfigManager configManager)
    {
        _configManager = configManager;
    }
    
    public async Task<SqlConnection> UseWebApiDatabaseAsync()
    {
        try 
        {
            var connectionString = _configManager.GetConnectionString("defaultConnection");

            if(string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Ошибка установки соединения с базой данных. Отсутствуют параметры подключения.");
            }

            var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            if(connection.State == ConnectionState.Open)
            {
                return connection;
            }
            else 
            {
                throw new Exception("Ошибка установки соединения с базой данных. Не удалось открыть соединение.");
            }
        }
        catch(Exception ex) 
        {
            throw new Exception(ex.Message, ex);
        }
    }
}