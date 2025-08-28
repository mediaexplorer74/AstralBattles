// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Infrastructure.ILogger
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using System;


namespace AstralBattles.Core.Infrastructure
{
  public interface ILogger
  {
    void LogError(Exception exc);

    void LogWarrning(string msg, params object[] args);

    void Log(string msg, params object[] args);
  }
}
