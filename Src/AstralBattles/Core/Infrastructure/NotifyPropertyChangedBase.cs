// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Infrastructure.NotifyPropertyChangedBase
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using System.ComponentModel;


namespace AstralBattles.Core.Infrastructure
{
  public class NotifyPropertyChangedBase : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged = delegate { };

    protected void RaisePropertyChanged(string property)
    {
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(property));
    }


  }
}
