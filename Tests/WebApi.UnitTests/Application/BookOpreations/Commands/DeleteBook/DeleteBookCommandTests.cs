using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOpreations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenBookIsNotFound_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)

            DeleteBookCommand command =new DeleteBookCommand(_context);
            command.BookId=100;

            //act (Çalıştırma) & assert (Doğrulama)
            FluentActions
                .Invoking(()=>command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap mevcut değil.");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeDeleted()
        {
            //arrange
            DeleteBookCommand command =new DeleteBookCommand(_context);
            command.BookId=1;
            

            //act
            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var book=_context.Books.SingleOrDefault(book=> book.Id == command.BookId);
            book.Should().BeNull();
        }
        
    }
}