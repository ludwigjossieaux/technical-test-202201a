using MediatR;
using People.Architecture.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Architecture.Application.Features.People.Queries.GetPerson
{
    public class GetPersonQuery : IRequest<PersonVm>
    {
        public GetPersonQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
