// Decompiled with JetBrains decompiler
// Type: AstralBattles.ViewModels.BattlefieldViewModel
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core;
using AstralBattles.Core.Model;
using AstralBattles.Core.Services;
using AstralBattles.Localizations;
using AstralBattles.Views;
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;

#nullable disable
namespace AstralBattles.ViewModels
{
  public class BattlefieldViewModel : ViewModelBaseEx, IBattlefield
  {
    private Player activePlayer;
    private string cardDescription;
    private string cardName;
    private Player firstPlayer;
    private Element firstPlayerSelectedElement;
    private ObservableCollection<GameLogItem> gameLog;
    private GameRulesEngineBase gameRulesEngine;
    private Player inactivePlayer;
    private bool isFirstPlayerTurn;
    private bool isWaitingForUserInput = true;
    private GameLogItem lastLogItem;
    private Card lastUsedCard;
    private int roundIndex;
    private Player secondPlayer;
    private Card selectedCard;
    private Card selectedCardOrCardType;
    private Field selectedField;
    private bool showLogMode;
    private int turn;
    private bool waitingNextPlayersTurn;
    private Player firstPlayerInitial;
    private Player secondPlayerInitial;
    private bool isFirstPlayerInitialTurn;
    private bool isSecondPlayerInitialTurn;
    private string selectedCardOrCardTypeShortDesc;
    private Card opponentIsSummoningCard;
    private string opponentIsSummoningCardTitle;

    public BattlefieldViewModel()
    {
      if (this.IsInDesignMode)
      {
        this.FirstPlayer = DesignTimeDataContext.Instance.Player;
        this.SecondPlayer = DesignTimeDataContext.Instance.Player;
        this.FirstPlayerSelectedElement = this.FirstPlayer.Elements[0];
      }
      else
      {
        this.InitializeCommads();
        this.GameLog = new ObservableCollection<GameLogItem>();
        this.GameLog.CollectionChanged += new NotifyCollectionChangedEventHandler(this.GameLogCollectionChanged);
        this.SkipTurn = (ICommand) new RelayCommand(new Action(this.SkipTurnAction));
      }
    }

    public BattlefieldViewModel(bool newGame)
    {
      this.InitializeCommads();
      this.GameLog = new ObservableCollection<GameLogItem>();
      this.GameLog.CollectionChanged += new NotifyCollectionChangedEventHandler(this.GameLogCollectionChanged);
      if (!newGame)
        return;
      this.GameRulesEngine = this.CreateGameRulesEngine();
      this.GameRulesEngine.StartGame();
      this.FirstPlayerInitial = this.FirstPlayer;
      this.SecondPlayerInitial = this.SecondPlayer;
      this.UpdateIsPlayerInitialTurn();
      this.DeselectCardsInBook();
      this.DeselectFields();
    }

    private void RemoveDeadCards()
    {
      try
      {
        this.FirstPlayer.BusyFields.Where<Field>((Func<Field, bool>) (i => i.Card.Health < 1)).ForEach<Field>((Action<Field>) (i => i.Card = (CreatureCard) null));
        this.SecondPlayer.BusyFields.Where<Field>((Func<Field, bool>) (i => i.Card.Health < 1)).ForEach<Field>((Action<Field>) (i => i.Card = (CreatureCard) null));
        this.FirstPlayerInitial.BusyFields.Where<Field>((Func<Field, bool>) (i => i.Card.Health < 1)).ForEach<Field>((Action<Field>) (i => i.Card = (CreatureCard) null));
        this.SecondPlayerInitial.BusyFields.Where<Field>((Func<Field, bool>) (i => i.Card.Health < 1)).ForEach<Field>((Action<Field>) (i => i.Card = (CreatureCard) null));
      }
      catch
      {
      }
    }

    protected virtual GameRulesEngineBase CreateGameRulesEngine() => (GameRulesEngineBase) null;

    public GameRulesEngineBase GameRulesEngine
    {
      get => this.gameRulesEngine;
      set => this.gameRulesEngine = value;
    }

    public void OnSpell(SpellCard spell)
    {
      this.GameLog.Add(new GameLogItem(CommonResources.HasCasted, new object[2]
      {
        (object) this.ActivePlayer.Name,
        (object) spell.Localization.DefaultLanguage.DisplayName
      }));
      SoundPlayer.PlaySound(spell.Name);
    }

    public void OnSummon(CreatureCard creature, bool bySecondPlayer)
    {
      this.GameLog.Add(new GameLogItem(CommonResources.HasSummoned, new object[2]
      {
        (object) this.ActivePlayer.Name,
        (object) creature.Localization.DefaultLanguage.DisplayName
      }));
      SoundPlayer.PlaySound("Summon");
    }

    public void OnSkip()
    {
      this.GameLog.Add(new GameLogItem(CommonResources.HasSkipped, new object[1]
      {
        (object) this.ActivePlayer.Name
      }));
    }

    public Player GetPlayerByName(string name)
    {
      return this.SecondPlayer.Name == name ? this.SecondPlayer : this.FirstPlayer;
    }

    public void UseCard(Card card)
    {
      this.LastUsedCard = !this.IsFirstPlayerTurn ? (Card) null : card;
      this.gameRulesEngine.SummonCard(card, this.SelectedField);
    }

    public void EndTurn()
    {
    }

    public void StartNewTurn()
    {
      this.GameLog.Add(new GameLogItem(CommonResources.RoundStarted, new object[1]
      {
        (object) this.Turn
      })
      {
        IsNewRoundLog = true
      });
    }

    public void GameOver(Player winner)
    {
      this.GameLog.Add(new GameLogItem(CommonResources.HasWon, new object[1]
      {
        (object) winner.Name
      }));
      int num = (int) MessageBox.Show(string.Format(CommonResources.HasWon, (object) winner.Name));
      this.OnGameOver(winner);
    }

    protected virtual void OnGameOver(Player winner)
    {
    }

    public IEnumerable<Field> GetAllNonEmptyFields()
    {
      return this.ActivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)).Concat<Field>(this.InactivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)));
    }

    public void SerializeCurrentState()
    {
      PhoneApplicationFrame rootVisual = (PhoneApplicationFrame) Application.Current.RootVisual;
      if (rootVisual == null || !(((ContentControl) rootVisual).Content is Battlefield))
        return;
      this.OnSaveState();
    }

    protected virtual void OnSaveState()
    {
    }

    private void GameLogCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.LastLogItem = this.GameLog.LastOrDefault<GameLogItem>();
    }

    private void DeselectCardsInBook()
    {
      this.FirstPlayer.Elements.SelectMany<Element, Card>((Func<Element, IEnumerable<Card>>) (i => (IEnumerable<Card>) i.Cards)).Where<Card>((Func<Card, bool>) (i => i.IsSelected)).ForEach<Card>((Action<Card>) (i => i.IsSelected = false));
      this.SelectedCard = (Card) null;
    }

    private void DeselectFields()
    {
      this.FirstPlayer.Fields.Where<Field>((Func<Field, bool>) (i => i.IsWaitingForSelect)).ForEach<Field>((Action<Field>) (i => i.IsWaitingForSelect = false));
      this.SecondPlayer.Fields.Where<Field>((Func<Field, bool>) (i => i.IsWaitingForSelect)).ForEach<Field>((Action<Field>) (i => i.IsWaitingForSelect = false));
    }

    public void SetIsWaitingForSelectClear()
    {
      this.SecondPlayer.Fields.ForEach<Field>((Action<Field>) (i => i.IsWaitingForSelect = false));
      this.FirstPlayer.Fields.ForEach<Field>((Action<Field>) (i => i.IsWaitingForSelect = false));
    }

    private void SelectCardInBook()
    {
      if (this.FirstPlayerSelectedElement != null)
        this.FirstPlayerSelectedElement.Cards.ForEach<Card>((Action<Card>) (i => i.IsSelected = false));
      if (this.SelectedCard != null)
        this.SelectedCard.IsSelected = true;
      this.DeselectFields();
    }

    public void UseSelectedCard()
    {
      this.UseCard(this.SelectedCard);
      if (this.SelectedCard != null)
        this.SelectedCard.IsSelected = false;
      this.SelectedCard = (Card) null;
    }

    public void OnDeserialized(bool bySerializer = true)
    {
      if (bySerializer)
      {
        this.FirstPlayerInitial = this.FirstPlayer;
        this.SecondPlayerInitial = this.SecondPlayer;
        this.UpdateIsPlayerInitialTurn();
      }
      GameService.CurrentGame = this.GameRulesEngine;
      this.GameRulesEngine.Battlefield = (IBattlefield) this;
      this.GameLogCollectionChanged((object) this, (NotifyCollectionChangedEventArgs) null);
      if (bySerializer)
        this.RemoveDeadCards();
      this.FirstPlayer.Elements.ForEach<Element>((Action<Element>) (i => i.IsSelected = false));
      this.FirstPlayer.Elements[0].IsSelected = true;
      this.FirstPlayerSelectedElement = this.FirstPlayer.Elements[0];
      this.SelectedField = this.FirstPlayer.Fields.First<Field>();
      this.DeselectCardsInBook();
      this.DeselectFields();
      this.GameRulesEngine.UpdateCardIsActive();
    }

    public void Refresh() => this.OnDeserialized(false);

    public void SetNextOrPreviousElement(bool next)
    {
      int num = this.FirstPlayer.Elements.IndexOf(this.FirstPlayerSelectedElement);
      bool flag1 = num == this.FirstPlayer.Elements.Count - 1;
      bool flag2 = num == 0;
      this.FirstPlayerSelectedElement = this.FirstPlayer.Elements[!flag1 || !next ? (!flag2 || next ? (!next ? num - 1 : num + 1) : this.FirstPlayer.Elements.Count - 1) : 0];
    }

    public void OnShowComputersChoose(Card card)
    {
      this.OpponentIsSummoningCard = card;
      if (card is SpellCard)
        this.OpponentIsSummoningCardTitle = string.Format(CommonResources.Casting, (object) this.ActivePlayer.Name, (object) card.Localization.DefaultLanguage.DisplayName);
      else
        this.OpponentIsSummoningCardTitle = string.Format(CommonResources.Summoning, (object) this.ActivePlayer.Name, (object) card.Localization.DefaultLanguage.DisplayName);
    }

    public void OnClosingSummoningDialog()
    {
      this.GameRulesEngine.OnClosingSummoningDialog();
      this.OpponentIsSummoningCard = (Card) null;
      this.OpponentIsSummoningCardTitle = (string) null;
    }

    public bool ShowLogMode
    {
      get => this.showLogMode;
      set
      {
        this.showLogMode = value;
        this.RaisePropertyChanged(nameof (ShowLogMode));
      }
    }

    public bool WaitingNextPlayersTurn
    {
      get => this.waitingNextPlayersTurn;
      set
      {
        this.waitingNextPlayersTurn = value;
        this.RaisePropertyChanged(nameof (WaitingNextPlayersTurn));
      }
    }

    public string CardName
    {
      get => this.cardName;
      set
      {
        this.cardName = value;
        this.RaisePropertyChanged(nameof (CardName));
      }
    }

    public string CardDescription
    {
      get => this.cardDescription;
      set
      {
        this.cardDescription = value;
        this.RaisePropertyChanged(nameof (CardDescription));
      }
    }

    public ObservableCollection<GameLogItem> GameLog
    {
      get => this.gameLog;
      set
      {
        this.gameLog = value;
        this.RaisePropertyChanged(nameof (GameLog));
      }
    }

    public Player FirstPlayer
    {
      get => this.firstPlayer;
      set
      {
        this.firstPlayer = value;
        this.RaisePropertyChanged(nameof (FirstPlayer));
        this.RaisePropertyChanged("ActivePlayer");
        this.RaisePropertyChanged("InactivePlayer");
      }
    }

    public Player SecondPlayer
    {
      get => this.secondPlayer;
      set
      {
        this.secondPlayer = value;
        this.RaisePropertyChanged(nameof (SecondPlayer));
        this.RaisePropertyChanged("ActivePlayer");
        this.RaisePropertyChanged("InactivePlayer");
      }
    }

    [XmlIgnore]
    public Player FirstPlayerInitial
    {
      get => this.firstPlayerInitial;
      set
      {
        if (this.firstPlayerInitial == value)
          return;
        this.firstPlayerInitial = value;
        this.RaisePropertyChanged(nameof (FirstPlayerInitial));
      }
    }

    [XmlIgnore]
    public Player SecondPlayerInitial
    {
      get => this.secondPlayerInitial;
      set
      {
        if (this.secondPlayerInitial == value)
          return;
        this.secondPlayerInitial = value;
        this.RaisePropertyChanged(nameof (SecondPlayerInitial));
      }
    }

    [XmlIgnore]
    public bool IsFirstPlayerInitialTurn
    {
      get => this.isFirstPlayerInitialTurn;
      set
      {
        if (this.isFirstPlayerInitialTurn == value)
          return;
        this.isFirstPlayerInitialTurn = value;
        this.RaisePropertyChanged(nameof (IsFirstPlayerInitialTurn));
      }
    }

    [XmlIgnore]
    public bool IsSecondPlayerInitialTurn
    {
      get => this.isSecondPlayerInitialTurn;
      set
      {
        if (this.isSecondPlayerInitialTurn == value)
          return;
        this.isSecondPlayerInitialTurn = value;
        this.RaisePropertyChanged(nameof (IsSecondPlayerInitialTurn));
      }
    }

    public void UpdateIsPlayerInitialTurn()
    {
      this.IsFirstPlayerInitialTurn = this.FirstPlayerInitial == this.FirstPlayer;
      this.IsSecondPlayerInitialTurn = this.SecondPlayerInitial == this.FirstPlayer;
    }

    public GameLogItem LastLogItem
    {
      get => this.lastLogItem;
      set
      {
        this.lastLogItem = value;
        this.RaisePropertyChanged(nameof (LastLogItem));
      }
    }

    public int Turn
    {
      get => this.turn;
      set
      {
        this.turn = value;
        this.RaisePropertyChanged(nameof (Turn));
      }
    }

    [XmlIgnore]
    public Player ActivePlayer
    {
      get
      {
        if (this.activePlayer != null)
          return this.activePlayer;
        return this.IsFirstPlayerTurn ? (this.activePlayer = this.FirstPlayer) : (this.activePlayer = this.SecondPlayer);
      }
      set
      {
        this.activePlayer = value;
        this.RaisePropertyChanged(nameof (ActivePlayer));
      }
    }

    [XmlIgnore]
    public Player InactivePlayer
    {
      get
      {
        if (this.inactivePlayer != null)
          return this.inactivePlayer;
        return this.IsFirstPlayerTurn ? (this.inactivePlayer = this.SecondPlayer) : (this.inactivePlayer = this.FirstPlayer);
      }
      set
      {
        this.inactivePlayer = value;
        this.RaisePropertyChanged(nameof (InactivePlayer));
      }
    }

    [XmlIgnore]
    public bool IsWaitingForUserInput
    {
      get => this.isWaitingForUserInput;
      set
      {
        this.isWaitingForUserInput = value;
        this.RaisePropertyChanged(nameof (IsWaitingForUserInput));
      }
    }

    public Field SelectedField
    {
      get => this.selectedField;
      set
      {
        this.selectedField = value;
        this.RaisePropertyChanged(nameof (SelectedField));
        if (value == null || value.IsEmpty)
          return;
        this.SelectedCardOrCardType = (Card) value.Card;
      }
    }

    public int RoundIndex
    {
      get => this.roundIndex;
      set
      {
        this.roundIndex = value;
        this.RaisePropertyChanged(nameof (RoundIndex));
      }
    }

    public Card LastUsedCard
    {
      get => this.lastUsedCard;
      set
      {
        this.lastUsedCard = value;
        this.RaisePropertyChanged(nameof (LastUsedCard));
      }
    }

    public bool IsFirstPlayerTurn
    {
      get => this.isFirstPlayerTurn;
      set
      {
        this.isFirstPlayerTurn = value;
        this.RaisePropertyChanged(nameof (IsFirstPlayerTurn));
        this.ActivePlayer = value ? this.FirstPlayer : this.SecondPlayer;
        this.InactivePlayer = value ? this.SecondPlayer : this.FirstPlayer;
      }
    }

    public Card SelectedCardOrCardType
    {
      get => this.selectedCardOrCardType;
      set
      {
        this.selectedCardOrCardType = value;
        this.RaisePropertyChanged(nameof (SelectedCardOrCardType));
        switch (value)
        {
          case null:
          case FakeCard _:
            this.SelectedCardOrCardTypeShortDesc = string.Empty;
            break;
          case CreatureCard _:
            this.SelectedCardOrCardTypeShortDesc = string.Format(CommonResources.CreatureShortDesc2, (object) value.DamageString, (object) value.Health, (object) value.Cost);
            break;
          case SpellCard _:
            this.SelectedCardOrCardTypeShortDesc = string.Format(CommonResources.SpellShortDesc2, (object) value.Cost);
            break;
        }
      }
    }

    public string SelectedCardOrCardTypeShortDesc
    {
      get => this.selectedCardOrCardTypeShortDesc;
      set
      {
        this.selectedCardOrCardTypeShortDesc = value;
        this.RaisePropertyChanged(nameof (SelectedCardOrCardTypeShortDesc));
      }
    }

    [XmlIgnore]
    public Element FirstPlayerSelectedElement
    {
      get => this.firstPlayerSelectedElement;
      set
      {
        this.DeselectFields();
        this.DeselectCardsInBook();
        if (value != null)
          value.IsSelected = true;
        this.firstPlayerSelectedElement = value;
        if (this.ActivePlayer != null)
          this.ActivePlayer.Elements.Where<Element>((Func<Element, bool>) (i => i != value)).ForEach<Element>((Action<Element>) (i => i.IsSelected = false));
        this.RaisePropertyChanged(nameof (FirstPlayerSelectedElement));
      }
    }

    [XmlIgnore]
    public Card SelectedCard
    {
      get => this.selectedCard;
      set
      {
        this.selectedCard = value;
        if (value != null)
          this.SelectedCardOrCardType = value;
        this.RaisePropertyChanged(nameof (SelectedCard));
        this.SelectCardInBook();
        this.SetIsWaitingForSelectClear();
        if (value == null || !this.IsFirstPlayerTurn || !value.IsActive)
          return;
        switch (value)
        {
          case CreatureCard _:
            this.ActivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => i.IsEmpty)).ForEach<Field>((Action<Field>) (i => i.IsWaitingForSelect = true));
            break;
          case SpellCard _:
            SpellCard spellCard = value as SpellCard;
            if (spellCard.Target == SpellTarget.OpponentsCard)
              this.InactivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)).ForEach<Field>((Action<Field>) (i => i.IsWaitingForSelect = true));
            if (spellCard.Target == SpellTarget.OpponentsCardNotSpecial)
            {
              this.InactivePlayer.Fields.Where<Field>((Func<Field, bool>) (i =>
              {
                if (i.IsEmpty)
                  return false;
                return i.Card.ElementType == ElementTypeEnum.Fire || i.Card.ElementType == ElementTypeEnum.Water || i.Card.ElementType == ElementTypeEnum.Air || i.Card.ElementType == ElementTypeEnum.Earth;
              })).ForEach<Field>((Action<Field>) (i => i.IsWaitingForSelect = true));
              break;
            }
            if (spellCard.Target == SpellTarget.OwnersCard)
            {
              this.ActivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)).ForEach<Field>((Action<Field>) (i => i.IsWaitingForSelect = true));
              break;
            }
            if (spellCard.Target == SpellTarget.IndeterminateOwnerEmpty)
            {
              this.ActivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => i.IsEmpty)).ForEach<Field>((Action<Field>) (i => i.IsWaitingForSelect = true));
              break;
            }
            if (spellCard.Target == SpellTarget.Indeterminate)
            {
              this.InactivePlayer.Fields.ForEach<Field>((Action<Field>) (i => i.IsWaitingForSelect = true));
              break;
            }
            if (spellCard.Target != SpellTarget.IndeterminateOwner)
              break;
            this.ActivePlayer.Fields.ForEach<Field>((Action<Field>) (i => i.IsWaitingForSelect = true));
            break;
        }
      }
    }

    public Card OpponentIsSummoningCard
    {
      get => this.opponentIsSummoningCard;
      set
      {
        this.opponentIsSummoningCard = value;
        this.RaisePropertyChanged(nameof (OpponentIsSummoningCard));
      }
    }

    public string OpponentIsSummoningCardTitle
    {
      get => this.opponentIsSummoningCardTitle;
      set
      {
        this.opponentIsSummoningCardTitle = value;
        this.RaisePropertyChanged(nameof (OpponentIsSummoningCardTitle));
      }
    }

    private void InitializeCommads()
    {
      this.FirstPlayerFieldSelect = (ICommand) new RelayCommand(new Action(this.FirstPlayerFieldSelectCommand));
      this.SecondPlayerFieldSelect = (ICommand) new RelayCommand(new Action(this.SecondPlayerFieldSelectCommand));
      this.CloseLog = (ICommand) new RelayCommand((Action) (() => this.ShowLogMode = false));
      this.ShowLog = (ICommand) new RelayCommand((Action) (() => this.ShowLogMode = true));
      this.SkipTurn = (ICommand) new RelayCommand(new Action(this.SkipTurnAction));
    }

    [XmlIgnore]
    public ICommand CloseLog { get; set; }

    [XmlIgnore]
    public ICommand ShowLog { get; set; }

    [XmlIgnore]
    public ICommand SecondPlayerFieldSelect { get; set; }

    [XmlIgnore]
    public ICommand SkipTurn { get; set; }

    [XmlIgnore]
    public ICommand FirstPlayerFieldSelect { get; set; }

    private void FirstPlayerFieldSelectCommand()
    {
      this.SelectedField = this.FirstPlayer.Fields.First<Field>((Func<Field, bool>) (i => i.IsSelected));
      if (this.SelectedField.IsWaitingForSelect)
        this.UseSelectedCard();
      this.SetIsWaitingForSelectClear();
      this.SecondPlayer.Fields.ForEach<Field>((Action<Field>) (i => i.IsSelected = false));
      this.DeselectCardsInBook();
    }

    private void SecondPlayerFieldSelectCommand()
    {
      this.SelectedField = this.SecondPlayer.Fields.First<Field>((Func<Field, bool>) (i => i.IsSelected));
      if (this.SelectedField.IsWaitingForSelect)
        this.UseSelectedCard();
      this.SetIsWaitingForSelectClear();
      this.SelectedField = (Field) null;
      this.FirstPlayer.Fields.ForEach<Field>((Action<Field>) (i => i.IsSelected = false));
      this.DeselectCardsInBook();
    }

    private void SkipTurnAction()
    {
      this.SetIsWaitingForSelectClear();
      this.gameRulesEngine.SummonCard((Card) null, (Field) null);
    }
  }
}
