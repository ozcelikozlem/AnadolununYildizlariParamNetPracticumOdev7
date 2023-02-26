using AutoMapper;
using FluentAssertions;
using WebApi.Application.UserOperations.Queries.GetUsers;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.UserOperations.Queries.GetUsers
{
    public class GetUsersQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetUsersQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGetUsersQuery_UserList_ShouldBeReturned()
        {
            // Arrange
            var query = new GetUsersQuery(_context, _mapper);

            // Act
            var result = query.Handle();

            // Assert
            result.Should().NotBeNull();
            result[0].Name.Should().Be("Özlem");
            result[0].Surname.Should().Be("Özçelik");
            result[0].Email.Should().Be("ozlm@gmail.com");
            result[0].Password.Should().Be("1234");
        }
    }
}