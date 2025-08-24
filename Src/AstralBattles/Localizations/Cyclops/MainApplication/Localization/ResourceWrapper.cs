// Decompiled with JetBrains decompiler
// Type: AstralBattles.Localizations.Cyclops.MainApplication.Localization.ResourceWrapper
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using System.ComponentModel;

#nullable disable
namespace AstralBattles.Localizations.Cyclops.MainApplication.Localization
{
  public class ResourceWrapper : INotifyPropertyChanged
  {
    private CommonResources commonResources = new CommonResources();

    public CommonResources CommonResources
    {
      get => this.commonResources;
      set
      {
        this.commonResources = value;
        this.OnPropertyChanged(nameof (CommonResources));
      }
    }

    public void Refresh() => this.CommonResources = new CommonResources();

    public event PropertyChangedEventHandler PropertyChanged = delegate { };

    protected void OnPropertyChanged(string name)
    {
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(name));
    }
  }
}
