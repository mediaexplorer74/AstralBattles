
using AstralBattles.ServiceReference;
using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using AstralBattles.Core.Infrastructure;

namespace AstralBattles.ViewModels
{
  public class NetworkViewModel : ViewModelBaseEx
  {
    private int loadsCount;
    private ObservableCollection<PlayerInfo> top25;
    private PlayerInfo selectedPlayer;
    private PlayerInfo currentPlayer;
    private GameInfo selectedGameItem;
    private ObservableCollection<GameInfo> games;

    public NetworkViewModel()
    {
      Refresh = new RelayCommand(() => RefreshData());
      CreateTicket = new RelayCommand(() => { /* CreateTicket functionality - stub for MVP */ });
      
      if (App.IsInDesignMode)
      {
        ObservableCollection<PlayerInfo> observableCollection1 = new ObservableCollection<PlayerInfo>();
        observableCollection1.Add(new PlayerInfo()
        {
          Games = 10,
          Place = 1,
          Leaves = 3,
          Wins = 4,
          Loses = 6,
          PhotoName = "face43",
          Name = "Nagg",
          Id = 1
        });
        observableCollection1.Add(new PlayerInfo()
        {
          Games = 1,
          Place = 2,
          Leaves = 0,
          Wins = 0,
          Loses = 1,
          PhotoName = "face4",
          Name = "DEg",
          Id = 2
        });
        observableCollection1.Add(new PlayerInfo()
        {
          Games = 2,
          Place = 3,
          Leaves = 1,
          Wins = 0,
          Loses = 2,
          PhotoName = "face3",
          Name = "Un1c0rn",
          Id = 3
        });
        Top25 = observableCollection1;
        ObservableCollection<GameInfo> observableCollection2 = new ObservableCollection<GameInfo>();
        observableCollection2.Add(new GameInfo()
        {
          IsSelected = true,
          Description = "чмоке чмоке! жду wei fjwejf iw nweinf iwe fnwefin wenfoiwe fnowe fiowneio fnweifn oweni fonweo wewifo we io",
          HosterCountry = "Belaruswdwdwd",
          HosterName = "Hellridernagg",
          StartedAt = DateTime.Now
        });
        observableCollection2.Add(new GameInfo()
        {
          Description = "Prepare to DIE!!!11",
          IsSelected = false,
          HosterCountry = "USA",
          HosterName = "Hellrider",
          StartedAt = DateTime.Now
        });
        Games = observableCollection2;
        CurrentPlayer = new PlayerInfo()
        {
          Country = "Russia",
          Games = 30,
          Leaves = 10,
          Loses = 4,
          Name = "Hellrider",
          PhotoName = "face11",
          Wins = 20
        };
      }
      else
        RefreshData();
    }

    public RelayCommand Refresh { get; set; }

    public RelayCommand CreateTicket { get; set; }

    private void RefreshData()
    {
      loadsCount = 0;
      GameServiceClient gameServiceClient = new GameServiceClient((Binding) new BasicHttpBinding(), new EndpointAddress("http://192.168.1.2/AstralBattles.Server2/GamingService.svc"));
      IsBusy = true;
      gameServiceClient.GetAvailableGamesCompleted += new EventHandler<GetAvailableGamesCompletedEventArgs>(ServiceGetAvailableGamesCompleted);
      gameServiceClient.GetTop25PlayersCompleted += new EventHandler<GetTop25PlayersCompletedEventArgs>(ServiceGetTop25PlayersCompleted);
      gameServiceClient.GetAvailableGamesAsync();
      gameServiceClient.GetTop25PlayersAsync();
    }

    private void ServiceGetAvailableGamesCompleted(
      object sender,
      GetAvailableGamesCompletedEventArgs e)
    {
      ++loadsCount;
      IsBusy = loadsCount < 2;
      int place = 1;
      e.Result.ForEach<GameInfo>((Action<GameInfo>) (i => i.Place = place++));
      Games = e.Result;
    }

    private void ServiceGetTop25PlayersCompleted(object sender, GetTop25PlayersCompletedEventArgs e)
    {
      ++loadsCount;
      IsBusy = loadsCount < 2;
      int place = 1;
      ObservableCollection<PlayerInfo> result = e.Result;
      result.ForEach<PlayerInfo>((Action<PlayerInfo>) (i => i.Place = place++));
      Top25 = result;
    }

    public ObservableCollection<PlayerInfo> Top25
    {
      get => top25;
      set
      {
        top25 = value;
        RaisePropertyChanged(nameof (Top25));
      }
    }

    public PlayerInfo SelectedPlayer
    {
      get => selectedPlayer;
      set
      {
        selectedPlayer = value;
        RaisePropertyChanged(nameof (SelectedPlayer));
      }
    }

    public PlayerInfo CurrentPlayer
    {
      get => currentPlayer;
      set
      {
        currentPlayer = value;
        RaisePropertyChanged(nameof (CurrentPlayer));
      }
    }

    public GameInfo SelectedGameItem
    {
      get => selectedGameItem;
      set
      {
        selectedGameItem = value;
        RaisePropertyChanged(nameof (SelectedGameItem));
      }
    }

    public ObservableCollection<GameInfo> Games
    {
      get => games;
      set
      {
        games = value;
        RaisePropertyChanged(nameof (Games));
      }
    }
  }
}
