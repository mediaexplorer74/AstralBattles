// Decompiled with JetBrains decompiler
// Type: AstralBattles.ServiceReference.GetAvailableGamesCompletedEventArgs
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace AstralBattles.ServiceReference
{
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  public class GetAvailableGamesCompletedEventArgs : AsyncCompletedEventArgs
  {
    private object[] results;

    public GetAvailableGamesCompletedEventArgs(
      object[] results,
      Exception exception,
      bool cancelled,
      object userState)
      : base(exception, cancelled, userState)
    {
      this.results = results;
    }

    public ObservableCollection<GameInfo> Result
    {
      get
      {
        this.RaiseExceptionIfNecessary();
        return (ObservableCollection<GameInfo>) this.results[0];
      }
    }
  }
}
