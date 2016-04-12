namespace PasswordGenerator
{
    using System.Configuration;


    public class ConfigurationProvider : IConfigurationProvider
    {
        public ConnectionStringSettings GetConnectionString(string connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connectionName];
        }
    }
}