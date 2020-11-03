using System;
using System.Threading;
using Controller;
using Model;

namespace RaceTrack
{
    class Program
    {
        public static void Main(string[] args)
        {
            Data.Initialize();
            Data.NextRace();
            //adding driverschanged event to the currentrace
            Data.CurrentRace.DriversChanged += Visual.OnDriversChanged;
            Visual.Initialize();
            Visual.DrawTrack(Data.CurrentRace.Track);
            
            Console.WriteLine($"Current track: {Data.CurrentRace.Track.Name}");

            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}
