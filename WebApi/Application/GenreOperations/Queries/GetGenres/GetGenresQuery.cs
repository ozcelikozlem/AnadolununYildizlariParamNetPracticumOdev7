using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        public readonly IBookStoreDbContext _contex;
        public readonly IMapper _mapper;
        public GetGenresQuery(IBookStoreDbContext contex, IMapper mapper)
        {
            _contex = contex;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var genres = _contex.Genres.Where(x => x.IActive).OrderBy(x => x.Id);
            List<GenresViewModel> returnObj = _mapper.Map<List<GenresViewModel>>(genres);
            return returnObj;
        }
    }

    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}