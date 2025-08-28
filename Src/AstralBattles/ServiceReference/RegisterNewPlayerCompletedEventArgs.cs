// Decompiled with JetBrains decompiler
// Type: AstralBattles.ServiceReference.RegisterNewPlayerCompletedEventArgs
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;


namespace AstralBattles.ServiceReference
{
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  [DebuggerStepThrough]
  public class RegisterNewPlayerCompletedEventArgs : AsyncCompletedEventArgs
  {
    private object[] results;

    public RegisterNewPlayerCompletedEventArgs(
      object[] results,
      Exception exception,
      bool cancelled,
      object userState)
      : base(exception, cancelled, userState)
    {
      this.results = results;
    }

    public PlayerInfo Result
    {
      get
      {
        this.RaiseExceptionIfNecessary();
        return (PlayerInfo) this.results[0];
      }
    }
  }
}
