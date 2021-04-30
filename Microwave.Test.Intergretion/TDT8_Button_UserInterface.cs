using System;
using System.Net.Http;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Intergretion
{
    [TestFixture]
    public class TDT8_Button_UserInterface
    {
       // private Button button;
        private IUserInterface UI;
        private IDisplay display;
        private IButton buttonOfPower;
        private IButton buttonOfTime;
        private IButton buttonOfstartCancel;
        private ILight light;
        private ICookController CC;
        private IDoor door; 

        [SetUp]
        public void SetUp()
        {
            door = Substitute.For<IDoor>();
            display = Substitute.For<IDisplay>();
            buttonOfPower = new Button();
            buttonOfTime = new Button();
            buttonOfstartCancel = new Button();
            light = Substitute.For<ILight>();
            CC = Substitute.For<ICookController>();

            UI = new UserInterface(buttonOfPower, buttonOfTime, buttonOfstartCancel, door, display, light, CC);
        }

        [Test]
        public void OnPowerPressed()
        {
            buttonOfPower.Press();

            display.Received().ShowPower(50);
        }

        [Test]
        public void OnTimerPressed()
        {
            //arrange
            buttonOfPower.Press();

            //act
            buttonOfTime.Press();

            //assert
            display.Received().ShowTime(1,0);

        }

        [Test]
        public void OnStartCancelPressed()
        {
            buttonOfPower.Press();
            buttonOfTime.Press();

            buttonOfstartCancel.Press();
            
            CC.Received().StartCooking(50,60);
            light.Received().TurnOn();
        }

        [Test]
        public void OnStartCancelPressed_myStateIsCooking()
        {
            buttonOfPower.Press();
            buttonOfTime.Press();
            buttonOfstartCancel.Press();
            buttonOfstartCancel.Press();

            CC.Received().Stop();
            display.Received().Clear();
            light.Received().TurnOff();
        }

    }
}