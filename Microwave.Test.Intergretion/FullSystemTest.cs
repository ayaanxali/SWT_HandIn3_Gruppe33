using System;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Intergretion
{
    [TestFixture]
    public class FullSystemTest
    {
        private IUserInterface UI;
        private ICookController CC;
        private IDisplay display;
        private IButton buttonOfPower;
        private IButton buttonOfTime;
        private IButton buttonOfstartCancel;
        private ILight light;
        private IDoor door;
        private ITimer timer;
        private PowerTube powerTube;
        private IOutput output;

        [SetUp]
        public void SetUp()
        {
            display = Substitute.For<IDisplay>();
            buttonOfPower = new Button();
            buttonOfTime = new Button();
            buttonOfstartCancel = new Button();
            door = Substitute.For<IDoor>();
            light = Substitute.For<ILight>();
            timer = Substitute.For<ITimer>();
            output = Substitute.For<IOutput>();
            powerTube = new PowerTube(output);
            CC = new CookController(timer, display, powerTube);
            UI = new UserInterface(buttonOfPower, buttonOfTime, buttonOfstartCancel, door, display, light, CC);
        }
        [TestCase(1, 50)]
        [TestCase(2, 100)]
        [TestCase(14, 700)]//this failed because of error in powertube
        public void PressedPowerButton_StartCooking_OutPutShowsPower(int nummerOfPressed, int ExpectedPower)
        {
            for (int i = 0; i < nummerOfPressed; i++)
            {
                //buttonOfPower.Pressed += Raise.EventWith(this, EventArgs.Empty);
                buttonOfPower.Press();
            }

            buttonOfTime.Press();
            buttonOfstartCancel.Press();
            //buttonOfTime.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //buttonOfstartCancel.Pressed += Raise.EventWith(this, EventArgs.Empty);

            output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("PowerTube") && s.Contains(Convert.ToString(ExpectedPower))));
        }

        [TestCase(2,2)]
        [TestCase(5,5)]
        public void PressedTimerButton_StartCooking_OutputShowsTime(int numberOfPressed, int ExpectedTime)
        {
           buttonOfPower.Press();
           for (int i = 0; i < numberOfPressed; i++)
           {
               buttonOfTime.Press();
           }
           
           output.Received().OutputLine(Arg.Is<string>(s => s.Contains("PowerTube") && s.Contains(Convert.ToString(ExpectedTime))));

        }
    }
}