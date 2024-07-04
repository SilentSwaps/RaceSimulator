using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Timers;

namespace Controller
{
  public delegate void OnDriversChanged(object sender, DriversChangedEventArgs e);

  public delegate void OnRaceFinished(object sender, RaceFinishedEventArgs e);

  public delegate void OnStartNextRace(object sender, EventArgs e);

  public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random;
        private Dictionary<Section, SectionData> _positions;
        public Dictionary<IParticipant, int> _laps;
        private Timer timer;
        const int maxLaps = 3;
        //events
        public event OnDriversChanged DriversChanged:

        public event OnRaceFinished RaceFinished;

        public event OnStartNextRace StartNextRace;

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            _positions = new Dictionary<Section, SectionData>();
            _laps = new Dictionary<IParticipant, int>();
            timer = new Timer(250);
            timer.Elapsed += OnTimedEvent;

            Start();
            RandomizeEquipment();
            SetStartPosition();
            InitLaps();
        }
        //start timer
        public void Start()
        {
            timer.Start();
        }
        //random equipment
        public void RandomizeEquipment()
        {
            foreach (IParticipant participant in Participants)
            {
                if (participant.IEquipment.IsBroken)
                {
                    if (_random.Next(100) == 50)
                    {
                        participant.IEquipment.IsBroken = false;

                      if (participant.IEquipment.Performance > 3)
                      {
                          participant.IEquipment.Performance--;
                      }
                    }
                }
                else if (_random.Next(2000) == 1000)
                {
                    participant.IEquipment.IsBroken = true;
                }
            }
        }

        public void SetStartPosition()
        {
            foreach (Section section in Track.Sections)
            {
                SectionData sectiondata = GetSectionData(section);
                if (section.SectionType == SectionTypes.StartGrid)
                {
                    sectiondata.Left = Participants[0];
                    sectiondata.DistanceLeft = 0;
                    sectiondata.Right = Participants[1];
                    sectiondata.DistanceRight = 0;
                }
            }
        }

        public SectionData GetSectionData(Section section)
        {
            if (!_positions.ContainsKey(section))
            {
                _positions.Add(section, new SectionData());
            }
            return _positions[section];
        }
        
        public bool HasSectionData(Section section)
        {
            return _positions.ContainsKey(section);
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            MoveParticipantsOnTrack();
            timer.Start();
        }
        /*
         * elke section is 100 meter
         */
        public void MoveParticipantsOnTrack()
        {
            foreach (Section section in Track.Sections)
            {
                SectionData sd = GetSectionData(section);
                foreach (IParticipant participant in Participants)
                {
                  RandomizeEquipment();
                    if (sd.Left != null)
                    {
                        if (sd.Left.Equals(participant))
                        {
                            //check if left is broken
                            if (!participant.IEquipment.IsBroken)
                            {
                                int speed = 40;
                                sd.DistanceLeft += speed;

                                if (sd.DistanceLeft >= 100)
                                {
                                    SetLeftOnNext(section, sd);
                                }
                            }
                        }
                    }
                    if (sd.Right != null)
                    {
                        if (sd.Right.Equals(participant))
                        {
                            if (!participant.IEquipment.IsBroken)
                            {
                                int speed = 35;
                                sd.DistanceRight += speed;

                                if (sd.DistanceRight >= 100)
                                {
                                    SetRightOnNext(section, sd);
                                }
                            }
                        }
                    }
                }//end foreach
            }//end foreach
            DriversChanged?.Invoke(this, new DriversChangedEventArgs(Track, Participants));
        }

        public void SetLeftOnNext(Section section, SectionData sectionData)
        {
            var next = Track.Sections.Find(section).Next;
            Section nextSection = next != null ? next.Value : Track.Sections.First.Value;
            SectionData nextSectionData = GetSectionData(nextSection);

            //if next section is finish
            if (nextSection.SectionType == SectionTypes.Finish)
            {
                //if laps driven is max laps remove driver
                if (_laps[sectionData.Left] == maxLaps)
                {
                    sectionData.Left = null;
                    nextSectionData.Left = sectionData.Left;
                    if (IsFinished())
                    {
                        RaceFinished?.Invoke(this, new RaceFinishedEventArgs());
                        CleanUpDelegates();
                    }
                }
                else
                {
                    _laps[sectionData.Left]++;
                    nextSectionData.Left = sectionData.Left;
                    nextSectionData.DistanceLeft = sectionData.DistanceLeft - 100;
                    sectionData.Left = null;
                    sectionData.DistanceLeft = 0;

                }
            }
            else
            {
                nextSectionData.Left = sectionData.Left;
                nextSectionData.DistanceLeft = sectionData.DistanceLeft - 100;
                sectionData.Left = null;
                sectionData.DistanceLeft = 0;
            }
        }

        public void SetRightOnNext(Section section, SectionData sectionData)
        {
            var next = Track.Sections.Find(section).Next;
            Section nextSection = next != null ? next.Value : Track.Sections.First.Value;
            SectionData nextSectionData = GetSectionData(nextSection);

            //if next section is finish
            if (nextSection.SectionType == SectionTypes.Finish)
            {
                //if laps driven is max laps remove driver
                if (_laps[sectionData.Right] == maxLaps)
                {
                    sectionData.Right = null;
                    nextSectionData.Right = sectionData.Right;
                    if (IsFinished())
                    {
                        RaceFinished?.Invoke(this, new RaceFinishedEventArgs());
                        CleanUpDelegates();
                    }
                }
                else
                {
                    _laps[sectionData.Right]++;
                    nextSectionData.Right = sectionData.Right;
                    nextSectionData.DistanceRight = sectionData.DistanceRight - 100;
                    sectionData.Right = null;
                    sectionData.DistanceLeft = 0;

                }
            }
            else
            {
                nextSectionData.Right = sectionData.Right;
                nextSectionData.DistanceRight = sectionData.DistanceRight - 100;
                sectionData.Right = null;
                sectionData.DistanceRight = 0;
            }
        }

        public void InitLaps()
        {
            foreach(IParticipant participant in Participants)
            {
                _laps.Add(participant, 0);
            }
        }

        public bool IsFinished()
        {
            int count = 0;
            foreach(Section section in Track.Sections)
            {
                SectionData sd = GetSectionData(section);
                if(sd.Left == null && sd.Right == null)
                {
                    count++;
                }
            }
            
            if((count + 1 ) == Track.Sections.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CleanUpDelegates()
        {
            timer.Stop();
            timer.Elapsed -= OnTimedEvent;

            
            Data.CurrentRace.DriversChanged -= DriversChanged;
            StartNextRace?.Invoke(this, EventArgs.Empty);
        }
    }
}
