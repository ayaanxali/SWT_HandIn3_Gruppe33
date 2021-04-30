using System;
using System.IO;
using System.Security.Cryptography;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NUnit.Framework;

namespace Microwave.Test.Intergretion
{
    [TestFixture]
    public class SystemTest
    {
        private StringWriter readConsole;
        private IButton buttonOfPower;
        private IButton buttonOfTime;
        private IButton buttonOfstartCancel;
        private IOutput output;

        [SetUp]
        public void SetUp()
        {
            output= new Output();
            readConsole = new StringWriter();
            System.Console.SetOut(readConsole);
        }

        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 4)]
        [TestCase(14, 5)]
        public void OnStartCancalPressed_Simonshelp_Settime(int power, int time)
        {
            string result = "";
            int timesRun = power + 1;

            for (int j = 1; j < timesRun; j++)
            {
                buttonOfPower.Press();
                result += string.Join("", "Display shows: " + 50 * j + "W\r\n");
            }

            for (int i = 0; i < time;)
            {
                i++;
                buttonOfTime.Press();
                string eachTime = "Display shows: 0" + i + ":00\r\n";
                result += string.Join("", eachTime);
            }

            buttonOfstartCancel.Press();
            result += string.Join("", "Light is turned on\r\nPowertube works with " + 50 * power + "\r\n");


            var text = readConsole.ToString();
            Assert.AreEqual(result, text);

        }
    }
}