using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace WebApi.UnitTests.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
         private readonly IMapper _mapper;
        public CreateUserCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistUserEmailIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)
            var user = new User(){
                Name ="Özlemm",
                Surname="Özçelikkk",
                Email="ozlm@gmailk.com",
                Password="12346"};
            _context.Users.Add(user);
            _context.SaveChanges();

            CreateUserCommand command =new CreateUserCommand(_context,_mapper);
            command.Model = new CreateUserModel(){Email = user.Email};

            //act (Çalıştırma) & assert (Doğrulama)
            FluentActions
                .Invoking(()=>command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kullanici zaten mevcut.");

        }

        [Fact]
        public void WhenValidInputsAreGiven_User_ShouldBeCreated()
        {
            //arrange
            CreateUserCommand command =new CreateUserCommand(_context,_mapper);
            CreateUserModel model = new CreateUserModel()
            {
                Name ="Özzlem",
                Surname="Özçzelik",
                Email="ozlmz@gmail.com",
                Password="12634" };
            command.Model = model;
            _context.SaveChanges();

            //act
            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var genre=_context.Users.SingleOrDefault(g=> g.Email == model.Email);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
            genre.Surname.Should().Be(model.Surname);
            genre.Password.Should().Be(model.Password);

        }
    }
}