// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Model.SpellCard
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll


namespace AstralBattles.Core.Model
{
  public class SpellCard : Card
  {
    public SpellTarget Target { get; set; }

    public string AbilityOf { get; set; }

    public bool IsAbility => !string.IsNullOrWhiteSpace(this.AbilityOf);

    public override Card Clone()
    {
      SpellCard spellCard = new SpellCard();
      spellCard.Cost = this.Cost;
      spellCard.Skills = this.Skills;
      spellCard.IsActive = this.IsActive;
      spellCard.ElementType = this.ElementType;
      spellCard.Health = this.Health;
      spellCard.AbilityOf = this.AbilityOf;
      spellCard.Level = this.Level;
      spellCard.DisplayName = this.DisplayName;
      spellCard.Description = this.Description;
      spellCard.Damage = this.Damage;
      spellCard.Target = this.Target;
      spellCard.Localization = this.Localization;
      spellCard.IsHidden = this.IsHidden;
      spellCard.Name = this.Name;
      return (Card) spellCard;
    }
  }
}
