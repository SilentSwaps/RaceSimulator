using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public interface IEquipment
    {
        //each section  = 100 meter
        //
        public int Quality { get; set; }
        //
        public int Performance { get; set; }
        //speed in meters per second
        public int Speed { get; set; }
        //car broken or not
        public bool IsBroken { get; set; }
    }
}
