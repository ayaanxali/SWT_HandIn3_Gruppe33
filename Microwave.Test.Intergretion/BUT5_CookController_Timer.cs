using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer=Microwave.Classes.Boundary.Timer;

namespace Microwave.Test.Intergretion
{
    class BUT5_CookController_Timer
    {
        private IPowerTube powerTube;
        private IDisplay display;
        private ILight light;
        private IUserInterface UI;
        private ITimer timer;
        private ICookController cookController;
        private IOutput output;

        [SetUp]
        public void SetUp()
        {
            //output = Substitute.For<IOutput>();
            powerTube = Substitute.For<IPowerTube>();
            //display = new Display(output);
            display = Substitute.For<IDisplay>();
            light = new Light(output);
            timer = new Timer();
            UI = Substitute.For<IUserInterface>();
            cookController = new CookController(timer, display, powerTube, UI);
        }

        [TestCase(50, 50)]
        public void CookController_OnTimerTick_OutputIsReceivedOne(int power, int time)
        {
            cookController.StartCooking(power,time);
            Thread.Sleep(1010*5);//5 is in sec

            cookController.Stop();
            Assert.That(timer.TimeRemaining, Is.EqualTo(45));
        }

        [TestCase(60,5)]
        public void test(int time, int waittime)
        {
            cookController.StartCooking(50,time);
            Thread.Sleep(1110*waittime);

            display.Received().ShowTime(00,time-waittime);
            //output.Received(1).OutputLine("Display shows: 00:45");
        }

        [Test]
        public void TimeExpired()
        {
            cookController.StartCooking(50,6);
            Thread.Sleep(8000);

            powerTube.Received().TurnOff();

        }
    }
}
