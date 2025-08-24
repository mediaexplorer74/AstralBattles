// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Ai.SmartestComputer
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using AstralBattles.Core.Infrastructure;
using AstralBattles.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace AstralBattles.Core.Ai
{
  public class SmartestComputer : ComputerIntellect
  {
    private static readonly ILogger Logger = LogFactory.GetLogger<SmartestComputer>();
    public static readonly Random Random = new Random(Environment.TickCount);
    private static string[] MassDestructionSpells = new string[6]
    {
      "Armageddon",
      "StoneRain",
      "DrainSouls",
      "CursedFog",
      "AcidicRain",
      "DivineJustice"
    };
    private static string[] SafeMassDestructionSpells = new string[15]
    {
      "Inferno",
      "FlameWave",
      "ChainLightning",
      "DarkRitual",
      "BloodRitual",
      "SonicBoom",
      "ManaBurn",
      "Fireball",
      "Disintegrate",
      "Cannonade",
      "Madness",
      "WrathofGod",
      "PoisonousCloud",
      "ChaoticWave",
      "HellFire"
    };
    private static string[] DealDamageToCreatureSpells = new string[4]
    {
      "Inferno",
      "Fireball",
      "DoomBolt",
      "CalltoThunder"
    };
    private static string[] ManaMiners = new string[9]
    {
      "elfHermit",
      "PriestOfFire",
      "MerfolkElder",
      "MerfolkApostate",
      "KeeperofDeath",
      "Hypnotist",
      "DwarvenCraftsman",
      "InsanianShaman",
      "DemonQuartermaster"
    };
    private static string[] ToBeKilledAsapCreatures = new string[5]
    {
      "AstralGuard",
      "MindMaster",
      "LightningCloud",
      "Vampire",
      "MasterLich"
    };

    public bool GetTrueRandomly(int percentage)
    {
      return SmartestComputer.Random.Next(0, 101) <= percentage;
    }

    public override Card GetCard(out Field field)
    {
      try
      {
        Card cardInternal = this.GetCardInternal(out field);
        if (cardInternal is SpellCard)
        {
          SpellCard spellCard = cardInternal as SpellCard;
          if ((spellCard.Target == SpellTarget.Indeterminate || spellCard.Target == SpellTarget.OpponentsCard) && this.Battlefield.ActivePlayer.Fields.Contains(field))
          {
            field = this.Battlefield.InactivePlayer.BusyFields.FirstOrDefault<Field>();
            if (field == null)
              return this.GetCardInternal(out field);
          }
        }
        return cardInternal;
      }
      catch (Exception ex)
      {
        SmartestComputer.Logger.LogError(ex);
        DebugHelper.Break();
        field = (Field) null;
        return (Card) null;
      }
    }

    private Card GetCardInternal(out Field field)
    {
      int roundIndex = this.Battlefield.RoundIndex;
      Player me = this.Battlefield.ActivePlayer;
      Player inactivePlayer = this.Battlefield.InactivePlayer;
      List<Field> list1 = inactivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)).ToList<Field>();
      List<Field> list2 = me.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)).ToList<Field>();
      if (roundIndex <= 2)
        this.FavoriteElement = me.Elements.Where<Element>((Func<Element, bool>) (i => i.ElementType != me.SpecialElement)).GetRandomElement<Element>().ElementType;
      if (this.GetTrueRandomly(48))
      {
        Card cardInternal = this.SummonSomeCard(out field);
        if (cardInternal != null && field != null)
          return cardInternal;
      }
      Field neededToBeKilledAsap = this.GetCardNeededToBeKilledAsap();
      if (neededToBeKilledAsap != null)
      {
        Card destroyCard = this.TryToDestroyCard(neededToBeKilledAsap);
        if (destroyCard != null && destroyCard is SpellCard)
        {
          field = neededToBeKilledAsap;
          if (field != null && destroyCard != null)
            return destroyCard;
        }
        else if (destroyCard is CreatureCard && me.Fields[inactivePlayer.Fields.IndexOf(neededToBeKilledAsap)].IsEmpty)
        {
          field = me.Fields[inactivePlayer.Fields.IndexOf(neededToBeKilledAsap)];
          if (field != null && destroyCard != null)
            return destroyCard;
        }
      }
      SpellCard curePlayerSpell = this.GetCurePlayerSpell();
      if (curePlayerSpell != null && me.Health < 18 && this.GetTrueRandomly(80))
      {
        field = this.Battlefield.ActivePlayer.Fields[0];
        if (field != null)
          return (Card) curePlayerSpell;
      }
      if (list1.Count > list2.Count + 3 && this.GetTrueRandomly(75))
      {
        SpellCard cardInternal = this.TryDestroyOpponentsCards(out field);
        if (cardInternal != null && field != null)
          return (Card) cardInternal;
      }
      Field creatureNeededCure = this.GetCreatureNeededCure();
      if (creatureNeededCure != null && this.GetTrueRandomly(65))
      {
        SpellCard cureCreatureSpell = this.GetCureCreatureSpell();
        if (cureCreatureSpell != null)
        {
          field = creatureNeededCure;
          if (field != null)
            return (Card) cureCreatureSpell;
        }
      }
      if (me.GetElementByType(this.FavoriteElement).Mana >= 10)
      {
        Card cardInternal = this.UseFavoriteCreature(out field);
        if (cardInternal != null)
        {
          this.FavoriteElement = me.Elements.Where<Element>((Func<Element, bool>) (i => i.ElementType != this.FavoriteElement && me.SpecialElement != i.ElementType)).GetRandomElement<Element>().ElementType;
          if (field != null && cardInternal != null)
            return cardInternal;
        }
      }
      SpellCard destructionSpell = this.GetMassDestructionSpell(out field);
      if (destructionSpell != null && list1.Count > 3 && this.GetTrueRandomly(75) && field != null)
        return (Card) destructionSpell;
      Field neededToBeKilled = this.GetCardNeededToBeKilled();
      if (neededToBeKilled != null && this.GetTrueRandomly(55))
      {
        Card destroyCard = this.TryToDestroyCard(neededToBeKilled);
        if (destroyCard != null && destroyCard is SpellCard)
        {
          field = neededToBeKilled;
          if (field != null && destroyCard != null)
            return destroyCard;
        }
        else if (destroyCard is CreatureCard && destroyCard != null && me.Fields[inactivePlayer.Fields.IndexOf(neededToBeKilled)].IsEmpty)
        {
          field = me.Fields[inactivePlayer.Fields.IndexOf(neededToBeKilled)];
          if (field != null && destroyCard != null)
            return destroyCard;
        }
      }
      if (roundIndex < 3 || this.GetTrueRandomly(20))
      {
        Card cardInternal = this.TrySummonManaMiners(out field);
        if (cardInternal != null && field != null)
          return cardInternal;
      }
      Card cardInternal1 = this.SummonSomeCard(out field);
      if (cardInternal1 != null && field != null)
        return cardInternal1;
      Card cardInternal2 = this.CastSomeSpell(out field);
      if (cardInternal2 != null && field != null)
        return cardInternal2;
      Card cardInternal3 = this.SummonSomeCard(out field, true);
      if (cardInternal3 != null && field != null)
        return cardInternal3;
      field = (Field) null;
      return (Card) null;
    }

    public ElementTypeEnum FavoriteElement { get; set; }

    private SpellCard GetMassDestructionSpell(out Field field)
    {
      field = this.Battlefield.InactivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)).OrderByDescending<Field, int>((Func<Field, int>) (i => i.Card.Health)).FirstOrDefault<Field>();
      return field == null ? (SpellCard) null : this.GetCards().OfType<SpellCard>().Join<SpellCard, string, string, SpellCard>((IEnumerable<string>) SmartestComputer.MassDestructionSpells, (Func<SpellCard, string>) (o => o.Name), (Func<string, string>) (i => i), (Func<SpellCard, string, SpellCard>) ((o, i) => o)).GetRandomElement<SpellCard>();
    }

    private Field GetCreatureNeededCure()
    {
      return this.Battlefield.ActivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty && i.Card.Cost > 9 && i.Card.Health * 3 <= i.Card.InitialHealth)).GetRandomElement<Field>();
    }

    private Card UseFavoriteCreature(out Field field)
    {
      Card randomElement1 = this.Battlefield.ActivePlayer.GetElementByType(this.FavoriteElement).Cards.Where<Card>((Func<Card, bool>) (i => i is CreatureCard && i.IsActive && !i.IsHidden)).OrderByDescending<Card, int>((Func<Card, int>) (i => i.Cost)).GetRandomElement<Card>();
      if (randomElement1 == null || randomElement1.Cost < 8)
      {
        field = (Field) null;
        return (Card) null;
      }
      FieldPair randomElement2 = this.GetFields().Where<FieldPair>((Func<FieldPair, bool>) (i => i.MyField.IsEmpty)).GetRandomElement<FieldPair>();
      field = randomElement2.MyField;
      return field == null ? (Card) null : randomElement1;
    }

    private Card TryToDestroyCard(Field toKill)
    {
      return toKill == null ? (Card) null : this.GetCards().Join<Card, string, string, Card>((IEnumerable<string>) SmartestComputer.DealDamageToCreatureSpells, (Func<Card, string>) (o => o.Name), (Func<string, string>) (i => i), (Func<Card, string, Card>) ((o, i) => o)).GetRandomElement<Card>() ?? this.GetCards().Join<Card, string, string, Card>((IEnumerable<string>) SmartestComputer.SafeMassDestructionSpells, (Func<Card, string>) (o => o.Name), (Func<string, string>) (i => i), (Func<Card, string, Card>) ((o, i) => o)).GetRandomElement<Card>() ?? (Card) this.GetCards().OfType<CreatureCard>().OrderByDescending<CreatureCard, int>((Func<CreatureCard, int>) (i => i.Damage)).GetRandomElement<CreatureCard>();
    }

    private SpellCard TryDestroyOpponentsCards(out Field field)
    {
      return this.GetMassDestructionSpell(out field);
    }

    private Field GetCardNeededToBeKilledAsap()
    {
      return ((IEnumerable<string>) SmartestComputer.ToBeKilledAsapCreatures).Join<string, Field, string, Field>(this.Battlefield.InactivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)), (Func<string, string>) (o => o), (Func<Field, string>) (i => i.Card.Name), (Func<string, Field, Field>) ((o, i) => i)).GetRandomElement<Field>();
    }

    private Field GetCardNeededToBeKilled()
    {
      return ((IEnumerable<string>) SmartestComputer.ManaMiners).Concat<string>((IEnumerable<string>) SmartestComputer.ToBeKilledAsapCreatures).Join<string, Field, string, Field>(this.Battlefield.InactivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)), (Func<string, string>) (o => o), (Func<Field, string>) (i => i.Card.Name), (Func<string, Field, Field>) ((o, i) => i)).GetRandomElement<Field>();
    }

    static SmartestComputer()
    {
      SmartestComputer.MassDestructionSpells = ((IEnumerable<string>) SmartestComputer.MassDestructionSpells).Concat<string>((IEnumerable<string>) SmartestComputer.SafeMassDestructionSpells).ToArray<string>();
    }

    private Card TrySummonManaMiners(out Field field)
    {
      FieldPair randomElement1 = this.GetFields().Where<FieldPair>((Func<FieldPair, bool>) (i =>
      {
        if (!i.MyField.IsEmpty)
          return false;
        if (i.HisField.IsEmpty)
          return true;
        return i.HisField.Card.Level < 4 && i.HisField.Card.Damage < 5;
      })).GetRandomElement<FieldPair>();
      if (randomElement1 == null)
      {
        field = (Field) null;
        return (Card) null;
      }
      Card randomElement2 = this.GetCards().Join<Card, string, string, Card>((IEnumerable<string>) SmartestComputer.ManaMiners, (Func<Card, string>) (o => o.Name), (Func<string, string>) (i => i), (Func<Card, string, Card>) ((o, i) => o)).GetRandomElement<Card>();
      if (randomElement2 != null)
      {
        field = randomElement1.MyField;
        return randomElement2;
      }
      field = (Field) null;
      return (Card) null;
    }

    private SpellCard GetCureCreatureSpell()
    {
      return this.TryGetActiveCard(ElementTypeEnum.Earth, "NatureRitual") as SpellCard;
    }

    private SpellCard GetCurePlayerSpell()
    {
      return this.TryGetActiveCard(ElementTypeEnum.Earth, "Rejuvenation") as SpellCard;
    }

    private Card TryGetActiveCard(ElementTypeEnum element, string name)
    {
      return this.Battlefield.ActivePlayer.GetElementByType(element).Cards.Where<Card>((Func<Card, bool>) (i => i.IsActive && !i.IsHidden)).FirstOrDefault<Card>((Func<Card, bool>) (i => i.Name == name));
    }

    private Card SummonSomeCard(out Field field, bool ignoreFavoriteElement = false)
    {
      bool byCost = this.GetTrueRandomly(75);
      FieldPair fieldPair = this.GetFields().Where<FieldPair>((Func<FieldPair, bool>) (i => i.MyField.IsEmpty && !i.HisField.IsEmpty)).OrderByDescending<FieldPair, int>((Func<FieldPair, int>) (i => !byCost ? i.HisField.Card.Damage : i.HisField.Card.Cost)).GetRandomElement<FieldPair>() ?? this.GetFields().Where<FieldPair>((Func<FieldPair, bool>) (i => i.MyField.IsEmpty)).GetRandomElement<FieldPair>();
      if (fieldPair == null)
      {
        field = (Field) null;
        return (Card) null;
      }
      CreatureCard biggestCreature = this.GetBiggestCreature(ignoreFavoriteElement || this.GetTrueRandomly(70));
      if (biggestCreature == null)
      {
        field = (Field) null;
        return (Card) null;
      }
      field = fieldPair.MyField;
      if (biggestCreature.Name == "GiantSpider")
      {
        List<FieldPair> fields = this.GetFields();
        if (fields[0].MyField.IsEmpty && fields[1].MyField.IsEmpty && fields[2].MyField.IsEmpty)
          field = fields[1].MyField;
        if (fields[1].MyField.IsEmpty && fields[2].MyField.IsEmpty && fields[3].MyField.IsEmpty)
          field = fields[2].MyField;
        if (fields[2].MyField.IsEmpty && fields[3].MyField.IsEmpty && fields[4].MyField.IsEmpty)
          field = fields[3].MyField;
      }
      return (Card) biggestCreature;
    }

    private Card CastSomeSpell(out Field field)
    {
      SpellCard randomElement = this.GetCards(true).OfType<SpellCard>().GetRandomElement<SpellCard>();
      if (randomElement == null)
      {
        field = (Field) null;
        return (Card) null;
      }
      if (randomElement.Target == SpellTarget.OpponentsCard || randomElement.Target == SpellTarget.Indeterminate)
      {
        field = this.Battlefield.InactivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)).GetRandomElement<Field>();
        return field == null ? (Card) null : (Card) randomElement;
      }
      if (randomElement.Target == SpellTarget.IndeterminateOwner || randomElement.Target == SpellTarget.OwnersCard)
      {
        field = this.Battlefield.ActivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)).GetRandomElement<Field>();
        return field == null ? (Card) null : (Card) randomElement;
      }
      field = (Field) null;
      return (Card) null;
    }

    private CreatureCard GetBiggestCreature(bool ignoreFavoriteElement)
    {
      return this.GetCards(ignoreFavoriteElement).OfType<CreatureCard>().OrderByDescending<CreatureCard, int>((Func<CreatureCard, int>) (i => i.Cost)).ThenByDescending<CreatureCard, int>((Func<CreatureCard, int>) (i => i.Health)).FirstOrDefault<CreatureCard>();
    }

    private IEnumerable<Card> GetCards(bool ignoreFavoriteElement = false)
    {
      return ((IEnumerable<Card>) this.Battlefield.ActivePlayer.Elements.Where<Element>((Func<Element, bool>) (i => !ignoreFavoriteElement || i.ElementType != this.FavoriteElement)).SelectMany<Element, Card>((Func<Element, IEnumerable<Card>>) (i => (IEnumerable<Card>) i.Cards)).ToArray<Card>()).Where<Card>((Func<Card, bool>) (i =>
      {
        if (!i.IsActive || i.IsHidden)
          return false;
        return !(i is SpellCard) || !((SpellCard) i).IsAbility;
      }));
    }

    private List<FieldPair> GetFields()
    {
      List<FieldPair> fields = new List<FieldPair>(5);
      int index = -1;
      fields.AddRange(this.Battlefield.ActivePlayer.Fields.Select<Field, FieldPair>((Func<Field, FieldPair>) (field =>
      {
        int num = ++index;
        return new FieldPair()
        {
          MyField = field,
          Index = num,
          HisField = this.Battlefield.InactivePlayer.Fields[index]
        };
      })));
      return fields;
    }
  }
}
