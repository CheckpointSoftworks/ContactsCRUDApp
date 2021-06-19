namespace ContactsCRUDApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedUserIdForContact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "UserId", c => c.Guid(nullable: false));
            DropColumn("dbo.Contacts", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contacts", "MyProperty", c => c.Guid(nullable: false));
            DropColumn("dbo.Contacts", "UserId");
        }
    }
}
