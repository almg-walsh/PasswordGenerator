namespace PasswordGenerator
{
    using System.Configuration;

    public interface IConfigurationProvider
    {
        /// <summary>
        /// Gets the connetion string.
        /// </summary>
        /// <param name="connectionName">Name of the connection.</param>
        /// <returns>The connection string setting for the supplied connection.</returns>
        ConnectionStringSettings GetConnectionString(string connectionName);
    }
}