using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(1," ")]
        [InlineData(1,"Lor")]
        [InlineData(0,"Lor")]
        [InlineData(0," ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id ,string name)
        {
            //arrange
           UpdateGenreCommand command = new UpdateGenreCommand(null);
           command.Model =new UpdateGenreModel()
           {
                Name = name,
                IsActive =true
           };
           command.GenreId = id;

            //act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenValidInoutsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.Model =new UpdateGenreModel()
            {
                Name = "True Crime",

            };
            command.GenreId = 3;
            
            //act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}