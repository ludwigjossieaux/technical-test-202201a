using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Architecture.Application.Features.People.Commands.AddPerson
{
    public class AddPersonCommandValidator : AbstractValidator<AddPersonCommand>
    {
        public AddPersonCommandValidator()
        {
            RuleFor(x => x.Nom)
                .NotEmpty().WithMessage("Nom is required.");

            RuleFor(x => x.Prenom)
                .NotEmpty().WithMessage("Prenom is required.");
        }
    }
}
