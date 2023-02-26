using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApi.Application.UserOperations.Queries.GetUserDetail;
using WebApi.UnitTests.TestSetup;
using Xunit;

namespace WebApi.UnitTests.Application.UserOperations.Queries.GetUserDetail
{
    public class GetUserDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void WhenUserIdBeZeroOrLessThenZero_Validator_ShouldBeReturnErrors(int userId )
        {
            //arrange
            GetUserDetailQuery query = new GetUserDetailQuery(null,null);
            query.UserId = userId;

            //act
            GetUserDetailQueryValidator validator = new GetUserDetailQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenUserIdBeGreaterThanZero_Validator_ShouldBeReturnErrors()
        {
            //arrange
            GetUserDetailQuery query = new GetUserDetailQuery(null,null);
            query.UserId = 1;

            //act
            GetUserDetailQueryValidator validator = new GetUserDetailQueryValidator();
            var result = validator.Validate(query);

            //assert
            result.Errors.Count.Should().Be(0);

        }
    }
}