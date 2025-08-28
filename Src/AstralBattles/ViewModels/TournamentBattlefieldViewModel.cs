// Decompiled with JetBrains decompiler
// Type: AstralBattles.ViewModels.TournamentBattlefieldViewModel
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core;
using AstralBattles.Core.Infrastructure;
using AstralBattles.Core.Model;
using AstralBattles.Core.Services;
using AstralBattles.Options;
using AstralBattles.Views;


namespace AstralBattles.ViewModels
{
  public class TournamentBattlefieldViewModel : BattlefieldViewModel
  {
    public TournamentBattlefieldViewModel(bool newGame)
      : base(newGame)
    {
    }

    public TournamentBattlefieldViewModel()
    {
    }

    protected override void OnSaveState()
    {
      Serializer.Write<TournamentBattlefieldViewModel>(this, "CurrentTournamentGame__1_452.xml");
      Windows.Storage.ApplicationData.Current.LocalSettings.Values["LastPlayedMode__1_452"] = GameModes.Tournament;
    }

    protected override GameRulesEngineBase CreateGameRulesEngine()
    {
      TournamentGameRulesEngine tournamentGameRulesEngine = new TournamentGameRulesEngine((IBattlefield) this);
      tournamentGameRulesEngine.GameDifficulty = OptionsManager.Current.GameDifficulty;
      return GameService.CurrentGame = (GameRulesEngineBase) tournamentGameRulesEngine;
    }

    protected override void OnGameOver(Player winner)
    {
      TournamentService.Instance.EndRound(winner == FirstPlayer, PointsCalculator.Calculate(RoundIndex, winner.Kills, winner.Deaths));
      if (TournamentService.Instance.Tournament.CurrentRoundIndex < 9)
        Serializer.Write<TournamentBattlefieldViewModel>(new TournamentBattlefieldViewModel(true), "CurrentTournamentGame__1_452.xml");
      else
        Serializer.Delete("CurrentTournamentGame__1_452.xml");
      PageNavigationService.OpenStatistics();
    }
  }
}
