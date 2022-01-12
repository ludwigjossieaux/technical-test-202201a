using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using People.Api.Controllers;
using People.Architecture.Application.Exceptions;
using People.Architecture.Application.Features.People.Commands.AddPerson;
using People.Architecture.Application.Features.People.Commands.DeletePerson;
using People.Architecture.Application.Features.People.Commands.UpdatePerson;
using People.Architecture.Application.Features.People.Queries.GetPeople;
using People.Architecture.Application.Features.People.Queries.GetPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace People.Tests.Api
{
    public class PersonControllerTests
    {
        [Fact]
        public void Constructor_Should_Throw_Exception_When_Mediator_Not_Provided()
        {
            // Arrange

            // Act
            Action act = () => new PersonController(null); 

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        // GetPeople

        [Fact]
        public async Task GetPeople_Should_Return_Ok()
        {
            // Arrange
            var mediator = A.Fake<IMediator>();
            var controller = new PersonController(mediator);

            // Act
            var result = await controller.GetPeople();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetPeople_Should_Return_BadRequest_When_Exception_Is_Thrown()
        {
            // Arrange
            var mediator = A.Fake<IMediator>();            
            A.CallTo(() => mediator.Send(A<GetPeopleQuery>._, CancellationToken.None))
                .Throws(() => new Exception());
            var controller = new PersonController(mediator);

            // Act
            var result = await controller.GetPeople();

            // Assert
            result.Result.Should().BeOfType<BadRequestResult>();
        }

        // GetPerson

        [Fact]
        public async Task GetPerson_Should_Return_Ok()
        {
            // Arrange
            var mediator = A.Fake<IMediator>();
            var controller = new PersonController(mediator);

            // Act
            var result = await controller.GetPerson(new Guid());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetPerson_Should_Return_NotFound_When_NotFoundException_Is_Thrown()
        {
            // Arrange
            var mediator = A.Fake<IMediator>();
            A.CallTo(() => mediator.Send(A<GetPersonQuery>._, CancellationToken.None))
                .Throws(() => new NotFoundException("", Guid.NewGuid()));
            var controller = new PersonController(mediator);

            // Act
            var result = await controller.GetPerson(new Guid());

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetPerson_Should_Return_BadRequest_When_Exception_Is_Thrown()
        {
            // Arrange
            var mediator = A.Fake<IMediator>();
            A.CallTo(() => mediator.Send(A<GetPersonQuery>._, CancellationToken.None))
                .Throws(() => new Exception());
            var controller = new PersonController(mediator);

            // Act
            var result = await controller.GetPerson(new Guid());

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        // AddPerson

        [Fact]
        public async Task AddPerson_Should_Return_CreatedAtRoute()
        {
            // Arrange
            var mediator = A.Fake<IMediator>();
            var controller = new PersonController(mediator);

            // Act
            var result = await controller.AddPerson(new AddPersonCommand("John", "Smith"));

            // Assert
            result.Should().BeOfType<CreatedAtRouteResult>();
        }

        [Fact]
        public async Task AddPerson_Should_Return_BadRequest_When_Exception_Is_Thrown()
        {
            // Arrange
            var mediator = A.Fake<IMediator>();
            A.CallTo(() => mediator.Send(A<AddPersonCommand>._, CancellationToken.None))
                .Throws(() => new Exception());
            var controller = new PersonController(mediator);

            // Act
            var result = await controller.AddPerson(new AddPersonCommand("John", "Smith"));

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        // UpdatePerson

        [Fact]
        public async Task UpdatePerson_Should_Return_NoContent()
        {
            // Arrange
            var mediator = A.Fake<IMediator>();
            var controller = new PersonController(mediator);

            // Act
            var result = await controller.UpdatePerson(new UpdatePersonCommand(Guid.NewGuid(), "John", "Smith"));

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task UpdatePerson_Should_Return_NotFound_When_NotFoundException_Is_Thrown()
        {
            // Arrange
            var mediator = A.Fake<IMediator>();
            A.CallTo(() => mediator.Send(A<UpdatePersonCommand>._, CancellationToken.None))
                .Throws(() => new NotFoundException("", Guid.NewGuid()));
            var controller = new PersonController(mediator);

            // Act
            var result = await controller.UpdatePerson(new UpdatePersonCommand(Guid.NewGuid(), "John", "Smith"));

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task UpdatePerson_Should_Return_BadRequest_When_Exception_Is_Thrown()
        {
            // Arrange
            var mediator = A.Fake<IMediator>();
            A.CallTo(() => mediator.Send(A<UpdatePersonCommand>._, CancellationToken.None))
                .Throws(() => new Exception());
            var controller = new PersonController(mediator);

            // Act
            var result = await controller.UpdatePerson(new UpdatePersonCommand(Guid.NewGuid(), "John", "Smith"));

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        // DeletePerson

        [Fact]
        public async Task DeletePerson_Should_Return_NoContent()
        {
            // Arrange
            var mediator = A.Fake<IMediator>();
            var controller = new PersonController(mediator);

            // Act
            var result = await controller.DeletePerson(Guid.NewGuid());

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeletePerson_Should_Return_NotFound_When_NotFoundException_Is_Thrown()
        {
            // Arrange
            var mediator = A.Fake<IMediator>();
            A.CallTo(() => mediator.Send(A<DeletePersonCommand>._, CancellationToken.None))
                .Throws(() => new NotFoundException("", Guid.NewGuid()));
            var controller = new PersonController(mediator);

            // Act
            var result = await controller.DeletePerson(Guid.NewGuid());

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DeletePerson_Should_Return_BadRequest_When_Exception_Is_Thrown()
        {
            // Arrange
            var mediator = A.Fake<IMediator>();
            A.CallTo(() => mediator.Send(A<DeletePersonCommand>._, CancellationToken.None))
                .Throws(() => new Exception());
            var controller = new PersonController(mediator);

            // Act
            var result = await controller.DeletePerson(Guid.NewGuid());

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

    }
}
