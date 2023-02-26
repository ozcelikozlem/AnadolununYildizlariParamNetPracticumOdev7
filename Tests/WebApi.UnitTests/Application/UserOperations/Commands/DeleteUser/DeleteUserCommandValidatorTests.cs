using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.UserOperations.Commands.DeleteUser;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void WhenUserIdBeZeroOrLessThenZero_Validator_ShouldBeReturnErrors(int userId )
        {
            //arrange
           DeleteUserCommand command = new DeleteUserCommand(null);
           command.UserId= userId;

            //act
             DeleteUserCommandValidator validator = new DeleteUserCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenUserIdBeGreaterThanZero_Validator_ShouldBeReturnErrors()
        {
            //arrange
            DeleteUserCommand command = new DeleteUserCommand(null);
            command.UserId = 1;

            //act
            DeleteUserCommandValidator validator = new DeleteUserCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}