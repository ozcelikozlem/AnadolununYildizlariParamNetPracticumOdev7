using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenGivenGenreIsNotFound_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)

            UpdateGenreCommand command =new UpdateGenreCommand(_context);
            command.GenreId=100;

            //act (Çalıştırma) & assert (Doğrulama)
            FluentActions
                .Invoking(()=>command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Mevcut Değil");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
        {
            //arrange
            UpdateGenreCommand command =new UpdateGenreCommand(_context);
            UpdateGenreModel model = new UpdateGenreModel(){Name ="Novala",IsActive=true};
            command.Model = model;
            command.GenreId=3;
            //act
            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var genre=_context.Genres.SingleOrDefault(g=> g.Id == command.GenreId);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
            genre.IActive.Should().Be(model.IsActive);
        }
    }
}