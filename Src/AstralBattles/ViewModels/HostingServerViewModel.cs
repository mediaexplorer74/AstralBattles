// Decompiled with JetBrains decompiler
// Type: AstralBattles.ViewModels.HostingServerViewModel
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Infrastructure.Communications;
using AstralBattles.Core.Model;
using System;

#nullable disable
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
      if (this.IsInDesignMode)
      {
        this.FirstPlayer = new CreatePlayerInfo()
        {
          Element = ElementTypeEnum.Illusion,
          Face = "face34",
          Name = "Egorka"
        };
        this.EnableServer = true;
      }
      else
      {
        ServerController.Instance.OnOpponentJoin += new EventHandler<PlayerCreateInfoEventArgs>(this.OnOpponentJoin);
        ServerController.Instance.OnOpponentChangedInfo += new EventHandler<PlayerCreateInfoEventArgs>(this.OnOpponentChangedInfo);
        ServerController.Instance.OnOpponentDisconnect += new EventHandler(this.OnOpponentDisconnect);
        this.EnableServer = false;
      }
    }

    private void OnOpponentDisconnect(object sender, EventArgs e)
    {
      this.SecondPlayer = (CreatePlayerInfo) null;
    }

    private void OnOpponentChangedInfo(object sender, PlayerCreateInfoEventArgs e)
    {
      this.SecondPlayer = e.Player.Clone();
    }

    private void OnOpponentJoin(object sender, PlayerCreateInfoEventArgs e)
    {
      this.SecondPlayer = e.Player.Clone();
    }

    public bool EnableServer
    {
      get => this.enableServer;
      set
      {
        if (this.enableServer == value)
          return;
        this.enableServer = value;
        this.RaisePropertyChanged(nameof (EnableServer));
        if (this.enableServer)
          this.IsWaitingForOpponent = true;
        ServerController.Instance.IsEnabled = value;
      }
    }

    public bool IsWaitingForOpponent
    {
      get => this.isWaitingForOpponent;
      set
      {
        this.isWaitingForOpponent = value;
        this.RaisePropertyChanged(nameof (IsWaitingForOpponent));
      }
    }

    public string Status
    {
      get => this.status;
      set
      {
        this.status = value;
        this.RaisePropertyChanged(nameof (Status));
      }
    }

    public CreatePlayerInfo FirstPlayer
    {
      get => this.firstPlayer;
      set
      {
        this.firstPlayer = value;
        this.RaisePropertyChanged(nameof (FirstPlayer));
      }
    }

    public CreatePlayerInfo SecondPlayer
    {
      get => this.secondPlayer;
      set
      {
        this.secondPlayer = value;
        this.RaisePropertyChanged(nameof (SecondPlayer));
        this.IsWaitingForOpponent = value == null;
      }
    }

    private void StartListening() => ServerController.Instance.IsEnabled = true;
  }
}
