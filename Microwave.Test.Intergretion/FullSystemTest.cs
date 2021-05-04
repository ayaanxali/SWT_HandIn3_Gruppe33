using System;
using System.Threading;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer = Microwave.Classes.Boundary.Timer;

namespace Microwave.Test.Intergretion
{
    [TestFixture]
    public class TDT_FullSystem
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
            output = Substitute.For<IOutput>();
            display = new Display(output);
            buttonOfPower = new Button();
            buttonOfTime = new Button();
            buttonOfstartCancel = new Button();
            door = new Door();
            light = new Light(output);
            timer = new Timer();
            powerTube = new PowerTube(output);
            CC = new CookController(timer, display, powerTube);
            UI = new UserInterface(buttonOfPower, buttonOfTime, buttonOfstartCancel, door, display, light, CC);
        }
        [TestCase(1, 50)]
        [TestCase(2, 100)]
        [TestCase(14, 700)]
        public void PressedPowerButton_setPower_OutPutShowsPower(int nummerOfPressed, int ExpectedPower)
        {
            for (int i = 0; i < nummerOfPressed; i++)
            {
                buttonOfPower.Press();
            }

            buttonOfTime.Press();
            buttonOfstartCancel.Press();

            output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("PowerTube") && s.Contains(Convert.ToString(ExpectedPower))));
        }

        [TestCase(1,1)]
        [TestCase(2,2)]
        [TestCase(3,3)]
        public void PressedTimerButton_setTime_OutputShowsTime(int numberOfPressed, int ExpectedTime)
        {
           buttonOfPower.Press();
           for (int i = 0; i < numberOfPressed; i++)
           {
               buttonOfTime.Press();
           }
           output.Received().OutputLine(Arg.Is<string>(s => s.Contains("Display") && s.Contains(Convert.ToString(ExpectedTime))));
        }

        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public void PressedTimerButton_Cooking(int numberOfPressed, int ExpectedTime)
        {
            buttonOfPower.Press();
            for (int i = 0; i < numberOfPressed; i++)
            {
                buttonOfTime.Press();
            }
            buttonOfstartCancel.Press();
            //Thread.Sleep(1010*ExpectedTime); denne linje skal muligvis være der for at testen venter til at tiden faktisk er udløbet. prøv uden og med linjen. 

            output.Received().OutputLine(Arg.Is<string>(s => s.Contains("PowerTube is turned off")));
            output.Received().OutputLine(Arg.Is<string>(s => s.Contains("Display cleared")));
            output.Received().OutputLine(Arg.Is<string>(s => s.Contains("Light turned off")));
        }

        [Test]
        public void OnStartCancelPressed_SetPower_OutputShow()
        {
            buttonOfPower.Press();
            buttonOfTime.Press();

            buttonOfstartCancel.Press();

            //muligvis skal rækkefølgen ændres
            output.Received().OutputLine(Arg.Is<string>(s => s.Contains("Light is turned on")));
            output.Received().OutputLine(Arg.Is<string>(s => s.Contains("PowerTube") && s.Contains(Convert.ToString(50))));
        }

        [Test]
        public void DoorOpened_StateIsCooking()
        {
            buttonOfPower.Press();
            buttonOfTime.Press();
            buttonOfstartCancel.Press();

            door.Open();

            output.Received().OutputLine(Arg.Is<string>(s => s.Contains("PowerTube turned off")));
            output.Received().OutputLine(Arg.Is<string>(s => s.Contains("Display cleared")));
        }

        [Test]
        public void OnStartCancelPressed_StateIsCooking()
        {
            buttonOfPower.Press();
            buttonOfTime.Press();
            buttonOfstartCancel.Press();

            buttonOfstartCancel.Press();

            //muligvis skal rækkefølgen ændres. 
            output.Received().OutputLine(Arg.Is<string>(s => s.Contains("PowerTube turned off")));
            output.Received().OutputLine(Arg.Is<string>(s => s.Contains("Light is turned off")));
            output.Received().OutputLine(Arg.Is<string>(s => s.Contains("Display cleared")));
           
        }
    }
}