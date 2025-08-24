// Decompiled with JetBrains decompiler
// Type: AstralBattles.ViewModels.ViewModelBaseEx
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Localizations.Cyclops.MainApplication.Localization;
using GalaSoft.MvvmLight;
using System;
using System.Windows;
using System.Xml.Serialization;

#nullable disable
namespace AstralBattles.ViewModels
{
  public class ViewModelBaseEx : ViewModelBase
  {
    private bool isBusy;
    private ResourceWrapper resources;

    public ViewModelBaseEx()
    {
      this.Resources = Application.Current.Resources[(object) "ResourceWrapper"] as ResourceWrapper;
    }

    public bool IsBusy
    {
      get => this.isBusy;
      set
      {
        this.isBusy = value;
        this.RaisePropertyChanged(nameof (IsBusy));
      }
    }

    [XmlIgnore]
    public ResourceWrapper Resources
    {
      get
      {
        if (this.resources == null)
          this.Resources = Application.Current.Resources[(object) "ResourceWrapper"] as ResourceWrapper;
        return this.resources;
      }
      set
      {
        this.resources = value;
        this.RaisePropertyChanged(nameof (Resources));
      }
    }

    public DateTime SerializationDate { get; set; }
  }
}
