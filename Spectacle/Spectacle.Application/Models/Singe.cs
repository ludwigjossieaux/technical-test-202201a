using Spectacle.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectacle.Application.Models
{
    public class Singe: ISinge
    {
        private readonly string _nom;

        private List<ITour> _tours = new List<ITour>();

        public Singe(string nom)
        {
            _nom = nom ?? throw new ArgumentNullException(nameof(nom));
        }

        public void AjouterTour(ITour tour)
        {
            _tours.Add(tour);
        }

        public string ObtenirNom()
        {
            return _nom;
        }

        public List<ITour> ObtenirTours()
        {
            return _tours;
        }
    }
}
