using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Versioning;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DbOperations;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.AddControllers
    {
        [Authorize]
        [ApiController]
        [Route("api/[controller]s")]
        public class bookController : ControllerBase
        {
            private readonly IBookStoreDbContext _context;
            private readonly IMapper _mapper;
        public bookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet] // GET: api/books
            public IActionResult GetBooks()
            {
                GetBooksQuery query = new GetBooksQuery(_context,_mapper);
                var result = query.Handle();
                return Ok(result);
            }

           
            [HttpGet("{id}")] // GET: api/books/id
            public IActionResult GetBookById(int id)
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
                query.BookId =id;
                GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
                validator.ValidateAndThrow(query);
                var result =query.Handle();
                return Ok(result);
            }

            /*[HttpGet] //GET:api/books/getquery
            [Route("getquery")]
            public Book Get([FromQuery] string id)
            {
                var book=_context.Books.Where(book=>book.Id==Convert.ToInt32(id)).SingleOrDefault();
                return book;
            }*/

            [HttpPost]// POST:api/books
            public IActionResult AddBook([FromBody] CreateBookModel newBook)
            {
                CreateBookCommand command = new CreateBookCommand(_context,_mapper);
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
                return Ok();
            }

            [HttpPut("{id}")]//PUT [FromBody] : api/books/id
            public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId=id;
                command.Model=updatedBook;
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
                return Ok();
                
            }

            [HttpDelete("{id}")]//DELETE [FromBody] :: api/books/id
            public IActionResult DeleteBook(int id)
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId=id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
                return Ok();
            }

            /*[HttpGet("lastadded")] // GET: api/books/lastadded
            public List<Book> GetBooksLast()
            {
                var bookListDesc = _context.Books.OrderByDescending(x=>x.Id).ToList<Book>();
                return bookListDesc;
            }
            
            [HttpGet("alphabetical")] // GET: api/books/alphabetical
            public List<Book> GetBooksAlphabetical()
            {
                var bookListAlphabetical = _context.Books.OrderBy(x=>x.Title).ToList<Book>();
                return bookListAlphabetical;
            }

            [HttpGet("newedition")] // GET: api/books/newedition
            public List<Book> GetBooksEdition()
            {
                var bookListEdition = _context.Books.OrderByDescending(x=>x.PublishDate).ToList<Book>();
                return bookListEdition;
            }
            [HttpGet("list")] //GET:api/books/list?genreId=id
            public IEnumerable<Book> GetProductsByCategory(string genreId)
            {
                var bookList = _context.Books.OrderBy(x=>x.Id).ToList<Book>();
                return bookList.Where(
                p => string.Equals(p.GenreId.ToString(), genreId, StringComparison.OrdinalIgnoreCase));
            }
            [HttpPut]//PUT [FrmomQuery]:: api/books/updatebookquery
            [Route("updatebookquery")]
            public IActionResult UpdateBook([FromQuery] string  id, [FromBody] Book updatedBook)
            {
                var book =_context.Books.SingleOrDefault(x=> x.Id==Convert.ToInt32(id));
                // if(book is null)
                // {
                //     return BadRequest();
                // }
                book.GenreId=updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
                book.PageCount= updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
                book.PublishDate= updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
                book.Title=updatedBook.Title != default ? updatedBook.Title : book.Title;
                _context.SaveChanges();
                return Ok("Book Registration Updated");
            }
            [HttpDelete]//DELETE [FromQuery] : api/books/deletebookquery
            [Route("deletebookquery")]
            public IActionResult DeleteBook([FromQuery] string  id)
            {
                var book=_context.Books.SingleOrDefault(x=>x.Id==Convert.ToInt32(id));
                // if (book is null)
                // {
                //     return BadRequest();
                // }
                _context.Books.Remove(book);
                _context.SaveChanges();
                return Ok("Book Registration Updated");
            }
            [HttpPatch] // PATCH [FromQuery]: api/books/patchbookquery
            [Route("patchbookquery")]
            public IActionResult PatchBook([FromQuery] string id,JsonPatchDocument<Book> patchBook)
            {
                // if (patchBook == null)
                // {
                //     return BadRequest(ModelState);
                // }
                var book =_context.Books.SingleOrDefault(x=> x.Id==Convert.ToInt32(id));
                // if(book is null)
                // {
                //     return NotFound();
                // }
                patchBook.ApplyTo(book,ModelState);
                if (ModelState.IsValid)
                {
                    _context.SaveChanges();
                    return Ok(book);
                }
                return BadRequest(ModelState);
            }
            // op: “add”, “remove”, “replace”, “move”, “copy” ve “test”.
            // [
            // {
            // "op":"replace",
            // "path":"/players/2/nationality",
            // "value":"Hırvatistan"
            // }
            ]
            [HttpPatch("{id}")] // PATCH [FromBody]: api/books/id
            public IActionResult PatchBook(int id,JsonPatchDocument<Book> patchBook)
            {
                if (patchBook == null)
                {
                    return BadRequest(ModelState);
                }
                 var book =_context.Books.SingleOrDefault(x=> x.Id==id);
                if(book is null)
                {
                    throw new InvalidOperationException("Kitap mevcut değil.");
                }
                patchBook.ApplyTo(book,ModelState);
                if (ModelState.IsValid)
                {
                    _context.SaveChanges();
                    return Ok(book);
                }
                return BadRequest(ModelState);
            }*/

        }
    }