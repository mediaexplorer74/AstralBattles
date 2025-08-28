using AstralBattles.Core.Infrastructure.Communications;
using AstralBattles.Core.Model;
using System;
using AstralBattles.Core.Infrastructure;

namespace AstralBattles.ViewModels
{ 
   public class HostingServerViewModel : ViewModelBaseEx
  {
    private bool enableServer;
    private CreatePlayerInfo firstPlayer;
    private bool isWaitingForOpponent;
    private CreatePlayerInfo secondPlayer;
    private string status;

    public HostingServerViewModel()
    {
      if (App.IsInDesignMode)
      {
        FirstPlayer = new CreatePlayerInfo()
        {
          Element = ElementTypeEnum.Illusion,
          Face = "face34",
          Name = "Egorka"
        };
        EnableServer = true;
      }
      else
      {
        ServerController.Instance.OnOpponentJoin += new EventHandler<PlayerCreateInfoEventArgs>(OnOpponentJoin);
        ServerController.Instance.OnOpponentChangedInfo += new EventHandler<PlayerCreateInfoEventArgs>(OnOpponentChangedInfo);
        ServerController.Instance.OnOpponentDisconnect += new EventHandler(OnOpponentDisconnect);
        EnableServer = false;
      }
    }

    private void OnOpponentDisconnect(object sender, EventArgs e)
    {
      SecondPlayer = (CreatePlayerInfo) null;
    }

    private void OnOpponentChangedInfo(object sender, PlayerCreateInfoEventArgs e)
    {
      SecondPlayer = e.Player.Clone();
    }

    private void OnOpponentJoin(object sender, PlayerCreateInfoEventArgs e)
    {
      SecondPlayer = e.Player.Clone();
    }

    public bool EnableServer
    {
      get => enableServer;
      set
      {
        if (enableServer == value)
          return;
        enableServer = value;
        RaisePropertyChanged(nameof (EnableServer));
        if (enableServer)
          IsWaitingForOpponent = true;
        ServerController.Instance.IsEnabled = value;
      }
    }

    public bool IsWaitingForOpponent
    {
      get => isWaitingForOpponent;
      set
      {
        isWaitingForOpponent = value;
        RaisePropertyChanged(nameof (IsWaitingForOpponent));
      }
    }

    public string Status
    {
      get => status;
      set
      {
        status = value;
        RaisePropertyChanged(nameof (Status));
      }
    }

    public CreatePlayerInfo FirstPlayer
    {
      get => firstPlayer;
      set
      {
        firstPlayer = value;
        RaisePropertyChanged(nameof (FirstPlayer));
      }
    }

    public CreatePlayerInfo SecondPlayer
    {
      get => secondPlayer;
      set
      {
        secondPlayer = value;
        RaisePropertyChanged(nameof (SecondPlayer));
        IsWaitingForOpponent = value == null;
      }
    }

    private void StartListening() => ServerController.Instance.IsEnabled = true;
  }
}
