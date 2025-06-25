using LibraryManagement.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static LibraryManagement.Application.MemberBookDto;

namespace LibraryManagement.Controllers
{
    public class LibraryController : ApiController
    {
        private readonly LibraryService _service;

        public LibraryController()
        {
            _service = new LibraryService();
        }

        // GET: api/library
        [HttpGet]
        [Route("api/library")]
        public IHttpActionResult GetLibrariesWithAllData()
        {
            var result = _service.GetLibrariesWithAllData();
            return Ok(result);
        }

        // GET: api/library/books/author/1
        [HttpGet]
        [Route("api/library/books/author/{authorId:int}")]
        public IHttpActionResult GetBooksByAuthorId(int authorId)
        {
            var result = _service.GetBooksByAuthorId(authorId);
            return Ok(result);
        }

        // GET: api/library/book/history/1
        [HttpGet]
        [Route("api/library/book/history/{bookId:int}")]
        public IHttpActionResult GetBookHistoryByBookId(int bookId)
        {
            var result = _service.GetBookHistoryByBookId(bookId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // GET: api/library/member/1/books
        [HttpGet]
        [Route("api/library/member/{memberId:int}/books")]
        public IHttpActionResult GetBooksBorrowedByMemberId(int memberId)
        {
            var result = _service.GetBooksBorrowedByMemberId(memberId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/library/book
        [HttpPost]
        [Route("api/library/book")]
        public IHttpActionResult AddBookWithAuthorAndMembers([FromBody] BookCreateRequest request)
        {
            if (request == null)
                return BadRequest("Request cannot be null.");

            try
            {
                _service.AddBookWithAuthorAndMembers(request);
                return Ok("Book added successfully.");
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
