namespace LibraryManagement.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initilaMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Libraries", t => t.AddressId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Libraries",
                c => new
                    {
                        LibraryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.LibraryId);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        AuthorId = c.Int(nullable: false),
                        LibraryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Libraries", t => t.LibraryId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.LibraryId);
            
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                    })
                .PrimaryKey(t => t.AuthorId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.MemberId);
            
            CreateTable(
                "dbo.IssuedBooks",
                c => new
                    {
                        BookId = c.Int(nullable: false),
                        MemberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookId, t.MemberId })
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.MemberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "LibraryId", "dbo.Libraries");
            DropForeignKey("dbo.IssuedBooks", "MemberId", "dbo.Members");
            DropForeignKey("dbo.IssuedBooks", "BookId", "dbo.Books");
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.Addresses", "AddressId", "dbo.Libraries");
            DropIndex("dbo.IssuedBooks", new[] { "MemberId" });
            DropIndex("dbo.IssuedBooks", new[] { "BookId" });
            DropIndex("dbo.Books", new[] { "LibraryId" });
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropIndex("dbo.Addresses", new[] { "AddressId" });
            DropTable("dbo.IssuedBooks");
            DropTable("dbo.Members");
            DropTable("dbo.Authors");
            DropTable("dbo.Books");
            DropTable("dbo.Libraries");
            DropTable("dbo.Addresses");
        }
    }
}
