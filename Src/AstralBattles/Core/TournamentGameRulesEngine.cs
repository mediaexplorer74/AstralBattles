// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.TournamentGameRulesEngine
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using AstralBattles.Core.Ai;
using AstralBattles.Core.Model;
using AstralBattles.Core.Services;
using System;

#nullable disable
namespace AstralBattles.Core
{
  public class TournamentGameRulesEngine : GameRulesEngineBase
  {
    public TournamentGameRulesEngine()
    {
    }

    public TournamentGameRulesEngine(IBattlefield bf)
      : base(bf)
    {
    }

    public ComputerIntellect Computer { get; set; }

    protected override Player CreateFirstPlayer() => PlayersFactory.CreatePlayer();

    protected override Player CreateSecondPlayer()
    {
      return PlayersFactory.CreateOpponent(TournamentService.Instance.GetCurrentOpponent(), this.GameDifficulty);
    }

    protected override void OnStartGame()
    {
      this.Computer = PlayersFactory.GetComputer(this.Battlefield);
    }

    protected override void OnEndTurnCompleted() => this.NewTurn();

    protected override void OnNewTurn()
    {
      if (this.Battlefield.IsFirstPlayerTurn)
      {
        this.SaveCurrentState();
        this.Battlefield.IsWaitingForUserInput = true;
      }
      else if (this.Battlefield.ActivePlayer.IsStunned)
      {
        this.Battlefield.ActivePlayer.RaiseStunned((Action) (() => this.SummonCard((Card) null, (Field) null)));
        this.Battlefield.ActivePlayer.IsStunned = false;
        this.NewTurn();
      }
      else
      {
        if (!this.Battlefield.ActivePlayer.IsAi)
          return;
        Field field;
        Card card = this.Computer.GetCard(out field);
        if (card != null && this.Battlefield.ActivePlayer.GetElementByType(card.ElementType).Mana < card.Cost)
          card = (Card) null;
        this.SummonCard(card, field, true);
      }
    }

    protected override void OnBeforeEndTurnCompleted()
    {
      this.Battlefield.IsFirstPlayerTurn = !this.Battlefield.IsFirstPlayerTurn;
    }
  }
}
