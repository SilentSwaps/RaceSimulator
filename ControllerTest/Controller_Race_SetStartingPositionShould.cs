using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Controller;
using Model;
using System.Linq;

namespace ControllerTest
{
    [TestFixture]
    class ControllerRaceSetStartingPositionShould
    {
        private LinkedList<Section> _sections;
        
        [SetUp]
        public void SetUp()
        {
            Data.Initialize();
            Data.NextRace();
            _sections = Data.CurrentRace.Track.Sections;
        }

        [Test]
        public void SetStartingPositions_TotalParticipantsSet_EqualToStartPositions()
        {
            int count = 0;
            int startCount = _sections.Count(item => item.SectionType == SectionTypes.StartGrid) * 2;
            int participantsCount = Enum.GetNames(typeof(TeamColors)).Length;

            foreach (Section section in _sections)
            {
                if (section.SectionType == SectionTypes.StartGrid)
                {
                    SectionData sectionData = Data.CurrentRace.GetSectionData(section);

                    if (sectionData.Left != null)
                        count += 1;

                    if (sectionData.Right != null)
                        count += 1;
                }
            }
            
            Assert.AreEqual(startCount >= participantsCount ? participantsCount : startCount, count);
        }

        [Test]
        public void SetStartingPositions_ParticipantOnStartGrid()
        {
            int count = 0;
            
            foreach (Section section in _sections)
            {
                if (section.SectionType != SectionTypes.StartGrid)
                {
                    SectionData sectionData = Data.CurrentRace.GetSectionData(section);

                    if (sectionData.Left != null)
                        count += 1;

                    if (sectionData.Right != null)
                        count += 1;
                }
            }
            
            Assert.AreEqual(0, count);
        }
    }
}