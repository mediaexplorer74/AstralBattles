// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.GameRulesEngineBase
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using AstralBattles.Core.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;


namespace AstralBattles.Core
{
  [XmlInclude(typeof (DuelWithAiGameRulesEngine))]
  [XmlInclude(typeof (CampaignGameRulesEngine))]
  [XmlInclude(typeof (TournamentGameRulesEngine))]
  [XmlInclude(typeof (TwoPlayersDuelRulesEngine))]
  public class GameRulesEngineBase
  {
    private IBattlefield bf;

    protected GameRulesEngineBase(IBattlefield bf)
    {
      this.Battlefield = this.bf = bf;
      this.SkillsManager = new SkillsManager();
      this.SpellManager = new SpellManager();
    }

    protected GameRulesEngineBase()
    {
    }

    [XmlIgnore]
    public IBattlefield Battlefield
    {
      get => this.bf;
      set => this.bf = value;
    }

    public SkillsManager SkillsManager { get; set; }

    public SpellManager SpellManager { get; set; }

    public Field SecondPlayerSummonsCardOn { get; set; }

    public Card SecondPlayerSummonsCard { get; set; }

    public void EndTurn()
    {
      Field[] array = this.bf.ActivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)).ToArray<Field>();
      if (array.Length == 0)
        this.EndTurnCompleted();
      else
        this.DoSyncAttack(array, 0, new Action(this.EndTurnCompleted));
    }

    private void EndTurnCompleted()
    {
      this.SkillsManager.OnEndTrun();
      this.OnBeforeEndTurnCompleted();
      this.bf.EndTurn();
      ++this.bf.RoundIndex;
      if (this.bf.FirstPlayer.Health < 1 || this.bf.SecondPlayer.Health < 1)
        this.GameOver();
      else
        this.OnEndTurnCompleted();
    }

    protected virtual void OnBeforeEndTurnCompleted()
    {
    }

    protected virtual void OnEndTurnCompleted()
    {
    }

    private void DoSyncAttack(Field[] fields, int index, Action lastAction)
    {
      if (index >= fields.Length)
        lastAction();
      else
        this.FieldAttack(fields[index], (Action) (() => this.DoSyncAttack(fields, index + 1, lastAction)));
    }

    private void FieldAttack(Field field, Action attackAction)
    {
      if (field.IsEmpty || field.Card.IsNotAttackable)
        attackAction();
      else
        field.Attack(field, this.Battlefield, attackAction);
    }

    private void IncreaseAllElementPower(Player player) => player.IncreaseAllElementsPower(1);

    public void NewTurn()
    {
      this.IncreaseAllElementPower(this.bf.ActivePlayer);
      this.SkillsManager.OnNewTurn();
      this.OnNewTurn();
    }

    protected virtual void OnNewTurn()
    {
    }

    protected void SaveCurrentState() => this.bf.SerializeCurrentState();

    public void UpdateCardIsActive()
    {
      foreach (Element element in (Collection<Element>) this.bf.FirstPlayer.Elements)
      {
        foreach (Card card in (Collection<Card>) element.Cards)
          card.IsActive = card.Cost <= element.Mana;
      }
      foreach (Element element in (Collection<Element>) this.bf.SecondPlayer.Elements)
      {
        foreach (Card card in (Collection<Card>) element.Cards)
          card.IsActive = card.Cost <= element.Mana;
      }
    }

    public void SummonCard(Card card, Field field, bool bySecondPlayer = false)
    {
      this.bf.IsWaitingForUserInput = false;
      if (card == null)
      {
        this.bf.OnSkip();
        this.EndTurn();
      }
      else
      {
        this.SecondPlayerSummonsCard = card;
        this.SecondPlayerSummonsCardOn = field;
        if (!bySecondPlayer)
          this.OnClosingSummoningDialog();
        else
          this.bf.OnShowComputersChoose(card);
      }
    }

    public void OnClosingSummoningDialog()
    {
      Card playerSummonsCard = this.SecondPlayerSummonsCard;
      Field field = this.SecondPlayerSummonsCardOn;
      if (playerSummonsCard == null || field == null)
        return;
      if (playerSummonsCard is CreatureCard)
      {
        CreatureCard creatureCard = playerSummonsCard.Clone() as CreatureCard;
        this.bf.OnSummon(creatureCard, true);
        this.SkillsManager.OnSummon(creatureCard, field);
        this.bf.ActivePlayer.DecreaseMana(playerSummonsCard.ElementType, playerSummonsCard.Cost);
        field.AssignCard(creatureCard, (Action) (() =>
        {
          this.SkillsManager.OnSummoned(field);
          if (this.Battlefield.InactivePlayer.Specialization == Specialization.Pyromancer)
            field.DoHarm(2, this.Battlefield);
          this.EndTurn();
        }));
      }
      else
      {
        this.bf.ActivePlayer.DecreaseMana(playerSummonsCard.ElementType, playerSummonsCard.Cost);
        this.SpellManager.CastSpell(playerSummonsCard as SpellCard, field, new Action(this.EndTurn));
      }
      this.SecondPlayerSummonsCardOn = (Field) null;
      this.SecondPlayerSummonsCard = (Card) null;
    }

    public void StartGame()
    {
      this.OnStartGame();
      this.bf.RoundIndex = 1;
      this.bf.FirstPlayer = this.CreateFirstPlayer();
      this.bf.FirstPlayer.CreateFields();
      this.bf.SecondPlayer = this.CreateSecondPlayer();
      this.bf.SecondPlayer.CreateFields();
      this.bf.FirstPlayer.Fields[0].IsSelected = true;
      this.bf.SelectedField = this.bf.FirstPlayer.Fields[0];
      this.bf.IsFirstPlayerTurn = true;
      this.bf.FirstPlayerSelectedElement = this.bf.FirstPlayer.Elements[0];
      this.bf.FirstPlayer.Elements[0].IsSelected = true;
      this.bf.SelectedCardOrCardType = this.bf.FirstPlayer.Elements[0].Cards[0];
      this.bf.FirstPlayer.Elements[0].Cards[0].IsSelected = true;
      this.bf.IsWaitingForUserInput = true;
      this.SkillsManager.OnStartGame();
    }

    protected virtual void OnStartGame()
    {
    }

    protected virtual Player CreateFirstPlayer() => (Player) null;

    protected virtual Player CreateSecondPlayer() => (Player) null;

    public GameDifficulty GameDifficulty { get; set; }

    private void GameOver()
    {
      if (this.bf.FirstPlayer.Health < 1 && this.bf.SecondPlayer.Health < 1)
        this.bf.GameOver(this.bf.FirstPlayer);
      else
        this.bf.GameOver(this.bf.FirstPlayer.Health > 0 ? this.bf.FirstPlayer : this.bf.SecondPlayer);
    }
  }
}
