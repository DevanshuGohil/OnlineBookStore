namespace Online_Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserInfoes", "Username", c => c.String(nullable: false));
            AlterColumn("dbo.UserInfoes", "emailId", c => c.String(nullable: false));
            AlterColumn("dbo.UserInfoes", "password", c => c.String(nullable: false));
            AlterColumn("dbo.UserInfoes", "city", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserInfoes", "city", c => c.String());
            AlterColumn("dbo.UserInfoes", "password", c => c.String());
            AlterColumn("dbo.UserInfoes", "emailId", c => c.String());
            AlterColumn("dbo.UserInfoes", "Username", c => c.String());
        }
    }
}
