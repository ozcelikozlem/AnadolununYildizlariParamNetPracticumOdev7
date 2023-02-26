using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly IBookStoreDbContext _context;
        public DeleteGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x=> x.Id == GenreId);
            if(genre is null)
            {
                throw new InvalidOperationException("Kitap Türü Mevcut Değil");
            }
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}