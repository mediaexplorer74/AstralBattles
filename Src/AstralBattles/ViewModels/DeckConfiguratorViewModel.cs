// Decompiled with JetBrains decompiler
// Type: AstralBattles.ViewModels.DeckConfiguratorViewModel
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Converters;
using AstralBattles.Core.Model;
using AstralBattles.Core.Services;
using AstralBattles.Localizations;
using AstralBattles.Model;
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Navigation;

#nullable disable
namespace AstralBattles.ViewModels
{
  public class DeckConfiguratorViewModel : ViewModelBaseEx
  {
    public const int LibraryCount = 12;
    public const int PlayerFieldsCount = 6;
    private DeckElement selectedElement;
    private ObservableCollection<DeckElement> deckElements;
    private Card selectedCard;
    private NavigationService navigationService;
    private ElementTextConverter elementTextConverter = new ElementTextConverter();
    private string selectedCardShortDescription;

    public DeckConfiguratorViewModel()
    {
      this.Done = new RelayCommand(new Action(this.DoneAction));
      this.Resort = new RelayCommand(new Action(this.ResortAction));
      this.DeckElements = new ObservableCollection<DeckElement>();
    }

    private void ResortAction()
    {
      this.IsBusy = true;
      foreach (DeckElement deckElement in (Collection<DeckElement>) this.DeckElements)
      {
        deckElement.Library = new ObservableCollection<DeckField>((IEnumerable<DeckField>) deckElement.Library.OrderBy<DeckField, int>((Func<DeckField, int>) (i => !i.IsEmpty ? i.Card.Cost : 100)).ThenBy<DeckField, int>((Func<DeckField, int>) (i => !i.IsEmpty ? i.Card.Level : 100)));
        deckElement.PlayerFields = new ObservableCollection<DeckField>((IEnumerable<DeckField>) deckElement.PlayerFields.OrderBy<DeckField, int>((Func<DeckField, int>) (i => !i.IsEmpty ? i.Card.Cost : 100)).ThenBy<DeckField, int>((Func<DeckField, int>) (i => !i.IsEmpty ? i.Card.Level : 100)));
      }
      this.IsBusy = false;
    }

    private DeckField SubscribeEvents(DeckField field)
    {
      field.RequestMoveEvent += new EventHandler(this.FieldRequestMoveEvent);
      field.PropertyChanged += new PropertyChangedEventHandler(this.FieldPropertyChanged);
      return field;
    }

    private void FieldRequestMoveEvent(object sender, EventArgs e)
    {
      if (!(sender is DeckField destField))
        return;
      this.MoveAction(destField);
    }

    private void FieldPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName != "IsSelected" || !(sender is DeckField))
        return;
      DeckField field = sender as DeckField;
      if (!field.IsEmpty && field.IsSelected)
        this.SelectedCard = field.Card;
      if (!field.IsSelected)
        return;
      this.SelectedElement.Library.Concat<DeckField>((IEnumerable<DeckField>) this.SelectedElement.PlayerFields).ForEach<DeckField>((Action<DeckField>) (i =>
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
      DeckField deckField = this.SelectedElement.Library.Concat<DeckField>((IEnumerable<DeckField>) this.SelectedElement.PlayerFields).FirstOrDefault<DeckField>((Func<DeckField, bool>) (i => i.IsSelected));
      if (deckField == null || deckField.IsEmpty || destField == null || !destField.IsWaitingForChoise)
        return;
      destField.Card = deckField.Card;
      deckField.Card = (Card) null;
      this.DeselectAll(true);
    }

    public void DeselectAll(bool full = false)
    {
      this.DeckElements.ForEach<DeckElement>((Action<DeckElement>) (i => i.Library.ForEach<DeckField>((Action<DeckField>) (j => j.IsSelected = false))));
      this.DeckElements.ForEach<DeckElement>((Action<DeckElement>) (i => i.PlayerFields.ForEach<DeckField>((Action<DeckField>) (j => j.IsSelected = false))));
      if (!full)
        return;
      this.ClearWaitingsForChoise();
    }

    public void ClearWaitingsForChoise()
    {
      this.DeckElements.ForEach<DeckElement>((Action<DeckElement>) (i => i.PlayerFields.ForEach<DeckField>((Action<DeckField>) (j => j.IsWaitingForChoise = false))));
      this.DeckElements.ForEach<DeckElement>((Action<DeckElement>) (i => i.Library.ForEach<DeckField>((Action<DeckField>) (j => j.IsWaitingForChoise = false))));
    }

    private void DoneAction()
    {
      PhoneApplicationService.Current.State["ConfiguratedDeck"] = (object) this.ToDeckObj(this.DeckElements);
      if (!this.navigationService.CanGoBack)
        return;
      this.navigationService.GoBack();
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
      get => this.selectedElement;
      set
      {
        this.selectedElement = value;
        this.RaisePropertyChanged(nameof (SelectedElement));
        this.DeselectAll(true);
      }
    }

    public RelayCommand Done { get; private set; }

    public RelayCommand Move { get; private set; }

    public RelayCommand Resort { get; private set; }

    public ObservableCollection<DeckElement> DeckElements
    {
      get => this.deckElements;
      set
      {
        this.deckElements = value;
        this.RaisePropertyChanged(nameof (DeckElements));
      }
    }

    public Card SelectedCard
    {
      get => this.selectedCard;
      set
      {
        if (this.selectedCard == value)
          return;
        this.selectedCard = value;
        this.RaisePropertyChanged(nameof (SelectedCard));
        if (this.selectedCard is SpellCard)
          this.SelectedCardShortDescription = string.Format(CommonResources.SpellShortDesc, this.elementTextConverter.Convert((object) this.selectedCard.ElementType, (Type) null, (object) null, (CultureInfo) null), (object) this.selectedCard.Cost);
        if (!(this.selectedCard is CreatureCard))
          return;
        this.SelectedCardShortDescription = string.Format(CommonResources.CreatureShortDesc, this.elementTextConverter.Convert((object) this.selectedCard.ElementType, (Type) null, (object) null, (CultureInfo) null), (object) this.selectedCard.Damage, (object) this.selectedCard.Health, (object) this.selectedCard.Cost);
      }
    }

    public void SetNextOrPreviousElement(bool next)
    {
      if (this.DeckElements.Count < 1)
        return;
      int num = this.DeckElements.IndexOf(this.SelectedElement);
      bool flag1 = num == this.DeckElements.Count - 1;
      bool flag2 = num == 0;
      this.SelectedElement = this.DeckElements[!flag1 || !next ? (!flag2 || next ? (!next ? num - 1 : num + 1) : this.DeckElements.Count - 1) : 0];
    }

    public void OnNavigatedTo(NavigationEventArgs e, NavigationService navigationService)
    {
      this.navigationService = navigationService;
      if (!(PhoneApplicationService.Current.State.PopValueOrDefault<object, string>("ConfiguratedDeck") is Deck deck))
        return;
      this.DeckElements = this.ToDeckElements(deck);
      this.SelectedElement = this.DeckElements.First<DeckElement>();
      this.SelectedCard = CardRegistry.GetCardByName("GoblinBerserker");
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
            PlayerFields = new ObservableCollection<DeckField>(item.Value.OrderBy<Card, int>((Func<Card, int>) (i => i != null ? i.Cost : 100)).ThenBy<Card, int>((Func<Card, int>) (i => i != null ? i.Level : 100)).Select<Card, DeckField>((Func<Card, DeckField>) (i => this.SubscribeEvents(new DeckField()
            {
              Card = i
            })))),
            Library = this.CreateLibrary(cardRegistry, item.Value)
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
      return new ObservableCollection<DeckField>(list.OrderBy<Card, int>((Func<Card, int>) (i => i != null ? i.Cost : 100)).ThenBy<Card, int>((Func<Card, int>) (i => i != null ? i.Level : 100)).Select<Card, DeckField>((Func<Card, DeckField>) (i => this.SubscribeEvents(new DeckField()
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
      get => this.selectedCardShortDescription;
      set
      {
        this.selectedCardShortDescription = value;
        this.RaisePropertyChanged(nameof (SelectedCardShortDescription));
      }
    }
  }
}
