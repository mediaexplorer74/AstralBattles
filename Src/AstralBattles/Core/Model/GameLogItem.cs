// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Model.GameLogItem
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll


namespace AstralBattles.Core.Model
{
  public class GameLogItem
  {
    public GameLogItem()
    {
    }

    public GameLogItem(string format, params object[] args)
    {
      this.Text = string.Format(format, args);
    }

    public bool IsNewRoundLog { get; set; }

    public string Text { get; set; }

    public override string ToString() => this.Text;
  }
}
