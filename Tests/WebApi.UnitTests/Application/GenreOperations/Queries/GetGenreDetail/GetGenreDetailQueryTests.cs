using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
  {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

       // [Fact]
        public void WhenGivenGenreIsNotFound_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)

            GetGenreDetailQuery query =new GetGenreDetailQuery(_context,_mapper);
            query.GenreId=9;

            //act (Çalıştırma) & assert (Doğrulama)
            FluentActions
                .Invoking(()=>query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadi.");

        }

       // [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeReturned()
        {
            // arrange
            GetGenreDetailQuery query = new(_context, _mapper);
            query.GenreId = 1;

           var genre = _context.Genres.Where(g => g.Id == query.GenreId).SingleOrDefault();

            // act
            GenreDetailViewModel vm = query.Handle();

            // assert
            vm.Should().NotBeNull();
            vm.Name.Should().Be(genre.Name);
        }

  }
}