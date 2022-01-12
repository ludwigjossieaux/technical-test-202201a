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
    public class ConsoleDisplayTests
    {
        [Fact]
        public void WriteLine_Should_Write_To_Console()
        {
            // Arrange
            var display = new ConsoleDisplay();
            using var consoleText = new StringWriter();
            Console.SetOut(consoleText);

            // Act
            display.WriteLine("Hello World");

            // Assert
            consoleText.ToString().Should().Contain("Hello World");
        }
    }
}
