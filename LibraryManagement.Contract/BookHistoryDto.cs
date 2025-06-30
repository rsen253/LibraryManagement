using System.Collections.Generic;

namespace LibraryManagement.Contract
{
    public class BookHistoryDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public List<MemberDto> BorrowedBy { get; set; }
    }
}
