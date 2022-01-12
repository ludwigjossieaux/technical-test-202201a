using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using People.Architecture.Application.Contracts;
using People.Architecture.Application.Exceptions;
using People.Architecture.Application.Features.People.Commands.UpdatePerson;
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
    public class PeopleCommandsUpdatePersonTests
    {
        // Validator

        [Fact]
        public void Validator_Should_Be_False_When_Id_Not_Provided()
        {
            // Arrange
            var validator = new UpdatePersonCommandValidator();

            // Act
            var result = validator.Validate(new UpdatePersonCommand(new Guid(), "Smith", "John"));

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validator_Should_Be_False_When_Nom_Not_Provided()
        {
            // Arrange
            var validator = new UpdatePersonCommandValidator();

            // Act
            var result = validator.Validate(new UpdatePersonCommand(Guid.NewGuid(), "", "John"));

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validator_Should_Be_False_When_Prenom_Not_Provided()
        {
            // Arrange
            var validator = new UpdatePersonCommandValidator();

            // Act
            var result = validator.Validate(new UpdatePersonCommand(Guid.NewGuid(), "Smith", ""));

            // Assert
            result.IsValid.Should().BeFalse();
        }

        // Handler

        [Fact]
        public void Handler_Constructor_Should_Throw_Exception_When_Repository_Not_Provided()
        {
            // Arrange
            var repo = A.Fake<IPersonRepository>();
            var mapper = A.Fake<IMapper>();
            var logger = A.Fake<ILogger<UpdatePersonCommandHandler>>();

            // Act
            Action act = () => new UpdatePersonCommandHandler(null, mapper, logger);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Handler_Constructor_Should_Throw_Exception_When_Mapper_Not_Provided()
        {
            // Arrange
            var repo = A.Fake<IPersonRepository>();
            var mapper = A.Fake<IMapper>();
            var logger = A.Fake<ILogger<UpdatePersonCommandHandler>>();

            // Act
            Action act = () => new UpdatePersonCommandHandler(repo, null, logger);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Handler_Constructor_Should_Throw_Exception_When_Logger_Not_Provided()
        {
            // Arrange
            var repo = A.Fake<IPersonRepository>();
            var mapper = A.Fake<IMapper>();
            var logger = A.Fake<ILogger<UpdatePersonCommandHandler>>();

            // Act
            Action act = () => new UpdatePersonCommandHandler(repo, mapper, null);

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
            var logger = A.Fake<ILogger<UpdatePersonCommandHandler>>();

            A.CallTo(() => repo.GetAsync(A<Guid>._)).Returns(model);

            // Act
            var request = new UpdatePersonCommand(model.Id, "John", "Smith");
            var handler = new UpdatePersonCommandHandler(repo, mapper, logger);
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().Be(Unit.Value);
            A.CallTo(() => repo.UpdateAsync(A<Person>._)).MustHaveHappened();
        }

        [Fact]
        public async Task Handler_Should_Throw_NotFoundException_When_Not_Found()
        {
            // Arrange
            var repo = A.Fake<IPersonRepository>();
            var mapper = A.Fake<IMapper>();
            var logger = A.Fake<ILogger<UpdatePersonCommandHandler>>();
            var request = new UpdatePersonCommand(Guid.NewGuid(), "John", "Smith");
            var handler = new UpdatePersonCommandHandler(repo, mapper, logger);

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
            var logger = A.Fake<ILogger<UpdatePersonCommandHandler>>();
            var request = new UpdatePersonCommand(Guid.NewGuid(), "John", "Smith");
            var handler = new UpdatePersonCommandHandler(repo, mapper, logger);

            // Act
            Func<Task> act = () => handler.Handle(request, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ATestException>();
        }
    }
}
