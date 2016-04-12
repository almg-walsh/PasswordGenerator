namespace PasswordGenerator.Models
{
    using System.Data.Entity;

    /// <summary>
    /// The Context for the Database.
    /// </summary>
    /// <seealso cref="DbContext" />
    public class UserAccountDbContext : DbContext, IUserAccountDbContext
    {
        IConfigurationProvider _configurationProvider;

        public UserAccountDbContext(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;

            Database.Connection.ConnectionString = _configurationProvider.GetConnectionString("UserDbContext").ConnectionString;
        }

        /// <summary>
        /// Gets or sets the user account.
        /// </summary>
        /// <value>
        /// The user account.
        /// </value>
        public DbSet<UserAccount> UserAccount { get; set; }

        public void Commit()
        {
            SaveChanges();
        }
    }
}
