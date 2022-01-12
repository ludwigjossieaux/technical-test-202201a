using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using People.Architecture.Application.Contracts;
using People.Architecture.Application.Exceptions;
using People.Architecture.Application.Features.People.Commands.DeletePerson;
using People.Architecture.Domain.Entities;
using People.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace People.Tests.Application.Features
{
    public class PeopleCommandsDeletePersonTests
    {
        // Handler

        [Fact]
        public void Handler_Constructor_Should_Throw_Exception_When_Repository_Not_Provided()
        {
            // Arrange
            var repo = A.Fake<IPersonRepository>();
            var mapper = A.Fake<IMapper>();
            var logger = A.Fake<ILogger<DeletePersonCommandHandler>>();

            // Act
            Action act = () => new DeletePersonCommandHandler(null, mapper, logger);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Handler_Constructor_Should_Throw_Exception_When_Mapper_Not_Provided()
        {
            // Arrange
            var repo = A.Fake<IPersonRepository>();
            var mapper = A.Fake<IMapper>();
            var logger = A.Fake<ILogger<DeletePersonCommandHandler>>();

            // Act
            Action act = () => new DeletePersonCommandHandler(repo, null, logger);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Handler_Constructor_Should_Throw_Exception_When_Logger_Not_Provided()
        {
            // Arrange
            var repo = A.Fake<IPersonRepository>();
            var mapper = A.Fake<IMapper>();
            var logger = A.Fake<ILogger<DeletePersonCommandHandler>>();

            // Act
            Action act = () => new DeletePersonCommandHandler(repo, mapper, null);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Handler_Should_Return_Unit()
        {
            // Arrange
            var repo = A.Fake<IPersonRepository>();
            var mapper = A.Fake<IMapper>();
            var model = new Person { Id = Guid.NewGuid() };            
            var logger = A.Fake<ILogger<DeletePersonCommandHandler>>();

            A.CallTo(() => repo.GetAsync(A<Guid>._)).Returns(model);

            // Act
            var request = new DeletePersonCommand(model.Id);
            var handler = new DeletePersonCommandHandler(repo, mapper, logger);
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            A.CallTo(() => repo.DeleteAsync(A<Person>._)).MustHaveHappened();
        }

        [Fact]
        public async Task Handler_Should_Throw_NotFoundException_When_Not_Found()
        {
            // Arrange
            var repo = A.Fake<IPersonRepository>();
            var mapper = A.Fake<IMapper>();
            var logger = A.Fake<ILogger<DeletePersonCommandHandler>>();
            var request = new DeletePersonCommand(Guid.NewGuid());
            var handler = new DeletePersonCommandHandler(repo, mapper, logger);

            A.CallTo(() => repo.GetAsync(A<Guid>._)).Returns(Task.FromResult<Person>(null));

            // Act
            Func<Task> act = () => handler.Handle(request, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Handler_Should_Throw_Exception_When_Error_Occured()
        {
            // Arrange
            var repo = A.Fake<IPersonRepository>();
            A.CallTo(() => repo.GetAsync(A<Guid>._)).Throws(new ATestException());
            var mapper = A.Fake<IMapper>();
            var logger = A.Fake<ILogger<DeletePersonCommandHandler>>();
            var request = new DeletePersonCommand(Guid.NewGuid());
            var handler = new DeletePersonCommandHandler(repo, mapper, logger);

            // Act
            Func<Task> act = () => handler.Handle(request, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ATestException>();
        }
    }
}
