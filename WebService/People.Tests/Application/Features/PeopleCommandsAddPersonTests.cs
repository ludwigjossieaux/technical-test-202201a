using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using People.Architecture.Application.Contracts;
using People.Architecture.Application.Features.People.Commands.AddPerson;
using People.Architecture.Application.Models;
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
    public class PeopleCommandsAddPersonTests
    {
        // Validator

        [Fact]
        public void Validator_Should_Be_False_When_Nom_Not_Provided()
        {
            // Arrange
            var validator = new AddPersonCommandValidator();

            // Act
            var result = validator.Validate(new AddPersonCommand("", "John"));

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validator_Should_Be_False_When_Prenom_Not_Provided()
        {
            // Arrange
            var validator = new AddPersonCommandValidator();

            // Act
            var result = validator.Validate(new AddPersonCommand("Smith", ""));

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
            var logger = A.Fake<ILogger<AddPersonCommandHandler>>();

            // Act
            Action act = () => new AddPersonCommandHandler(null, mapper, logger);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Handler_Constructor_Should_Throw_Exception_When_Mapper_Not_Provided()
        {
            // Arrange
            var repo = A.Fake<IPersonRepository>();
            var mapper = A.Fake<IMapper>();
            var logger = A.Fake<ILogger<AddPersonCommandHandler>>();

            // Act
            Action act = () => new AddPersonCommandHandler(repo, null, logger);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Handler_Constructor_Should_Throw_Exception_When_Logger_Not_Provided()
        {
            // Arrange
            var repo = A.Fake<IPersonRepository>();
            var mapper = A.Fake<IMapper>();
            var logger = A.Fake<ILogger<AddPersonCommandHandler>>();

            // Act
            Action act = () => new AddPersonCommandHandler(repo, mapper, null);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Handler_Should_Return_Created_Guid()
        {
            // Arrange
            var repo = A.Fake<IPersonRepository>();
            var mapper = A.Fake<IMapper>();
            var model = new Person { Id = Guid.NewGuid() };
            A.CallTo(() => mapper.Map<Person>(A<AddPersonCommand>._)).Returns(model);
            var logger = A.Fake<ILogger<AddPersonCommandHandler>>();

            // Act
            var request = new AddPersonCommand("Smith", "John");
            var handler = new AddPersonCommandHandler(repo, mapper, logger);
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().Be(model.Id);
            A.CallTo(() => repo.AddAsync(A<Person>._)).MustHaveHappened();
        }

        [Fact]
        public async Task Handler_Should_Throw_Exception_When_Error_Occured()
        {
            // Arrange
            var repo = A.Fake<IPersonRepository>();
            A.CallTo(() => repo.AddAsync(A<Person>._)).Throws(new ATestException());
            var mapper = A.Fake<IMapper>();
            var logger = A.Fake<ILogger<AddPersonCommandHandler>>();
            var request = new AddPersonCommand("Smith", "John");
            var handler = new AddPersonCommandHandler(repo, mapper, logger);

            // Act
            Func<Task> act = () => handler.Handle(request, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ATestException>();
        }
    }
}
