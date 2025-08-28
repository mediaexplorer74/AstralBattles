using AstralBattles.ViewModels;
using Windows.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;


namespace AstralBattles.Views
{
public partial class CreatePlayerView : Page
  {
    
    public CreatePlayerView() => this.InitializeComponent();

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      // TODO: Replace with UWP navigation parameter handling
      bool enableDeckEditor = false; // Default value for MVP build
      this.editDeckControl.Visibility = enableDeckEditor ? Visibility.Visible : Visibility.Collapsed;
      ((CreatePlayerViewModel) ((FrameworkElement) this).DataContext).OnNavigatedTo(e, null, null, enableDeckEditor);
      base.OnNavigatedTo(e);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
      ((CreatePlayerViewModel) ((FrameworkElement) this).DataContext).OnNavigatedFrom(e, null, null);
      base.OnNavigatedFrom(e);
    }

    // Removed OnBackKeyPress - not available in UWP
  }
}

