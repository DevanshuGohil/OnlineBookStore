namespace Online_Book_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Bookname = c.String(),
                        Authorname = c.String(),
                        BookType = c.String(),
                        price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        UserInfoId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        emailId = c.String(),
                        password = c.String(),
                        MobileNo = c.Int(nullable: false),
                        city = c.String(),
                        role = c.String(),
                    })
                .PrimaryKey(t => t.UserInfoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserInfoes");
            DropTable("dbo.Books");
        }
    }
}
