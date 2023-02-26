using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void WhenGenreIdBeZeroOrLessThenZero_Validator_ShouldBeReturnErrors(int genreId )
        {
            //arrange
           DeleteGenreCommand command = new DeleteGenreCommand(null);
           command.GenreId = genreId;

            //act
             DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenGenreIdBeGreaterThanZero_Validator_ShouldBeReturnErrors()
        {
            //arrange
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = 3;

            //act
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}