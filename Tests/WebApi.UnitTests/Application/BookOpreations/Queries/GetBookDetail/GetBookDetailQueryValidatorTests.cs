using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.BookOpreations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void WhenBookIdBeZeroOrLessThenZero_Validator_ShouldBeReturnErrors(int bookId )
        {
            //arrange
           GetBookDetailQuery query = new GetBookDetailQuery(null,null);
           query.BookId = bookId;

            //act
             GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenBookIdBeGreaterThanZero_Validator_ShouldBeReturnErrors()
        {
            //arrange
           GetBookDetailQuery query = new GetBookDetailQuery(null,null);
           query.BookId = 3;

            //act
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}