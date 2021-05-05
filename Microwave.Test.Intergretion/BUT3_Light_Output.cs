using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Intergretion
{
    [TestFixture]

    class BUT3_Light_Output
    {
        private Light sut;
        private IOutput output;
        private StringWriter readConsole;

        [SetUp]
        public void SetUp()
        {
            output = new Output();
            sut = new Light(output);
            readConsole = new StringWriter();
            System.Console.SetOut(readConsole);
        }

        [Test]
        public void Light_WhenTurnOnIsCalled_OutputLineContainsTurnedOn()
        {
            sut.TurnOn();

            var _consoleOutput = readConsole.ToString();
            Assert.That(_consoleOutput.Contains("Light is turned on"));
        }

        [Test]
        public void Light_WhenTurnOfIsCalled_OutputLineContainsTurnedOff()
        {
            sut.TurnOn();

            sut.TurnOff();

            var _consoleOutput = readConsole.ToString();
            Assert.That(_consoleOutput.Contains("Light is turned off"));
        }

    }
}
