using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenreQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGetGenresQuery_GenreList_ShouldBeReturned()
        {
            // Arrange
            var query = new GetGenresQuery(_context, _mapper);

            // Act
            var result = query.Handle();

            // Assert
            result.Should().NotBeNull();
            result[0].Name.Should().Be("Personal Growth");
            result[1].Name.Should().Be("Science Fiction");
            result[2].Name.Should().Be("True Crime");
            result[3].Name.Should().Be("Noval");
            result[4].Name.Should().Be("Romance");
        }
    }
}