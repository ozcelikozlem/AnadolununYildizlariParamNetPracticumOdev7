using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _context;
        public DeleteAuthorCommand(IBookStoreDbContext context)
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
            var booksAuthor = _context.Books.Where(x=> x.AuthorId== AuthorId).ToList();
            if (booksAuthor.Count>0)
            {
                throw new InvalidOperationException("Yazarin kitaplari mevcut. Yazari silemezsin!");
            }
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}