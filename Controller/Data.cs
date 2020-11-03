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

        public static void Initialize()
        {
            Competition = new Competition();
            AddParticipants();
            AddTracks();
        }

        public static void AddParticipants()
        {
            Car racecar = new Car(5, 2, 12, false);

            Driver leroy = new Driver("Leroy", 1, racecar, TeamColors.Red);
            Driver pietertje = new Driver("Pietertje", 2, racecar, TeamColors.Green);

            Competition.Participants.Add(leroy);
            Competition.Participants.Add(pietertje);
        }

        public static void AddTracks()
        {
            SectionTypes[] sections = {SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Finish};

            Track steenwijk = new Track("Rondje Steenwiek", sections);
            Track zwolle = new Track("Zwollywood", sections);

            Competition.Tracks.Enqueue(steenwijk);
            Competition.Tracks.Enqueue(zwolle);
        }

        public static void NextRace()
        {
            Track nextTrack = Competition.NextTrack();

            if (nextTrack != null)
            {
                CurrentRace = new Race(nextTrack, Competition.Participants);
            }
        }
    }
}
