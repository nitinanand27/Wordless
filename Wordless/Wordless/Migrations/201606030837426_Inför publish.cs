namespace Wordless.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Införpublish : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        BookTitle = c.String(name: "Book-Title", maxLength: 50),
                        Description = c.String(maxLength: 200),
                        No_of_Downloads = c.Int(name: "No._of_Downloads", nullable: false),
                        Price = c.Decimal(nullable: false, precision: 6, scale: 2),
                        AuthorId = c.Int(nullable: false),
                        Genre = c.Int(nullable: false),
                        FileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Users", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Files", t => t.FileId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.FileId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 15),
                        Password = c.String(maxLength: 15),
                        Name = c.String(maxLength: 20),
                        Email = c.String(),
                        Author = c.Boolean(nullable: false),
                        Admin = c.Boolean(nullable: false),
                        Funds = c.Decimal(nullable: false, precision: 6, scale: 2),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Text = c.String(maxLength: 300),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 200),
                        ContentType = c.String(),
                        Content = c.Binary(),
                        UploadedOn = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Purchased-Books",
                c => new
                    {
                        PurchasedBookId = c.Int(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        Recension = c.String(maxLength: 200),
                        DateofPurchase = c.DateTime(name: "Date of Purchase", nullable: false),
                        BookId = c.Int(nullable: false),
                        BuyerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PurchasedBookId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.BuyerId)
                .Index(t => t.BookId)
                .Index(t => t.BuyerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "FileId", "dbo.Files");
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.Purchased-Books", "BuyerId", "dbo.Users");
            DropForeignKey("dbo.Purchased-Books", "BookId", "dbo.Books");
            DropForeignKey("dbo.Files", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "BookId", "dbo.Books");
            DropIndex("dbo.Purchased-Books", new[] { "BuyerId" });
            DropIndex("dbo.Purchased-Books", new[] { "BookId" });
            DropIndex("dbo.Files", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "BookId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Books", new[] { "FileId" });
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropTable("dbo.Purchased-Books");
            DropTable("dbo.Files");
            DropTable("dbo.Comments");
            DropTable("dbo.Users");
            DropTable("dbo.Books");
        }
    }
}
