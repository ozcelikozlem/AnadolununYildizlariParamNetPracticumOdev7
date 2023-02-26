using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.UserOperations.Queries.GetUserDetail;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.UserOperations.Queries.GetUserDetail
{
    public class GetUserDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetUserDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

       // [Fact]
        public void WhenGivenUserIsNotFound_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)

            GetUserDetailQuery query =new GetUserDetailQuery(_context,_mapper);
            query.UserId=9;

            //act (Çalıştırma) & assert (Doğrulama)
            FluentActions
                .Invoking(()=>query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kulannıcı Bulunamadi.");

        }

       // [Fact]
        public void WhenValidInputsAreGiven_User_ShouldBeReturned()
        {
            // arrange
            GetUserDetailQuery query = new(_context, _mapper);
            query.UserId = 1;

           var user = _context.Users.Where(g => g.Id == query.UserId).SingleOrDefault();

            // act
            UserDetailViewModel vm = query.Handle();

            // assert
            vm.Should().NotBeNull();
            vm.Name.Should().Be(user.Name);
            vm.Surname.Should().Be(user.Surname);
            vm.Email.Should().Be(user.Email);
            vm.Password.Should().Be(user.Password);
        }
    }
}