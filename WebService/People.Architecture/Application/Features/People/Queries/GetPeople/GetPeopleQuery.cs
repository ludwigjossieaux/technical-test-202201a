using MediatR;
using People.Architecture.Application.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Architecture.Application.Features.People.Queries.GetPeople
{
    public class GetPeopleQuery : IRequest<IEnumerable<PersonVm>>
    {
        public GetPeopleQuery(string nomFilter = "", string prenomFilter = "")
        {
            NomFilter = nomFilter;
            PrenomFilter = prenomFilter;
        }

        public string NomFilter { get; }
        public string PrenomFilter { get; }
    }
}
