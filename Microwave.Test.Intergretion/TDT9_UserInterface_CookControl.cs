using System;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Intergretion
{
    [TestFixture]
    public class TDT9_UserInterface_CookControl
    {
        private IUserInterface UI;
        private CookController CC;
        private IDisplay display;
        private IButton buttonOfPower;
        private IButton buttonOfTime;
        private IButton buttonOfstartCancel;
        private ILight light;
        private IDoor door;
        private ITimer timer;
        private IPowerTube powerTube;
        private IOutput output;


        [SetUp]
        public void SetUp()
        {
            display = Substitute.For<IDisplay>();
            buttonOfPower = new Button();
            buttonOfTime = new Button();
            buttonOfstartCancel = new Button();
            door = new Door();
            light = Substitute.For<ILight>();
            timer = Substitute.For<ITimer>();
            output = Substitute.For<IOutput>();
            powerTube = Substitute.For<IPowerTube>();
            CC = new CookController(timer,display,powerTube);
            UI = new UserInterface(buttonOfPower,buttonOfTime,buttonOfstartCancel,door,display,light,CC);
            CC.UI = UI;
        }
        //right now it is to test the interface method in cookcontrol that UI uses
        //maybe in another test we will test interfacet for UI from COOKcrontol that is where CooingIsDone thould be tested
        [TestCase(1,50)]
        [TestCase(2,100)]
        [TestCase(14,700)]
        public void Start_StartCooking(int nummerOfPressed,int ExpectedPower)
        {

            for (int i = 0; i < nummerOfPressed; i++)
            {
                buttonOfPower.Press();
            }

            buttonOfTime.Press();
            buttonOfstartCancel.Press();

            powerTube.Received().TurnOn(ExpectedPower);
        }

        [Test]
        public void Stop_StopCooking()
        {
            buttonOfPower.Press();
            buttonOfTime.Press();
            buttonOfstartCancel.Press();
            buttonOfstartCancel.Press();

            powerTube.Received().TurnOff();
            timer.Received().Stop();
        }
    }
}