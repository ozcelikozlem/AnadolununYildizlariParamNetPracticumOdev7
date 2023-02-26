using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOpreations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,"Lord Of The Rings",0,0)]
        [InlineData(0,"Lord Of The Rings",0,1)]
        [InlineData(0,"Lord Of The Rings",1,1)]
        [InlineData(0,"Lor",1,1)]
        [InlineData(0,"",1,1)]
        [InlineData(-5,"Lord Of The Rings",0,0)]
        [InlineData(-5,"Lord Of The Rings",0,1)]
        [InlineData(-5,"Lord Of The Rings",1,1)]
        [InlineData(-5,"Lor",1,1)]
        [InlineData(-5,"",1,1)]
        [InlineData(1,"Lord Of The Rings",0,0)]
        [InlineData(1,"",0,0)]
        [InlineData(1,"",1,0)]
        [InlineData(1,"",0,1)]
        [InlineData(1,"",1,1)]
        [InlineData(1,"Lor",1,0)]
        [InlineData(1,"Lor",0,1)]
        [InlineData(1,"Lor",1,1)]
        //[InlineData(1,"Lord Of The Rings",1,1)] --> successful case
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId,string title, int genreId, int authorId )
        {
            //arrange
           UpdateBookCommand command = new UpdateBookCommand(null);
           command.Model =new UpdateBookModel()
           {
                Title = title,
                GenreId = genreId,
                AuthorId = authorId,
           };
           command.BookId = bookId;

            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenValidInoutsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model =new UpdateBookModel()
            {
                Title = "Lord of The Rings",
                GenreId = 1,
                AuthorId = 1,
            };
            command.BookId = 3;
            
            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}