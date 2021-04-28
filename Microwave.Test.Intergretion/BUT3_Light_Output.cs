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
        private Light light;
        private IOutput output;

        [SetUp]
        public void SetUp()
        {
            output = Substitute.For<IOutput>();
            light = new Light(output);
        }

        [Test]
        public void Light_WhenTurnOnIsCalled_OutputLineContainsTurnedOn()
        {
            light.TurnOn();

            output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("Light") && s.Contains("turned on")));
        }

        [Test]
        public void Light_WhenTurnOfIsCalled_OutputLineContainsTurnedOff()
        {
            light.TurnOn();

            light.TurnOff();

            output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("Light") && s.Contains("turned off")));
        }

    }
}
