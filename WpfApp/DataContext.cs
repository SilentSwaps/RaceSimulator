using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Controller;
using Model;

namespace WpfApp
{
    public class DataContext: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Moest get set zijn ipv gelijk een init
        public List<Driver> Drivers
        {
            get;
            set;
        }
        
        private Dictionary<IParticipant, int> _laps;

        public Dictionary<IParticipant, int> Laps
        {
            get => _laps;
            set
            {
                if (_laps != value)
                {
                    _laps = value;
                    OnPropertyChanged(nameof(Laps));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public string currentRace = Data.CurrentRace.Track.Name;    

        public DataContext()
        {
            if (Data.CurrentRace != null)
            {
                Data.CurrentRace.DriversChanged += CurrentRaceOnDriversChanged;
            }
            Drivers = Data.Competition.Participants.Select(p => (Driver)p).ToList();
            _laps = Data.CurrentRace._laps;
        }

        public void CurrentRaceOnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            Laps = new Dictionary<IParticipant, int>(Data.CurrentRace._laps);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}