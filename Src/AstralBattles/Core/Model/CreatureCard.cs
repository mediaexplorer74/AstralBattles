// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Model.CreatureCard
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

#nullable disable
namespace AstralBattles.Core.Model
{
  public class CreatureCard : Card
  {
    public bool IsJustSummoned { get; set; }

    public int InitialDamage { get; set; }

    public int InitialHealth { get; set; }

    public string Ability { get; set; }

    public string ReplacedBy { get; set; }

    public override Card Clone()
    {
      CreatureCard creatureCard = new CreatureCard();
      creatureCard.Cost = this.Cost;
      creatureCard.Skills = this.Skills;
      creatureCard.IsActive = this.IsActive;
      creatureCard.ElementType = this.ElementType;
      creatureCard.Health = this.Health;
      creatureCard.Level = this.Level;
      creatureCard.DisplayName = this.DisplayName;
      creatureCard.Description = this.Description;
      creatureCard.ReplacedBy = this.ReplacedBy;
      creatureCard.Damage = this.Damage;
      creatureCard.InitialDamage = this.Damage;
      creatureCard.IsDamageIndeterminated = this.IsDamageIndeterminated;
      creatureCard.Ability = this.Ability;
      creatureCard.InitialHealth = this.Health;
      creatureCard.IsNotAttackable = this.IsNotAttackable;
      creatureCard.IsJustSummoned = true;
      creatureCard.Localization = this.Localization;
      creatureCard.IsHidden = this.IsHidden;
      creatureCard.Name = this.Name;
      return (Card) creatureCard;
    }

    public bool IsNotAttackable { get; set; }
  }
}
