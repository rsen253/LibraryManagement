using System.Collections.Generic;

namespace LibraryManagement.Application
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
    }

    public class BookHistoryDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public List<MemberDto> BorrowedBy { get; set; }
    }

    public class MemberDto
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
    }

    public class MemberBookDto
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public List<BookDto> BorrowedBooks
        {
            get; set;

        }

        public class BookCreateRequest
        {
            public string Title { get; set; }
            public string AuthorName { get; set; }
            public int LibraryId { get; set; }
            public List<string> MemberNames { get; set; }
        }

    }
}
