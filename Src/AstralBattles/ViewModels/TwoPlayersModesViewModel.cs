// Decompiled with JetBrains decompiler
// Type: AstralBattles.ViewModels.TwoPlayersModesViewModel
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
  public class TwoPlayersModesViewModel : ViewModelBaseEx
  {
    public ICommand OnOneDevice { get; private set; }

    public ICommand ViaNetwork { get; private set; }

    public TwoPlayersModesViewModel()
    {
      this.OnOneDevice = (ICommand) new RelayCommand(new Action(this.OnOneDeviceAction));
      this.ViaNetwork = (ICommand) new RelayCommand(new Action(this.ViaNetworkAction));
    }

    private void ViaNetworkAction() => PageNavigationService.OpenViaNetworkGameModes();

    private void OnOneDeviceAction() => PageNavigationService.TwoPlayersOptions();
  }
}
