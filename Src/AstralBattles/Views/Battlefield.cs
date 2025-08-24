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
    internal PhoneApplicationPage battlefieldView;
    internal Grid LayoutRoot;
    internal VisualStateGroup VisualStateGroup;
    internal VisualState Default;
    internal Storyboard fakeStoryboard;
    internal Grid ContentPanel;
    internal CreatureControl spField1;
    internal CreatureControl spField2;
    internal CreatureControl spField3;
    internal CreatureControl spField4;
    internal CreatureControl spField5;
    internal CreatureControl fpField1;
    internal CreatureControl fpField2;
    internal CreatureControl fpField3;
    internal CreatureControl fpField4;
    internal CreatureControl fpField5;
    internal PlayerFaceControl secondPlayerFace;
    internal PlayerFaceControl firstPlayerFace;
    internal Grid CardsPanelLayoutRoot;
    internal CardsBook firstBook;
    internal CardsBook secondBook;
    internal OpponentSummoningNotification summoningDialog;
    internal PlayersTurnNotification nextTurnDialog;
    private bool _contentLoaded;

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

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/AstralBattles;component/Views/Battlefield.xaml", UriKind.Relative));
      this.battlefieldView = (PhoneApplicationPage) ((FrameworkElement) this).FindName("battlefieldView");
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.VisualStateGroup = (VisualStateGroup) ((FrameworkElement) this).FindName("VisualStateGroup");
      this.Default = (VisualState) ((FrameworkElement) this).FindName("Default");
      this.fakeStoryboard = (Storyboard) ((FrameworkElement) this).FindName("fakeStoryboard");
      this.ContentPanel = (Grid) ((FrameworkElement) this).FindName("ContentPanel");
      this.spField1 = (CreatureControl) ((FrameworkElement) this).FindName("spField1");
      this.spField2 = (CreatureControl) ((FrameworkElement) this).FindName("spField2");
      this.spField3 = (CreatureControl) ((FrameworkElement) this).FindName("spField3");
      this.spField4 = (CreatureControl) ((FrameworkElement) this).FindName("spField4");
      this.spField5 = (CreatureControl) ((FrameworkElement) this).FindName("spField5");
      this.fpField1 = (CreatureControl) ((FrameworkElement) this).FindName("fpField1");
      this.fpField2 = (CreatureControl) ((FrameworkElement) this).FindName("fpField2");
      this.fpField3 = (CreatureControl) ((FrameworkElement) this).FindName("fpField3");
      this.fpField4 = (CreatureControl) ((FrameworkElement) this).FindName("fpField4");
      this.fpField5 = (CreatureControl) ((FrameworkElement) this).FindName("fpField5");
      this.secondPlayerFace = (PlayerFaceControl) ((FrameworkElement) this).FindName("secondPlayerFace");
      this.firstPlayerFace = (PlayerFaceControl) ((FrameworkElement) this).FindName("firstPlayerFace");
      this.CardsPanelLayoutRoot = (Grid) ((FrameworkElement) this).FindName("CardsPanelLayoutRoot");
      this.firstBook = (CardsBook) ((FrameworkElement) this).FindName("firstBook");
      this.secondBook = (CardsBook) ((FrameworkElement) this).FindName("secondBook");
      this.summoningDialog = (OpponentSummoningNotification) ((FrameworkElement) this).FindName("summoningDialog");
      this.nextTurnDialog = (PlayersTurnNotification) ((FrameworkElement) this).FindName("nextTurnDialog");
    }
  }
}

