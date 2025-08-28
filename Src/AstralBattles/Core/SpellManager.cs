// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.SpellManager
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using AstralBattles.Core.Model;
using AstralBattles.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AstralBattles.Core
{
  public class SpellManager
  {
    private Random random = new Random();

    public void CastSpell(SpellCard spell, Field chosedField, Action endTurnAction)
    {
      IBattlefield battlefield = GameService.CurrentGame.Battlefield;
      Player activePlayer = battlefield.ActivePlayer;
      Player inactivePlayer = battlefield.InactivePlayer;
      battlefield.OnSpell(spell);
      switch (spell.Name)
      {
        case "ChainLightning":
          int damage1 = this.IncreaseSpellDamageByCards(9);
          inactivePlayer.DoHarm(damage1, battlefield);
          inactivePlayer.ForEachBusyFields((Action<Field>) (i => i.DoHarm(damage1, battlefield, 1)));
          break;
        case "LightningBolt":
          int num1 = this.IncreaseSpellDamageByCards(5 + activePlayer.GetElementByType(ElementTypeEnum.Air).Mana);
          inactivePlayer.DoHarm(num1, battlefield);
          break;
        case "Tornado":
          chosedField.DoHarm(1, battlefield, true, true);
          break;
        case "CalltoThunder":
          int num2 = this.IncreaseSpellDamageByCards(6);
          chosedField.DoHarm(num2, battlefield, 1);
          inactivePlayer.DoHarm(num2, battlefield);
          break;
        case "StoneRain":
          int damage2 = this.IncreaseSpellDamageByCards(25);
          battlefield.GetAllNonEmptyFields().ForEach<Field>((Action<Field>) (c => c.DoHarm(this.IncreaseSpellDamageByCards(damage2), battlefield, 1)));
          break;
        case "NaturalFury":
          int num3 = this.IncreaseSpellDamageByCards(activePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)).Select<Field, int>((Func<Field, int>) (i => i.Card.Damage)).OrderByDescending<int, int>((Func<int, int>) (i => i)).Take<int>(2).Sum());
          inactivePlayer.DoHarm(num3, battlefield);
          break;
        case "NatureRitual":
          int num4 = this.IncreaseSpellDamageByCards(8);
          activePlayer.Cure(num4);
          chosedField.Cure(num4, (Action) null);
          break;
        case "Rejuvenation":
          int num5 = activePlayer.GetElementByType(ElementTypeEnum.Earth).Mana * 2;
          activePlayer.Cure(num5);
          break;
        case "Armageddon":
          int damage3 = this.IncreaseSpellDamageByCards(8 + activePlayer.GetElementByType(ElementTypeEnum.Fire).Mana);
          inactivePlayer.DoHarm(damage3, battlefield);
          battlefield.GetAllNonEmptyFields().ForEach<Field>((Action<Field>) (c => c.DoHarm(damage3, battlefield, 1)));
          break;
        case "FlameWave":
          int damage4 = this.IncreaseSpellDamageByCards(9);
          inactivePlayer.ForEachBusyFields((Action<Field>) (i => i.DoHarm(damage4, battlefield, 1)));
          break;
        case "Inferno":
          int num6 = this.IncreaseSpellDamageByCards(18);
          int backDamage = this.IncreaseSpellDamageByCards(10);
          chosedField.DoHarm(num6, battlefield, 1);
          inactivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty && i != chosedField)).ForEach<Field>((Action<Field>) (i => i.DoHarm(backDamage, battlefield, 1)));
          break;
        case "Meditation":
          activePlayer.IncreaseMana(ElementTypeEnum.Fire, 1);
          activePlayer.IncreaseMana(ElementTypeEnum.Air, 1);
          activePlayer.IncreaseMana(ElementTypeEnum.Earth, 1);
          break;
        case "AcidicRain":
          int damage5 = this.IncreaseSpellDamageByCards(15);
          battlefield.GetAllNonEmptyFields().ForEach<Field>((Action<Field>) (c => c.DoHarm(this.IncreaseSpellDamageByCards(damage5), battlefield, 1)));
          inactivePlayer.IncreaseAllElementsPower(-1);
          break;
        case "CursedFog":
          int damage6 = this.IncreaseSpellDamageByCards(12);
          battlefield.GetAllNonEmptyFields().ForEach<Field>((Action<Field>) (i => i.DoHarm(damage6, battlefield, 1)));
          inactivePlayer.DoHarm(damage6 / 2, battlefield);
          break;
        case "DrainSouls":
          Field[] array = battlefield.GetAllNonEmptyFields().ToArray<Field>();
          activePlayer.Cure(this.IncreaseSpellDamageByCards(array.Length * 2));
          ((IEnumerable<Field>) array).ForEach<Field>((Action<Field>) (i => i.DoHarm(1, battlefield, true, true)));
          break;
        case "BloodRitual":
          int damage7 = this.IncreaseSpellDamageByCards(chosedField.Card.Health);
          chosedField.DoHarm(damage7, battlefield, true);
          inactivePlayer.ForEachBusyFields((Action<Field>) (i => i.DoHarm(damage7, battlefield, 1)));
          break;
        case "DarkRitual":
          int damage8 = this.IncreaseSpellDamageByCards(3);
          inactivePlayer.ForEachBusyFields((Action<Field>) (i => i.DoHarm(damage8, battlefield, 1)));
          activePlayer.ForEachBusyFields((Action<Field>) (i => i.Cure(damage8, (Action) null)));
          break;
        case "EmissaryofDorlakSpell":
          chosedField.DoHarm(chosedField.Card.Health, battlefield, true, quickDeath: true, ignoreReplacedBy: true);
          chosedField.AssignCard(CardRegistry.GetCardByName("EmissaryofDorlak").Clone() as CreatureCard, (Action) null);
          break;
        case "HealingSpray":
          int num7 = this.IncreaseSpellDamageByCards(8);
          chosedField.Cure(num7, (Action) null);
          int num8 = activePlayer.Fields.IndexOf(chosedField);
          Field field1 = (Field) null;
          Field field2 = (Field) null;
          if (num8 > 0)
            field1 = activePlayer.Fields[num8 - 1];
          if (num8 < activePlayer.Fields.Count - 1)
            field2 = activePlayer.Fields[num8 + 1];
          if (field1 != null && !field1.IsEmpty)
            field1.Cure(6, (Action) null);
          if (field2 != null && !field2.IsEmpty)
          {
            field2.Cure(6, (Action) null);
            break;
          }
          break;
        case "StealEssence":
          int num9 = this.IncreaseSpellDamageByCards(5);
          if (chosedField.DoHarm(num9, battlefield, 1))
          {
            activePlayer.IncreaseMana(ElementTypeEnum.Sorcery, 4);
            break;
          }
          break;
        case "RitualofGlory":
          activePlayer.ForEachBusyFields((Action<Field>) (i =>
          {
            i.Cure(i.Card.InitialHealth - i.Card.Health, (Action) null);
            i.SetSingleUseDamageBonus(3);
          }));
          break;
        case "SonicBoom":
          int damage9 = this.IncreaseSpellDamageByCards(11);
          inactivePlayer.DoHarm(damage9, battlefield);
          inactivePlayer.ForEachBusyFields((Action<Field>) (i =>
          {
            i.Card.IsJustSummoned = true;
            i.DoHarm(damage9, battlefield, 1);
          }));
          break;
        case "Fireball":
          int num10 = this.IncreaseSpellDamageByCards(8);
          chosedField.DoHarm(num10, battlefield, 1);
          int num11 = inactivePlayer.Fields.IndexOf(chosedField);
          Field field3 = (Field) null;
          Field field4 = (Field) null;
          if (num11 > 0)
            field3 = inactivePlayer.Fields[num11 - 1];
          if (num11 < inactivePlayer.Fields.Count - 1)
            field4 = inactivePlayer.Fields[num11 + 1];
          if (field3 != null && !field3.IsEmpty)
            field3.DoHarm(6, battlefield, 1);
          if (field4 != null && !field4.IsEmpty)
          {
            field4.DoHarm(6, battlefield, 1);
            break;
          }
          break;
        case "Sacrifice":
          chosedField.DoHarm(1, battlefield, true);
          activePlayer.IncreaseAllBasicElementsPower(3);
          break;
        case "ManaBurn":
          int damage10 = this.IncreaseSpellDamageByCards(inactivePlayer.Elements.Max<Element>((Func<Element, int>) (i => i.Mana)));
          inactivePlayer.DoHarm(damage10, battlefield);
          inactivePlayer.ForEachBusyFields((Action<Field>) (i => i.DoHarm(damage10, battlefield, 1)));
          inactivePlayer.DecreaseMana(inactivePlayer.Elements.OrderByDescending<Element, int>((Func<Element, int>) (i => i.Mana)).First<Element>().ElementType, 2);
          break;
        case "Disintegrate":
          int damage11 = this.IncreaseSpellDamageByCards(11);
          chosedField.DoHarm(1, battlefield, true);
          inactivePlayer.ForEachBusyFields((Action<Field>) (i => i.DoHarm(damage11, battlefield, 1)));
          break;
        case "Cannonade":
          int damage12 = this.IncreaseSpellDamageByCards(19);
          inactivePlayer.ForEachBusyFields((Action<Field>) (i => i.DoHarm(damage12, battlefield, 1)));
          break;
        case "Overtime":
          activePlayer.IncreaseMana(ElementTypeEnum.Mechanical, 1);
          break;
        case "Madness":
          inactivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty && i.Card.Damage > 0)).ForEach<Field>((Action<Field>) (i => i.DoHarm(i.Card.Damage, battlefield, 1)));
          break;
        case "Hypnosys":
          List<Field> list = inactivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)).OrderByDescending<Field, int>((Func<Field, int>) (i => i.Card.Damage)).Take<Field>(2).ToList<Field>();
          if (list.Count > 0)
          {
            int num12 = this.IncreaseSpellDamageByCards(list.Sum<Field>((Func<Field, int>) (i => i.Card.Damage)));
            inactivePlayer.DoHarm(num12, battlefield);
            break;
          }
          break;
        case "DivineJustice":
          int damage13 = this.IncreaseSpellDamageByCards(12);
          battlefield.GetAllNonEmptyFields().Where<Field>((Func<Field, bool>) (i => i != chosedField)).ForEach<Field>((Action<Field>) (i => i.DoHarm(damage13, battlefield, 1)));
          chosedField.Cure(damage13, (Action) null);
          break;
        case "DivineIntervention":
          activePlayer.IncreaseAllBasicElementsPower(2);
          activePlayer.Cure(12);
          break;
        case "WrathofGod":
          int damage14 = this.IncreaseSpellDamageByCards(12);
          inactivePlayer.ForEachBusyFields((Action<Field>) (i => i.DoHarm(damage14, battlefield, 1)));
          int num13 = inactivePlayer.Fields.Count<Field>((Func<Field, bool>) (i => !i.IsEmpty));
          activePlayer.IncreaseMana(ElementTypeEnum.Holy, num13);
          break;
        case "Weakness":
          inactivePlayer.IncreaseAllElementsPower(-1);
          break;
        case "PoisonousCloud":
          inactivePlayer.IncreaseAllElementsPower(-1);
          inactivePlayer.ForEachBusyFields((Action<Field>) (i => i.DoHarm(i.Card.Health / 2, battlefield, 1)));
          break;
        case "DoomBolt":
          Field randomElement1 = inactivePlayer.BusyFields.GetRandomElement<Field>();
          if (randomElement1 != null)
          {
            randomElement1.DoHarm(this.IncreaseSpellDamageByCards(25), battlefield, 1);
            break;
          }
          break;
        case "ChaoticWave":
          activePlayer.ForEachBusyFields((Action<Field>) (i => i.Cure(this.random.Next(2, 13), (Action) null)));
          inactivePlayer.ForEachBusyFields((Action<Field>) (i => i.DoHarm(this.random.Next(2, 13), battlefield, 1)));
          break;
        case "Explosion":
          Field field5 = inactivePlayer.Fields[activePlayer.Fields.IndexOf(chosedField)];
          chosedField.DoHarm(1, battlefield, true);
          if (!field5.IsEmpty)
          {
            field5.DoHarm(this.IncreaseSpellDamageByCards(28), battlefield, 1);
            break;
          }
          break;
        case "PowerChains":
          inactivePlayer.DecreaseMana(chosedField.Card.ElementType, 3);
          chosedField.DoHarm(this.IncreaseSpellDamageByCards(12), battlefield, 1);
          break;
        case "HellFire":
          int killed = 0;
          inactivePlayer.ForEachBusyFields((Action<Field>) (i => killed += i.DoHarm(this.IncreaseSpellDamageByCards(13), battlefield, 1) ? 1 : 0));
          activePlayer.IncreaseMana(ElementTypeEnum.Fire, killed);
          break;
        case "NaturalHealing":
          activePlayer.ForEachBusyFields((Action<Field>) (i => i.Cure(18, (Action) null)));
          activePlayer.BusyFields.InvokeOnFirstOrDefault<Field>((Func<Field, bool>) (i => i.Card.Name == "MagicHamster"), (Action<Field>) (i => i.DoAct()));
          break;
        case "Poison":
          chosedField.DoHarm(14, battlefield, 1);
          activePlayer.BusyFields.InvokeOnFirstOrDefault<Field>((Func<Field, bool>) (i => i.Card.Name == "Scorpion"), (Action<Field>) (i => i.DoAct()));
          break;
        case "Enrage":
          activePlayer.BusyFields.Where<Field>((Func<Field, bool>) (i => i.Card.Name == "Wolverine")).ForEach<Field>((Action<Field>) (i =>
          {
            i.Card.InitialDamage += 2;
            i.SetDamageBonus(i.DamageBonus);
            i.Cure(100, (Action) null);
          }));
          activePlayer.BusyFields.InvokeOnFirstOrDefault<Field>((Func<Field, bool>) (i => i.Card.Name == "Wolverine"), (Action<Field>) (i => i.DoAct()));
          break;
        case "PumpEnergy":
          activePlayer.IncreaseAllBasicElementsPower(1);
          activePlayer.BusyFields.InvokeOnFirstOrDefault<Field>((Func<Field, bool>) (i => i.Card.Name == "EnergyBeast"), (Action<Field>) (i => i.DoAct()));
          break;
        case "MoveFalcon":
          inactivePlayer.ForEachBusyFields((Action<Field>) (i => i.DoHarm(4, battlefield, 1)));
          Field field6 = activePlayer.BusyFields.FirstOrDefault<Field>((Func<Field, bool>) (i => i.Card.Name == "DeathFalcon"));
          if (field6 != null)
          {
            chosedField.AssignCard(field6.Card, (Action) null);
            field6.RemoveCardSafely();
            break;
          }
          break;
        case "Trumpet":
          activePlayer.IncreaseMana(ElementTypeEnum.Beast, 1);
          activePlayer.BusyFields.InvokeOnFirstOrDefault<Field>((Func<Field, bool>) (i => i.Card.Name == "WhiteElephant"), (Action<Field>) (i => i.DoAct()));
          break;
        case "Gaze":
          chosedField.DoHarm(6, battlefield, 1);
          activePlayer.BusyFields.InvokeOnFirstOrDefault<Field>((Func<Field, bool>) (i => i.Card.Name == "Basilisk"), (Action<Field>) (i => i.DoAct()));
          break;
        case "BreatheFire":
          inactivePlayer.DoHarm(10, battlefield);
          inactivePlayer.ForEachBusyFields((Action<Field>) (i => i.DoHarm(10, battlefield, 1)));
          activePlayer.BusyFields.InvokeOnFirstOrDefault<Field>((Func<Field, bool>) (i => i.Card.Name == "AncientDragon"), (Action<Field>) (i => i.DoAct()));
          break;
        case "RescueOperation":
          Field randomElement2 = activePlayer.Fields.Where<Field>((Func<Field, bool>) (i => i.IsEmpty)).GetRandomElement<Field>();
          if (randomElement2 == null)
          {
            chosedField.DoAct();
            break;
          }
          randomElement2.AssignCard(chosedField.Card, (Action) null);
          chosedField.RemoveCardSafely();
          break;
        case "ArmyofRats":
          int damage15 = this.IncreaseSpellDamageByCards(12);
          inactivePlayer.ForEachBusyFields((Action<Field>) (i => i.DoHarm(damage15, battlefield, 1)));
          Field randomMyField = activePlayer.BusyFields.GetRandomElement<Field>();
          if (randomMyField != null)
          {
            ((Action) (() => randomMyField.DoHarm(damage15, battlefield, 1))).ExecuteWithDelay(1200);
            break;
          }
          break;
      }
      endTurnAction.ExecuteWithDelay(3000);
    }

    private int IncreaseSpellDamageByCards(int initialDamage)
    {
      IBattlefield battlefield = GameService.CurrentGame.Battlefield;
      int num = initialDamage;
      foreach (Field field in battlefield.ActivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)))
      {
        Skill skill1 = field.Card["IncreaseSpellsDamage_"];
        Skill skill2 = field.Card["SpellsDamageIncreaseBy50Percentage"];
        if (skill1 != null)
          num += skill1.GetArgumentAsInt();
        if (skill2 != null)
          num += (int) ((double) num * 0.5);
      }
      return num;
    }
  }
}
