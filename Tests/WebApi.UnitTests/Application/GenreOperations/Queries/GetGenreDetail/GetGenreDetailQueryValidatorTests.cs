using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void WhenGenreIdBeZeroOrLessThenZero_Validator_ShouldBeReturnErrors(int genreId )
        {
            //arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(null,null);
            query.GenreId = genreId;

            //act
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenGenreIdBeGreaterThanZero_Validator_ShouldBeReturnErrors()
        {
            //arrange
            GetGenreDetailQuery query = new GetGenreDetailQuery(null,null);
            query.GenreId = 3;

            //act
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}