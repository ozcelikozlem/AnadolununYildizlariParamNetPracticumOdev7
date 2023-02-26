using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsOueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorsOueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGetAuthorsQuery_AuthorList_ShouldBeReturned()
        {
            // Arrange
            var query = new GetAuthorsQuery(_context, _mapper);

            // Act
        var result = query.Handle();

            // Assert
            result.Should().NotBeNull();
            result[0].AuthorName.Should().Be("Eric");
            result[0].AuthorSurname.Should().Be("Ries");
            result[0].AuthorDob.Should().Be(new DateTime(1978,09,22));

            result[1].AuthorName.Should().Be("Charlotte");
            result[1].AuthorSurname.Should().Be("Stetson");
            result[1].AuthorDob.Should().Be(new DateTime(1935,08,17));

            result[2].AuthorName.Should().Be("Frank");
            result[2].AuthorSurname.Should().Be("Herbert");
            result[2].AuthorDob.Should().Be(new DateTime(1920,10,08));

            result[3].AuthorName.Should().Be("Michael");
            result[3].AuthorSurname.Should().Be("Capuzzo");
            result[3].AuthorDob.Should().Be(new DateTime(1957,05,01));

        }
    }
}