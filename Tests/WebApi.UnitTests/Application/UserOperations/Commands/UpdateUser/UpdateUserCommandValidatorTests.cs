using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.UserOperations.Commands.UpdateUser;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(1," "," "," "," ")]
        [InlineData(0," "," "," "," ")]
        [InlineData(1,"aaa"," "," "," ")]
        [InlineData(1,"aaaa"," "," "," ")]
        [InlineData(1,"aaaa","aaaa "," "," ")]
        [InlineData(1,"aaaa","aaaa ","aaaa "," ")]
        [InlineData(1,"aaaa","aaaa "," ","aaaa ")]
        [InlineData(1,"aaaa"," ","aaaa "," ")]
        [InlineData(1,"aaaa"," "," ","aaaa ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id ,string name,string surname,string email,string password)
        {
            //arrange
           UpdateUserCommand command = new UpdateUserCommand(null);
           command.Model =new UpdateUserModel()
           {
                Name = name,
                Surname=surname,
                Email=email,
                Password=password
           };
           command.UserId = id;

            //act
            UpdateUserCommandValidator validator = new UpdateUserCommandValidator();
            var result = validator.Validate(command);

            //assert
            
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenValidInoutsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            UpdateUserCommand command = new UpdateUserCommand(null);
            command.Model =new UpdateUserModel()
            {
                Name ="Özlem",
                Surname="Özçelik",
                Email="ozlm@gmail.com",
                Password="1234"
            };
            command.UserId = 1;
            
            //act
            UpdateUserCommandValidator validator = new UpdateUserCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}