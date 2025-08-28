using AstralBattles.Core;
using AstralBattles.Core.Model;
using AstralBattles.Core.Services;
using AstralBattles.Localizations;
using AstralBattles.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Windows.UI.Xaml; 
using System.Xml.Serialization;
using AstralBattles.Core.Infrastructure;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

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
      if (App.IsInDesignMode)
      {
        FirstPlayer = DesignTimeDataContext.Instance.Player;
        SecondPlayer = DesignTimeDataContext.Instance.Player;
        FirstPlayerSelectedElement = FirstPlayer.Elements[0];
      }
      else
      {
        InitializeCommads();
        GameLog = new ObservableCollection<GameLogItem>();
        GameLog.CollectionChanged += new NotifyCollectionChangedEventHandler(GameLogCollectionChanged);
        SkipTurn = (ICommand) new RelayCommand(SkipTurnAction);
      }
    }

    public BattlefieldViewModel(bool newGame)
    {
      InitializeCommads();
      GameLog = new ObservableCollection<GameLogItem>();
      GameLog.CollectionChanged += new NotifyCollectionChangedEventHandler(GameLogCollectionChanged);
      if (!newGame)
        return;
      GameRulesEngine = CreateGameRulesEngine();
      GameRulesEngine.StartGame();
      FirstPlayerInitial = FirstPlayer;
      SecondPlayerInitial = SecondPlayer;
      UpdateIsPlayerInitialTurn();
      DeselectCardsInBook();
      DeselectFields();
    }

    private void RemoveDeadCards()
    {
      try
      {
        FirstPlayer.BusyFields.Where<Field>((Func<Field, bool>) (i => i.Card.Health < 1)).ForEach<Field>((Action<Field>) (i => i.Card = (CreatureCard) null));
        SecondPlayer.BusyFields.Where<Field>((Func<Field, bool>) (i => i.Card.Health < 1)).ForEach<Field>((Action<Field>) (i => i.Card = (CreatureCard) null));
        FirstPlayerInitial.BusyFields.Where<Field>((Func<Field, bool>) (i => i.Card.Health < 1)).ForEach<Field>((Action<Field>) (i => i.Card = (CreatureCard) null));
        SecondPlayerInitial.BusyFields.Where<Field>((Func<Field, bool>) (i => i.Card.Health < 1)).ForEach<Field>((Action<Field>) (i => i.Card = (CreatureCard) null));
      }
      catch
      {
      }
    }

    protected virtual GameRulesEngineBase CreateGameRulesEngine() => (GameRulesEngineBase) null;

    public GameRulesEngineBase GameRulesEngine
    {
      get => gameRulesEngine;
      set => gameRulesEngine = value;
    }

    public void OnSpell(SpellCard spell)
    {
      GameLog.Add(new GameLogItem(CommonResources.HasCasted, new object[2]
      {
        (object) ActivePlayer.Name,
        (object) spell.Localization.DefaultLanguage.DisplayName
      }));
      SoundPlayer.PlaySound(spell.Name);
    }

    public void OnSummon(CreatureCard creature, bool bySecondPlayer)
    {
      GameLog.Add(new GameLogItem(CommonResources.HasSummoned, new object[2]
      {
        (object) ActivePlayer.Name,
        (object) creature.Localization.DefaultLanguage.DisplayName
      }));
      SoundPlayer.PlaySound("Summon");
    }

    public void OnSkip()
    {
      GameLog.Add(new GameLogItem(CommonResources.HasSkipped, new object[1]
      {
        (object) ActivePlayer.Name
      }));
    }

    public Player GetPlayerByName(string name)
    {
      return SecondPlayer.Name == name ? SecondPlayer : FirstPlayer;
    }

    public void UseCard(Card card)
    {
      LastUsedCard = !IsFirstPlayerTurn ? (Card) null : card;
      gameRulesEngine.SummonCard(card, SelectedField);
    }

    public void EndTurn()
    {
    }

    public void StartNewTurn()
    {
      GameLog.Add(new GameLogItem(CommonResources.RoundStarted, new object[1]
      {
        (object) Turn
      })
      {
        IsNewRoundLog = true
      });
    }

    public async void GameOver(Player winner)
    {
      GameLog.Add(new GameLogItem(CommonResources.HasWon, new object[1]
      {
        (object) winner.Name
      }));

      int num = (int)await new ContentDialog
      {
        Title = "Game Over",
        Content = string.Format(CommonResources.HasWon, winner.Name),
        CloseButtonText = "OK"
      }.ShowAsync();

      OnGameOver(winner);
    }

    protected virtual void OnGameOver(Player winner)
    {
    }

    public IEnumerable<Field> GetAllNonEmptyFields()
    {
      return ActivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)).Concat<Field>(InactivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)));
    }

    public void SerializeCurrentState()
    {
      // UWP Frame handling - stub for MVP
      // Original: PhoneApplicationFrame rootVisual = (PhoneApplicationFrame) Application.Current.RootVisual;
      OnSaveState();
    }

    protected virtual void OnSaveState()
    {
    }

    private void GameLogCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      LastLogItem = GameLog.LastOrDefault<GameLogItem>();
    }

    private void DeselectCardsInBook()
    {
      FirstPlayer.Elements.SelectMany<Element, Card>((Func<Element, IEnumerable<Card>>) (i => (IEnumerable<Card>) i.Cards)).Where<Card>((Func<Card, bool>) (i => i.IsSelected)).ForEach<Card>((Action<Card>) (i => i.IsSelected = false));
      SelectedCard = (Card) null;
    }

    private void DeselectFields()
    {
      FirstPlayer.Fields.Where<Field>((Func<Field, bool>) (i => i.IsWaitingForSelect)).ForEach<Field>((Action<Field>) (i => i.IsWaitingForSelect = false));
      SecondPlayer.Fields.Where<Field>((Func<Field, bool>) (i => i.IsWaitingForSelect)).ForEach<Field>((Action<Field>) (i => i.IsWaitingForSelect = false));
    }

    public void SetIsWaitingForSelectClear()
    {
      SecondPlayer.Fields.ForEach<Field>((Action<Field>) (i => i.IsWaitingForSelect = false));
      FirstPlayer.Fields.ForEach<Field>((Action<Field>) (i => i.IsWaitingForSelect = false));
    }

    private void SelectCardInBook()
    {
      if (FirstPlayerSelectedElement != null)
        FirstPlayerSelectedElement.Cards.ForEach<Card>((Action<Card>) (i => i.IsSelected = false));
      if (SelectedCard != null)
        SelectedCard.IsSelected = true;
      DeselectFields();
    }

    public void UseSelectedCard()
    {
      UseCard(SelectedCard);
      if (SelectedCard != null)
        SelectedCard.IsSelected = false;
      SelectedCard = (Card) null;
    }

    public void OnDeserialized(bool bySerializer = true)
    {
      if (bySerializer)
      {
        FirstPlayerInitial = FirstPlayer;
        SecondPlayerInitial = SecondPlayer;
        UpdateIsPlayerInitialTurn();
      }
      GameService.CurrentGame = GameRulesEngine;
      GameRulesEngine.Battlefield = (IBattlefield) this;
      GameLogCollectionChanged((object) this, (NotifyCollectionChangedEventArgs) null);
      if (bySerializer)
        RemoveDeadCards();
      FirstPlayer.Elements.ForEach<Element>((Action<Element>) (i => i.IsSelected = false));
      FirstPlayer.Elements[0].IsSelected = true;
      FirstPlayerSelectedElement = FirstPlayer.Elements[0];
      SelectedField = FirstPlayer.Fields.First<Field>();
      DeselectCardsInBook();
      DeselectFields();
      GameRulesEngine.UpdateCardIsActive();
    }

    public void Refresh() => OnDeserialized(false);

    public void SetNextOrPreviousElement(bool next)
    {
      int num = FirstPlayer.Elements.IndexOf(FirstPlayerSelectedElement);
      bool flag1 = num == FirstPlayer.Elements.Count - 1;
      bool flag2 = num == 0;
      FirstPlayerSelectedElement = FirstPlayer.Elements[!flag1 || !next ? (!flag2 || next ? (!next ? num - 1 : num + 1) : FirstPlayer.Elements.Count - 1) : 0];
    }

    public void OnShowComputersChoose(Card card)
    {
      OpponentIsSummoningCard = card;
      if (card is SpellCard)
        OpponentIsSummoningCardTitle = string.Format(CommonResources.Casting, (object) ActivePlayer.Name, (object) card.Localization.DefaultLanguage.DisplayName);
      else
        OpponentIsSummoningCardTitle = string.Format(CommonResources.Summoning, (object) ActivePlayer.Name, (object) card.Localization.DefaultLanguage.DisplayName);
    }

    public void OnClosingSummoningDialog()
    {
      GameRulesEngine.OnClosingSummoningDialog();
      OpponentIsSummoningCard = (Card) null;
      OpponentIsSummoningCardTitle = (string) null;
    }

    public bool ShowLogMode
    {
      get => showLogMode;
      set
      {
        showLogMode = value;
        RaisePropertyChanged(nameof (ShowLogMode));
      }
    }

    public bool WaitingNextPlayersTurn
    {
      get => waitingNextPlayersTurn;
      set
      {
        waitingNextPlayersTurn = value;
        RaisePropertyChanged(nameof (WaitingNextPlayersTurn));
      }
    }

    public string CardName
    {
      get => cardName;
      set
      {
        cardName = value;
        RaisePropertyChanged(nameof (CardName));
      }
    }

    public string CardDescription
    {
      get => cardDescription;
      set
      {
        cardDescription = value;
        RaisePropertyChanged(nameof (CardDescription));
      }
    }

    public ObservableCollection<GameLogItem> GameLog
    {
      get => gameLog;
      set
      {
        gameLog = value;
        RaisePropertyChanged(nameof (GameLog));
      }
    }

    public Player FirstPlayer
    {
      get => firstPlayer;
      set
      {
        firstPlayer = value;
        RaisePropertyChanged(nameof (FirstPlayer));
        RaisePropertyChanged("ActivePlayer");
        RaisePropertyChanged("InactivePlayer");
      }
    }

    public Player SecondPlayer
    {
      get => secondPlayer;
      set
      {
        secondPlayer = value;
        RaisePropertyChanged(nameof (SecondPlayer));
        RaisePropertyChanged("ActivePlayer");
        RaisePropertyChanged("InactivePlayer");
      }
    }

    [XmlIgnore]
    public Player FirstPlayerInitial
    {
      get => firstPlayerInitial;
      set
      {
        if (firstPlayerInitial == value)
          return;
        firstPlayerInitial = value;
        RaisePropertyChanged(nameof (FirstPlayerInitial));
      }
    }

    [XmlIgnore]
    public Player SecondPlayerInitial
    {
      get => secondPlayerInitial;
      set
      {
        if (secondPlayerInitial == value)
          return;
        secondPlayerInitial = value;
        RaisePropertyChanged(nameof (SecondPlayerInitial));
      }
    }

    [XmlIgnore]
    public bool IsFirstPlayerInitialTurn
    {
      get => isFirstPlayerInitialTurn;
      set
      {
        if (isFirstPlayerInitialTurn == value)
          return;
        isFirstPlayerInitialTurn = value;
        RaisePropertyChanged(nameof (IsFirstPlayerInitialTurn));
      }
    }

    [XmlIgnore]
    public bool IsSecondPlayerInitialTurn
    {
      get => isSecondPlayerInitialTurn;
      set
      {
        if (isSecondPlayerInitialTurn == value)
          return;
        isSecondPlayerInitialTurn = value;
        RaisePropertyChanged(nameof (IsSecondPlayerInitialTurn));
      }
    }

    public void UpdateIsPlayerInitialTurn()
    {
      IsFirstPlayerInitialTurn = FirstPlayerInitial == FirstPlayer;
      IsSecondPlayerInitialTurn = SecondPlayerInitial == FirstPlayer;
    }

    public GameLogItem LastLogItem
    {
      get => lastLogItem;
      set
      {
        lastLogItem = value;
        RaisePropertyChanged(nameof (LastLogItem));
      }
    }

    public int Turn
    {
      get => turn;
      set
      {
        turn = value;
        RaisePropertyChanged(nameof (Turn));
      }
    }

    [XmlIgnore]
    public Player ActivePlayer
    {
      get
      {
        if (activePlayer != null)
          return activePlayer;
        return IsFirstPlayerTurn ? (activePlayer = FirstPlayer) : (activePlayer = SecondPlayer);
      }
      set
      {
        activePlayer = value;
        RaisePropertyChanged(nameof (ActivePlayer));
      }
    }

    [XmlIgnore]
    public Player InactivePlayer
    {
      get
      {
        if (inactivePlayer != null)
          return inactivePlayer;
        return IsFirstPlayerTurn ? (inactivePlayer = SecondPlayer) : (inactivePlayer = FirstPlayer);
      }
      set
      {
        inactivePlayer = value;
        RaisePropertyChanged(nameof (InactivePlayer));
      }
    }

    [XmlIgnore]
    public bool IsWaitingForUserInput
    {
      get => isWaitingForUserInput;
      set
      {
        isWaitingForUserInput = value;
        RaisePropertyChanged(nameof (IsWaitingForUserInput));
      }
    }

    public Field SelectedField
    {
      get => selectedField;
      set
      {
        selectedField = value;
        RaisePropertyChanged(nameof (SelectedField));
        if (value == null || value.IsEmpty)
          return;
        SelectedCardOrCardType = (Card) value.Card;
      }
    }

    public int RoundIndex
    {
      get => roundIndex;
      set
      {
        roundIndex = value;
        RaisePropertyChanged(nameof (RoundIndex));
      }
    }

    public Card LastUsedCard
    {
      get => lastUsedCard;
      set
      {
        lastUsedCard = value;
        RaisePropertyChanged(nameof (LastUsedCard));
      }
    }

    public bool IsFirstPlayerTurn
    {
      get => isFirstPlayerTurn;
      set
      {
        isFirstPlayerTurn = value;
        RaisePropertyChanged(nameof (IsFirstPlayerTurn));
        ActivePlayer = value ? FirstPlayer : SecondPlayer;
        InactivePlayer = value ? SecondPlayer : FirstPlayer;
      }
    }

    public Card SelectedCardOrCardType
    {
      get => selectedCardOrCardType;
      set
      {
        selectedCardOrCardType = value;
        RaisePropertyChanged(nameof (SelectedCardOrCardType));
        switch (value)
        {
          case null:
          case FakeCard _:
            SelectedCardOrCardTypeShortDesc = string.Empty;
            break;
          case CreatureCard _:
            SelectedCardOrCardTypeShortDesc = string.Format(CommonResources.CreatureShortDesc2, (object) value.DamageString, (object) value.Health, (object) value.Cost);
            break;
          case SpellCard _:
            SelectedCardOrCardTypeShortDesc = string.Format(CommonResources.SpellShortDesc2, (object) value.Cost);
            break;
        }
      }
    }

    public string SelectedCardOrCardTypeShortDesc
    {
      get => selectedCardOrCardTypeShortDesc;
      set
      {
        selectedCardOrCardTypeShortDesc = value;
        RaisePropertyChanged(nameof (SelectedCardOrCardTypeShortDesc));
      }
    }

    [XmlIgnore]
    public Element FirstPlayerSelectedElement
    {
      get => firstPlayerSelectedElement;
      set
      {
        DeselectFields();
        DeselectCardsInBook();
        if (value != null)
          value.IsSelected = true;
        firstPlayerSelectedElement = value;
        if (ActivePlayer != null)
          ActivePlayer.Elements.Where<Element>((Func<Element, bool>) (i => i != value)).ForEach<Element>((Action<Element>) (i => i.IsSelected = false));
        RaisePropertyChanged(nameof (FirstPlayerSelectedElement));
      }
    }

    [XmlIgnore]
    public Card SelectedCard
    {
      get => selectedCard;
      set
      {
        selectedCard = value;
        if (value != null)
          SelectedCardOrCardType = value;
        RaisePropertyChanged(nameof (SelectedCard));
        SelectCardInBook();
        SetIsWaitingForSelectClear();
        if (value == null || !IsFirstPlayerTurn || !value.IsActive)
          return;
        switch (value)
        {
          case CreatureCard _:
            ActivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => i.IsEmpty)).ForEach<Field>((Action<Field>) (i => i.IsWaitingForSelect = true));
            break;
          case SpellCard _:
            SpellCard spellCard = value as SpellCard;
            if (spellCard.Target == SpellTarget.OpponentsCard)
              InactivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)).ForEach<Field>((Action<Field>) (i => i.IsWaitingForSelect = true));
            if (spellCard.Target == SpellTarget.OpponentsCardNotSpecial)
            {
              InactivePlayer.Fields.Where<Field>((Func<Field, bool>) (i =>
              {
                if (i.IsEmpty)
                  return false;
                return i.Card.ElementType == ElementTypeEnum.Fire || i.Card.ElementType == ElementTypeEnum.Water || i.Card.ElementType == ElementTypeEnum.Air || i.Card.ElementType == ElementTypeEnum.Earth;
              })).ForEach<Field>((Action<Field>) (i => i.IsWaitingForSelect = true));
              break;
            }
            if (spellCard.Target == SpellTarget.OwnersCard)
            {
              ActivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)).ForEach<Field>((Action<Field>) (i => i.IsWaitingForSelect = true));
              break;
            }
            if (spellCard.Target == SpellTarget.IndeterminateOwnerEmpty)
            {
              ActivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => i.IsEmpty)).ForEach<Field>((Action<Field>) (i => i.IsWaitingForSelect = true));
              break;
            }
            if (spellCard.Target == SpellTarget.Indeterminate)
            {
              InactivePlayer.Fields.ForEach<Field>((Action<Field>) (i => i.IsWaitingForSelect = true));
              break;
            }
            if (spellCard.Target != SpellTarget.IndeterminateOwner)
              break;
            ActivePlayer.Fields.ForEach<Field>((Action<Field>) (i => i.IsWaitingForSelect = true));
            break;
        }
      }
    }

    public Card OpponentIsSummoningCard
    {
      get => opponentIsSummoningCard;
      set
      {
        opponentIsSummoningCard = value;
        RaisePropertyChanged(nameof (OpponentIsSummoningCard));
      }
    }

    public string OpponentIsSummoningCardTitle
    {
      get => opponentIsSummoningCardTitle;
      set
      {
        opponentIsSummoningCardTitle = value;
        RaisePropertyChanged(nameof (OpponentIsSummoningCardTitle));
      }
    }

    private void InitializeCommads()
    {
      FirstPlayerFieldSelect = (ICommand) new RelayCommand(FirstPlayerFieldSelectCommand);
      SecondPlayerFieldSelect = (ICommand) new RelayCommand(SecondPlayerFieldSelectCommand);
      CloseLog = (ICommand) new RelayCommand((Action) (() => ShowLogMode = false));
      ShowLog = (ICommand) new RelayCommand((Action) (() => ShowLogMode = true));
      SkipTurn = (ICommand) new RelayCommand(SkipTurnAction);
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
      SelectedField = FirstPlayer.Fields.First<Field>((Func<Field, bool>) (i => i.IsSelected));
      if (SelectedField.IsWaitingForSelect)
        UseSelectedCard();
      SetIsWaitingForSelectClear();
      SecondPlayer.Fields.ForEach<Field>((Action<Field>) (i => i.IsSelected = false));
      DeselectCardsInBook();
    }

    private void SecondPlayerFieldSelectCommand()
    {
      SelectedField = SecondPlayer.Fields.First<Field>((Func<Field, bool>) (i => i.IsSelected));
      if (SelectedField.IsWaitingForSelect)
        UseSelectedCard();
      SetIsWaitingForSelectClear();
      SelectedField = (Field) null;
      FirstPlayer.Fields.ForEach<Field>((Action<Field>) (i => i.IsSelected = false));
      DeselectCardsInBook();
    }

    private void SkipTurnAction()
    {
      SetIsWaitingForSelectClear();
      gameRulesEngine.SummonCard((Card) null, (Field) null);
    }
  }
}
