// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Model.Player
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using AstralBattles.Core.Infrastructure;
using AstralBattles.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.Serialization;

#nullable disable
namespace AstralBattles.Core.Model
{
  [XmlInclude(typeof (CampaignOpponent))]
  public class Player : NotifyPropertyChangedBase, IHarmable
  {
    private string description;
    private string displayName;
    private ObservableCollection<Element> elements;
    private ObservableCollection<Field> fields;
    private int health;
    private bool isAi;
    private bool isOnTheTop;
    private string name;
    private string photo;
    private Specialization specialization;
    private ElementTypeEnum specialElement;
    private bool isStunned;

    public Player()
    {
      this.Elements = new ObservableCollection<Element>();
      this.Fields = new ObservableCollection<Field>();
    }

    public string[] CreaturesAtStart { get; set; }

    public ObservableCollection<Element> Elements
    {
      get => this.elements;
      set
      {
        this.elements = value;
        this.RaisePropertyChanged(nameof (Elements));
        this.UpdateCardsIsActive();
      }
    }

    public ObservableCollection<Field> Fields
    {
      get => this.fields;
      set
      {
        this.fields = value;
        this.RaisePropertyChanged(nameof (Fields));
        if (value == null)
          return;
        if (value.Count >= 0)
          this.SubscribeFieldsEvents((IEnumerable<Field>) value);
        value.CollectionChanged += new NotifyCollectionChangedEventHandler(this.FieldsCollectionChanged);
      }
    }

    [XmlIgnore]
    public IEnumerable<Field> BusyFields
    {
      get => this.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty));
    }

    public void ForEachBusyFields(Action<Field> actor) => this.BusyFields.ForEach<Field>(actor);

    public bool IsAi
    {
      get => this.isAi;
      set
      {
        this.isAi = value;
        this.RaisePropertyChanged(nameof (IsAi));
      }
    }

    public ElementTypeEnum SpecialElement
    {
      get => this.specialElement;
      set
      {
        this.specialElement = value;
        this.RaisePropertyChanged(nameof (SpecialElement));
      }
    }

    public bool IsStunned
    {
      get => this.isStunned;
      set
      {
        this.isStunned = value;
        this.RaisePropertyChanged(nameof (IsStunned));
      }
    }

    public string Photo
    {
      get => this.photo;
      set
      {
        this.photo = value;
        this.RaisePropertyChanged(nameof (Photo));
      }
    }

    public void UpdateCardsIsActive()
    {
      foreach (Element element in (Collection<Element>) this.Elements)
      {
        foreach (Card card in (Collection<Card>) element.Cards)
          card.IsActive = card.Cost <= element.Mana;
      }
      if (GameService.CurrentGame == null)
        return;
      GameService.CurrentGame.SkillsManager.OnElementPowerChanges(this);
    }

    public void IncreaseMana(ElementTypeEnum elementType, int value)
    {
      this.GetElementByType(elementType).IncreaseMana(value);
      this.UpdateCardsIsActive();
    }

    public void DecreaseMana(ElementTypeEnum elementType, int value)
    {
      this.GetElementByType(elementType).DecreaseMana(value);
      this.UpdateCardsIsActive();
    }

    public void IncreaseAllElementsPower(int power)
    {
      this.Elements.ForEach<Element>((Action<Element>) (i => i.IncreaseMana(power)));
      this.UpdateCardsIsActive();
    }

    public void IncreaseAllBasicElementsPower(int i)
    {
      this.GetElementByType(ElementTypeEnum.Fire).IncreaseMana(i);
      this.GetElementByType(ElementTypeEnum.Water).IncreaseMana(i);
      this.GetElementByType(ElementTypeEnum.Air).IncreaseMana(i);
      this.GetElementByType(ElementTypeEnum.Earth).IncreaseMana(i);
      this.UpdateCardsIsActive();
    }

    public Specialization Specialization
    {
      get => this.specialization;
      set
      {
        this.specialization = value;
        this.RaisePropertyChanged(nameof (Specialization));
      }
    }

    public int Health
    {
      get => this.health;
      set
      {
        this.health = value;
        this.RaisePropertyChanged(nameof (Health));
      }
    }

    public string Name
    {
      get => this.name;
      set
      {
        this.name = value;
        this.RaisePropertyChanged(nameof (Name));
      }
    }

    public string Description
    {
      get => this.description;
      set
      {
        this.description = value;
        this.RaisePropertyChanged(nameof (Description));
      }
    }

    public string DisplayName
    {
      get => this.displayName;
      set
      {
        this.displayName = value;
        this.RaisePropertyChanged(nameof (DisplayName));
      }
    }

    public bool IsOnTheTop
    {
      get => this.isOnTheTop;
      set
      {
        this.isOnTheTop = value;
        this.RaisePropertyChanged(nameof (IsOnTheTop));
      }
    }

    public int AiDifficult { get; set; }

    public int Kills { get; set; }

    public int Summons { get; set; }

    public int Deaths { get; set; }

    public bool DoHarm(int value, IBattlefield bf)
    {
      if (this.Fields.Any<Field>((Func<Field, bool>) (i => !i.IsEmpty && i.Card["ReduceDamageToOwnerByHalf"] != null)))
        value /= 2;
      Field field = this.BusyFields.FirstOrDefault<Field>((Func<Field, bool>) (i => i.Card["TakesDamageToOwnerToItselfInstead"] != null));
      if (field != null)
      {
        field.DoHarm(value, bf);
        return false;
      }
      this.Health -= value;
      this.GotDamage((object) this, new IntValueChangedEventArgs(value, (Action) null));
      if (this.Health < 1)
        this.Died((object) this, EventArgs.Empty);
      return this.Health < 1;
    }

    public void CreateFields()
    {
      ObservableCollection<Field> observableCollection = new ObservableCollection<Field>();
      observableCollection.Add(new Field(this));
      observableCollection.Add(new Field(this));
      observableCollection.Add(new Field(this));
      observableCollection.Add(new Field(this));
      observableCollection.Add(new Field(this));
      this.Fields = observableCollection;
      if (this.CreaturesAtStart == null)
        return;
      int index = 0;
      foreach (string name in this.CreaturesAtStart)
      {
        this.Fields[index].Card = CardRegistry.GetCardByName(name).Clone() as CreatureCard;
        ++index;
      }
      this.CreaturesAtStart = (string[]) null;
      this.UpdateCardsIsActive();
    }

    private void FieldSelected(object sender, EventArgs e)
    {
      this.Fields.Where<Field>((Func<Field, bool>) (i => i != sender)).ForEach<Field>((Action<Field>) (i => i.IsSelected = false));
    }

    private void FieldsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action != NotifyCollectionChangedAction.Add)
        return;
      this.SubscribeFieldsEvents(e.NewItems.OfType<Field>());
    }

    private void SubscribeFieldsEvents(IEnumerable<Field> fieldsToSubscribe)
    {
      fieldsToSubscribe.ForEach<Field>((Action<Field>) (i =>
      {
        i.Selected += new EventHandler(this.FieldSelected);
        i.Died += new EventHandler<IntValueChangedEventArgs>(this.CardDied);
        i.Kills += new EventHandler<IntValueChangedEventArgs>(this.CardKills);
        i.Summoned += new EventHandler<IntValueChangedEventArgs>(this.CardSummoned);
      }));
    }

    private void CardSummoned(object sender, IntValueChangedEventArgs e) => ++this.Summons;

    private void CardKills(object sender, IntValueChangedEventArgs e) => ++this.Kills;

    private void CardDied(object sender, IntValueChangedEventArgs e) => ++this.Deaths;

    public Element GetElementByType(ElementTypeEnum type)
    {
      return this.elements.First<Element>((Func<Element, bool>) (i => i.ElementType == type));
    }

    public void Cure(int value)
    {
      this.Health += value;
      this.GotHealth((object) this, new IntValueChangedEventArgs(value, (Action) null));
    }

    public event EventHandler Died = delegate { };

    public event EventHandler<IntValueChangedEventArgs> Stunned = delegate { };

    public event EventHandler<IntValueChangedEventArgs> GotDamage = delegate { };

    public event EventHandler<IntValueChangedEventArgs> GotHealth = delegate { };

    public override string ToString() => string.Format(this.Name);

    public void RaiseStunned(Action callback)
    {
      this.Stunned((object) this, new IntValueChangedEventArgs(0, callback));
    }
  }
}
