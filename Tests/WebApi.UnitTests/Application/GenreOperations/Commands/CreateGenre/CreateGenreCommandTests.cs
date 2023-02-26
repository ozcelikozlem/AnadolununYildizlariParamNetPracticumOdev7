using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DbOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
             //arrange (Hazırlık)
            var genre = new Genre(){Name="Crime"};
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command =new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel(){Name = genre.Name};

            //act (Çalıştırma) & assert (Doğrulama)
            FluentActions
                .Invoking(()=>command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Zaten Mevcut.");


        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            //arrange
            CreateGenreCommand command =new CreateGenreCommand(_context);
            CreateGenreModel model = new CreateGenreModel(){Name="Crime"};
            command.Model = model;

            //act
            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var genre=_context.Genres.SingleOrDefault(g=> g.Name == model.Name);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
        }
    }
}