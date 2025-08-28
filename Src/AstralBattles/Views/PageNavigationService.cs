// Decompiled with JetBrains decompiler
// Type: AstralBattles.Views.PageNavigationService
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace AstralBattles.Views
{
  public static class PageNavigationService
  {
    public const string FaceSelectionUrl = "/Views/FaceSelectionView.xaml";
    public const string CreatePlayerViewUrl = "/Views/CreatePlayerView.xaml";

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

    private static void Navigate(string uri)
    {
      // UWP navigation - find current Frame and navigate
      if (Window.Current?.Content is Frame rootFrame)
      {
        rootFrame.Navigate(typeof(MainPage), uri); // Simplified for MVP - would need proper page type mapping
      }
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
