using System;
using System.Threading;
using Controller;

namespace RaceTrack
{
    class Program
    {
        public static void Main(string[] args)
        {
            Data.Initialize();

            Console.WriteLine($"Current track: {Data.CurrentRace.Track.Name}");

            for (; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}
