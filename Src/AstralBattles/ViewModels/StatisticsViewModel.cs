using AstralBattles.Core.Infrastructure;
using AstralBattles.Core.Model;
using AstralBattles.Core.Services;
using AstralBattles.Localizations;
using AstralBattles.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using System.Windows.Input;


namespace AstralBattles.ViewModels
{
  public class StatisticsViewModel : ViewModelBaseEx
  {
    private string roundName;
    private ObservableCollection<StatisticsMember> total;
    private ObservableCollection<AstralBattles.Core.Infrastructure.Tuple<string, string>> roundResult;
    private ObservableCollection<AstralBattles.Core.Infrastructure.Tuple<string, string>> nextRound;

    public StatisticsViewModel()
    {
      if (App.IsInDesignMode)
      {
        List<string> names = new PlayersRegistry().GetNames();
        ObservableCollection<StatisticsMember> observableCollection1 = new ObservableCollection<StatisticsMember>();
        observableCollection1.Add(new StatisticsMember()
        {
          Name = names[0],
          Wins = 4,
          Points = 123
        });
        observableCollection1.Add(new StatisticsMember()
        {
          Name = names[1],
          Wins = 3,
          Points = 134
        });
        observableCollection1.Add(new StatisticsMember()
        {
          Name = names[2],
          Wins = 3,
          Points = 12
        });
        observableCollection1.Add(new StatisticsMember()
        {
          Name = names[3],
          Wins = 4,
          Points = 44,
          IsPlayer = true
        });
        observableCollection1.Add(new StatisticsMember()
        {
          Name = names[4],
          Wins = 2,
          Points = 123
        });
        observableCollection1.Add(new StatisticsMember()
        {
          Name = names[5],
          Wins = 1,
          Points = 134
        });
        observableCollection1.Add(new StatisticsMember()
        {
          Name = names[6],
          Wins = 7,
          Points = 12
        });
        observableCollection1.Add(new StatisticsMember()
        {
          Name = names[7],
          Wins = 5,
          Points = 134
        });
        observableCollection1.Add(new StatisticsMember()
        {
          Name = names[8],
          Wins = 4,
          Points = 25
        });
        observableCollection1.Add(new StatisticsMember()
        {
          Name = names[8],
          Wins = 3,
          Points = 99
        });
        Total = observableCollection1;
        ObservableCollection<AstralBattles.Core.Infrastructure.Tuple<string, string>> observableCollection2 = new ObservableCollection<AstralBattles.Core.Infrastructure.Tuple<string, string>>();
        observableCollection2.Add(new AstralBattles.Core.Infrastructure.Tuple<string, string>(names[0], names[1]));
        observableCollection2.Add(new AstralBattles.Core.Infrastructure.Tuple<string, string>(names[6], names[2]));
        observableCollection2.Add(new AstralBattles.Core.Infrastructure.Tuple<string, string>(names[7], names[3]));
        observableCollection2.Add(new AstralBattles.Core.Infrastructure.Tuple<string, string>(names[8], names[4]));
        observableCollection2.Add(new AstralBattles.Core.Infrastructure.Tuple<string, string>(names[8], names[5]));
        RoundResult = NextRound = observableCollection2;
      }
      else
      {
        RoundName = CommonResources.Game + " " + (object) TournamentService.Instance.Tournament.CurrentRoundIndex;
        StartNewRound = (ICommand) new RelayCommand(StartNewRoundAction, new Func<bool>(StartNewRoundCanExecute));
        Total = new ObservableCollection<StatisticsMember>(TournamentService.Instance.Tournament.Stat.OrderByDescending<PlayerPoint, int>((Func<PlayerPoint, int>) (i => i.Wins)).ThenByDescending<PlayerPoint, int>((Func<PlayerPoint, int>) (i => i.Points)).Select<PlayerPoint, StatisticsMember>((Func<PlayerPoint, StatisticsMember>) (i => new StatisticsMember()
        {
          Name = i.Name,
          Points = i.Points,
          IsPlayer = i.IsPlayer,
          Wins = i.Wins
        })));
        if (TournamentService.Instance.Tournament.CurrentRoundIndex == 0)
          NextRound = new ObservableCollection<AstralBattles.Core.Infrastructure.Tuple<string, string>>();
        else
          RoundResult = new ObservableCollection<AstralBattles.Core.Infrastructure.Tuple<string, string>>(TournamentService.Instance.Tournament.PreviousRound);
        if (TournamentService.Instance.Tournament.CurrentRoundIndex == 9)
          NextRound = new ObservableCollection<AstralBattles.Core.Infrastructure.Tuple<string, string>>();
        else
          NextRound = new ObservableCollection<AstralBattles.Core.Infrastructure.Tuple<string, string>>(TournamentService.Instance.Tournament.CurrentRound);
      }
    }

    private bool StartNewRoundCanExecute() => true;

    public void OnNavigatedTo() => IsBusy = false;

    private void StartNewRoundAction()
    {
      if (TournamentService.Instance.Tournament.CurrentRoundIndex > 8)
      {
        int num1 = TournamentService.Instance.Tournament.Stat.OrderByDescending<PlayerPoint, int>((Func<PlayerPoint, int>) (i => i.Wins)).ThenByDescending<PlayerPoint, int>((Func<PlayerPoint, int>) (i => i.Points)).ToList<PlayerPoint>().IndexOf(TournamentService.Instance.Tournament.Stat.First<PlayerPoint>((Func<PlayerPoint, bool>) (i => i.Name == TournamentService.Instance.Tournament.CurrentPlayer.Name))) + 1;
        string str = num1.ToString() + "th";
        switch (num1)
        {
          case 1:
            str = num1.ToString() + "st";
            break;
          case 2:
            str = num1.ToString() + "nd";
            break;
          case 3:
            str = num1.ToString() + "rd";
            break;
        }
        Serializer.Delete("CurrentTournamentGame__1_452.xml");
        // Replace MessageBox with UWP ContentDialog for MVP
        // int num2 = (int) MessageBox.Show(string.Format(CommonResources.Congratulations, (object) str));
        PageNavigationService.OpenMainMenu();
      }
      else
      {
        IsBusy = true;
        PageNavigationService.OpenBattlefield(false);
      }
    }

    public string RoundName
    {
      get => roundName;
      set
      {
        roundName = value;
        RaisePropertyChanged(nameof (RoundName));
      }
    }

    public ObservableCollection<StatisticsMember> Total
    {
      get => total;
      set
      {
        total = value;
        RaisePropertyChanged(nameof (Total));
      }
    }

    public ObservableCollection<AstralBattles.Core.Infrastructure.Tuple<string, string>> RoundResult
    {
      get => roundResult;
      set
      {
        roundResult = value;
        RaisePropertyChanged(nameof (RoundResult));
      }
    }

    public ObservableCollection<AstralBattles.Core.Infrastructure.Tuple<string, string>> NextRound
    {
      get => nextRound;
      set
      {
        nextRound = value;
        RaisePropertyChanged(nameof (NextRound));
      }
    }

    public ICommand StartNewRound { get; set; }
  }
}
