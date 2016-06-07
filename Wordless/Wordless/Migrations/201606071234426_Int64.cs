namespace Wordless.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Int64 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Files", "UserId", "dbo.Users");
            DropForeignKey("dbo.Purchased-Books", "BuyerId", "dbo.Users");
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Users");
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Files", new[] { "UserId" });
            DropIndex("dbo.Purchased-Books", new[] { "BuyerId" });
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Books", "AuthorId", c => c.Long(nullable: false));
            AlterColumn("dbo.Users", "UserId", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Comments", "UserId", c => c.Long(nullable: false));
            AlterColumn("dbo.Files", "UserId", c => c.Long(nullable: false));
            AlterColumn("dbo.Purchased-Books", "BuyerId", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.Users", "UserId");
            CreateIndex("dbo.Books", "AuthorId");
            CreateIndex("dbo.Comments", "UserId");
            CreateIndex("dbo.Files", "UserId");
            CreateIndex("dbo.Purchased-Books", "BuyerId");
            AddForeignKey("dbo.Comments", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Files", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Purchased-Books", "BuyerId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Books", "AuthorId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.Purchased-Books", "BuyerId", "dbo.Users");
            DropForeignKey("dbo.Files", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropIndex("dbo.Purchased-Books", new[] { "BuyerId" });
            DropIndex("dbo.Files", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Purchased-Books", "BuyerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Files", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "UserId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Books", "AuthorId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Users", "UserId");
            CreateIndex("dbo.Purchased-Books", "BuyerId");
            CreateIndex("dbo.Files", "UserId");
            CreateIndex("dbo.Comments", "UserId");
            CreateIndex("dbo.Books", "AuthorId");
            AddForeignKey("dbo.Books", "AuthorId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Purchased-Books", "BuyerId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Files", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Comments", "UserId", "dbo.Users", "UserId");
        }
    }
}
