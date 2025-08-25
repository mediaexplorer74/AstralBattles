// Decompiled with JetBrains decompiler
// Type: AstralBattles.Views.TournamentTable
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.ViewModels;
using Windows.UI.Xaml;
using System;
using System.ComponentModel;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable

namespace AstralBattles.Views
{
public partial class TournamentTable : Page
  {
   
    public TournamentTable() => this.InitializeComponent();

    private void AppbarNextClick(object sender, EventArgs e)
    {
      ((StatisticsViewModel) ((FrameworkElement) this).DataContext).StartNewRound.Execute((object) null);
    }

    protected virtual void OnBackKeyPress(CancelEventArgs e)
    {
      PageFrame.OpenMainMenu();
    }

    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      ((StatisticsViewModel) ((FrameworkElement) this).DataContext).OnNavigatedTo();
      ((Page) this).OnNavigatedTo(e);
    }

    
  }
}

