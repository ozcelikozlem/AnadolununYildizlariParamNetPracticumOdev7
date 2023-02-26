using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DbOperations;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOpreations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGivenBookIsNotFound_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazırlık)

            GetBookDetailQuery query =new GetBookDetailQuery(_context,_mapper);
            query.BookId=100;

            //act (Çalıştırma) & assert (Doğrulama)
            FluentActions
                .Invoking(()=>query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap mevcut değil.");

        }

         [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeReturned()
        {
            // arrange
            GetBookDetailQuery query = new(_context, _mapper);
            query.BookId = 1;

            var book = _context.Books.Include(x => x.Genre).Include(x => x.Author).Where(b => b.Id == query.BookId).SingleOrDefault();

            // act
            BookDetailViewModel vm = query.Handle();

            // assert
            vm.Should().NotBeNull();
            vm.Title.Should().Be(book.Title);
            vm.PageCount.Should().Be(book.PageCount);
            vm.Genre.Should().Be(book.Genre.Name);
            vm.Author.Should().Be(book.Author.AuthorName + " " + book.Author.AuthorSurname);
            vm.PublishDate.Should().Be(book.PublishDate.ToString("dd/MM/yyyy 00:00:00"));
        }

    }
}