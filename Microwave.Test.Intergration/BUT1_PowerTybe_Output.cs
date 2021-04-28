using System;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Intergration
{
    public class Tests
    {
        private PowerTube powerTyber;
        private IOutput output; 

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();
            powerTyber = new PowerTube(output);
            
        }

        [Test]
        public void PowerTupe_WhenTurnedOff_OutputLineContainsTurnedOff()
        {
            powerTyber.TurnOn(10);

            powerTyber.TurnOff();

            output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("PowerTube") && s.Contains("turned off")));
            
        }

        [TestCase(99)]
        [TestCase(2)]
        public void PowerTupe_WhenTurnedOn_OutputLineContainsTurnedOn(int power)
        {
            powerTyber.TurnOn(power);

            output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("PowerTube") && s.Contains(Convert.ToString(power))));
        }
    }
}