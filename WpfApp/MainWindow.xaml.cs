using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Controller;
using Model;

namespace WpfApp
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      // Begin
      // DataContext = new DataContext();

      Data.Initialize();
      Data.NextRace();
      InitializeComponent();
      //
      SetEvents();
    }

    private void SetEvents()
    {
      Data.CurrentRace.DriversChanged += CurrentRaceOnDriversChanged;
      Data.CurrentRace.RaceFinished += CurrentRaceOnRaceFinished;

      Dispatcher.Invoke(() =>
      {
        Data.CurrentRace.DriversChanged += ((DataContext)DataContext).CurrentRaceOnDriversChanged;
      });
    }

    private void CurrentRaceOnRaceFinished(object sender, RaceFinishedEventArgs e)
    {
      ClearEvents();
      Images.Clear();
      Data.NextRace();

      if (Data.CurrentRace != null)
      {
        SetEvents();
        ShowTrack(Data.CurrentRace.Track);
      }
    }

    private void CurrentRaceOnDriversChanged(object sender, DriversChangedEventArgs e)
    {
      // driver changed
      ShowTrack(e.Track);
    }

    private void ShowTrack(Track track)
    {
      Track.Dispatcher.BeginInvoke(
        DispatcherPriority.Render,
        new Action(() =>
        {
          Track.Source = null;
          Track.Source = Visual.DrawTrack(track);
        })
      );
    }

    private void ClearEvents()
    {
      Data.CurrentRace.DriversChanged -= CurrentRaceOnDriversChanged;
      Data.CurrentRace.RaceFinished -= CurrentRaceOnRaceFinished;
    }

    private void Race_Statistics_OnClick(object sender, RoutedEventArgs e)
    {
      var window = new RaceStatsWindow();
      window.Show();
    }

    private void Competition_Statistics_OnClick(object sender, RoutedEventArgs e)
    {
      var window = new CompetitionStatsWindow();
      window.Show();
    }

    private void Close_OnClick(object sender, RoutedEventArgs e)
    {
      Application.Current.Shutdown();
    }
  }
}
