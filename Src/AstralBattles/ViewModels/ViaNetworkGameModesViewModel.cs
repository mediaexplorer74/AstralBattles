// Decompiled with JetBrains decompiler
// Type: AstralBattles.ViewModels.ViaNetworkGameModesViewModel
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;

#nullable disable
namespace AstralBattles.ViewModels
{
  public class ViaNetworkGameModesViewModel : ViewModelBaseEx
  {
    public ICommand HostAServer { get; private set; }

    public ICommand JoinToServer { get; private set; }

    public ViaNetworkGameModesViewModel()
    {
      this.HostAServer = (ICommand) new RelayCommand(new Action(this.HostAServerAction));
      this.JoinToServer = (ICommand) new RelayCommand(new Action(this.JoinToServerAction));
    }

    private void HostAServerAction() => PageNavigationService.OpenServerHosting();

    private void JoinToServerAction() => PageNavigationService.TwoPlayersOptions();
  }
}
