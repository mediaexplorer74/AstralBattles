// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Services.PlayersRegistry
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using System.Collections.Generic;


namespace AstralBattles.Core.Services
{
  public class PlayersRegistry : IPlayersRegistry
  {
    private static List<string> list = new List<string>()
    {
      "Lina",
      "Batalus",
      "Starper",
      "Frida",
      "Izza",
      "Vatrax",
      "Roland",
      "Kassandra",
      "Forwor"
    };

    public List<string> GetNames() => PlayersRegistry.list;
  }
}
