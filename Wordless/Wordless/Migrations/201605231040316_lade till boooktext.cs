namespace Wordless.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ladetillboooktext : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "BookText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "BookText");
        }
    }
}
