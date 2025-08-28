// Decompiled with JetBrains decompiler
// Type: AstralBattles.ServiceReference.PlayerInfo
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;


namespace AstralBattles.ServiceReference
{
  [GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
  [DebuggerStepThrough]
  [DataContract(Name = "PlayerInfo", Namespace = "http://schemas.datacontract.org/2004/07/AstralBattles.Server.Service.DataContracts")]
  public class PlayerInfo : INotifyPropertyChanged
  {
    private string CountryField;
    private int GamesField;
    private int IdField;
    private int LeavesField;
    private int LosesField;
    private string NameField;
    private string PhotoNameField;
    private int WinsField;

    public int Place { get; set; }

    [DataMember]
    public string Country
    {
      get => this.CountryField;
      set
      {
        if (object.ReferenceEquals((object) this.CountryField, (object) value))
          return;
        this.CountryField = value;
        this.RaisePropertyChanged(nameof (Country));
      }
    }

    [DataMember]
    public int Games
    {
      get => this.GamesField;
      set
      {
        if (this.GamesField.Equals(value))
          return;
        this.GamesField = value;
        this.RaisePropertyChanged(nameof (Games));
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
    public int Leaves
    {
      get => this.LeavesField;
      set
      {
        if (this.LeavesField.Equals(value))
          return;
        this.LeavesField = value;
        this.RaisePropertyChanged(nameof (Leaves));
      }
    }

    [DataMember]
    public int Loses
    {
      get => this.LosesField;
      set
      {
        if (this.LosesField.Equals(value))
          return;
        this.LosesField = value;
        this.RaisePropertyChanged(nameof (Loses));
      }
    }

    [DataMember]
    public string Name
    {
      get => this.NameField;
      set
      {
        if (object.ReferenceEquals((object) this.NameField, (object) value))
          return;
        this.NameField = value;
        this.RaisePropertyChanged(nameof (Name));
      }
    }

    [DataMember]
    public string PhotoName
    {
      get => this.PhotoNameField;
      set
      {
        if (object.ReferenceEquals((object) this.PhotoNameField, (object) value))
          return;
        this.PhotoNameField = value;
        this.RaisePropertyChanged(nameof (PhotoName));
      }
    }

    [DataMember]
    public int Wins
    {
      get => this.WinsField;
      set
      {
        if (this.WinsField.Equals(value))
          return;
        this.WinsField = value;
        this.RaisePropertyChanged(nameof (Wins));
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
