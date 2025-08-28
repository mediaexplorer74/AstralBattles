// Decompiled with JetBrains decompiler
// Type: AstralBattles.Views.CampaignOptions
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace AstralBattles.Views
{
  public partial class CampaignOptions : Page
  {
  
    public CampaignOptions() => this.InitializeComponent();

    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      if (e.Parameter != null && e.Parameter.ToString().EndsWith("?back=true"))
        Frame.BackStack.Clear();
      ((TournamentOptionsViewModel) ((FrameworkElement) this).DataContext).OnNavigatedTo(e.NavigationMode, null);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
      ((TournamentOptionsViewModel) ((FrameworkElement) this).DataContext).OnNavigatedFrom();
    }
  }
}

