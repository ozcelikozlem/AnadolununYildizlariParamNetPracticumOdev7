using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.UserOperations.Queries.GetUsers
{
    public class GetUsersQuery
    {
    public readonly IBookStoreDbContext _contex;
        public readonly IMapper _mapper;
        public GetUsersQuery(IBookStoreDbContext contex, IMapper mapper)
        {
            _contex = contex;
            _mapper = mapper;
        }

        public List<UsersViewModel> Handle()
        {
            var users = _contex.Users.OrderBy(x => x.Id);
            List<UsersViewModel> returnObj = _mapper.Map<List<UsersViewModel>>(users);
            return returnObj;
        }
    }

    public class UsersViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}