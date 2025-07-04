﻿using LibraryManagement.Application;
using LibraryManagement.CustomAttributes;
using System.Web.Http;
using static LibraryManagement.Contract.MemberBookDto;

namespace LibraryManagement.Controllers
{
    public class LibraryController : ApiController
    {
        private readonly ILibraryService _service;

        public LibraryController(ILibraryService service)
        {
            _service = service;
        }

        // GET: api/library
        [HttpGet]
        [Route("api/library")]
        [SimpleTokenAuthorize]
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
