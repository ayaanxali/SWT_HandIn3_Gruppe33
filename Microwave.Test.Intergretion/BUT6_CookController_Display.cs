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

    class BUT6_CookController_Display
    {
        private IPowerTube powerTube;
        private IDisplay display;
        private ILight light;
        private IUserInterface UI;
        private ITimer timer;
        private CookController cookController;
        private IOutput output;

        [SetUp]
        public void SetUp()
        {
            output = Substitute.For<IOutput>();
            powerTube = new PowerTube(output);
            display = new Display(output);
            //display = Substitute.For<IDisplay>();
            light = new Light(output);
            timer = Substitute.For<ITimer>();
            UI = Substitute.For<IUserInterface>();
            cookController = new CookController(timer, display, powerTube, UI);
        }

        [TestCase(50,05,00)]
        public void CookController_StartCookingAndDisplayShowsTime_OutputIsReceivecOne(int power, int time1, int time2)
        {
            cookController.StartCooking(power,time1);
            timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            display.ShowTime(time1,time2);
            
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("05:00")));

        }
        [TestCase(05, 00)]
        public void CookController_StopCookingAndDisplayShowsTime_OutputIsReceivecOne( int time1, int time2)
        {
            cookController.Stop();
            timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            display.ShowTime(time1, time2);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("05:00")));

        }
    }
}
