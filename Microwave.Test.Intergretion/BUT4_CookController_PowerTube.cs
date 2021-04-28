using System;
using System.Collections.Generic;
using System.Text;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Intergretion
{
    class BUT4_CookController_PowerTube
    {
        private IPowerTube powerTube;
        private IDisplay display;
        private ILight light;
        private IUserInterface UI;
        private ITimer timer;
        private CookController cookController;
        private IOutput output;

        [SetUp]
        public void SetUp()
        {
            output = Substitute.For<IOutput>();
            powerTube = new PowerTube(output);
            display = new Display(output);
            light = new Light(output);
            timer = Substitute.For<ITimer>();
            UI = Substitute.For<IUserInterface>();
           cookController = new CookController(timer, display, powerTube, UI);
        }

    }
}
