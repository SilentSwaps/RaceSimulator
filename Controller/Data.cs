using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace;

        public delegate void OnRaceFinished(object sender, EventArgs e);
        public static event OnRaceFinished Onracefinished;

        public static void Initialize()
        {
            Competition = new Competition();
            AddParticipants();
            AddTracks();
        }

        public static void AddParticipants()
        {
            Car car1 = new Car(10, 10, 2, false);
            Car car2 = new Car(10, 10, 9, false);

            Driver mark = new Driver("Mark", 0, car1, TeamColors.Red);
            Driver leroy = new Driver("Leroy", 0, car2, TeamColors.Blue);

            Competition.Participants.Add(mark);
            Competition.Participants.Add(leroy);
        }

        public static void AddTracks()
        {
            SectionTypes[] sections = { SectionTypes.RightCorner,
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.Finish,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight
            };
            SectionTypes[] sectionszwolle =
            {
                SectionTypes.RightCorner,
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.Finish,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
            };

            Track steenwijk = new Track("Rondje Steenwiek", sections);
            Track zwolle = new Track("Zwollywood", sectionszwolle);

            Competition.Tracks.Enqueue(steenwijk);
            Competition.Tracks.Enqueue(zwolle);
        }

        public static void NextRace()
        {
            Track nextTrack = Competition.NextTrack();
            CurrentRace = nextTrack != null ? new Race(nextTrack, Competition.Participants) : null;
        }

        public static void Finishedrace()
        {
            NextRace();
            Onracefinished?.Invoke(null, new EventArgs());
        }
    }
}
