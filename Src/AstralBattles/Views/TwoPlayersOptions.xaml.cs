using AstralBattles.ViewModels;
using Windows.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;


namespace AstralBattles.Views
{
public partial class TwoPlayersOptions : Page
  {
    
    public TwoPlayersOptions() => this.InitializeComponent();

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      // TODO: Replace with UWP navigation parameter handling
      // Frame.ClearBackStack() not available in UWP
      // For MVP build, skipping back stack clearing
      ((TwoPlayersOptionsViewModel) ((FrameworkElement) this).DataContext).OnNavigatedTo(e.NavigationMode, e.Uri, null);
      base.OnNavigatedTo(e);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
      ((TwoPlayersOptionsViewModel) ((FrameworkElement) this).DataContext).OnNavigatedFrom();
      base.OnNavigatedFrom(e);
    }
  }
}

