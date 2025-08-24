// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Infrastructure.DefaultLogger
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using System;

#nullable disable
namespace AstralBattles.Core.Infrastructure
{
  public class DefaultLogger : ILogger
  {
    private readonly Type type;
    private static readonly object SyncRoot = new object();

    public DefaultLogger(Type type) => this.type = type;

    public void LogError(Exception exc)
    {
      if (exc == null)
        return;
      this.LogInternal(string.Format("\r\n-----------{2}------------Error: {0}; StackTrace: {1}\r\n-------------------------", (object) exc.Message, (object) exc.StackTrace, (object) this.type.Name));
    }

    public void LogWarrning(string msg, params object[] args)
    {
      this.LogInternal("WARRNING " + this.type.Name + ": " + string.Format(msg, args));
    }

    public void Log(string msg, params object[] args)
    {
      this.LogInternal("INFO " + this.type.Name + ": " + string.Format(msg, args));
    }

    private void LogInternal(string msg)
    {
    }
  }
}
