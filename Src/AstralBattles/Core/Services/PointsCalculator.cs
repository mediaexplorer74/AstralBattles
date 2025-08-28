// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Services.PointsCalculator
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll


namespace AstralBattles.Core.Services
{
  public static class PointsCalculator
  {
    public static int Calculate(int rounds, int kills, int deaths)
    {
      int num = (int) (((double) kills * 2.8 + 2.5) / ((double) rounds * 1.1 + (double) deaths * 1.1 + 1.0) * 100.0);
      if (num < 1)
        num = 1;
      if (num > 100)
        num = 100;
      return num;
    }
  }
}
