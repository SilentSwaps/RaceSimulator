using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace ControllerTest
{
    [TestFixture]
    class Model_Competition_NextTrackShould
    {
        private Competition _competition;

        [SetUp]
        public void SetUP()
        {
            _competition = new Competition();
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            var result = _competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            LinkedList<Section> section = new LinkedList<Section>();
            Track track = new Track("Steenwijk", section);
            _competition.Tracks.Enqueue(track);
            var result = _competition.NextTrack();
            Assert.AreEqual(result, track);
        }

        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            LinkedList<Section> section = new LinkedList<Section>();
            Track track = new Track("Steenwijk", section);
            _competition.Tracks.Enqueue(track);
            var result = _competition.NextTrack();
            result = _competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            LinkedList<Section> section = new LinkedList<Section>();
            Track track = new Track("Steenwijk", section);
            Track track2 = new Track("zwollywood", section);
            _competition.Tracks.Enqueue(track);
            _competition.Tracks.Enqueue(track2);
            var result = _competition.NextTrack();
            Assert.AreEqual(result, track);
            var result2 = _competition.NextTrack();
            Assert.AreEqual(result2, track2);
        }
    }
}
