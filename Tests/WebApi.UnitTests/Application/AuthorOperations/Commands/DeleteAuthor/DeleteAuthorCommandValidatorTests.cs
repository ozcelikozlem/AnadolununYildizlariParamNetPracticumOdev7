using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void WhenBookIdBeZeroOrLessThenZero_Validator_ShouldBeReturnErrors(int authorId )
        {
            //arrange
           DeleteAuthorCommand command = new DeleteAuthorCommand(null);
           command.AuthorId = authorId;

            //act
             DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenBookIdBeGreaterThanZero_Validator_ShouldBeReturnErrors()
        {
            //arrange
           DeleteAuthorCommand command = new DeleteAuthorCommand(null);
           command.AuthorId = 3;

            //act
             DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}