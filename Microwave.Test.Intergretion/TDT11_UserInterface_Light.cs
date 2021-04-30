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
    class TDT11_UserInterface_Light
    {
        private IUserInterface UI;
        private ICookController CC;
        private IDisplay display;
        private IOutput output;
        private IButton buttonOfPower;
        private IButton buttonOfTime;
        private IButton buttonOfstartCancel;
        private ILight light;
        private IDoor door;
        [SetUp]
        public void SetUp()
        {
            buttonOfPower = Substitute.For<IButton>();
            buttonOfTime = Substitute.For<IButton>();
            buttonOfstartCancel = Substitute.For<IButton>();
            door = Substitute.For<IDoor>();
            output = Substitute.For<IOutput>();
            light = new Light(output);
            CC = Substitute.For<ICookController>();
            display = Substitute.For<IDisplay>();
            UI = new UserInterface(buttonOfPower, buttonOfTime, buttonOfstartCancel, door, display, light, CC);
        }

        [Test]
        public void Light_OnStartCancelPressed_TurnOn()
        {
            //buttonOfPower.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //buttonOfTime.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //buttonOfstartCancel.Pressed += Raise.EventWith(this, EventArgs.Empty);

            UI.OnPowerPressed(this, EventArgs.Empty);
            UI.OnTimePressed(this, EventArgs.Empty);
            UI.OnStartCancelPressed(this, EventArgs.Empty);

            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }

        [Test]
        public void Light_OnStartCancelPressed_TurnOff()
        {
            //buttonOfPower.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //buttonOfTime.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //buttonOfstartCancel.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //buttonOfstartCancel.Pressed += Raise.EventWith(this, EventArgs.Empty);

            UI.OnPowerPressed(this, EventArgs.Empty);
            UI.OnTimePressed(this, EventArgs.Empty);
            UI.OnStartCancelPressed(this, EventArgs.Empty);
            UI.OnStartCancelPressed(this, EventArgs.Empty);

            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }
    }
}
