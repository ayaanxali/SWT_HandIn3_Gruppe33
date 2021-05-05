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

    public class BUT2_Display_Output
    {
        private Display sut;
        private IOutput output;
        private StringWriter readConsole;

        [SetUp]
        public void SetUp()
        {
            output = new Output();
            sut = new Display(output);
            readConsole = new StringWriter();
            System.Console.SetOut(readConsole);
        }

        [TestCase(3,30)]
        [TestCase(2, 59)]
        [TestCase(0, 00)]
        public void Display_ShowTimeIsCalled_OutputLineContainsMinAndSec(int min, int sec)
        {
            sut.ShowTime(min,sec);

            var _consoleOutput = readConsole.ToString();

            Assert.That(_consoleOutput.Contains("Display shows: 0" + min + ":" + sec));
        }

        [TestCase(699)]
        [TestCase(250)]
        [TestCase(2)]
        public void Display_ShowPowerIsCalled_OutputLineContainsPower(int power)
        {
            sut.ShowPower(power);

            var _consoleOutput = readConsole.ToString();

            Assert.That(_consoleOutput.Contains("Display shows: " + power + " W"));
        }

        [Test]
        public void Display_WhenClearIsCalled_OutPutLineContainsCleared()
        {
            sut.Clear();
            
            var _consoleOutput = readConsole.ToString();

            Assert.That(_consoleOutput.Contains("Display cleared"));
        }
    }
}
