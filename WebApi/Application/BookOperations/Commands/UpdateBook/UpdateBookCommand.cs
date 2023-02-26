using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public int BookId {get; set;}
        public UpdateBookModel Model {get; set;}
        public UpdateBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var book =_dbContext.Books.SingleOrDefault(x=> x.Id==BookId);
            if(book is null)
             {
                throw new InvalidOperationException("Kitap mevcut değil.");
            }
            book.GenreId=Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.Title=Model.Title != default ? Model.Title : book.Title;
             book.AuthorId=Model.AuthorId != default ? Model.AuthorId : book.AuthorId;
            _dbContext.SaveChanges();
        }
    }
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId {get; set;}
    }
}