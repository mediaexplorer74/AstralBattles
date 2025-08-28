using AstralBattles.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;



namespace AstralBattles.Views
{
public partial class CampaignOptionsView : Page
  {
    
    public CampaignOptionsView() => this.InitializeComponent();

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      // TODO: Replace with UWP navigation parameter handling
      // Frame.ClearBackStack() and QueryString not available in UWP
      // For MVP build, skipping back stack clearing and QueryString usage
      ((CampaignOptionsViewModel) ((FrameworkElement) this).DataContext).OnNavigatedTo(e.NavigationMode, null);
      base.OnNavigatedTo(e);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
      ((CampaignOptionsViewModel) ((FrameworkElement) this).DataContext).OnNavigatedFrom();
      base.OnNavigatedFrom(e);
    }
   
  }
}

