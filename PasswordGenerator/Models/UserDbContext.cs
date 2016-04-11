namespace PasswordGenerator.Models
{
    using System.Data.Entity;

    /// <summary>
    /// The Context for the Database.
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public class UserDbContext : DbContext 
    {
        /// <summary>
        /// Gets or sets the user account.
        /// </summary>
        /// <value>
        /// The user account.
        /// </value>
        public DbSet<UserAccount> UserAccount { get; set; }
    }
}
