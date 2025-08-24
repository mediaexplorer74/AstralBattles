// Decompiled with JetBrains decompiler
// Type: AstralBattles.ViewModels.QuickDuelWithAiBattlefieldViewModel
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core;
using AstralBattles.Core.Infrastructure;
using AstralBattles.Core.Model;
using AstralBattles.Core.Services;
using AstralBattles.Views;
using System.IO.IsolatedStorage;

#nullable disable
namespace AstralBattles.ViewModels
{
  public class QuickDuelWithAiBattlefieldViewModel : BattlefieldViewModel
  {
    public QuickDuelWithAiBattlefieldViewModel(bool newGame)
      : base(newGame)
    {
    }

    public QuickDuelWithAiBattlefieldViewModel()
    {
    }

    protected override void OnSaveState()
    {
      Serializer.Write<QuickDuelWithAiBattlefieldViewModel>(this, "DuelWithAiBattlefieldViewModel__1_452.xml");
      IsolatedStorageSettings.ApplicationSettings["LastPlayedMode__1_452"] = (object) GameModes.DuelWithAi;
    }

    protected override GameRulesEngineBase CreateGameRulesEngine()
    {
      return GameService.CurrentGame = (GameRulesEngineBase) new DuelWithAiGameRulesEngine((IBattlefield) this);
    }

    protected override void OnGameOver(Player winner)
    {
      Serializer.Delete("DuelWithAiBattlefieldViewModel__1_452.xml");
      PageNavigationService.TwoPlayersOptions(true);
    }
  }
}
