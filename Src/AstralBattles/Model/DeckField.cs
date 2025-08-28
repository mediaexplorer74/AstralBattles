// Decompiled with JetBrains decompiler
// Type: AstralBattles.Model.DeckField
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Model;
using AstralBattles.ViewModels;
using System;


namespace AstralBattles.Model
{
  public class DeckField : ViewModelBaseEx
  {
    private Card card;
    private bool isSelected;
    private bool isWaitingForChoise;
    private bool isLibrary;

    public Card Card
    {
      get => this.card;
      set
      {
        this.card = value;
        this.RaisePropertyChanged(nameof (Card));
        this.RaisePropertyChanged("IsEmpty");
      }
    }

    public bool IsEmpty => this.Card == null;

    public bool IsSelected
    {
      get => this.isSelected;
      set
      {
        this.isSelected = value;
        this.RaisePropertyChanged(nameof (IsSelected));
      }
    }

    public bool IsWaitingForChoise
    {
      get => this.isWaitingForChoise;
      set
      {
        this.isWaitingForChoise = value;
        this.RaisePropertyChanged(nameof (IsWaitingForChoise));
      }
    }

    public bool IsLibrary
    {
      get => this.isLibrary;
      set
      {
        this.isLibrary = value;
        this.RaisePropertyChanged(nameof (IsLibrary));
      }
    }

    public event EventHandler RequestMoveEvent = delegate { };

    public void RequestMove() => this.RequestMoveEvent((object) this, EventArgs.Empty);
  }
}
