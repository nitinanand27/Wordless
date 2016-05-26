namespace Wordless.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lastchange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "UploadedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "UploadedOn");
        }
    }
}
