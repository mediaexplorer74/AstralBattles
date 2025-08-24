// Decompiled with JetBrains decompiler
// Type: AstralBattles.Views.Battlefield
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Controls;
using AstralBattles.Core.Infrastructure;
using AstralBattles.ViewModels;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using System.Windows.Navigation;

#nullable disable

namespace AstralBattles.Views
{
  public partial class Battlefield : Page
  {
    
    public Battlefield() => this.InitializeComponent();

    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      if (((FrameworkElement) this).DataContext is BattlefieldViewModel)
        return;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      if (((Page) this).NavigationContext.QueryString.GetBoolValue("isCampaign"))
        flag4 = true;
      if (((Page) this).NavigationContext.QueryString.GetBoolValue("isTwoPlayersDuel"))
        flag2 = true;
      if (((Page) this).NavigationContext.QueryString.GetBoolValue("isAiDuel"))
        flag3 = true;
      bool flag5 = flag2 && !flag3;
      object obj;
      if (!((Page) this).NavigationContext.QueryString.GetBoolValue("continueGame"))
      {
        obj = !flag4 ? (!flag5 ? (!flag3 ? (object) new TournamentBattlefieldViewModel(true) : (object) new QuickDuelWithAiBattlefieldViewModel(true)) : (object) new TwoPlayersDuelBattlefieldViewModel(true)) : (object) new CampaignBattlefieldViewModel(true);
      }
      else
      {
        try
        {
          flag1 = true;
          BattlefieldViewModel battlefieldViewModel = !flag4 ? (!flag5 ? (!flag3 ? (BattlefieldViewModel) Serializer.Read<TournamentBattlefieldViewModel>("CurrentTournamentGame__1_452.xml") : (BattlefieldViewModel) Serializer.Read<QuickDuelWithAiBattlefieldViewModel>("DuelWithAiBattlefieldViewModel__1_452.xml")) : (BattlefieldViewModel) Serializer.Read<TwoPlayersDuelBattlefieldViewModel>("CurrentTwoPlayerDuelGame__1_452.xml")) : (BattlefieldViewModel) Serializer.Read<CampaignBattlefieldViewModel>("CampaignBattlefieldViewModel__1_452.xml");
          battlefieldViewModel.OnDeserialized();
          obj = (object) battlefieldViewModel;
        }
        catch (Exception ex)
        {
          obj = !flag4 ? (!flag5 ? (!flag3 ? (object) new TournamentBattlefieldViewModel(true) : (object) new QuickDuelWithAiBattlefieldViewModel(true)) : (object) new TwoPlayersDuelBattlefieldViewModel(true)) : (object) new CampaignBattlefieldViewModel(true);
        }
      }
      if (flag5)
      {
        this.summoningDialog.Visibility = Visibility.Collapsed;
      }
      else
      {
        this.secondBook.Visibility = Visibility.Collapsed;
        this.nextTurnDialog.Visibility = Visibility.Collapsed;
      }
      ((FrameworkElement) this).DataContext = obj;
      if (flag1)
        Thread.Sleep(1000);
      ((Page) this).OnNavigatedTo(e);
    }

    protected virtual void OnBackKeyPress(CancelEventArgs e)
    {
      BattlefieldViewModel dataContext = (BattlefieldViewModel) ((FrameworkElement) this).DataContext;
      if (dataContext.ShowLogMode)
      {
        dataContext.ShowLogMode = false;
        e.Cancel = true;
      }
      base.OnBackKeyPress(e);
    }

    private void PhoneApplicationPageLoaded(object sender, RoutedEventArgs e)
    {
    }

    private void SummoningDialogPanelHiding(object sender, EventArgs e)
    {
      ((BattlefieldViewModel) ((FrameworkElement) this).DataContext).OnClosingSummoningDialog();
    }

    private void NextTurnDialogPanelHiding(object sender, EventArgs e)
    {
      if (!(((FrameworkElement) this).DataContext is TwoPlayersDuelBattlefieldViewModel dataContext))
        return;
      dataContext.OnClosingNextTurnDialog();
    }

    private void SecondPlayerFieldSelecting(object sender, EventArgs e)
    {
      ((BattlefieldViewModel) ((FrameworkElement) this).DataContext).SecondPlayerFieldSelect.Execute((object) null);
    }

    private void FirstPlayerFieldSelecting(object sender, EventArgs e)
    {
      ((BattlefieldViewModel) ((FrameworkElement) this).DataContext).FirstPlayerFieldSelect.Execute((object) null);
    }

   
  }
}

