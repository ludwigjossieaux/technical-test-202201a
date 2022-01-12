using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectacle.Application.Interfaces
{
    public interface ISpectateur
    {
        void AfficheReactionAuTourDuSinge(ITour tour, ISinge singe);
    }
}
