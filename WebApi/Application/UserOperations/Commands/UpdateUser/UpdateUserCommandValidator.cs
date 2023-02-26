using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace WebApi.Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(4).When(x => x.Model.Name != string.Empty);
            RuleFor(command => command.Model.Surname).MinimumLength(4).When(x => x.Model.Surname != string.Empty);
            RuleFor(command => command.Model.Password).MinimumLength(4).When(x => x.Model.Password != string.Empty);
            RuleFor(command => command.Model.Email).MinimumLength(4).When(x => x.Model.Email != string.Empty);

        }
    }
}