using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]s")]
    public class authorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public authorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAuthor()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context,_mapper);
            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpGet("id")]
        public IActionResult GetAuthorDetail(int id)
        {
            AuthorDetailViewModel result;
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context,_mapper);
            query.AuthorId =id;
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            command.Model=newAuthor;

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpPut("id")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateAuthorModel updateAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId=id;
            command.Model = updateAuthor;

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId=id;

            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
        
    }
}