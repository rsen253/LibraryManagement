using LibraryManagement.Contract;
using LibraryManagement.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using static LibraryManagement.Contract.MemberBookDto;

namespace LibraryManagement.Data
{
    public interface ILibraryRepository
    {
        void AddBookWithAuthorAndMembers(MemberBookDto.BookCreateRequest request);
        BookHistoryDto GetBookHistoryByBookId(int bookId);
        MemberBookDto GetBooksBorrowedByMemberId(int memberId);
        List<BookDto> GetBooksByAuthorId(int authorId);
        List<Library> GetLibrariesWithAllData();
    }

    public class LibraryRepository : ILibraryRepository
    {
        private readonly LibraryContext _context;

        public LibraryRepository()
        {
            _context = new LibraryContext();
        }

        private void InsertSampleDataIfNotExists()
        {

            var author = new Author { FullName = "J.K. Rowling" };
            var book1 = new Book { Title = "Harry Potter 1", Author = author };
            var book2 = new Book { Title = "Harry Potter 2", Author = author };

            var member1 = new Member { Name = "John Doe" };
            var member2 = new Member { Name = "Jane Smith" };

            // Assign books to members (Many-to-Many)
            book1.Members = new List<Member> { member1, member2 };
            book2.Members = new List<Member> { member1 };

            var address = new Address { Location = "123 Magic Street" };
            var library = new Library
            {
                Name = "Hogwarts Library",
                Address = address,
                Books = new List<Book> { book1, book2 }
            };

            _context.Libraries.Add(library);
            _context.SaveChanges();
        }

        public List<Library> GetLibrariesWithAllData()
        {
            var libraries = _context.Libraries
                .Include(l => l.Address)
                .Include(l => l.Books.Select(b => b.Author))
                .Include(l => l.Books.Select(b => b.Members))
                .ToList();

            return libraries;
        }

        public List<BookDto> GetBooksByAuthorId(int authorId)
        {
            // without author
            var test = _context.Books.ToList();

            return _context.Books
                .Where(b => b.AuthorId == authorId)
                .Include(b => b.Author)
                .Select(b => new BookDto
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    AuthorName = b.Author.FullName
                }).ToList();
        }

        public BookHistoryDto GetBookHistoryByBookId(int bookId)
        {
            var book = _context.Books
                .Include(b => b.Members)
                .FirstOrDefault(b => b.BookId == bookId);

            if (book == null) return null;

            return new BookHistoryDto
            {
                BookId = book.BookId,
                Title = book.Title,
                BorrowedBy = book.Members.Select(m => new MemberDto
                {
                    MemberId = m.MemberId,
                    Name = m.Name
                }).ToList()
            };
        }

        public MemberBookDto GetBooksBorrowedByMemberId(int memberId)
        {
            var member = _context.Members
                .Include(m => m.Books.Select(b => b.Author))
                .FirstOrDefault(m => m.MemberId == memberId);

            if (member == null) return null;

            return new MemberBookDto
            {
                MemberId = member.MemberId,
                MemberName = member.Name,
                BorrowedBooks = member.Books.Select(b => new BookDto
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    AuthorName = b.Author.FullName
                }).ToList()
            };
        }

        public void AddBookWithAuthorAndMembers(BookCreateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title) ||
                string.IsNullOrWhiteSpace(request.AuthorName) ||
                request.LibraryId <= 0)
                throw new ArgumentException("Invalid request data.");

            // Fetch or create the author
            var author = _context.Authors.FirstOrDefault(a => a.FullName == request.AuthorName);
            if (author == null)
            {
                author = new Author { FullName = request.AuthorName };
                _context.Authors.Add(author);
            }

            // Fetch the library
            var library = _context.Libraries.FirstOrDefault(l => l.LibraryId == request.LibraryId);
            if (library == null)
                throw new InvalidOperationException("Library not found.");

            // Fetch or create members
            var members = new List<Member>();
            foreach (var memberName in request.MemberNames.Distinct())
            {
                var member = _context.Members.FirstOrDefault(m => m.Name == memberName);
                if (member == null)
                {
                    member = new Member { Name = memberName };
                    _context.Members.Add(member);
                }
                members.Add(member);
            }

            // Create the new book
            var book = new Book
            {
                Title = request.Title,
                Author = author,
                Library = library,
                Members = members
            };

            _context.Books.Add(book);
            _context.SaveChanges();
        }

    }
}
