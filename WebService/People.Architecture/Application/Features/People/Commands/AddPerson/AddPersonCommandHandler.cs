using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using People.Architecture.Application.Contracts;
using People.Architecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Architecture.Application.Features.People.Commands.AddPerson
{
    public class AddPersonCommandHandler : IRequestHandler<AddPersonCommand, Guid>
    {
        private readonly IPersonRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<AddPersonCommandHandler> _logger;

        public AddPersonCommandHandler(IPersonRepository repo, IMapper mapper, ILogger<AddPersonCommandHandler> logger)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Guid> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var person = _mapper.Map<Person>(request);
                await _repo.AddAsync(person);
                _logger.LogInformation("Added person successfully: Id = {PersonId}", person.Id);
                return person.Id;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error adding person");
                throw;
            }
        }
    }
}
