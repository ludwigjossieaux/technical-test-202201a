using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Architecture.Application.Features.People.Commands.UpdatePerson
{
    public class UpdatePersonCommand : IRequest<Unit>
    {
        public UpdatePersonCommand(Guid id, string nom, string prenom)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
        }

        public Guid Id { get; }
        public string Nom { get; }
        public string Prenom { get; }
    }
}
