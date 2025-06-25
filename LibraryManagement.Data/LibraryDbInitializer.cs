using LibraryManagement.Domain;
using System.Collections.Generic;
using System.Data.Entity;

namespace LibraryManagement.Data
{
    public class LibraryDbInitializer : CreateDatabaseIfNotExists<LibraryContext>
    {
        protected override void Seed(LibraryContext context)
        {
            var author = new Author { FullName = "J.K. Rowling" };

            var book1 = new Book { Title = "Harry Potter and the Sorcerer's Stone", Author = author };
            var book2 = new Book { Title = "Harry Potter and the Chamber of Secrets", Author = author };

            var member1 = new Member { Name = "Alice Johnson" };
            var member2 = new Member { Name = "Bob Smith" };

            book1.Members = new List<Member> { member1, member2 };
            book2.Members = new List<Member> { member1 };

            var address = new Address { Location = "123 Magic Lane" };

            var library = new Library
            {
                Name = "Hogwarts Central Library",
                Address = address,
                Books = new List<Book> { book1, book2 }
            };

            context.Libraries.Add(library);
            context.SaveChanges();
        }
    }
}