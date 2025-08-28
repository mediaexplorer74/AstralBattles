// Decompiled with JetBrains decompiler
// Type: AstralBattles.ServiceReference.GameInfo
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;


namespace AstralBattles.ServiceReference
{
  [DebuggerStepThrough]
  [DataContract(Name = "GameInfo", Namespace = "http://schemas.datacontract.org/2004/07/AstralBattles.Server.Service.DataContracts")]
  [GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
  public class GameInfo : INotifyPropertyChanged
  {
    private bool isSelected;
    private string DescriptionField;
    private string HosterCountryField;
    private int HosterIdField;
    private string HosterNameField;
    private int IdField;
    private DateTime StartedAtField;

    public int Place { get; set; }

    public bool IsSelected
    {
      get => this.isSelected;
      set
      {
        this.isSelected = value;
        this.RaisePropertyChanged(nameof (IsSelected));
      }
    }

    [DataMember]
    public string Description
    {
      get => this.DescriptionField;
      set
      {
        if (object.ReferenceEquals((object) this.DescriptionField, (object) value))
          return;
        this.DescriptionField = value;
        this.RaisePropertyChanged(nameof (Description));
      }
    }

    [DataMember]
    public string HosterCountry
    {
      get => this.HosterCountryField;
      set
      {
        if (object.ReferenceEquals((object) this.HosterCountryField, (object) value))
          return;
        this.HosterCountryField = value;
        this.RaisePropertyChanged(nameof (HosterCountry));
      }
    }

    [DataMember]
    public int HosterId
    {
      get => this.HosterIdField;
      set
      {
        if (this.HosterIdField.Equals(value))
          return;
        this.HosterIdField = value;
        this.RaisePropertyChanged(nameof (HosterId));
      }
    }

    [DataMember]
    public string HosterName
    {
      get => this.HosterNameField;
      set
      {
        if (object.ReferenceEquals((object) this.HosterNameField, (object) value))
          return;
        this.HosterNameField = value;
        this.RaisePropertyChanged(nameof (HosterName));
      }
    }

    [DataMember]
    public int Id
    {
      get => this.IdField;
      set
      {
        if (this.IdField.Equals(value))
          return;
        this.IdField = value;
        this.RaisePropertyChanged(nameof (Id));
      }
    }

    [DataMember]
    public DateTime StartedAt
    {
      get => this.StartedAtField;
      set
      {
        if (this.StartedAtField.Equals(value))
          return;
        this.StartedAtField = value;
        this.RaisePropertyChanged(nameof (StartedAt));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void RaisePropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
