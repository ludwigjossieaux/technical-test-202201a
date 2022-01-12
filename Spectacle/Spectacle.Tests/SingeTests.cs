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
    public class SingeTests
    {
        [Fact]
        public void Constructor_Should_Throw_ArgumentNullException_When_Nom_Is_Not_Provided()
        {
            // Arrange
            string name = null;

            // Act
            Action act = () => new Singe(name);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_Should_Not_Throw_Exception_When_Nom_Is_Provided()
        {
            // Arrange
            string name = "le singe";

            // Act
            Action act = () => new Singe(name);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void ObtenirNom_Should_Return_Nom()
        {
            // Arrange
            var name = "le singe";
            var singe = new Singe(name);

            // Act
            var result = singe.ObtenirNom();

            // Assert
            result.Should().Be(name);
        }

        [Fact]
        public void AjouterTour_Should_Add_Tour_To_List()
        {
            // Arrange
            var singe = new Singe("le singe");
            singe.ObtenirTours().Count().Should().Be(0);

            // Act
            singe.AjouterTour(new TourAcrobatie("marcher sur les mains"));

            // Assert
            singe.ObtenirTours().Count().Should().Be(1);
            singe.ObtenirTours()[0].ObtenirNom().Should().Be("marcher sur les mains");
        }

        [Fact]
        public void ObtenirTours_Should_Return_List_Of_Tours()
        {
            // Arrange
            var singe = new Singe("le singe");
            singe.AjouterTour(new TourAcrobatie("marcher sur les mains"));
            singe.AjouterTour(new TourMusique("jouer de la guitare"));

            // Act
            var tours = singe.ObtenirTours();

            // Assert
            tours.Count().Should().Be(2);
            tours[0].ObtenirNom().Should().Be("marcher sur les mains");
            tours[1].ObtenirNom().Should().Be("jouer de la guitare");
        }
    }
}
