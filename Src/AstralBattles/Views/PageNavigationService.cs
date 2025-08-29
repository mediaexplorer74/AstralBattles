﻿// Decompiled with JetBrains decompiler
// Type: AstralBattles.Views.PageNavigationService
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace AstralBattles.Views
{
  public static class PageNavigationService
  {
    // Page type mappings for UWP navigation
    private static readonly Dictionary<string, Type> PageTypeMap = new Dictionary<string, Type>
    {
      {"/Views/MainPage.xaml", typeof(MainPage)},
      {"/Views/Battlefield.xaml", typeof(Battlefield)},
      {"/Views/CampaignMap.xaml", typeof(CampaignMap)},
      {"/Views/TwoPlayersOptions.xaml", typeof(TwoPlayersOptions)},
      {"/Views/CampaignOptionsView.xaml", typeof(CampaignOptionsView)},
      {"/Views/FaceSelectionView.xaml", typeof(FaceSelectionView)},
      {"/Views/TournamentOptions.xaml", typeof(TournamentTable)}, // Assuming TournamentOptions maps to TournamentTable
      {"/Views/DeckConfigurator.xaml", typeof(DeckConfigurator)},
      {"/Views/CreatePlayerView.xaml", typeof(CreatePlayerView)},
      {"/Views/TwoPlayersModes.xaml", typeof(TwoPlayersModes)},
      {"/Views/ViaNetworkGameModes.xaml", typeof(ViaNetworkGameModes)},
      {"/Views/HostingServer.xaml", typeof(HostingServer)},
      {"/Views/TournamentTable.xaml", typeof(TournamentTable)}
    };

    public static void OpenBattlefield(bool continueGame, bool is2PlayersDuel = false, bool isAiDuel = false)
    {
      PageNavigationService.Navigate("/Views/Battlefield.xaml?continueGame=" + (object) continueGame + "&isTwoPlayersDuel=" + (object) is2PlayersDuel + "&isAiDuel=" + (object) isAiDuel);
    }

    public static void OpenCampaignBattlefield(bool continueGame)
    {
      PageNavigationService.Navigate("/Views/Battlefield.xaml?continueGame=" + (object) continueGame + "&isCampaign=true");
    }

    public static void OpenStatistics()
    {
      PageNavigationService.Navigate("/Views/TournamentTable.xaml");
    }

    private static Frame GetCurrentFrame()
    {
      return Window.Current?.Content as Frame;
    }

    private static void Navigate(string uri, object parameter = null)
    {
      var frame = GetCurrentFrame();
      if (frame != null && PageTypeMap.TryGetValue(ExtractPagePath(uri), out Type pageType))
      {
        frame.Navigate(pageType, parameter ?? ExtractParameters(uri));
      }
    }

    private static string ExtractPagePath(string uri)
    {
      // Extract just the page path without parameters
      int questionMarkIndex = uri.IndexOf('?');
      return questionMarkIndex >= 0 ? uri.Substring(0, questionMarkIndex) : uri;
    }

    private static object ExtractParameters(string uri)
    {
      // Extract parameters from URI for navigation
      int questionMarkIndex = uri.IndexOf('?');
      return questionMarkIndex >= 0 ? uri.Substring(questionMarkIndex + 1) : null;
    }

    public static void OpenMainMenu()
    {
      PageNavigationService.Navigate("/Views/MainPage.xaml?back=true");
    }

    public static void OpenCampaignMap()
    {
      PageNavigationService.Navigate("/Views/CampaignMap.xaml");
    }

    public static void TwoPlayersOptions(bool back = false)
    {
      PageNavigationService.Navigate("/Views/TwoPlayersOptions.xaml" + (back ? "?back=true" : ""));
    }

    public static void CampaignOptions(bool back = false)
    {
      PageNavigationService.Navigate("/Views/CampaignOptionsView.xaml" + (back ? "?back=true" : ""));
    }

    public static void FaceSelectionView()
    {
      PageNavigationService.Navigate("/Views/FaceSelectionView.xaml");
    }

    public static void OpenTournamentOptions()
    {
      PageNavigationService.Navigate("/Views/TournamentOptions.xaml");
    }

    public static void OpenDeckEditor()
    {
      PageNavigationService.Navigate("/Views/DeckConfigurator.xaml");
    }

    public static void OpenCreateNewPlayer(bool enableDeckEditor = false)
    {
      PageNavigationService.Navigate("/Views/CreatePlayerView.xaml" + (enableDeckEditor ? "?enableDeckEditor=true" : ""));
    }

    public static void OpenTwoPlayersModes()
    {
      PageNavigationService.Navigate("/Views/TwoPlayersModes.xaml");
    }

    public static void OpenViaNetworkGameModes()
    {
      PageNavigationService.Navigate("/Views/ViaNetworkGameModes.xaml");
    }

    public static void OpenServerHosting()
    {
      PageNavigationService.Navigate("/Views/HostingServer.xaml");
    }
  }
}
