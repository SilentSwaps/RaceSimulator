using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Driver : IParticipant
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment IEquipment { get; set; }
        public TeamColors TeamColor { get; set; }

        public Driver(string name, int points, IEquipment iEquipment, TeamColors teamColor)
        {
            Name = name;
            Points = points;
            IEquipment = iEquipment;
            TeamColor = teamColor;
        }
    }
}
