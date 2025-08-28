﻿using AstralBattles.Views;
using System;
using AstralBattles.Core.Infrastructure;
using System.Windows.Input;


namespace AstralBattles.ViewModels
{
   public class ViaNetworkGameModesViewModel : ViewModelBaseEx
  {
    public ICommand HostAServer { get; private set; }

    public ICommand JoinToServer { get; private set; }

    public ViaNetworkGameModesViewModel()
    {
      HostAServer = (ICommand) new RelayCommand(HostAServerAction);
      JoinToServer = (ICommand) new RelayCommand(JoinToServerAction);
    }

    private void HostAServerAction() => PageNavigationService.OpenServerHosting();

    private void JoinToServerAction() => PageNavigationService.TwoPlayersOptions();
  }
}
