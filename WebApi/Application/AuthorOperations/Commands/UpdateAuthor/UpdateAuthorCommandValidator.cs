using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator: AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.AuthorName).MinimumLength(4).When(x => x.Model.AuthorName != string.Empty);
            RuleFor(command => command.Model.AuthorSurname).MinimumLength(4).When(x => x.Model.AuthorSurname != string.Empty);
            RuleFor(command => command.Model.AuthorDob).LessThan(DateTime.Now.Date);
            
        }
    }
}