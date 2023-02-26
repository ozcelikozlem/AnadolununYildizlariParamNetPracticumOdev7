using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(" ")]
        [InlineData("L")]
        [InlineData("Lor")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            //arrange
           CreateGenreCommand command = new CreateGenreCommand(null);
           command.Model =new CreateGenreModel()
           {
                Name = name,
           };

            //act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenValidInoutsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(null);
            command.Model =new CreateGenreModel()
            {
                Name = "Truee Crimee",

            };
            
            //act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}