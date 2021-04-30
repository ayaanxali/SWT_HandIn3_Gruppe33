using System;
using System.Collections.Generic;
using System.Text;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Intergretion
{
    [TestFixture]

    class BUT4_CookController_PowerTube
    {
        private IPowerTube powerTube;
        private IDisplay display;
        private ILight light;
        private IUserInterface UI;
        private ITimer timer;
        private CookController sut;
        private IOutput output;

        [SetUp]
        public void SetUp()
        {
            output = Substitute.For<IOutput>();
            powerTube = new PowerTube(output);
            display = new Display(output);
            light = new Light(output);
            timer = Substitute.For<ITimer>();
            UI = Substitute.For<IUserInterface>();
           sut = new CookController(timer, display, powerTube, UI);
        }

        [TestCase(50,50)]
        public void CookController_StartCooking_OutputIsReceivedOne(int power,int time)
        {
            sut.StartCooking(power,time);

            output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("PowerTube") && s.Contains(Convert.ToString(power))));
        }

        [TestCase(50, 50)]
        public void CookController_StopCooking_OutputIsReceivedOne(int power, int time)
        {
            sut.StartCooking(power, time);

            sut.Stop();

            output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("PowerTube") && s.Contains("turned off")));
        }


    }
}
