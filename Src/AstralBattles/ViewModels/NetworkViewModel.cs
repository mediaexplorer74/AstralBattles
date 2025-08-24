// Decompiled with JetBrains decompiler
// Type: AstralBattles.ViewModels.NetworkViewModel
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.ServiceReference;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;

#nullable disable
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
      if (this.IsInDesignMode)
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
        this.Top25 = observableCollection1;
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
        this.Games = observableCollection2;
        this.CurrentPlayer = new PlayerInfo()
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
        this.RefreshData();
    }

    public RelayCommand Refresh { get; set; }

    public RelayCommand CreateTicket { get; set; }

    private void RefreshData()
    {
      this.loadsCount = 0;
      GameServiceClient gameServiceClient = new GameServiceClient((Binding) new BasicHttpBinding(), new EndpointAddress("http://192.168.1.2/AstralBattles.Server2/GamingService.svc"));
      this.IsBusy = true;
      gameServiceClient.GetAvailableGamesCompleted += new EventHandler<GetAvailableGamesCompletedEventArgs>(this.ServiceGetAvailableGamesCompleted);
      gameServiceClient.GetTop25PlayersCompleted += new EventHandler<GetTop25PlayersCompletedEventArgs>(this.ServiceGetTop25PlayersCompleted);
      gameServiceClient.GetAvailableGamesAsync();
      gameServiceClient.GetTop25PlayersAsync();
    }

    private void ServiceGetAvailableGamesCompleted(
      object sender,
      GetAvailableGamesCompletedEventArgs e)
    {
      ++this.loadsCount;
      this.IsBusy = this.loadsCount < 2;
      int place = 1;
      e.Result.ForEach<GameInfo>((Action<GameInfo>) (i => i.Place = place++));
      this.Games = e.Result;
    }

    private void ServiceGetTop25PlayersCompleted(object sender, GetTop25PlayersCompletedEventArgs e)
    {
      ++this.loadsCount;
      this.IsBusy = this.loadsCount < 2;
      int place = 1;
      ObservableCollection<PlayerInfo> result = e.Result;
      result.ForEach<PlayerInfo>((Action<PlayerInfo>) (i => i.Place = place++));
      this.Top25 = result;
    }

    public ObservableCollection<PlayerInfo> Top25
    {
      get => this.top25;
      set
      {
        this.top25 = value;
        this.RaisePropertyChanged(nameof (Top25));
      }
    }

    public PlayerInfo SelectedPlayer
    {
      get => this.selectedPlayer;
      set
      {
        this.selectedPlayer = value;
        this.RaisePropertyChanged(nameof (SelectedPlayer));
      }
    }

    public PlayerInfo CurrentPlayer
    {
      get => this.currentPlayer;
      set
      {
        this.currentPlayer = value;
        this.RaisePropertyChanged(nameof (CurrentPlayer));
      }
    }

    public GameInfo SelectedGameItem
    {
      get => this.selectedGameItem;
      set
      {
        this.selectedGameItem = value;
        this.RaisePropertyChanged(nameof (SelectedGameItem));
      }
    }

    public ObservableCollection<GameInfo> Games
    {
      get => this.games;
      set
      {
        this.games = value;
        this.RaisePropertyChanged(nameof (Games));
      }
    }
  }
}
