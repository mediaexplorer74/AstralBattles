// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.TwoPlayersDuelRulesEngine
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using AstralBattles.Core.Model;
using AstralBattles.Core.Services;
using System;

#nullable disable
namespace AstralBattles.Core
{
  public class TwoPlayersDuelRulesEngine : GameRulesEngineBase
  {
    public TwoPlayersDuelRulesEngine()
    {
    }

    public TwoPlayersDuelRulesEngine(IBattlefield bf)
      : base(bf)
    {
    }

    protected override void OnEndTurnCompleted()
    {
      Player firstPlayer = this.Battlefield.FirstPlayer;
      this.Battlefield.FirstPlayer = this.Battlefield.SecondPlayer;
      this.Battlefield.SecondPlayer = firstPlayer;
      this.Battlefield.ActivePlayer = this.Battlefield.FirstPlayer;
      this.Battlefield.InactivePlayer = this.Battlefield.SecondPlayer;
      this.Battlefield.FirstPlayer.Elements[0].IsSelected = true;
      this.Battlefield.FirstPlayerSelectedElement = this.Battlefield.FirstPlayer.Elements[0];
      this.Battlefield.UpdateIsPlayerInitialTurn();
      this.Battlefield.Refresh();
      this.Battlefield.WaitingNextPlayersTurn = true;
    }

    protected override Player CreateFirstPlayer() => PlayersFactory.CreateFirstPlayerForDuel();

    protected override Player CreateSecondPlayer() => PlayersFactory.CreateSecondPlayerForDuel();

    public void OnClosingNextTurnDialog()
    {
      this.Battlefield.WaitingNextPlayersTurn = false;
      this.NewTurn();
    }

    protected override void OnNewTurn()
    {
      this.SaveCurrentState();
      if (this.Battlefield.FirstPlayer.IsStunned)
      {
        this.Battlefield.FirstPlayer.IsStunned = false;
        this.Battlefield.FirstPlayer.RaiseStunned((Action) (() => this.SummonCard((Card) null, (Field) null)));
      }
      else
        this.Battlefield.IsWaitingForUserInput = true;
    }
  }
}
