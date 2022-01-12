using Spectacle.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectacle.Application.Models
{
    public class Dresseur
    {
        private readonly List<ISpectateur> _spectateurs = new List<ISpectateur>();
        private readonly ISinge _singe;

        public Dresseur(ISinge singe)
        {
            _singe = singe ?? throw new ArgumentNullException(nameof(singe));
        }

        public void AcceuilleSpectateur(ISpectateur spectateur)
        {
            _spectateurs.Add(spectateur);
        }

        public void DemarreSpectacle()
        {
            foreach(var tour in _singe.ObtenirTours())
            {
                foreach(var spectateur in _spectateurs)
                {
                    spectateur.AfficheReactionAuTourDuSinge(tour, _singe);
                }
            }
        }
    }
}
