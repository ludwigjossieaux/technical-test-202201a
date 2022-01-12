using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using People.Architecture.Application.Contracts;
using People.Architecture.Application.Features.People.Queries.GetPeople;
using People.Architecture.Application.Models;
using People.Architecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace People.Tests.Application.Features
{
    public class PeopleQueriesGetPeopleTests
    {
        // Handler

        [Fact]
        public void Handler_Constructor_Should_Throw_Exception_When_Repository_Not_Provided()
        {
            // Arrange
            var repo = A.Fake<IPersonRepository>();
            var mapper = A.Fake<IMapper>();

            // Act
            Action act = () => new GetPeopleQueryHandler(null, mapper);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Handler_Constructor_Should_Throw_Exception_When_Mapper_Not_Provided()
        {
            // Arrange
            var repo = A.Fake<IPersonRepository>();
            var mapper = A.Fake<IMapper>();

            // Act
            Action act = () => new GetPeopleQueryHandler(repo, null);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Handler_Should_Return_List_Of_PersonVm()
        {
            // Arrange
            var repo = A.Fake<IPersonRepository>();
            var mapper = A.Fake<IMapper>();
            var handler = new GetPeopleQueryHandler(repo, mapper);

            // Act
            var request = new GetPeopleQuery("j", "s");
            await handler.Handle(request, CancellationToken.None);

            // Assert
            A.CallTo(() => repo.ListAsync(A<string>._, A<string>._)).MustHaveHappened();
            A.CallTo(() => mapper.Map<List<PersonVm>>(A<IReadOnlyList<Person>>._)).MustHaveHappened();
        }
    }
}
