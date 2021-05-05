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
            buttonOfPower = new Button();
            buttonOfTime = new Button();
            buttonOfstartCancel = new Button();
            door = new Door();
            output = Substitute.For<IOutput>();
            light = new Light(output);
            CC = Substitute.For<ICookController>();
            display = Substitute.For<IDisplay>();
            UI = new UserInterface(buttonOfPower, buttonOfTime, buttonOfstartCancel, door, display, light, CC);
        }

        [Test]
        public void Light_OnStartCancelPressed_TurnOn()
        {
            buttonOfPower.Press();
            buttonOfTime.Press();
            buttonOfstartCancel.Press();

            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("on")));
        }

        [Test]
        public void Light_OnStartCancelPressed_TurnOff()
        {
            buttonOfPower.Press();
            buttonOfTime.Press();   
            buttonOfstartCancel.Press();
            buttonOfstartCancel.Press();

            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }
    }
}
