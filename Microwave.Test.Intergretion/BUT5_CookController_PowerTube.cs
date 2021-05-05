using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Intergretion
{
    [TestFixture]

    class BUT5_CookController_PowerTube
    {
        private IPowerTube powerTube;
        private IDisplay display;
        private ILight light;
        private IUserInterface UI;
        private ITimer timer;
        private CookController sut;
        private IOutput output;
        private StringWriter readConsole;

        [SetUp]
        public void SetUp()
        {
            output = new Output();
            powerTube = new PowerTube(output);
            display = Substitute.For<IDisplay>();
            light = Substitute.For<ILight>();
            timer = Substitute.For<ITimer>();
            UI = Substitute.For<IUserInterface>();
            sut = new CookController(timer, display, powerTube, UI);
            readConsole = new StringWriter();
            System.Console.SetOut(readConsole);
        }

        [TestCase(50,05)]
        [TestCase(100, 02)]
        [TestCase(2, 06)]
        public void CookController_StartCooking_OutputIsReceivedOne(int power,int time)
        {
            sut.StartCooking(power,time);

            var _consoleOutput = readConsole.ToString();
            Assert.That(_consoleOutput.Contains("PowerTube works with " + power));

        }

        [TestCase(50, 05)]
        public void CookController_StopCooking_OutputIsReceivedOne(int power, int time)
        {
            sut.StartCooking(power, time);

            sut.Stop();

            var _consoleOutput = readConsole.ToString();
            Assert.That(_consoleOutput.Contains("PowerTube turned off"));
        }


    }
}
