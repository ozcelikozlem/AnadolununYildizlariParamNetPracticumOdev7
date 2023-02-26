using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
          _context = testFixture.Context;
        }

         [Fact]
        public void WhenGivenAuthorIsNotFound_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)

            DeleteAuthorCommand command =new DeleteAuthorCommand(_context);
            command.AuthorId=100;

            //act (Çalıştırma) & assert (Doğrulama)
            FluentActions
                .Invoking(()=>command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Bulunamadi");

        }
        [Fact]
        public void WhenGivenAuthorHaveBook_InvalidOperationException_ShouldBeReturn()
        {
            // arrange (Hazırlık)

            DeleteAuthorCommand command = new(_context);
            command.AuthorId = 1;

            // act & assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazarin kitaplari mevcut. Yazari silemezsin!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeDeleted()
        {
            //arrange
            DeleteAuthorCommand command =new DeleteAuthorCommand(_context);
            command.AuthorId=1;

            //act
             DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
        }




    }
}