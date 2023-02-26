using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace WebApi.UnitTests.Application.BookOpreations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lord Of The Rings",0,0,0)]
        [InlineData("Lord Of The Rings",0,1,1)]
        [InlineData("Lord Of The Rings",100,0,0)]
        [InlineData("",0,0,0)]
        [InlineData("",100,0,0)]
        [InlineData("",100,0,1)]
        [InlineData("",100,1,0)]
        [InlineData("",100,1,1)]
        [InlineData("Lor",100,0,0)]
        [InlineData("Lor",100,1,1)]
        //[InlineData("Lord Of The Rings",100,1,1)] --> successful case
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId, int authorId )
        {
            //arrange
           CreateBookCommand command = new CreateBookCommand(null,null);
           command.Model =new CreateBookModel()
           {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),// a year ago
                GenreId = genreId,
                AuthorId = authorId,
           };

            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model =new CreateBookModel()
            {
                Title = "Lord of The Rings",
                PageCount = 200,
                PublishDate = DateTime.Now.Date,
                GenreId = 1,
                AuthorId = 1,
            };

            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenValidInoutsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(null,null);
            command.Model =new CreateBookModel()
            {
                Title = "Lord of The Rings",
                PageCount = 200,
                PublishDate = DateTime.Now.Date.AddYears(-2),// a year ago
                GenreId = 1,
                AuthorId = 1,
            };

            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);

        }
        
    }
}