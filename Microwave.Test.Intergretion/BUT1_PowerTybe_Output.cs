using System;
using System.IO;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Intergretion
{
    [TestFixture]

    public class Tests
    {
        private PowerTube sut;
        private IOutput output;
        private StringWriter readConsole;

        [SetUp]
        public void Setup()
        {
            output = new Output();
            sut = new PowerTube(output);
            readConsole = new StringWriter();
            System.Console.SetOut(readConsole);
        }

        [Test]
        public void PowerTupe_WhenTurnedOff_OutputLineContainsTurnedOff()
        {
            sut.TurnOn(10);

            sut.TurnOff();

            var _consoleOutput = readConsole.ToString();
            Assert.That(_consoleOutput.Contains("PowerTube turned off"));

            
        }
        [TestCase(699)]
        [TestCase(99)]
        [TestCase(2)]
        public void PowerTupe_WhenTurnedOn_OutputLineContainsTurnedOn(int power)
        {
            sut.TurnOn(power);

            var _consoleOutput = readConsole.ToString();
            Assert.That(_consoleOutput.Contains("PowerTube works with " + power));
        }
        
    }
}