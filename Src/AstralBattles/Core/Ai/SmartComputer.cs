// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Ai.SmartComputer
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using AstralBattles.Core.Infrastructure;
using AstralBattles.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

#nullable disable
namespace AstralBattles.Core.Ai
{
  public class SmartComputer : ComputerIntellect
  {
    private static readonly ILogger Logger = LogFactory.GetLogger<SmartComputer>();
    public static readonly Random Random = new Random(Environment.TickCount);
    private readonly string[] massDestructionSpells = new string[18]
    {
      "Armageddon",
      "StoneRain",
      "Inferno",
      "FlameWave",
      "DrainSouls",
      "CursedFog",
      "ChainLightning",
      "AcidicRain",
      "DarkRitual",
      "BloodRitual",
      "SonicBoom",
      "ManaBurn",
      "Fireball",
      "Disintegrate",
      "Cannonade",
      "Madness",
      "DivineJustice",
      "WrathofGod"
    };
    private readonly string[] safeMassDestructionSpells = new string[12]
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
      "WrathofGod"
    };
    private readonly string[] manaMiners = new string[6]
    {
      "elfHermit",
      "PriestOfFire",
      "MerfolkElder",
      "MerfolkApostate",
      "KeeperofDeath",
      "Hypnotist"
    };
    private readonly string[] toBeKilledAsapCreatures = new string[5]
    {
      "AstralGuard",
      "MindMaster",
      "LightningCloud",
      "Vampire",
      "MasterLich"
    };

    public bool GetTrueRandomly(int percentage) => SmartComputer.Random.Next(0, 101) <= percentage;

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
        SmartComputer.Logger.LogError(ex);
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
      if (this.GetTrueRandomly(50))
      {
        foreach (IStrategy strategy in StrategyRegistry.Strategies)
        {
          CardSummonInfo cardSummonInfo = strategy.Get(this.Battlefield, this.GetFields());
          if (cardSummonInfo != null && cardSummonInfo.Card != null)
          {
            field = cardSummonInfo.Field;
            return cardSummonInfo.Card;
          }
        }
      }
      Field neededToBeKilledAsap = this.GetCardNeededToBeKilledAsap();
      if (neededToBeKilledAsap != null)
      {
        Card destroyCard = this.TryToDestroyCard(neededToBeKilledAsap);
        if (destroyCard != null && destroyCard is SpellCard)
        {
          field = neededToBeKilledAsap;
          return destroyCard;
        }
        if (destroyCard is CreatureCard && me.Fields[inactivePlayer.Fields.IndexOf(neededToBeKilledAsap)].IsEmpty)
        {
          field = me.Fields[inactivePlayer.Fields.IndexOf(neededToBeKilledAsap)];
          return destroyCard;
        }
      }
      SpellCard curePlayerSpell = this.GetCurePlayerSpell();
      if (curePlayerSpell != null && me.Health < 18 && this.GetTrueRandomly(60))
      {
        field = this.Battlefield.ActivePlayer.Fields[0];
        return (Card) curePlayerSpell;
      }
      if (list1.Count > list2.Count + 3 && this.GetTrueRandomly(75))
      {
        Card cardInternal = this.TryDestroyOpponentsCards(out field);
        if (cardInternal != null)
          return cardInternal;
      }
      Field creatureNeededCure = this.GetCreatureNeededCure();
      if (creatureNeededCure != null && this.GetTrueRandomly(65))
      {
        SpellCard cureCreatureSpell = this.GetCureCreatureSpell();
        if (cureCreatureSpell != null)
        {
          field = creatureNeededCure;
          return (Card) cureCreatureSpell;
        }
      }
      if (me.GetElementByType(this.FavoriteElement).Mana >= 10)
      {
        Card cardInternal = this.UseFavoriteCreature(out field);
        if (cardInternal != null)
        {
          this.FavoriteElement = me.Elements.Where<Element>((Func<Element, bool>) (i => i.ElementType != this.FavoriteElement && me.SpecialElement != i.ElementType)).GetRandomElement<Element>().ElementType;
          return cardInternal;
        }
      }
      Card destructionSpell = this.GetMassDestructionSpell(out field);
      if (destructionSpell != null && this.GetTrueRandomly(35))
        return destructionSpell;
      Field neededToBeKilled = this.GetCardNeededToBeKilled();
      if (neededToBeKilled != null && this.GetTrueRandomly(40))
      {
        Card destroyCard = this.TryToDestroyCard(neededToBeKilled);
        if (destroyCard != null && destroyCard is SpellCard)
        {
          field = neededToBeKilled;
          return destroyCard;
        }
        if (destroyCard is CreatureCard && destroyCard != null && me.Fields[inactivePlayer.Fields.IndexOf(neededToBeKilled)].IsEmpty)
        {
          field = me.Fields[inactivePlayer.Fields.IndexOf(neededToBeKilled)];
          return destroyCard;
        }
      }
      if (roundIndex < 3 || this.GetTrueRandomly(20))
      {
        Card cardInternal = this.TrySummonManaMiners(out field);
        if (cardInternal != null)
          return cardInternal;
      }
      return this.SummonSomeCard(out field) ?? this.CastSomeSpell(out field);
    }

    public ElementTypeEnum FavoriteElement { get; set; }

    private Card GetMassDestructionSpell(out Field field)
    {
      field = this.Battlefield.InactivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)).OrderByDescending<Field, int>((Func<Field, int>) (i => i.Card.Health)).FirstOrDefault<Field>();
      return field == null ? (Card) null : this.GetCards().Join<Card, string, string, Card>((IEnumerable<string>) this.massDestructionSpells, (Func<Card, string>) (o => o.Name), (Func<string, string>) (i => i), (Func<Card, string, Card>) ((o, i) => o)).FirstOrDefault<Card>();
    }

    private Field GetCreatureNeededCure()
    {
      return this.Battlefield.ActivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty && i.Card.Cost > 9 && i.Card.Health * 3 <= i.Card.InitialHealth)).FirstOrDefault<Field>();
    }

    private Card UseFavoriteCreature(out Field field)
    {
      Card card = this.Battlefield.ActivePlayer.GetElementByType(this.FavoriteElement).Cards.Where<Card>((Func<Card, bool>) (i => i is CreatureCard && i.IsActive)).OrderByDescending<Card, int>((Func<Card, int>) (i => i.Cost)).FirstOrDefault<Card>();
      if (card == null || card.Cost < 8)
      {
        field = (Field) null;
        return (Card) null;
      }
      FieldPair randomElement = this.GetFields().Where<FieldPair>((Func<FieldPair, bool>) (i => i.MyField.IsEmpty)).GetRandomElement<FieldPair>();
      field = randomElement.MyField;
      return field == null ? (Card) null : card;
    }

    private Card TryToDestroyCard(Field toKill)
    {
      return toKill == null ? (Card) null : this.GetCards().Join<Card, string, string, Card>((IEnumerable<string>) this.safeMassDestructionSpells, (Func<Card, string>) (o => o.Name), (Func<string, string>) (i => i), (Func<Card, string, Card>) ((o, i) => o)).FirstOrDefault<Card>() ?? this.GetCards().OrderByDescending<Card, int>((Func<Card, int>) (i => i.Damage)).FirstOrDefault<Card>();
    }

    private IEnumerable<Card> GetCards(bool ignoreFavoriteElement = false)
    {
      return this.Battlefield.ActivePlayer.Elements.Where<Element>((Func<Element, bool>) (i => !ignoreFavoriteElement || i.ElementType != this.FavoriteElement)).SelectMany<Element, Card>((Func<Element, IEnumerable<Card>>) (i => (IEnumerable<Card>) i.Cards)).Where<Card>((Func<Card, bool>) (i => i.IsActive));
    }

    private Card TryDestroyOpponentsCards(out Field field)
    {
      return this.GetMassDestructionSpell(out field);
    }

    private Field GetCardNeededToBeKilledAsap()
    {
      return this.Battlefield.InactivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty && ((IEnumerable<string>) this.toBeKilledAsapCreatures).Any<string>((Func<string, bool>) (j => i.Card.Name == j)))).FirstOrDefault<Field>();
    }

    private Field GetCardNeededToBeKilled()
    {
      return ((IEnumerable<string>) this.toBeKilledAsapCreatures).Join<string, Field, string, Field>(this.Battlefield.InactivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)), (Func<string, string>) (o => o), (Func<Field, string>) (i => i.Card.Name), (Func<string, Field, Field>) ((o, i) => i)).FirstOrDefault<Field>();
    }

    private Card TrySummonManaMiners(out Field field)
    {
      FieldPair fieldPair = this.GetFields().Where<FieldPair>((Func<FieldPair, bool>) (i =>
      {
        if (!i.MyField.IsEmpty)
          return false;
        if (i.HisField.IsEmpty)
          return true;
        return i.HisField.Card.Level < 4 && i.HisField.Card.Damage < 4;
      })).FirstOrDefault<FieldPair>();
      if (fieldPair == null)
      {
        field = (Field) null;
        return (Card) null;
      }
      Card card = this.GetCards().Join<Card, string, string, Card>((IEnumerable<string>) this.manaMiners, (Func<Card, string>) (o => o.Name), (Func<string, string>) (i => i), (Func<Card, string, Card>) ((o, i) => o)).FirstOrDefault<Card>();
      if (card != null)
      {
        field = fieldPair.MyField;
        return card;
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
      return this.Battlefield.ActivePlayer.GetElementByType(element).Cards.Where<Card>((Func<Card, bool>) (i => i.IsActive)).FirstOrDefault<Card>((Func<Card, bool>) (i => i.Name == name));
    }

    private Card SummonSomeCard(out Field field)
    {
      bool byCost = this.GetTrueRandomly(60);
      FieldPair fieldPair = this.GetFields().Where<FieldPair>((Func<FieldPair, bool>) (i => i.MyField.IsEmpty && !i.HisField.IsEmpty)).OrderByDescending<FieldPair, int>((Func<FieldPair, int>) (i => !byCost ? i.HisField.Card.Damage : i.HisField.Card.Cost)).FirstOrDefault<FieldPair>() ?? this.GetFields().Where<FieldPair>((Func<FieldPair, bool>) (i => i.MyField.IsEmpty)).GetRandomElement<FieldPair>();
      if (fieldPair == null)
      {
        field = (Field) null;
        return (Card) null;
      }
      CreatureCard biggestCreature = this.GetBiggestCreature(this.GetTrueRandomly(70));
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
      field = this.Battlefield.ActivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)).GetRandomElement<Field>();
      return field == null ? (Card) null : (Card) randomElement;
    }

    private CreatureCard GetBiggestCreature(bool ignoreFavoriteElement)
    {
      return this.GetCards(ignoreFavoriteElement).OfType<CreatureCard>().OrderByDescending<CreatureCard, int>((Func<CreatureCard, int>) (i => i.Cost)).ThenByDescending<CreatureCard, int>((Func<CreatureCard, int>) (i => i.Damage)).FirstOrDefault<CreatureCard>();
    }

    private List<FieldPair> GetFields()
    {
      List<FieldPair> fields = new List<FieldPair>(5);
      int index = 0;
      foreach (Field field in (Collection<Field>) this.Battlefield.ActivePlayer.Fields)
        fields.Add(new FieldPair()
        {
          MyField = field,
          Index = index,
          HisField = this.Battlefield.InactivePlayer.Fields[index]
        });
      return fields;
    }
  }
}
