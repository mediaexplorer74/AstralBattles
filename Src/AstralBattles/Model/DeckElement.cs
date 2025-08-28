// Decompiled with JetBrains decompiler
// Type: AstralBattles.Model.DeckElement
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Model;
using AstralBattles.ViewModels;
using System.Collections.ObjectModel;


namespace AstralBattles.Model
{
  public class DeckElement : ViewModelBaseEx
  {
    private ElementTypeEnum elementType;
    private ObservableCollection<DeckField> library;
    private ObservableCollection<DeckField> playerFields;
    private bool isSelected;

    public ElementTypeEnum ElementType
    {
      get => this.elementType;
      set
      {
        this.elementType = value;
        this.RaisePropertyChanged(nameof (ElementType));
      }
    }

    public ObservableCollection<DeckField> Library
    {
      get => this.library;
      set
      {
        this.library = value;
        this.RaisePropertyChanged(nameof (Library));
      }
    }

    public ObservableCollection<DeckField> PlayerFields
    {
      get => this.playerFields;
      set
      {
        this.playerFields = value;
        this.RaisePropertyChanged(nameof (PlayerFields));
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
  }
}
