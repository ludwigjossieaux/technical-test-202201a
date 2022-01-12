using Spectacle.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectacle.Application.Models
{
    public class TourMusique : ITour
    {
        private readonly string _nom;

        public TourMusique(string nom)
        {
            _nom = nom ?? throw new ArgumentNullException(nameof(nom));
        }

        public string ObtenirNom()
        {
            return _nom;
        }

        public string ObtenirReactionStandard()
        {
            return "siffle";
        }
    }
}
