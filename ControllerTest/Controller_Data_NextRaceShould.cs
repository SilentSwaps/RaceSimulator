using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Controller;
using Model;

namespace ControllerTest
{
    [TestFixture]
    class ControllerNextTrackShould
    {

        [SetUp]
        public void SetUp()
        {
            Data.Initialize();
            Data.NextRace();
        }

        [Test]
        public void NextTrackExist_ReturnTrack()
        {
            Assert.IsNotNull(Data.CurrentRace);
        }

        [Test]
        public void NextTrackEmpty_ReturnNull()
        {
            Data.NextRace();
            Data.NextRace();

            Assert.IsNull(Data.CurrentRace);
        }
    }
}