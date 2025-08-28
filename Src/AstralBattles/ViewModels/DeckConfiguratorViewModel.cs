using AstralBattles.Converters;
using AstralBattles.Core.Model;
using AstralBattles.Core.Services;
using AstralBattles.Localizations;
using AstralBattles.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Windows.UI.Xaml.Navigation;
using AstralBattles.Core.Infrastructure;

namespace AstralBattles.ViewModels
{
   public class DeckConfiguratorViewModel : ViewModelBaseEx
  {
    public const int LibraryCount = 12;
    public const int PlayerFieldsCount = 6;
    private DeckElement selectedElement;
    private ObservableCollection<DeckElement> deckElements;
    private Card selectedCard;
    private Core.Infrastructure.NavigationService navigationService;
    private ElementTextConverter elementTextConverter = new ElementTextConverter();
    private string selectedCardShortDescription;

    public DeckConfiguratorViewModel()
    {
      Done = new RelayCommand(DoneAction);
      Resort = new RelayCommand(ResortAction);
      Move = new RelayCommand(_ => { /* Move logic handled by MoveAction method */ });
      DeckElements = new ObservableCollection<DeckElement>();
    }

    private void ResortAction()
    {
      IsBusy = true;
      foreach (DeckElement deckElement in (Collection<DeckElement>) DeckElements)
      {
        deckElement.Library = new ObservableCollection<DeckField>((IEnumerable<DeckField>) deckElement.Library.OrderBy<DeckField, int>((Func<DeckField, int>) (i => !i.IsEmpty ? i.Card.Cost : 100)).ThenBy<DeckField, int>((Func<DeckField, int>) (i => !i.IsEmpty ? i.Card.Level : 100)));
        deckElement.PlayerFields = new ObservableCollection<DeckField>((IEnumerable<DeckField>) deckElement.PlayerFields.OrderBy<DeckField, int>((Func<DeckField, int>) (i => !i.IsEmpty ? i.Card.Cost : 100)).ThenBy<DeckField, int>((Func<DeckField, int>) (i => !i.IsEmpty ? i.Card.Level : 100)));
      }
      IsBusy = false;
    }

    private DeckField SubscribeEvents(DeckField field)
    {
      field.RequestMoveEvent += new EventHandler(FieldRequestMoveEvent);
      field.PropertyChanged += new PropertyChangedEventHandler(FieldPropertyChanged);
      return field;
    }

    private void FieldRequestMoveEvent(object sender, EventArgs e)
    {
      if (!(sender is DeckField destField))
        return;
      MoveAction(destField);
    }

    private void FieldPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName != "IsSelected" || !(sender is DeckField))
        return;
      DeckField field = sender as DeckField;
      if (!field.IsEmpty && field.IsSelected)
        SelectedCard = field.Card;
      if (!field.IsSelected)
        return;
      SelectedElement.Library.Concat<DeckField>((IEnumerable<DeckField>) SelectedElement.PlayerFields).ForEach<DeckField>((Action<DeckField>) (i =>
      {
        if (i != field)
          i.IsSelected = false;
        if (field.IsEmpty)
          return;
        if (i.IsEmpty)
          i.IsWaitingForChoise = true;
        else
          i.IsWaitingForChoise = false;
      }));
    }

    public void MoveAction(DeckField destField)
    {
      DeckField deckField = SelectedElement.Library.Concat<DeckField>((IEnumerable<DeckField>) SelectedElement.PlayerFields).FirstOrDefault<DeckField>((Func<DeckField, bool>) (i => i.IsSelected));
      if (deckField == null || deckField.IsEmpty || destField == null || !destField.IsWaitingForChoise)
        return;
      destField.Card = deckField.Card;
      deckField.Card = (Card) null;
      DeselectAll(true);
    }

    public void DeselectAll(bool full = false)
    {
      DeckElements.ForEach<DeckElement>((Action<DeckElement>) (i => i.Library.ForEach<DeckField>((Action<DeckField>) (j => j.IsSelected = false))));
      DeckElements.ForEach<DeckElement>((Action<DeckElement>) (i => i.PlayerFields.ForEach<DeckField>((Action<DeckField>) (j => j.IsSelected = false))));
      if (!full)
        return;
      ClearWaitingsForChoise();
    }

    public void ClearWaitingsForChoise()
    {
      DeckElements.ForEach<DeckElement>((Action<DeckElement>) (i => i.PlayerFields.ForEach<DeckField>((Action<DeckField>) (j => j.IsWaitingForChoise = false))));
      DeckElements.ForEach<DeckElement>((Action<DeckElement>) (i => i.Library.ForEach<DeckField>((Action<DeckField>) (j => j.IsWaitingForChoise = false))));
    }

    private void DoneAction()
    {
      Windows.Storage.ApplicationData.Current.LocalSettings.Values["ConfiguratedDeck"] = (object) ToDeckObj(DeckElements);
      if (!navigationService.CanGoBack)
        return;
      navigationService.GoBack();
    }

    private Deck ToDeckObj(ObservableCollection<DeckElement> deckElement)
    {
      Deck deckObj = new Deck();
      foreach (DeckElement deckElement1 in (Collection<DeckElement>) deckElement)
        deckObj[deckElement1.ElementType] = new ObservableCollection<Card>((IEnumerable<Card>) deckElement1.PlayerFields.Select<DeckField, Card>((Func<DeckField, Card>) (i => i.Card)).OrderBy<Card, int>((Func<Card, int>) (i => i != null ? i.Cost : 100)).ThenBy<Card, int>((Func<Card, int>) (i => i != null ? i.Level : 100)));
      return deckObj;
    }

    public DeckElement SelectedElement
    {
      get => selectedElement;
      set
      {
        selectedElement = value;
        RaisePropertyChanged(nameof (SelectedElement));
        DeselectAll(true);
      }
    }

    public RelayCommand Done { get; private set; }

    public RelayCommand Move { get; private set; }

    public RelayCommand Resort { get; private set; }

    public ObservableCollection<DeckElement> DeckElements
    {
      get => deckElements;
      set
      {
        deckElements = value;
        RaisePropertyChanged(nameof (DeckElements));
      }
    }

    public Card SelectedCard
    {
      get => selectedCard;
      set
      {
        if (selectedCard == value)
          return;
        selectedCard = value;
        RaisePropertyChanged(nameof (SelectedCard));
        if (selectedCard is SpellCard)
          SelectedCardShortDescription = string.Format(CommonResources.SpellShortDesc,
              elementTextConverter.Convert((object) selectedCard.ElementType, (Type) null, (object) null, null),
              (object) selectedCard.Cost);
        if (!(selectedCard is CreatureCard))
          return;
        SelectedCardShortDescription = string.Format(CommonResources.CreatureShortDesc, 
            elementTextConverter.Convert((object) selectedCard.ElementType, (Type) null, (object) null, null),
            (object) selectedCard.Damage, (object) selectedCard.Health, (object) selectedCard.Cost);
      }
    }

    public void SetNextOrPreviousElement(bool next)
    {
      if (DeckElements.Count < 1)
        return;
      int num = DeckElements.IndexOf(SelectedElement);
      bool flag1 = num == DeckElements.Count - 1;
      bool flag2 = num == 0;
      SelectedElement = DeckElements[!flag1 || !next ? (!flag2 || next ? (!next ? num - 1 : num + 1) : DeckElements.Count - 1) : 0];
    }

    public void OnNavigatedTo(NavigationEventArgs e, Core.Infrastructure.NavigationService navigationService)
    {
      navigationService = navigationService;
      if (!(Windows.Storage.ApplicationData.Current.LocalSettings.Values.PopValueOrDefault<object, string>("ConfiguratedDeck")
                is Deck deck))
        return;
      DeckElements = ToDeckElements(deck);
      SelectedElement = DeckElements.First<DeckElement>();
      SelectedCard = CardRegistry.GetCardByName("GoblinBerserker");
      int navigationMode = (int) e.NavigationMode;
    }

    private ObservableCollection<DeckElement> ToDeckElements(Deck deck)
    {
      ObservableCollection<DeckElement> deckElements = new ObservableCollection<DeckElement>();
      if (deck == null)
        return deckElements;
      using (Dictionary<ElementTypeEnum, ObservableCollection<Card>>.Enumerator enumerator = deck.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<ElementTypeEnum, ObservableCollection<Card>> item = enumerator.Current;
          IEnumerable<Card> cardRegistry = CardRegistry.Cards.Where<Card>((Func<Card, bool>) (i => i.ElementType == item.Key));
          deckElements.Add(new DeckElement()
          {
            ElementType = item.Key,
            PlayerFields = new ObservableCollection<DeckField>(item.Value.OrderBy<Card, int>((Func<Card, int>) (i => i != null ? i.Cost : 100)).ThenBy<Card, int>((Func<Card, int>) (i => i != null ? i.Level : 100)).Select<Card, DeckField>((Func<Card, DeckField>) (i => SubscribeEvents(new DeckField()
            {
              Card = i
            })))),
            Library = CreateLibrary(cardRegistry, item.Value)
          });
        }
      }
      return deckElements;
    }

    private ObservableCollection<DeckField> CreateLibrary(
      IEnumerable<Card> cardRegistry,
      ObservableCollection<Card> playerCards)
    {
      List<Card> list = cardRegistry.Where<Card>((Func<Card, bool>) (i => !i.IsHidden && !playerCards.Any<Card>((Func<Card, bool>) (c => c != null && c.Name == i.Name)))).Take<Card>(12).ToList<Card>();
      while (list.Count < 12)
        list.Add((Card) null);
      return new ObservableCollection<DeckField>(list.OrderBy<Card, int>((Func<Card, int>) (i => i != null ? i.Cost : 100)).ThenBy<Card, int>((Func<Card, int>) (i => i != null ? i.Level : 100)).Select<Card, DeckField>((Func<Card, DeckField>) (i => SubscribeEvents(new DeckField()
      {
        IsLibrary = true,
        Card = i
      }))));
    }

    public void OnNavigatedFrom(NavigationEventArgs navigationEventArgs)
    {
    }

    public string SelectedCardShortDescription
    {
      get => selectedCardShortDescription;
      set
      {
        selectedCardShortDescription = value;
        RaisePropertyChanged(nameof (SelectedCardShortDescription));
      }
    }
  }
}
