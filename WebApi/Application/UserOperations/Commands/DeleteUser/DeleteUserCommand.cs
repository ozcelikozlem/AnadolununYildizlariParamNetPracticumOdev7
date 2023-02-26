using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DbOperations;

namespace WebApi.Application.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommand
    {
        public int UserId { get; set; }
        private readonly IBookStoreDbContext _context;
        public DeleteUserCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(x=> x.Id == UserId);
            if(user is null)
            {
                throw new InvalidOperationException("Kullanıcı Mevcut Değil");
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}