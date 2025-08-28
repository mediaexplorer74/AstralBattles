using AstralBattles.Views;
using System;
using AstralBattles.Core.Infrastructure;
using System.Windows.Input;


namespace AstralBattles.ViewModels
{
  public class TwoPlayersModesViewModel : ViewModelBaseEx
  {
    public ICommand OnOneDevice { get; private set; }

    public ICommand ViaNetwork { get; private set; }

    public TwoPlayersModesViewModel()
    {
      OnOneDevice = (ICommand) new RelayCommand(OnOneDeviceAction);
      ViaNetwork = (ICommand) new RelayCommand(ViaNetworkAction);
    }

    private void ViaNetworkAction() => PageNavigationService.OpenViaNetworkGameModes();

    private void OnOneDeviceAction() => PageNavigationService.TwoPlayersOptions();
  }
}
