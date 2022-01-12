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

namespace People.Architecture.Application.Features.People.Commands.UpdatePerson
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, Unit>
    {
        private readonly IPersonRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePersonCommandHandler> _logger;

        public UpdatePersonCommandHandler(IPersonRepository repo, IMapper mapper, ILogger<UpdatePersonCommandHandler> logger)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var orig = await _repo.GetAsync(request.Id);
                if (orig == null)
                {
                    throw new NotFoundException(nameof(Person), request.Id);
                }

                orig.Nom = request.Nom;
                orig.Prenom = request.Prenom;

                await _repo.UpdateAsync(orig);

                _logger.LogInformation("Updated person successfully: Id = {PersonId}", request.Id);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating person: Id = {PersonId}", request.Id);
                throw;
            }
        }
    }
}
