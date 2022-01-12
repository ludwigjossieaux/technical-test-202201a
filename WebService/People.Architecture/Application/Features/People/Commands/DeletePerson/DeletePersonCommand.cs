using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Architecture.Application.Features.People.Commands.DeletePerson
{
    public class DeletePersonCommand : IRequest<Unit>
    {
        public DeletePersonCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
