using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Architecture.Application.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
