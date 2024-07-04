using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class DriversChangedEventArgs : EventArgs
    {
    public Track Track { get; set; }
    public List<IParticipant> Participants { get; set; }

    public DriversChangedEventArgs(Track track, List<IParticipant> participants)
    {
      Track = track;
      Participants = participants;
    }
  }
}
