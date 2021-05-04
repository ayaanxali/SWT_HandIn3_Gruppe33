using System;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Intergretion
{
    [TestFixture]

    public class Tests
    {
        private PowerTube sut;
        private IOutput output; 

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();
            sut = new PowerTube(output);
            
        }

        [Test]
        public void PowerTupe_WhenTurnedOff_OutputLineContainsTurnedOff()
        {
            sut.TurnOn(10);

            sut.TurnOff();

            output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("PowerTube") && s.Contains("turned off")));
            
        }
        [TestCase(699)]
        [TestCase(99)]
        [TestCase(2)]
        public void PowerTupe_WhenTurnedOn_OutputLineContainsTurnedOn(int power)
        {
            sut.TurnOn(power);

            output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("PowerTube") && s.Contains(Convert.ToString(power))));
        }
        [TestCase(700)]
        [TestCase(1)]
        public void PowerTupe_WhenTurnedOn_OutputLineDoesNotreciev(int power)
        {
            sut.TurnOn(power);

            output.DidNotReceive().OutputLine(Arg.Is<string>(s => s.Contains("PowerTube") && s.Contains(Convert.ToString(power))));
        }
    }
}