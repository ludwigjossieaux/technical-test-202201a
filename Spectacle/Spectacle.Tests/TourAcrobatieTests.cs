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
    public class TourAcrobatieTests
    {
        [Fact]
        public void Constructor_Should_Throw_ArgumentNullException_When_Nom_Is_Not_Provided()
        {
            // Arrange

            // Act
            Action act = () => new TourAcrobatie(null);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ObtenirReactionStandard_Should_Return_Value()
        {
            // Arrange

            // Act
            var tour = new TourAcrobatie("marcher sur les mains");

            // Assert
            tour.ObtenirReactionStandard().Should().Be("applaudit");
        }

        [Fact]
        public void ObtenirNom_Should_Return_Nom()
        {
            // Arrange
            var name = "marcher sur les mains";

            // Act
            var tour = new TourAcrobatie(name);

            // Assert
            tour.ObtenirNom().Should().Be(name);
        }
    }
}
