using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.AuthorName).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.AuthorSurname).NotEmpty().MinimumLength(4);
            RuleFor(command => command.Model.AuthorDob.Date).LessThan(DateTime.Now.Date);
        }
       
    }
}