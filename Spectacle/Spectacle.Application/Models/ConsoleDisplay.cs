using Spectacle.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectacle.Application.Models
{
    public class ConsoleDisplay : IConsoleDisplay
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}
