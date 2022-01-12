using FluentAssertions;
using Spectacle.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Spectacle.Tests
{
    public class TourMusiqueTests
    {
        [Fact]
        public void Constructor_Should_Throw_ArgumentNullException_When_Nom_Is_Not_Provided()
        {
            // Arrange

            // Act
            Action act = () => new TourMusique(null);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ObtenirReactionStandard_Should_Return_Value()
        {
            // Arrange

            // Act
            var tour = new TourMusique("jouer de la guitare");

            // Assert
            tour.ObtenirReactionStandard().Should().Be("siffle");
        }

        [Fact]
        public void ObtenirNom_Should_Return_Nom()
        {
            // Arrange
            var name = "jouer de la guitare";

            // Act
            var tour = new TourMusique(name);

            // Assert
            tour.ObtenirNom().Should().Be(name);
        }
    }
}
