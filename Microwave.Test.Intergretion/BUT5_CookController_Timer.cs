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
    class BUT5_CookController_Timer
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
            light = new Light(output);
            timer = Substitute.For<ITimer>();
            UI = Substitute.For<IUserInterface>();
            cookController = new CookController(timer, display, powerTube, UI);
        }

        [TestCase(50, 50)]
        public void CookController_OnTimerTick_OutputIsReceivedOne(int power, int time)
        {
            cookController.StartCooking(power, time);
            timer.TimeRemaining.Returns(time);
            timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("PowerTube") && s.Contains(Convert.ToString(power))));
        }
    }
}
