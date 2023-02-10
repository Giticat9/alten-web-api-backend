using Microsoft.Extensions.Configuration;

namespace WebApi.Config;
public class ConfigManager : IConfigManager
{
    private readonly IConfiguration _configuration;
    
    public ConfigManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetConnectionString(string connectionName) 
    {
        return _configuration.GetConnectionString(connectionName) ?? "";
    }

    public IConfigurationSection GetConfigurationSection(string key)
    {
        return _configuration.GetSection(key);
    }
}