namespace PasswordGenerator.Migrations
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// The configuration.
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<Models.UserDbContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration" /> class.
        /// </summary>
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = "PasswordGenerator.Models.UserDbContext";
        }

        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void Seed(Models.UserDbContext context)
        {
            // This method will be called after migrating to the latest version.

            // You can use the DbSet<T>.AddOrUpdate() helper extension method 
            // to avoid creating duplicate seed data. E.g.
            // 
            // context.People.AddOrUpdate(
            // p => p.FullName,
            // new Person { FullName = "Andrew Peters" },
            // new Person { FullName = "Brice Lambson" },
            // new Person { FullName = "Rowan Miller" }
            // );
            // 
        }
    }
}
