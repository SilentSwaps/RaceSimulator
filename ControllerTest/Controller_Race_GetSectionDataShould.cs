using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Controller;
using Model;

namespace ControllerTest
{
    [TestFixture]
    class ControllerGetSectionData
    {

        [SetUp]
        public void SetUp()
        {
            Data.Initialize();
            Data.NextRace();
            Data.NextRace();
        }

        [Test]
        public void GetSectionDataExistingSection_ReturnSectionData()
        {
            Section section = Data.CurrentRace.Track.Sections.ElementAt(1);
            
            Assert.IsTrue(Data.CurrentRace.HasSectionData(section));
            
            SectionData sectionData = Data.CurrentRace.GetSectionData(section);
            
            Assert.IsNotNull(sectionData);
        }

        [Test]
        public void GetSectionDataNonExistingSection_ReturnSectionData()
        {
            Section section = Data.CurrentRace.Track.Sections.ElementAt(5);
            
            Assert.IsTrue(Data.CurrentRace.HasSectionData(section));

            SectionData sectionData = Data.CurrentRace.GetSectionData(section);
            
            Assert.IsNotNull(sectionData);
            Assert.IsTrue(Data.CurrentRace.HasSectionData(section));
        }
    }
}