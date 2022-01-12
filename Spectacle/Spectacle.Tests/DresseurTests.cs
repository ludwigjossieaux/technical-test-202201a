using FakeItEasy;
using FluentAssertions;
using Spectacle.Application.Interfaces;
using Spectacle.Application.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Spectacle.Tests
{
    public class DresseurTests
    {
        [Fact]
        public void Constructor_Should_Throw_ArgumentNullException_When_Singe_Is_Not_Provided()
        {
            // Arrange

            // Act
            Action act = () => new Dresseur(null);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void DemarreSpectacle_Should_Run_Tours_In_Order_And_Display_Spectateur_Reaction()
        {
            // Arrange
            var singe = new Singe("1");
            var tour_1 = new TourAcrobatie("marcher sur les mains");
            var tour_2 = new TourMusique("jouer de la guitare");
            singe.AjouterTour(tour_1);
            singe.AjouterTour(tour_2);
            var spectateur_1 = A.Fake<ISpectateur>();
            var spectateur_2 = A.Fake<ISpectateur>();
            var dresseur = new Dresseur(singe);
            dresseur.AcceuilleSpectateur(spectateur_1);
            dresseur.AcceuilleSpectateur(spectateur_2);

            // Act
            dresseur.DemarreSpectacle();

            // Assert
            A.CallTo(() => spectateur_1.AfficheReactionAuTourDuSinge(A<ITour>.Ignored, A<ISinge>.Ignored)).MustHaveHappenedTwiceExactly();
            A.CallTo(() => spectateur_2.AfficheReactionAuTourDuSinge(A<ITour>.Ignored, A<ISinge>.Ignored)).MustHaveHappenedTwiceExactly();
            A.CallTo(() => spectateur_1.AfficheReactionAuTourDuSinge(tour_1, singe)).MustHaveHappenedOnceExactly();
            A.CallTo(() => spectateur_2.AfficheReactionAuTourDuSinge(tour_1, singe)).MustHaveHappenedOnceExactly();
            A.CallTo(() => spectateur_1.AfficheReactionAuTourDuSinge(tour_2, singe)).MustHaveHappenedOnceExactly();
            A.CallTo(() => spectateur_2.AfficheReactionAuTourDuSinge(tour_2, singe)).MustHaveHappenedOnceExactly();
        }
    }
}
