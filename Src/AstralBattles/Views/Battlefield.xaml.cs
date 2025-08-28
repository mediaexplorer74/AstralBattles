using AstralBattles.Controls;
using AstralBattles.Core.Infrastructure;
using AstralBattles.ViewModels;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;



namespace AstralBattles.Views
{
  public partial class Battlefield : Page
  {
    
    public Battlefield() => this.InitializeComponent();

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
      if (((FrameworkElement) this).DataContext is BattlefieldViewModel)
        return;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      // TODO: Replace with UWP navigation parameter handling
      // For MVP build, using default values instead of QueryString
      // if (queryParams.ContainsKey("isCampaign"))
      //   flag4 = true;
      // if (queryParams.ContainsKey("isTwoPlayersDuel"))
      //   flag2 = true;
      // if (queryParams.ContainsKey("isAiDuel"))
      //   flag3 = true;
      bool flag5 = flag2 && !flag3;
      object obj;
      // For MVP: using default new game mode
      // if (!continueGame)
      {
        obj = !flag4 ? (!flag5 ? (!flag3 ? (object) new TournamentBattlefieldViewModel(true) : (object) new QuickDuelWithAiBattlefieldViewModel(true)) : (object) new TwoPlayersDuelBattlefieldViewModel(true)) : (object) new CampaignBattlefieldViewModel(true);
      }
      /*
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
      */
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
        await Task.Delay(1000); // Replace Thread.Sleep with async delay for UWP
      base.OnNavigatedTo(e);
    }

    // Removed OnBackKeyPress - not available in UWP

    private void PageLoaded(object sender, RoutedEventArgs e)
    {
      // UWP page loaded event - replaces PhoneApplicationPageLoaded
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

