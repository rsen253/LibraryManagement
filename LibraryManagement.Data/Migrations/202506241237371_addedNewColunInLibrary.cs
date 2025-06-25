namespace LibraryManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedNewColunInLibrary : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Libraries", "AddressId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Libraries", "AddressId");
        }
    }
}
