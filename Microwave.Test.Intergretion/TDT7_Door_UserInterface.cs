using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Intergretion
{
    [TestFixture]
    public class TDT7_Door_UserInterface
    {
        private Door door; //SUT
        private UserInterface UI;
        private IDisplay display;
        private IButton buttonOfPower;
        private IButton buttonOfTime;
        private IButton buttonOfstartCancel; 
        private ILight light;
        private ICookController CC;
        private ITimer timer; 

        [SetUp]
        public void SetUp()
        {
            door = new Door();
            
            display = Substitute.For<IDisplay>();
            buttonOfPower = Substitute.For<IButton>();
            buttonOfTime = Substitute.For<IButton>();
            buttonOfstartCancel = Substitute.For<IButton>();
            light = Substitute.For<ILight>();
            CC = Substitute.For<ICookController>();
           
            UI = new UserInterface(buttonOfPower,buttonOfTime,buttonOfstartCancel,door,display,light,CC);
        }

        [Test]
        public void test1()
        {
            door.Open();


        }
    }
}