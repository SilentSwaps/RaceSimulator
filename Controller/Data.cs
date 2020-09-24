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
            NextRace();
        }

        public static void AddParticipants()
        {
            Car racecar = new Car(100, 100, 250, false);

            Driver leroy = new Driver("Leroy", 1, racecar, TeamColors.Red);
            Driver pietertje = new Driver("Pietertje", 2, racecar, TeamColors.Green);
            Driver ernst = new Driver("ernst", 3, racecar, TeamColors.Yellow);
            Driver dwayne = new Driver("Dwayne, the rock", 4, racecar, TeamColors.Blue);

            Competition.Participants.Add(leroy);
            Competition.Participants.Add(pietertje);
            Competition.Participants.Add(ernst);
            Competition.Participants.Add(dwayne);
        }

        public static void AddTracks()
        {
            LinkedList<Section> section = new LinkedList<Section>();

            Track steenwijk = new Track("Rondje Steenwiek", section);
            Track zwolle = new Track("Zwollywood", section);

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
