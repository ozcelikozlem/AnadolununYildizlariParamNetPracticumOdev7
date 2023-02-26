using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOpreations.Queries.GetBooks
{
    public class GetBooksQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBooksQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGetBooksQuery_BookList_ShouldBeReturned()
        {
            // Arrange
            var query = new GetBooksQuery(_context, _mapper);

            // Act
        var result = query.Handle();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(4);

            result[0].Title.Should().Be("Lean Startup");
            result[0].Genre.Should().Be("Personal Growth");
            result[0].Author.Should().Be("Eric Ries");
            result[0].PageCount.Should().Be(200);
            result[0].PublishDate.Should().Be("12.06.2001 00:00:00");

            result[1].Title.Should().Be("Herland");
            result[1].Genre.Should().Be("Science Fiction");
            result[1].Author.Should().Be("Charlotte Stetson");
            result[1].PageCount.Should().Be(250);
            result[1].PublishDate.Should().Be("23.05.2010 00:00:00");

            result[2].Title.Should().Be("Dune");
            result[2].Genre.Should().Be("Science Fiction");
            result[2].Author.Should().Be("Frank Herbert");
            result[2].PageCount.Should().Be(540);
            result[2].PublishDate.Should().Be("21.12.2002 00:00:00");

            result[3].Title.Should().Be("The Murder Room: In which Three of the Greatest Detectives Use Forensic Science to Solve the World's Most Perplexing Cold Cases");
            result[3].Genre.Should().Be("True Crime");
            result[3].Author.Should().Be("Michael Capuzzo");
            result[3].PageCount.Should().Be(464);
            result[3].PublishDate.Should().Be("11.01.2009 00:00:00");

        }
    }
}