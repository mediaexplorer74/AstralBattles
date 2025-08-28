// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Model.Element
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using AstralBattles.Core.Infrastructure;
using System;
using System.Collections.ObjectModel;


namespace AstralBattles.Core.Model
{
  public class Element : NotifyPropertyChangedBase
  {
    private ElementTypeEnum elementType;
    private ObservableCollection<Card> cards;
    private int powerValue;
    private bool isSelected;

    public Element() => this.Cards = new ObservableCollection<Card>();

    public ElementTypeEnum ElementType
    {
      get => this.elementType;
      set
      {
        this.elementType = value;
        this.RaisePropertyChanged("PowerValue");
      }
    }

    public int Mana
    {
      get => this.powerValue;
      set
      {
        this.powerValue = value;
        this.RaisePropertyChanged(nameof (Mana));
      }
    }

    public ObservableCollection<Card> Cards
    {
      get => this.cards;
      set
      {
        this.cards = value;
        this.RaisePropertyChanged(nameof (Cards));
      }
    }

    public bool IsSelected
    {
      get => this.isSelected;
      set
      {
        this.isSelected = value;
        this.RaisePropertyChanged(nameof (IsSelected));
      }
    }

    public event EventHandler<IntValueChangedEventArgs> ManaDecreased = delegate { };

    public event EventHandler<IntValueChangedEventArgs> ManaIncreased = delegate { };

    public void DecreaseMana(int cost)
    {
      if (this.Mana < cost)
        this.Mana = 0;
      else
        this.Mana -= cost;
      this.ManaDecreased((object) this, new IntValueChangedEventArgs(cost, (Action) null));
    }

    public void IncreaseMana(int cost)
    {
      if (cost < 0)
      {
        this.DecreaseMana(-cost);
      }
      else
      {
        this.Mana += cost;
        this.ManaIncreased((object) this, new IntValueChangedEventArgs(cost, (Action) null));
      }
    }

    public override string ToString()
    {
      return string.Format("Element {0} ({1})", (object) this.ElementType, (object) this.Mana);
    }
  }
}
