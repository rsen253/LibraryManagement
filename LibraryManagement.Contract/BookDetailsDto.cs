using System.Collections.Generic;

namespace LibraryManagement.Contract
{
    public class BookDetailsDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }

        public List<MemberDto> Members { get; set; }
        public string AuthorName { get; set; }
    }
}
