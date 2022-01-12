using Spectacle.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectacle.Application.Models
{
    public class Spectateur: ISpectateur
    {
        private readonly string _nom;
        private readonly IConsoleDisplay _consoleDisplay;

        public Spectateur(string nom, IConsoleDisplay consoleDisplay)
        {
            _nom = nom ?? throw new ArgumentNullException(nameof(nom));
            _consoleDisplay = consoleDisplay ?? throw new ArgumentNullException(nameof(consoleDisplay));
        }

        public void AfficheReactionAuTourDuSinge(ITour tour, ISinge singe)
        {
            _consoleDisplay.WriteLine(
                $"{_nom} {tour.ObtenirReactionStandard()} pendant le tour '{tour.ObtenirNom()}' du singe {singe.ObtenirNom()}");
        }
    }
}
