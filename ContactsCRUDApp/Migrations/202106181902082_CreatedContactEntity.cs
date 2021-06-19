namespace ContactsCRUDApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedContactEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MyProperty = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhonePrimary = c.String(),
                        PhoneSecondary = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        StressAddress1 = c.String(),
                        StressAddress2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Contacts");
        }
    }
}
