using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenGenreIsNotFound_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)

            DeleteGenreCommand command =new DeleteGenreCommand(_context);
            command.GenreId=100;

            //act (Çalıştırma) & assert (Doğrulama)
            FluentActions
                .Invoking(()=>command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Mevcut Değil");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeDeleted()
        {
            //arrange
            DeleteGenreCommand command =new DeleteGenreCommand(_context);
            command.GenreId=1;
            

            //act
            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var genre=_context.Genres.SingleOrDefault(g=> g.Id == command.GenreId);
            genre.Should().BeNull();
        }
    }
}