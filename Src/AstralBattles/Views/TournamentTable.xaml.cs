using AstralBattles.ViewModels;
using Windows.UI.Xaml;
using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;



namespace AstralBattles.Views
{
public partial class TournamentTable : Page
  {
   
    public TournamentTable() => this.InitializeComponent();

    private void AppbarNextClick(object sender, EventArgs e)
    {
      ((StatisticsViewModel) ((FrameworkElement) this).DataContext).StartNewRound.Execute((object) null);
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      ((StatisticsViewModel) ((FrameworkElement) this).DataContext).OnNavigatedTo();
      base.OnNavigatedTo(e);
    }

    
  }
}

