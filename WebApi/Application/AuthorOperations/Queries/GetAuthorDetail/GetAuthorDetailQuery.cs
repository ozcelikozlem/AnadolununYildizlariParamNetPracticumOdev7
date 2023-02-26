using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.DbOperations;
namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId { get; set; }
        public readonly IBookStoreDbContext _contex;
        public readonly IMapper _mapper;
        public GetAuthorDetailQuery(IBookStoreDbContext contex, IMapper mapper)
        {
            _contex = contex;
            _mapper = mapper;
        }
        public AuthorDetailViewModel Handle()
        {
            var author = _contex.Authors.SingleOrDefault(x=> x.Id == AuthorId);
            if(author is null)
            {
                throw new InvalidOperationException("Yazar Bulunamadi.");
            }
            return _mapper.Map<AuthorDetailViewModel>(author);
        }
    }
    public class AuthorDetailViewModel
    {
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public DateTime AuthorDob { get; set; }
    }
}