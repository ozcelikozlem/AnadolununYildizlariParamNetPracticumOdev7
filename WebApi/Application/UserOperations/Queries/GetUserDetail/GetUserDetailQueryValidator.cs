using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace WebApi.Application.UserOperations.Queries.GetUserDetail
{
    public class GetUserDetailQueryValidator : AbstractValidator<GetUserDetailQuery>
    {
        public GetUserDetailQueryValidator()
        {
            RuleFor(query => query.UserId).GreaterThan(0);
        }
    }
}