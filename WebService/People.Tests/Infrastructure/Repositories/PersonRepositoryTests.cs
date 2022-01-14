using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using People.Architecture.Domain.Entities;
using People.Architecture.Infrastructure.Persistence;
using People.Architecture.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace People.Tests.Infrastructure.Repositories
{
    public class PersonRepositoryTests : IDisposable
    {
        private readonly PeopleDbContext _context;
        private readonly PeopleDbContext _contextForAssert;

        public PersonRepositoryTests()
        {
            var guid = Guid.NewGuid();
            var options = new DbContextOptionsBuilder<PeopleDbContext>()
                .UseInMemoryDatabase($"people_tests_{guid}")
                .Options;
            _context = new PeopleDbContext(options);
            _contextForAssert = new PeopleDbContext(options);
        }

        public void Dispose()
        {
            _context.Dispose();
            _contextForAssert.Dispose();
        }

        // ---

        [Fact]
        public async Task AddAsync_Should_Add_A_Person()
        {
            // Arrange
            var person = new Person
            {
                Prenom = "John",
                Nom = "Smith"
            };
            var repo = new PersonRepository(_context);
            var beforeActionDatetime = DateTime.UtcNow;
            (await _contextForAssert.People.CountAsync()).Should().Be(0);

            // Act
            var guid = await repo.AddAsync(person);

            // Assert
            (await _contextForAssert.People.CountAsync()).Should().Be(1);
            var result = await _contextForAssert.People.FindAsync(guid);
            result.Prenom.Should().Be("John");
            result.Nom.Should().Be("Smith");
            result.CreatedDate.Should().BeAfter(beforeActionDatetime);
            result.LastModifiedDate.Should().BeAfter(beforeActionDatetime);
        }

        [Fact]
        public async Task UpdateAsync_Should_Update_An_Existing_Person()
        {
            // Arrange
            var person = new Person
            {
                Prenom = "John",
                Nom = "Smith"
            };
            var repo = new PersonRepository(_context);
            var guid = await repo.AddAsync(person);
            var beforeActionDatetime = DateTime.UtcNow;
            (await _contextForAssert.People.CountAsync()).Should().Be(1);
            var personToModify = await _context.People.FindAsync(guid);

            // Act
            personToModify.Prenom = "John2";
            personToModify.Nom = "Smith2";
            await repo.UpdateAsync(personToModify);

            // Assert
            (await _contextForAssert.People.CountAsync()).Should().Be(1);
            var result = await _contextForAssert.People.FindAsync(guid);
            result.Prenom.Should().Be("John2");
            result.Nom.Should().Be("Smith2");
            result.CreatedDate.Should().BeBefore(beforeActionDatetime);
            result.LastModifiedDate.Should().BeAfter(beforeActionDatetime);
        }

        [Fact]
        public async Task GetAsync_Should_Return_An_Existing_Person()
        {
            // Arrange
            var person = new Person
            {
                Prenom = "John",
                Nom = "Smith"
            };
            var repo = new PersonRepository(_context);
            var guid = await repo.AddAsync(person);

            // Act
            var result = await repo.GetAsync(guid);

            // Assert
            result.Prenom.Should().Be("John");
            result.Nom.Should().Be("Smith");
        }

        [Fact]
        public async Task DeleteAsync_Should_Return_An_Existing_Person()
        {
            // Arrange
            var person = new Person
            {
                Prenom = "John",
                Nom = "Smith"
            };
            var repo = new PersonRepository(_context);
            var guid = await repo.AddAsync(person);
            (await _contextForAssert.People.CountAsync()).Should().Be(1);
            var personToDelete = await _context.People.FindAsync(guid);

            // Act
            await repo.DeleteAsync(personToDelete);

            // Assert
            (await _contextForAssert.People.CountAsync()).Should().Be(0);
        }

        [Fact]
        public async Task ListAsync_Should_Return_All_People()
        {
            // Arrange
            var repo = new PersonRepository(_context);
            var person = new Person
            {
                Prenom = "John",
                Nom = "Smith"
            };
            await repo.AddAsync(person);
            var person2 = new Person
            {
                Prenom = "Sarah",
                Nom = "Johnson"
            };
            await repo.AddAsync(person2);
            (await _contextForAssert.People.CountAsync()).Should().Be(2);

            // Act
            var result = await repo.ListAsync();

            // Assert
            result.Count().Should().Be(2);
        }

        [Fact]
        public async Task ListAsync_Should_Return_Only_People_With_Prenom_Matching_Criteria()
        {
            // Arrange
            var repo = new PersonRepository(_context);
            var person = new Person
            {
                Prenom = "John",
                Nom = "Smith"
            };
            await repo.AddAsync(person);
            var person2 = new Person
            {
                Prenom = "Sarah",
                Nom = "Johnson"
            };
            await repo.AddAsync(person2);
            (await _contextForAssert.People.CountAsync()).Should().Be(2);

            // Act
            var result = await repo.ListAsync(prenomFilter: "sar");

            // Assert
            result.Count().Should().Be(1);
            result[0].Prenom.Should().Be("Sarah");
            result[0].Nom.Should().Be("Johnson");
        }

        [Fact]
        public async Task ListAsync_Should_Return_Only_People_With_Nom_Matching_Criteria()
        {
            // Arrange
            var repo = new PersonRepository(_context);
            var person = new Person
            {
                Prenom = "John",
                Nom = "Smith"
            };
            await repo.AddAsync(person);
            var person2 = new Person
            {
                Prenom = "Sarah",
                Nom = "Johnson"
            };
            await repo.AddAsync(person2);
            (await _contextForAssert.People.CountAsync()).Should().Be(2);

            // Act
            var result = await repo.ListAsync(nomFilter: "mit");

            // Assert
            result.Count().Should().Be(1);
            result[0].Prenom.Should().Be("John");
            result[0].Nom.Should().Be("Smith");
        }

    }
}
