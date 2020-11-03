using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Model;

namespace Controller
{
    public delegate void OnDriversChanged(object sender, DriversChangedEventArgs args);

    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random;
        private Dictionary<Section, SectionData> _positions;
        private Timer timer;
        //events
        public event OnDriversChanged DriversChanged;

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            _positions = new Dictionary<Section, SectionData>();
            timer = new Timer(500);
            timer.Elapsed += OnTimedEvent;
            Start();
            RandomizeEquipment();
            SetStartPosition();
        }

        public void Start()
        {
            timer.Start();
        }

        public void RandomizeEquipment()
        {
            foreach (IParticipant participant in Participants)
            {
                participant.IEquipment.Quality = _random.Next(1, 10);
                participant.IEquipment.Performance = _random.Next(1, 10);
            }
        }

        public void SetStartPosition()
        {
            foreach(Section section in Track.Sections)
            {
                SectionData sectiondata = GetSectionData(section);
                if(section.SectionType == SectionTypes.StartGrid)
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

        private void OnTimedEvent(Object sender, ElapsedEventArgs e)
        {
            MoveParticipantsOnTrack();
        }
        /*
         * elke section is 100 meter
         * 
         */
        public void MoveParticipantsOnTrack()
        {
            //Section lastSection = _positions.First().Key;

            //foreach (KeyValuePair<Section, SectionData> valuePair in _positions)
            //{
                //SectionData sd = valuePair.Value;
                
                //if(sd.Left != null)
                //{
                    //IParticipant currentParticipant = valuePair.Value.Left;
                    //check if left is broken
                    //if (!sd.Left.IEquipment.IsBroken)
                    //{
                        //sectionData.Left.IEquipment.Performance * sectionData.Left.IEquipment.Speed
                        //sd.DistanceLeft += 40;
                        //if(sd.DistanceLeft >= 100)
                        //{
                            //SetLeftOnNext(lastSection, sectionData);
                        //}
                    //}
                //}
            //}
            DriversChanged?.Invoke(this, new DriversChangedEventArgs() { Track = this.Track });
        }

        public void SetLeftOnNext(Section lastSection, SectionData sectionData)
        {
            GetSectionData(lastSection).Left = null;
            GetSectionData(lastSection).DistanceLeft = sectionData.DistanceLeft;
            sectionData.DistanceLeft = sectionData.DistanceLeft;
            sectionData.Left = null;
        }
    }
}