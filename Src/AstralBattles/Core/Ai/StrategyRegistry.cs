// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Ai.StrategyRegistry
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll


namespace AstralBattles.Core.Ai
{
  public static class StrategyRegistry
  {
    public static IStrategy[] Strategies = (IStrategy[]) new HitStrongMonsterStrategy[1]
    {
      new HitStrongMonsterStrategy()
    };
  }
}
