using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public delegate void DriversChanged(DriversChangedEventArgs DriversChangedEventArgs);

    public class DriversChangedEventArgs : EventArgs
    {
        public Track Track { get; set; }
    }
}
