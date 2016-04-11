namespace PasswordGenerator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// The initial create.
    /// </summary>
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAccounts",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Password = c.String(),
                        Date = c.DateTime(nullable: false),
                        ValidPassword = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserAccounts");
        }
    }
}
