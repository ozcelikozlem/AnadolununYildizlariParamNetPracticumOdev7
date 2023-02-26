using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        public readonly IBookStoreDbContext _contex;
        public readonly IMapper _mapper;

        public GetAuthorsQuery(IBookStoreDbContext contex, IMapper mapper)
        {
            _contex = contex;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var authors = _contex.Authors.OrderBy(x => x.Id);
            List<AuthorsViewModel> returnObj = _mapper.Map<List<AuthorsViewModel>>(authors);
            return returnObj;
        }
    }
    public class AuthorsViewModel
    {
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public DateTime AuthorDob { get; set; }
    }
}