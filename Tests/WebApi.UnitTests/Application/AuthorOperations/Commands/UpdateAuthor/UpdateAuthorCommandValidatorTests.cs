using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,"Lord Of The Rings"," ")]
        [InlineData(0,"Lord Of The Rings","Lor")]
        [InlineData(0,"Lor","lor")]
        [InlineData(0," ","lordd")]
        [InlineData(-5,"Lord Of The Rings"," ")]
        [InlineData(-5,"Lord Of The Rings","Lor")]
        [InlineData(-5,"Lor","lor")]
        [InlineData(-5," ","lordd")]
        [InlineData(1,"Lord Of The Rings"," ")]
        [InlineData(1,"Lord Of The Rings","Lor")]
        [InlineData(1,"Lor","lor")]
        [InlineData(1," ","lordd")]
        //[InlineData(1,"Lord Of The Rings",1,1)] --> successful case
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int authorId,string authorName, string authorSurname )
        {
            //arrange
           UpdateAuthorCommand command = new UpdateAuthorCommand(null);
           command.Model =new UpdateAuthorModel()
           {
                AuthorName = authorName,
                AuthorSurname = authorSurname

           };
           command.AuthorId = authorId;

            //act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenDateEqualNowGiven_Validator_ShouldBeReturnError()
        {
            // arrange
            UpdateAuthorCommand command = new(null);
            command.Model = new UpdateAuthorModel { AuthorName = "Frank", AuthorSurname = "Herbertt", AuthorDob = DateTime.Now.Date };

            // act
            UpdateAuthorCommandValidator validator = new();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInoutsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model =new UpdateAuthorModel()
            {
                AuthorName = "Frank",
                AuthorSurname = "Herbert",
                AuthorDob= new DateTime(1920,10,08)
            };
            command.AuthorId = 3;
            
            //act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}