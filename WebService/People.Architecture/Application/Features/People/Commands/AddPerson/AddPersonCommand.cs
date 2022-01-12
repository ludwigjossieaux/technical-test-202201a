using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Architecture.Application.Features.People.Commands.AddPerson
{
    public class AddPersonCommand : IRequest<Guid>
    {
        public AddPersonCommand(string nom, string prenom)
        {
            Nom = nom;
            Prenom = prenom;
        }

        public string Nom { get; }
        public string Prenom { get; }
    }
}
