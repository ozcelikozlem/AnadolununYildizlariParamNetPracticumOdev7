using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOpreations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void WhenBookIdBeZeroOrLessThenZero_Validator_ShouldBeReturnErrors(int bookId )
        {
            //arrange
           DeleteBookCommand command = new DeleteBookCommand(null);
           command.BookId = bookId;

            //act
             DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenBookIdBeGreaterThanZero_Validator_ShouldBeReturnErrors()
        {
            //arrange
           DeleteBookCommand command = new DeleteBookCommand(null);
           command.BookId = 3;

            //act
             DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }

    }
}