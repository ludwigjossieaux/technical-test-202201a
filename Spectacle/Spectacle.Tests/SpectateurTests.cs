using FakeItEasy;
using FluentAssertions;
using Spectacle.Application.Interfaces;
using Spectacle.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Spectacle.Tests
{
    public class SpectateurTests
    {
        [Fact]
        public void Constructor_Should_Throw_ArgumentNullException_When_Nom_Is_Not_Provided()
        {
            // Arrange
            var display = A.Fake<IConsoleDisplay>();

            // Act
            Action act = () => new Spectateur(null, display);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_Should_Throw_ArgumentNullException_When_ConsoleDisplay_Is_Not_Provided()
        {
            // Arrange
            string name = "spectateur";

            // Act
            Action act = () => new Spectateur(name, null);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_Should_Not_Throw_Exception_When_All_Is_Provided()
        {
            // Arrange
            string name = "spectateur";
            var display = A.Fake<IConsoleDisplay>();

            // Act
            Action act = () => new Spectateur(name, display);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void AfficheReactionAuTourDuSinge_Should_Display_Spectateur_Reaction()
        {
            // Arrange
            var singe = new Singe("1");
            var tour = new TourAcrobatie("marcher sur les mains");
            var display = A.Fake<IConsoleDisplay>();
            var spectateur = new Spectateur("spectateur", display);

            // Act
            spectateur.AfficheReactionAuTourDuSinge(tour, singe);

            // Assert
            var expected = "spectateur applaudit pendant le tour 'marcher sur les mains' du singe 1";
            A.CallTo(() => display.WriteLine(expected)).MustHaveHappened();
        }
    }
}
