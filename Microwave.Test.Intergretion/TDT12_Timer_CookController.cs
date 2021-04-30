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
    class TDT12_Timer_CookController
    {
        private IPowerTube powerTube;
        private IDisplay display;
        private IUserInterface UI;
        private ITimer timer;
        private ICookController cookController;

        [SetUp]
        public void SetUp()
        {
            powerTube = Substitute.For<IPowerTube>();
            display = Substitute.For<IDisplay>();
            timer = new Timer();
            UI = Substitute.For<IUserInterface>();
            cookController = new CookController(timer, display, powerTube, UI);
        }

        [TestCase(50, 50)]
        public void Timer_StartCooking_(int power, int time)
        {
            timer.Start(time);
            
            //timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            //cookController.Received(1).StartCooking(50,60);
            powerTube.Received().TurnOn(power);

            
        }
    }
}
