using LibraryManagement.Contract;
using LibraryManagement.Domain;
using System.Collections.Generic;

namespace LibraryManagement.Application
{
    public interface ILibraryService
    {
        void AddBookWithAuthorAndMembers(MemberBookDto.BookCreateRequest request);
        BookHistoryDto GetBookHistoryByBookId(int bookId);
        MemberBookDto GetBooksBorrowedByMemberId(int memberId);
        List<BookDto> GetBooksByAuthorId(int authorId);
        List<Library> GetLibrariesWithAllData();
    }
}