using System;
using System.Collections.Generic;
using System.Text;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Intergretion
{
    [TestFixture]

    public class BUT2_Display_Output
    {
        private Display sut;
        private IOutput output;

        [SetUp]
        public void SetUp()
        {
            output = Substitute.For<IOutput>();
            sut = new Display(output);
        }

        [TestCase(03,30)]
        public void Display_ShowTimeIsCalled_OutputLineContainsMinAndSec(int min, int sec)
        {
            sut.ShowTime(min,sec);
            output.Received(1)
                .OutputLine(Arg.Is<string>(s => s.Contains("Display") && s.Contains("03:30")));
        }

        [TestCase(99)]
        public void Display_ShowPowerIsCalled_OutputLineContainsPower(int power)
        {
            sut.ShowPower(power);

            output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("Display") && s.Contains(Convert.ToString(power))));
        }

        [Test]
        public void Display_WhenClearIsCalled_OutPutLineContainsCleared()
        {
            sut.Clear();

            output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("Display") && s.Contains("cleared")));
        }
    }
}
