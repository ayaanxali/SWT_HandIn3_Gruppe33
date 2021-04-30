using System;
using System.Collections.Generic;
using System.Text;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using Newtonsoft.Json.Bson;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Intergretion
{
    [TestFixture]
    class TDT10_UserInterface_Display
    {
        private IUserInterface UI;
        private IUserInterface UI2;
        private ICookController CC;
        private IDisplay display;
        private IOutput output;
        private IButton buttonOfPower;
        private IButton buttonOfTime;
        private IButton buttonOfstartCancel;
        private ILight light;
        private IDoor door;
        //private ITimer timer;
        //private IPowerTube powerTube;

        [SetUp]
        public void SetUp()
        {
            buttonOfPower = Substitute.For<IButton>();
            buttonOfTime = Substitute.For<IButton>();
            buttonOfstartCancel = Substitute.For<IButton>();
            door = Substitute.For<IDoor>();
            light = Substitute.For<ILight>();
            //timer = Substitute.For<ITimer>();
            //powerTube = Substitute.For<IPowerTube>();
            output = Substitute.For<IOutput>();
            CC = Substitute.For<ICookController>();
            display = new Display(output);
            UI = new UserInterface(buttonOfPower, buttonOfTime, buttonOfstartCancel, door, display, light, CC);
            UI2 = Substitute.For<IUserInterface>();
            
        }
        
        [Test]
        public void Display_OnPowerPressed_ShowPower()
        {
            buttonOfPower.Pressed += Raise.EventWith(this, EventArgs.Empty);

            output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("Display") && s.Contains("W")));

        }

        [Test]
        public void Display_OnStartCancelPressed_Clear()
        {
            buttonOfPower.Pressed += Raise.EventWith(this, EventArgs.Empty);
            buttonOfstartCancel.Pressed += Raise.EventWith(this, EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(s => s.Contains("Display") && s.Contains("cleared")));
        }

        [Test]
        public void Display_OnTimePressed_ShowTime()
        {
            buttonOfPower.Pressed += Raise.EventWith(this, EventArgs.Empty);
            buttonOfTime.Pressed += Raise.EventWith(this, EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("01:00")));
        }

    }
}
