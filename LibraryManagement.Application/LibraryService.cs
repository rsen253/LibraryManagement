using LibraryManagement.Contract;
using LibraryManagement.Data;
using LibraryManagement.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LibraryManagement.Contract.MemberBookDto;

namespace LibraryManagement.Application
{
    public class LibraryService : ILibraryService
    {
        private readonly ILibraryRepository _repository;

        public LibraryService(ILibraryRepository repository)
        {
            _repository = repository;
        }

        public List<Library> GetLibrariesWithAllData()
        {
            var libraries = _repository.GetLibrariesWithAllData();

            return libraries;
        }

        public List<BookDto> GetBooksByAuthorId(int authorId)
        {
            return _repository.GetBooksByAuthorId(authorId);
        }

        public BookHistoryDto GetBookHistoryByBookId(int bookId)
        {
            return _repository.GetBookHistoryByBookId(bookId);
        }

        public MemberBookDto GetBooksBorrowedByMemberId(int memberId)
        {
            return _repository.GetBooksBorrowedByMemberId(memberId);
        }

        public void AddBookWithAuthorAndMembers(BookCreateRequest request)
        {
            _repository.AddBookWithAuthorAndMembers(request);
        }
    }
}
