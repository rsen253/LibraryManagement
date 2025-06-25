using LibraryManagement.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() : base("name=LibraryDb")
        {
        }

        public DbSet<Library> Libraries { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // One-to-One: Library <-> Address
            modelBuilder.Entity<Library>()
                .HasOptional(l => l.Address)
                .WithRequired(a => a.Library);

            // One-to-Many: Library -> Books
            modelBuilder.Entity<Library>()
                .HasMany(l => l.Books) // with whome
                .WithRequired(b => b.Library); // 
                //.HasForeignKey(b => b.LibraryId);

            // Many-to-One: Book -> Author
            modelBuilder.Entity<Book>()
                .HasRequired(b => b.Author)
                .WithMany(a => a.Books);
            //.HasForeignKey(b => b.AuthorId);

            // Many-to-Many: Book <-> Member
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Members)
                .WithMany(m => m.Books)
                .Map(m =>
                {
                    m.ToTable("IssuedBooks");
                    m.MapLeftKey("BookId");
                    m.MapRightKey("MemberId");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
