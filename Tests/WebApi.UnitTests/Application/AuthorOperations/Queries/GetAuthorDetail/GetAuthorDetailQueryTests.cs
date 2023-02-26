using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGivenAuthorIsNotFound_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)

            GetAuthorDetailQuery query =new GetAuthorDetailQuery(_context,_mapper);
            query.AuthorId=100;

            //act (Çalıştırma) & assert (Doğrulama)
            FluentActions
                .Invoking(()=>query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Bulunamadi.");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeReturned()
        {
            // arrange
            GetAuthorDetailQuery query = new(_context, _mapper);
            query.AuthorId = 1;

            var author = _context.Authors.SingleOrDefault(a => a.Id == query.AuthorId);

            // act
            AuthorDetailViewModel vm = query.Handle();

            // assert
            vm.Should().NotBeNull();
            vm.AuthorName.Should().Be(author.AuthorName);
            vm.AuthorSurname.Should().Be(author.AuthorSurname);
            vm.AuthorDob.Should().Be(author.AuthorDob);
        }


    }
}