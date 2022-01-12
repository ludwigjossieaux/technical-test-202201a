using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Architecture.Domain.Entities
{
    public class Person : EntityBase
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
    }
}
