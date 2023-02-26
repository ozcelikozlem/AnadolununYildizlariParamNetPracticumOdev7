using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace WebApi.UnitTests.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(" "," "," "," ")]
        [InlineData("Ozl","Ozl","Ozl","Ozl")]
        [InlineData("Ozl","Ozl"," "," ")]
        [InlineData("Ozl"," ","Ozl"," ")]
        [InlineData(" "," ","Ozl","Ozl")]
        [InlineData("Ozlem","Ozl","Ozl","Ozl")]
        [InlineData("Ozlem","Ozlem"," "," ")]
        [InlineData("Ozlem","Ozl","Ozlem","Ozl")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname, string email, string password )
        {
            //arrange
           CreateUserCommand command = new CreateUserCommand(null,null);
           command.Model =new CreateUserModel()
           {
                Name = name,
                Surname = surname,
                Email = email,// a year ago
                Password = password,
           };

            //act
            CreateUserCommandValidator validator = new CreateUserCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenValidInoutsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            CreateUserCommand command = new CreateUserCommand(null,null);
            command.Model =new CreateUserModel()
            {
                Name ="Özlemm",
                Surname="Özçelikk",
                Email="ozlm@gmaill.com",
                Password="12345"
            };

            //act
            CreateUserCommandValidator validator = new CreateUserCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}