using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }
        public UpdateAuthorModel Model {get; set;}
        private readonly IBookStoreDbContext _context;
        public UpdateAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x=> x.Id == AuthorId);
            if(author is null)
            {
                throw new InvalidOperationException("Yazar Bulunamadi");
            }
            if(_context.Authors.Any(x => x.AuthorName.ToLower() == Model.AuthorName.ToLower()&& x.AuthorSurname.ToLower() == Model.AuthorSurname.ToLower() && x.Id != AuthorId))
            {
                throw new InvalidOperationException("Ayni  Yazar Zaten Mevcut");
            }
            author.AuthorName=string.IsNullOrEmpty(Model.AuthorName.Trim()) ? author.AuthorName : Model.AuthorName;
            author.AuthorSurname=string.IsNullOrEmpty(Model.AuthorSurname.Trim()) ? author.AuthorSurname : Model.AuthorSurname;
            author.AuthorDob =Model.AuthorDob;
            _context.SaveChanges();
        }
    }
     public class UpdateAuthorModel
     {
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public DateTime AuthorDob { get; set; }
     }
}