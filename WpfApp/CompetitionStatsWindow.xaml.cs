using System.Windows;
using Controller;
using Model;

namespace WpfApp
{
    public partial class CompetitionStatsWindow : Window
    {
        public CompetitionStatsWindow()
        {
            InitializeComponent();
            Data.CurrentRace.RaceFinished += RaceFinished;
            InvokeDriversChanged();
        }
        
        private void RaceFinished(object sender, RaceFinishedEventArgs eventArgs)
        {
            if (Data.CurrentRace != null)
            {
                InvokeDriversChanged();
            }
        }

        private void InvokeDriversChanged()
        {
            Dispatcher.Invoke(() =>
            {
                Data.CurrentRace.DriversChanged += ((DataContext)DataContext).CurrentRaceOnDriversChanged;
            });
        }
    }
}