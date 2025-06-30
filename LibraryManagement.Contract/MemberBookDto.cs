using System.Collections.Generic;

namespace LibraryManagement.Contract
{
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
