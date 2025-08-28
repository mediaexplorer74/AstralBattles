// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Model.IntValueChangedEventArgs
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using System;


namespace AstralBattles.Core.Model
{
  public class IntValueChangedEventArgs : EventArgs
  {
    public IntValueChangedEventArgs(int value, Action callback)
    {
      this.Value = value;
      this.Callback = callback;
    }

    public int Value { get; private set; }

    public Action Callback { get; private set; }
  }
}
