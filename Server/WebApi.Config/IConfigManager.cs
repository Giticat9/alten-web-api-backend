using Microsoft.Extensions.Configuration;

namespace WebApi.Config;
public interface IConfigManager
{
    string GetConnectionString(string connectionName);

    IConfigurationSection GetConfigurationSection(string key);
}
