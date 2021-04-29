using System;
using System.Collections.Generic;
using System.Text;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Intergretion
{
    class BUT3_Light_Output
    {
        private Light sut;
        private IOutput output;

        [SetUp]
        public void SetUp()
        {
            output = Substitute.For<IOutput>();
            sut = new Light(output);
        }

        [Test]
        public void Light_WhenTurnOnIsCalled_OutputLineContainsTurnedOn()
        {
            sut.TurnOn();
            
            output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("Light") && s.Contains("turned on")));
        }

        [Test]
        public void Light_WhenTurnOfIsCalled_OutputLineContainsTurnedOff()
        {
            sut.TurnOn();

            sut.TurnOff();

            output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("Light") && s.Contains("turned off")));
        }

    }
}
