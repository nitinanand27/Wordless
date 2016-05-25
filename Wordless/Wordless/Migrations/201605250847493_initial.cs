namespace Wordless.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        BookText = c.String(),
                        TimesPurchased = c.Int(nullable: false),
                        Genre = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Author_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Users", t => t.Author_UserId)
                .Index(t => t.Author_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                        Author = c.Boolean(nullable: false),
                        Admin = c.Boolean(nullable: false),
                        Funds = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        CommentText = c.String(),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        ContentType = c.String(),
                        Content = c.Binary(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.PurchasedBooks",
                c => new
                    {
                        PurchasedBookId = c.Int(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        Recension = c.String(),
                        DateOfPurchase = c.DateTime(nullable: false),
                        Book_BookId = c.Int(),
                        Buyer_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.PurchasedBookId)
                .ForeignKey("dbo.Books", t => t.Book_BookId)
                .ForeignKey("dbo.Users", t => t.Buyer_UserId)
                .Index(t => t.Book_BookId)
                .Index(t => t.Buyer_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Author_UserId", "dbo.Users");
            DropForeignKey("dbo.PurchasedBooks", "Buyer_UserId", "dbo.Users");
            DropForeignKey("dbo.PurchasedBooks", "Book_BookId", "dbo.Books");
            DropForeignKey("dbo.Files", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "BookId", "dbo.Books");
            DropIndex("dbo.PurchasedBooks", new[] { "Buyer_UserId" });
            DropIndex("dbo.PurchasedBooks", new[] { "Book_BookId" });
            DropIndex("dbo.Files", new[] { "User_UserId" });
            DropIndex("dbo.Comments", new[] { "BookId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Books", new[] { "Author_UserId" });
            DropTable("dbo.PurchasedBooks");
            DropTable("dbo.Files");
            DropTable("dbo.Comments");
            DropTable("dbo.Users");
            DropTable("dbo.Books");
        }
    }
}
