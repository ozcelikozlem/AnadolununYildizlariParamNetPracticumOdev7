using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Users
    {
        public static void AddUsers(this BookStoreDbContext context)
        {
            context.Users.AddRange(
            new User
            {
                Name ="Özlem",
                Surname="Özçelik",
                Email="ozlm@gmail.com",
                Password="1234"
            });
        }
    }
}