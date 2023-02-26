using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreId { get; set; }
        public readonly IBookStoreDbContext _contex;
        public readonly IMapper _mapper;
        public GetGenreDetailQuery(IBookStoreDbContext contex, IMapper mapper)
        {
            _contex = contex;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _contex.Genres.Where(x => x.IActive && x.Id == GenreId );
            if(genre is null)
            {
                throw new InvalidOperationException("Kitap Türü Bulunamadi.");
            }
            return _mapper.Map<GenreDetailViewModel>(genre);
        }
    }
    public class GenreDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}