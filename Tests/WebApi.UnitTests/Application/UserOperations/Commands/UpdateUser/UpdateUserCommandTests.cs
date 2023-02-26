using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.UserOperations.Commands.UpdateUser;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateUserCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenUserIsNotFound_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)

            UpdateUserCommand command =new UpdateUserCommand(_context);
            command.UserId=100;

            //act (Çalıştırma) & assert (Doğrulama)
            FluentActions
                .Invoking(()=>command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kullanıcı Bulunamadı");

        }

        [Fact]
        public void WhenValidInputsAreGiven_User_ShouldBeUpdated()
        {
            //arrange
            UpdateUserCommand command =new UpdateUserCommand(_context);
            UpdateUserModel model = new UpdateUserModel()
            { 
                Name ="Özlema",
                Surname="Özçelika",
                Email="ozlmaa@gmail.com",
                Password="12534"
            };
            command.Model = model;
            command.UserId=1;
            //act
            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var user =_context.Users.SingleOrDefault(g=> g.Id == command.UserId);
            user.Should().NotBeNull();
            user.Name.Should().Be(model.Name);
            user.Surname.Should().Be(model.Surname);
            user.Password.Should().Be(model.Password);
        }
    }
}