using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using People.Architecture.Application.Contracts;
using People.Architecture.Application.Exceptions;
using People.Architecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Architecture.Application.Features.People.Commands.DeletePerson
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Unit>
    {
        private readonly IPersonRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<DeletePersonCommandHandler> _logger;

        public DeletePersonCommandHandler(IPersonRepository repo, IMapper mapper, ILogger<DeletePersonCommandHandler> logger)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var orig = await _repo.GetAsync(request.Id);
                if (orig == null)
                {
                    throw new NotFoundException(nameof(Person), request.Id);
                }
                await _repo.DeleteAsync(orig);
                _logger.LogInformation("Deleted person successfully: Id = {PersonId}", request.Id);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting person: {PersonId}", request.Id);
                throw;
            }
        }
    }
}
