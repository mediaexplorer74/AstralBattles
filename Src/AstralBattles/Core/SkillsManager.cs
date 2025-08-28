// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.SkillsManager
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using AstralBattles.Core.Model;
using AstralBattles.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace AstralBattles.Core
{
  public class SkillsManager
  {
    private Random random = new Random();

    public void OnNewTurn()
    {
      IBattlefield battlefield = GameService.CurrentGame.Battlefield;
      if (battlefield.ActivePlayer.Specialization == Specialization.Druid)
        battlefield.ActivePlayer.Cure(1);
      if (battlefield.ActivePlayer.Specialization == Specialization.IceLord)
        battlefield.InactivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)).GetRandomElement<Field>()?.DoHarm(1, battlefield);
      using (IEnumerator<Field> enumerator = this.GetActivePlayerFields().GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          Field field = enumerator.Current;
          Player player1 = field.OwnerName == battlefield.FirstPlayer.Name ? battlefield.FirstPlayer : battlefield.SecondPlayer;
          Player player2 = field.OwnerName == battlefield.FirstPlayer.Name ? battlefield.SecondPlayer : battlefield.FirstPlayer;
          CreatureCard card = field.Card;
          Skill skill1 = card["DealDamageToOwnerEveryRound_"];
          Skill skill2 = card["DealDamageToOpponentEveryRound_"];
          Skill skill3 = card["IncreaseElementPowerBy1EachTurn_"];
          Skill skill4 = card["IncreaseElementPowerBy2EachTurn_"];
          Skill skill5 = card["IncreaseOwnersPowers_"];
          Skill skill6 = card["DecreaseOpponentPowersEachTurn_"];
          Skill skill7 = card["Elemental"];
          Skill skill8 = card["HealOwnerEachTurn_"];
          Skill skill9 = card["Regeneration_"];
          Skill skill10 = card["HealEveryone_"];
          Skill skill11 = card["DealDamageToMostHitPointedOpponentsCreature_"];
          Skill skill12 = card["DealsDamageToOpponentEqualsSpecialElementManaEachTurn"];
          Skill skill13 = card["DealsDamageToAllOpponentsCreaturesEachTurn_"];
          Skill skill14 = card["HealsOwnerEachTurnFrom1To6"];
          Skill skill15 = card["DealsDamageToOpponentFrom1To6"];
          Skill skill16 = card["ReducedRandomOpponentsPowerEachTurn_"];
          Skill skill17 = card["IncreasesRandomOwnersPowerEachTurn_"];
          Skill skill18 = card["DealsDamageToRandomOpponentsCreature_"];
          Skill skill19 = card["SummonedSoldierToRandomSlotEachTurn"];
          Skill skill20 = card["MovesToRandomSlotEachTurn"];
          Skill skill21 = card["DealDamageToNeigboringSlots_"];
          ObservableCollection<Field> fields = battlefield.ActivePlayer.Fields;
          int num1 = fields.IndexOf(field);
          Field field1 = (Field) null;
          Field field2 = (Field) null;
          if (num1 < fields.Count - 1)
            field2 = fields[num1 + 1];
          if (num1 > 0)
            field1 = fields[num1 - 1];
          if (skill20 != null)
          {
            Field randomElement = battlefield.ActivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => i.IsEmpty)).GetRandomElement<Field>();
            if (randomElement != null)
            {
              randomElement.AssignCard(field.Card, (Action) (() => field.DoAct()));
              field.RemoveCardSafely();
            }
          }
          if (skill19 != null)
          {
            Field randomElement = player1.Fields.Where<Field>((Func<Field, bool>) (i => i.IsEmpty)).GetRandomElement<Field>();
            if (randomElement != null)
            {
              randomElement.AssignCard(CardRegistry.GetCardByName("Soldier").Clone() as CreatureCard, (Action) null);
              field.DoAct();
            }
          }
          if (skill18 != null)
          {
            Field randomElement = player2.BusyFields.GetRandomElement<Field>();
            if (randomElement != null)
            {
              randomElement.DoHarm(skill18.GetArgumentAsInt(), battlefield, 1);
              field.DoAct();
            }
          }
          if (skill17 != null)
            player1.IncreaseMana(player1.Elements.GetRandomElement<Element>().ElementType, skill17.GetArgumentAsInt());
          if (skill16 != null)
            player2.DecreaseMana(player2.Elements.GetRandomElement<Element>().ElementType, skill16.GetArgumentAsInt());
          if (skill14 != null)
          {
            player1.Cure(this.random.Next(1, 7));
            field.DoAct();
          }
          if (skill15 != null)
          {
            player2.DoHarm(this.random.Next(1, 7), battlefield);
            field.DoAct();
          }
          if (skill13 != null)
          {
            int damage = skill13.GetArgumentAsInt();
            player2.ForEachBusyFields((Action<Field>) (i => i.DoHarm(damage, battlefield, 1)));
            field.DoAct();
          }
          if (skill12 != null)
          {
            int mana = player1.GetElementByType(player1.SpecialElement).Mana;
            player2.DoHarm(mana, battlefield);
            field.DoAct();
          }
          if (skill11 != null)
          {
            int argumentAsInt = skill11.GetArgumentAsInt();
            Field field3 = player2.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)).OrderByDescending<Field, int>((Func<Field, int>) (i => i.Card.Health)).FirstOrDefault<Field>();
            if (field3 != null)
            {
              field3.DoHarm(argumentAsInt, battlefield, 1);
              field.DoAct();
            }
          }
          if (skill10 != null)
          {
            int heal = skill10.GetArgumentAsInt();
            player1.Cure(heal);
            player1.ForEachBusyFields((Action<Field>) (i => i.Cure(heal, (Action) null)));
            field.DoAct();
          }
          if (skill7 != null)
            player1.IncreaseMana(card.ElementType, 1);
          if (skill1 != null && !card.IsJustSummoned)
          {
            player1.DoHarm(skill1.GetArgumentAsInt(), battlefield);
            field.DoAct();
          }
          if (skill2 != null)
          {
            player2.DoHarm(skill2.GetArgumentAsInt(), battlefield);
            field.DoAct();
          }
          if (skill3 != null)
            player1.IncreaseMana(skill3.GetArgumentAsElement(), 1);
          if (skill4 != null)
            player1.IncreaseMana(skill4.GetArgumentAsElement(), 2);
          if (skill5 != null)
            player1.IncreaseAllElementsPower(1);
          if (skill8 != null)
          {
            player1.Cure(skill8.GetArgumentAsInt());
            field.DoAct();
          }
          if (skill9 != null)
          {
            int argumentAsInt = skill9.GetArgumentAsInt();
            int num2 = card.InitialHealth - card.Health;
            if (num2 >= argumentAsInt)
              field.Cure(argumentAsInt, (Action) null);
            if (num2 < argumentAsInt && num2 > 0)
              field.Cure(num2, (Action) null);
          }
          if (skill6 != null)
          {
            int argumentAsInt = skill6.GetArgumentAsInt();
            player2.IncreaseAllElementsPower(-argumentAsInt);
            field.DoAct();
          }
          if (skill21 != null)
          {
            int argumentAsInt = skill21.GetArgumentAsInt();
            if (field1 != null && !field1.IsEmpty)
              field1.DoHarm(argumentAsInt, battlefield);
            if (field2 != null && !field2.IsEmpty)
              field2.DoHarm(argumentAsInt, battlefield);
            field.DoAct();
          }
        }
      }
    }

    public void OnEndTrun()
    {
      IBattlefield battlefield = GameService.CurrentGame.Battlefield;
      foreach (Field busyField in battlefield.ActivePlayer.BusyFields)
      {
        Skill skill1 = busyField.Card["DestroysWeakOpponentsCreaturesEachTurnEnd_"];
        Skill skill2 = busyField.Card["ReducesRandomOwnersPowerEachTurn_"];
        Skill skill3 = busyField.Card["StunsOppositingSlotEachTurn"];
        Skill skill4 = busyField.Card["MovesToRandomSlotEachTurn"];
        Field field = battlefield.InactivePlayer.Fields[battlefield.ActivePlayer.Fields.IndexOf(busyField)];
        if (skill3 != null && field != null && !field.IsEmpty)
        {
          field.Card.IsJustSummoned = true;
          field.DoAct();
        }
        if (skill4 != null)
        {
          Field randomElement = battlefield.ActivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => i.IsEmpty)).GetRandomElement<Field>();
          if (randomElement != null)
          {
            randomElement.AssignCard(busyField.Card, (Action) null);
            busyField.RemoveCardSafely();
          }
        }
        if (skill2 != null)
        {
          int argumentAsInt = skill2.GetArgumentAsInt();
          battlefield.ActivePlayer.DecreaseMana(battlefield.ActivePlayer.Elements.GetRandomElement<Element>().ElementType, argumentAsInt);
        }
        if (skill1 != null)
        {
          int hpLimit = skill1.GetArgumentAsInt();
          battlefield.InactivePlayer.BusyFields.Where<Field>((Func<Field, bool>) (i => i.Card.Health <= hpLimit)).ForEach<Field>((Action<Field>) (i => i.DoHarm(1, battlefield, true)));
          busyField.DoAct();
        }
      }
    }

    public void OnSummon(CreatureCard card, Field field)
    {
      IBattlefield battlefield = GameService.CurrentGame.Battlefield;
      Skill skill1 = card["DoDamageAtSummoningToAll_"];
      Skill skill2 = card["DoDamageAtSummoningToOpponent_"];
      Skill skill3 = card["DoDamageAtSummoningToOpponentAndHisCards_"];
      Skill skill4 = card["DoDamageAtSummoningToOpponentsCards_"];
      Skill skill5 = card["DoDamageToOpponentAtSummoning_"];
      Skill skill6 = card["HealPlayerAtSummoning_"];
      Skill skill7 = card["ReducedOpponentsPowers_"];
      Skill skill8 = card["GriffinSkill"];
      Skill skill9 = card["IncreasePowerOnSummoningBy2_"];
      Skill skill10 = card["IncreasePowerOnSummoningBy3_"];
      Skill skill11 = card["GiantSpiderSkill"];
      Skill skill12 = card["SummonsClonesToNeighboringSlots"];
      Skill skill13 = card["DealsDamageToOppositeSlotHalflife"];
      Skill skill14 = card["DealDamageToOpposingSlot_"];
      Skill skill15 = card["IncreaseNeighborAttackBy_"];
      Skill skill16 = card["IncreaseOwnersCreaturesAttack_"];
      Skill increaseOpponentsCardsCost = card["IncreaseOpponentsCardsCost_"];
      Skill increaseOpponentsCardsCostPermanent = card["IncreaseOpponentsCardsCostPermanently_"];
      Skill increasesOpponentsSpellsCosts = card["IncreasesOpponentsSpellsCosts_"];
      Skill skill17 = card["DealsDamageToOpponentsCreatureByTheirsCostsAtSummoning"];
      Skill skill18 = card["HealsOwnersCreaturesAtSummoning_"];
      Skill skill19 = card["CompletlyHealsOwnersCreatures"];
      Skill skill20 = card["Stun"];
      Skill skill21 = card["DealsDamageToOpponentAndHisCreaturesEqualOwnerElementPower_"];
      Skill skill22 = card["HealsNeighboringOnSummoning_"];
      Skill skill23 = card["StunsOppositSlotOnSummoning"];
      Skill skill24 = card["TakesToOwnerPowersOfAllKindAtSummoning_"];
      ObservableCollection<Field> fields = battlefield.ActivePlayer.Fields;
      int index = fields.IndexOf(field);
      Field field1 = (Field) null;
      Field field2 = (Field) null;
      if (index < fields.Count - 1)
        field2 = fields[index + 1];
      if (index > 0)
        field1 = fields[index - 1];
      Field field3 = battlefield.InactivePlayer.Fields[index];
      if (skill14 != null && !field3.IsEmpty)
        field3.DoHarm(skill14.GetArgumentAsInt(), battlefield);
      if (skill21 != null)
      {
        ElementTypeEnum argumentAsElement = skill21.GetArgumentAsElement();
        int damage = battlefield.ActivePlayer.GetElementByType(argumentAsElement).Mana;
        battlefield.InactivePlayer.DoHarm(damage, battlefield);
        battlefield.InactivePlayer.ForEachBusyFields((Action<Field>) (i => i.DoHarm(damage, battlefield, 1)));
      }
      if (skill24 != null)
        battlefield.ActivePlayer.IncreaseAllElementsPower(skill24.GetArgumentAsInt());
      if (skill23 != null && !field3.IsEmpty)
        field3.Card.IsJustSummoned = true;
      if (increaseOpponentsCardsCost != null)
        battlefield.InactivePlayer.Elements.SelectMany<Element, Card>((Func<Element, IEnumerable<Card>>) (i => (IEnumerable<Card>) i.Cards)).ForEach<Card>((Action<Card>) (i => i.Cost += increaseOpponentsCardsCost.GetArgumentAsInt()));
      if (increaseOpponentsCardsCostPermanent != null)
        battlefield.InactivePlayer.Elements.SelectMany<Element, Card>((Func<Element, IEnumerable<Card>>) (i => (IEnumerable<Card>) i.Cards)).ForEach<Card>((Action<Card>) (i => i.Cost += increaseOpponentsCardsCostPermanent.GetArgumentAsInt()));
      if (increasesOpponentsSpellsCosts != null)
        battlefield.InactivePlayer.Elements.SelectMany<Element, Card>((Func<Element, IEnumerable<Card>>) (i => (IEnumerable<Card>) i.Cards)).OfType<SpellCard>().ForEach<SpellCard>((Action<SpellCard>) (i => i.Cost += increasesOpponentsSpellsCosts.GetArgumentAsInt()));
      if (skill20 != null)
        battlefield.InactivePlayer.IsStunned = true;
      if (field1 != null && !field1.IsEmpty && field1.Card["NeighborsAttacksOnSummoning"] != null || field2 != null && !field2.IsEmpty && field2.Card["NeighborsAttacksOnSummoning"] != null)
        card.IsJustSummoned = false;
      if (skill17 != null)
        battlefield.InactivePlayer.ForEachBusyFields((Action<Field>) (i => i.DoHarm(i.Card.Cost, battlefield, 1)));
      if (skill18 != null)
      {
        int value = skill18.GetArgumentAsInt();
        battlefield.ActivePlayer.ForEachBusyFields((Action<Field>) (i => i.Cure(value, (Action) null)));
      }
      if (skill19 != null)
        battlefield.ActivePlayer.ForEachBusyFields((Action<Field>) (i => i.Cure(i.Card.InitialHealth - i.Card.Health, (Action) null)));
      if (!string.IsNullOrEmpty(card.Ability))
      {
        SpellCard spellCard = CardRegistry.GetCardByName(card.Ability).Clone() as SpellCard;
        Element elementByType = battlefield.ActivePlayer.GetElementByType(card.ElementType);
        Card card1 = elementByType.Cards.First<Card>((Func<Card, bool>) (i => i.Name == card.Name));
        elementByType.Cards[elementByType.Cards.IndexOf(card1)] = (Card) spellCard;
      }
      if (skill22 != null)
      {
        int argumentAsInt = skill22.GetArgumentAsInt();
        if (field1 != null && !field1.IsEmpty)
          field1.Cure(argumentAsInt, (Action) null);
        if (field2 != null && !field2.IsEmpty)
          field2.Cure(argumentAsInt, (Action) null);
      }
      if (skill15 != null)
      {
        int argumentAsInt = skill15.GetArgumentAsInt();
        field1?.SetDamageBonus(field1.DamageBonus + argumentAsInt);
        field2?.SetDamageBonus(field2.DamageBonus + argumentAsInt);
      }
      if (skill13 != null && !field3.IsEmpty)
        field3.DoHarm(field3.Card.Health / 2, battlefield);
      if (skill16 != null)
      {
        int by = skill16.GetArgumentAsInt();
        battlefield.ActivePlayer.Fields.ForEach<Field>((Action<Field>) (i => i.SetDamageBonus(i.DamageBonus + by)));
      }
      if (skill9 != null)
      {
        ElementTypeEnum argumentAsElement = skill9.GetArgumentAsElement();
        battlefield.ActivePlayer.IncreaseMana(argumentAsElement, 2);
      }
      if (skill10 != null)
      {
        ElementTypeEnum argumentAsElement = skill10.GetArgumentAsElement();
        battlefield.ActivePlayer.IncreaseMana(argumentAsElement, 3);
      }
      if (skill2 != null)
        battlefield.InactivePlayer.DoHarm(int.Parse(skill2.Argument), battlefield);
      if (skill1 != null)
      {
        int damage = int.Parse(skill1.Argument);
        battlefield.InactivePlayer.ForEachBusyFields((Action<Field>) (i => i.DoHarm(damage, battlefield)));
        battlefield.ActivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty && i != field)).ForEach<Field>((Action<Field>) (i => i.DoHarm(damage, battlefield)));
      }
      if (skill3 != null)
      {
        int damage = int.Parse(skill3.Argument);
        battlefield.InactivePlayer.ForEachBusyFields((Action<Field>) (i => i.DoHarm(damage, battlefield)));
        battlefield.InactivePlayer.DoHarm(damage, battlefield);
      }
      if (skill4 != null)
      {
        int damage = int.Parse(skill4.Argument);
        battlefield.InactivePlayer.ForEachBusyFields((Action<Field>) (i => i.DoHarm(damage, battlefield)));
      }
      if (skill5 != null)
      {
        int argumentAsInt = skill5.GetArgumentAsInt();
        battlefield.ActivePlayer.DoHarm(argumentAsInt, battlefield);
      }
      if (skill6 != null)
      {
        int argumentAsInt = skill6.GetArgumentAsInt();
        battlefield.ActivePlayer.Cure(argumentAsInt);
      }
      if (skill7 != null)
        battlefield.InactivePlayer.IncreaseAllElementsPower(-skill7.GetArgumentAsInt());
      if (skill8 != null && battlefield.ActivePlayer.GetElementByType(card.ElementType).Mana >= 5)
        battlefield.InactivePlayer.DoHarm(5, battlefield);
      if (skill11 != null)
      {
        Card card2 = CardRegistry.Cards.First<Card>((Func<Card, bool>) (i => i.Name == "ForestSpider"));
        if (field1 != null && field1.IsEmpty)
          field1.AssignCard(card2.Clone() as CreatureCard, (Action) null);
        if (field2 != null && field2.IsEmpty)
          field2.AssignCard(card2.Clone() as CreatureCard, (Action) null);
      }
      if (skill12 != null)
      {
        Card card3 = CardRegistry.Cards.First<Card>((Func<Card, bool>) (i => i.Name == card.Name));
        if (field1 != null && field1.IsEmpty)
          field1.AssignCard(card3.Clone() as CreatureCard, (Action) null);
        if (field2 != null && field2.IsEmpty)
          field2.AssignCard(card3.Clone() as CreatureCard, (Action) null);
      }
      battlefield.FirstPlayer.UpdateCardsIsActive();
      battlefield.SecondPlayer.UpdateCardsIsActive();
    }

    public void OnSummoned(Field field)
    {
      IBattlefield battlefield = GameService.CurrentGame.Battlefield;
      if (battlefield.InactivePlayer.Fields.Any<Field>((Func<Field, bool>) (i => !i.IsEmpty && i.Card["DealsDamageToNewcomers_"] != null)))
        battlefield.InactivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty && i.Card["DealsDamageToNewcomers_"] != null)).ForEach<Field>((Action<Field>) (i => field.DoHarm(i.Card["DealsDamageToNewcomers_"].GetArgumentAsInt(), battlefield, 1)));
      battlefield.FirstPlayer.UpdateCardsIsActive();
      battlefield.SecondPlayer.UpdateCardsIsActive();
    }

    private IEnumerable<Field> GetActivePlayerFields()
    {
      return GameService.CurrentGame.Battlefield.ActivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty));
    }

    public void OnElementPowerChanges(Player player)
    {
      if (player.Fields == null)
        return;
      foreach (Field busyField in player.BusyFields)
      {
        if (busyField.Card["Elemental"] != null)
        {
          int mana = player.GetElementByType(busyField.Card.ElementType).Mana;
          busyField.Card.Damage = mana;
          busyField.Card.InitialDamage = mana;
          busyField.UpdateDamageBonuses();
        }
      }
    }

    public void OnStartGame()
    {
      IBattlefield battlefield = GameService.CurrentGame.Battlefield;
    }

    private void HandleSpecializationSkillsOnStarts(Player player)
    {
      switch (player.Specialization)
      {
        case Specialization.Pyromancer:
          player.IncreaseMana(ElementTypeEnum.Fire, 2);
          break;
        case Specialization.Stormbringer:
          player.IncreaseMana(ElementTypeEnum.Air, 2);
          Field field = player.Fields.FirstOrDefault<Field>((Func<Field, bool>) (i => i.IsEmpty));
          if (field == null)
            break;
          Card card = CardRegistry.Cards.First<Card>((Func<Card, bool>) (i => i.Name == "Griffin"));
          field.AssignCard(card.Clone() as CreatureCard, (Action) null);
          break;
        case Specialization.IceLord:
          player.IncreaseMana(ElementTypeEnum.Water, 2);
          break;
        case Specialization.Druid:
          player.IncreaseMana(ElementTypeEnum.Earth, 2);
          break;
      }
    }
  }
}
