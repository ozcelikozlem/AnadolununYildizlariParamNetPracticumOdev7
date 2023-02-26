using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model {get; set;}
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x=> x.AuthorName == Model.AuthorName && x.AuthorSurname == Model.AuthorSurname);
            if(author is not null)
            {
                throw new InvalidOperationException("Yazar Zaten Mevcut");
            }
            author = _mapper.Map<Author>(Model);
            // author = new Author();
            // author.AuthorName = Model.AuthorName;
            // author.AuthorSurname = Model.AuthorSurname;
            // author.AuthorDob = Model.AuthorDob;
            _context.Authors.Add(author);
            _context.SaveChanges();
        }
    }
    public class CreateAuthorModel
    {
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public DateTime AuthorDob { get; set; }
    }
    
}