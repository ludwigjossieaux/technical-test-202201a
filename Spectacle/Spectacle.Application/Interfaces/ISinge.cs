using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectacle.Application.Interfaces
{
    public interface ISinge
    {
        string ObtenirNom();
        void AjouterTour(ITour tour);
        List<ITour> ObtenirTours();
    }
}
