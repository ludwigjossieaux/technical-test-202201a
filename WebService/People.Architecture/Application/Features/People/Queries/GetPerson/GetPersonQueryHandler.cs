using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using People.Architecture.Application.Contracts;
using People.Architecture.Application.Exceptions;
using People.Architecture.Application.Models;
using People.Architecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Architecture.Application.Features.People.Queries.GetPerson
{
    public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, PersonVm>
    {
        private readonly IPersonRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPersonQueryHandler> _logger;

        public GetPersonQueryHandler(IPersonRepository repo, IMapper mapper, ILogger<GetPersonQueryHandler> logger)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PersonVm> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var orig = await _repo.GetAsync(request.Id);
                if (orig == null)
                {
                    throw new NotFoundException(nameof(Person), request.Id);
                }                
                return _mapper.Map<PersonVm>(orig);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving person: {PersonId}", request.Id);
                throw;
            }
        }
    }
}
