using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lor","")]
        [InlineData("","Lor")]
        [InlineData("Lor","Lor")]
        [InlineData("Loree","Lor")]
        [InlineData("Loree","")]
        [InlineData("","")]
        [InlineData("Lor","Loree")]
        [InlineData("","Loree")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string authorName, string authorSurname)
        {
            //arrange
           CreateAuthorCommand command = new CreateAuthorCommand(null,null);
           command.Model =new CreateAuthorModel()
           {
                AuthorName = authorName,
                AuthorSurname = authorSurname,
                AuthorDob = DateTime.Now.Date.AddYears(-1),// a year ago
           };

            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null,null);
            command.Model =new CreateAuthorModel()
            {
                AuthorName ="Erica",
                AuthorSurname="Riess",
                AuthorDob = DateTime.Now.Date,
            };

            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }
        [Fact]
        public void WhenValidInoutsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null,null);
            command.Model =new CreateAuthorModel()
            {
                AuthorName = "Lord",
                AuthorSurname ="Lord",
                AuthorDob = DateTime.Now.Date.AddYears(-2),// a year ago
            };

            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}