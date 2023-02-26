using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOpreations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenBookIsNotFound_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)

            UpdateBookCommand command =new UpdateBookCommand(_context);
            command.BookId=100;

            //act (Çalıştırma) & assert (Doğrulama)
            FluentActions
                .Invoking(()=>command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap mevcut değil.");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            //arrange
            UpdateBookCommand command =new UpdateBookCommand(_context);
            UpdateBookModel model = new UpdateBookModel(){Title ="Hobbit",GenreId=1,AuthorId=1};
            command.Model = model;
            command.BookId=1;
            //act
            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var book=_context.Books.SingleOrDefault(book=> book.Id == command.BookId);
            book.Should().NotBeNull();
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);
            book.Title.Should().Be(model.Title);
        }

    }
}