using FluentValidation;
using People.Architecture.Application.Features.People.Commands.UpdatePerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Architecture.Application.Features.People.Commands.UpdatePerson
{
    public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.Nom)
                .NotEmpty().WithMessage("Nom is required.");

            RuleFor(x => x.Prenom)
                .NotEmpty().WithMessage("Prenom is required.");
        }
    }
}
