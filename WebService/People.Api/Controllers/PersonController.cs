using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using People.Architecture.Application.Exceptions;
using People.Architecture.Application.Features.People.Commands.AddPerson;
using People.Architecture.Application.Features.People.Commands.DeletePerson;
using People.Architecture.Application.Features.People.Commands.UpdatePerson;
using People.Architecture.Application.Features.People.Queries.GetPeople;
using People.Architecture.Application.Features.People.Queries.GetPerson;
using People.Architecture.Application.Models;

namespace People.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonVm>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PersonVm>>> GetPeople([FromQuery] string filterNom = "", [FromQuery] string filterPrenom = "")
        {
            try
            {
                var query = new GetPeopleQuery(filterNom, filterPrenom);
                var people = await _mediator.Send(query);
                return Ok(people);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}", Name = "GetPerson")]
        [ProducesResponseType(typeof(PersonVm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPerson(Guid id)
        {
            try
            {
                var query = new GetPersonQuery(id);
                var person = await _mediator.Send(query);
                return Ok(person);
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
            catch(Exception)
            {
                return BadRequest();
            }            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddPerson([FromBody] AddPersonCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return CreatedAtRoute("GetPerson", new { id = id }, command);
            }
            catch(Exception)
            {
                return BadRequest();
            }            
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePerson([FromBody] UpdatePersonCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            try
            {
                var command = new DeletePersonCommand(id);
                await _mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
