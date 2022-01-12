using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using People.Architecture.Application.Contracts;
using People.Architecture.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Architecture.Application.Features.People.Queries.GetPeople
{
    public class GetPeopleQueryHandler : IRequestHandler<GetPeopleQuery, IEnumerable<PersonVm>>
    {
        private readonly IPersonRepository _repo;
        private readonly IMapper _mapper;

        public GetPeopleQueryHandler(IPersonRepository repo, IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<PersonVm>> Handle(GetPeopleQuery request, CancellationToken cancellationToken)
        {
            var list = await _repo.ListAsync(request.NomFilter, request.PrenomFilter);
            return _mapper.Map<List<PersonVm>>(list);
        }
    }
}
