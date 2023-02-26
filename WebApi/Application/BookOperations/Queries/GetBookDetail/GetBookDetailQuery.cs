using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId {get; set;}
        public GetBookDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Include(x=> x.Genre).Include(x=>x.Author).SingleOrDefault(x => x.Id == BookId);
            
            if(book is null)
            {
                throw new InvalidOperationException("Kitap mevcut değil.");
            }
            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);
            return vm;
            //new BookDetailViewModel();
            // vm.Title=book.Title;
            // vm.Genre=((GenreEnum)book.GenreId).ToString();
            // vm.PublishDate=book.PublishDate.Date.ToString("dd/MM/yyy");
            // vm.PageCount=book.PageCount;
            
        }
    }
    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
        public string Author {get; set;}
    }
}