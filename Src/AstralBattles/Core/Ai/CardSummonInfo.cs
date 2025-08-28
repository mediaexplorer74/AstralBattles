// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Ai.CardSummonInfo
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using AstralBattles.Core.Model;


namespace AstralBattles.Core.Ai
{
  public class CardSummonInfo
  {
    public bool Success { get; set; }

    public Field Field { get; set; }

    public Card Card { get; set; }

    public bool IsSpell => this.Card is SpellCard;

    public SpellTarget SpellTarget => ((SpellCard) this.Card).Target;

    public bool IsOpponentsField { get; set; }
  }
}
