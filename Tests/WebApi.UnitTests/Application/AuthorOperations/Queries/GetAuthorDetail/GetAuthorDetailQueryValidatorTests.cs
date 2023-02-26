using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void WhenAuthorIdBeZeroOrLessThenZero_Validator_ShouldBeReturnErrors(int authorId )
        {
            //arrange
           GetAuthorDetailQuery query = new GetAuthorDetailQuery(null,null);
           query.AuthorId = authorId;

            //act
             GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenAuthorIdBeGreaterThanZero_Validator_ShouldBeReturnErrors()
        {
            //arrange
           GetAuthorDetailQuery query = new GetAuthorDetailQuery(null,null);
           query.AuthorId = 3;

            //act
             GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}