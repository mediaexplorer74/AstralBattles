// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Services.SkillsRegistry
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using AstralBattles.Core.Model;
using System;
using System.Collections.Generic;

#nullable disable
namespace AstralBattles.Core.Services
{
  public class SkillsRegistry
  {
    public const string AttackAllEnimies = "AttackAllEnimies";
    public const string AttackIn1stRound = "AttackIn1stRound";
    public const string CompletlyHealsOwnersCreatures = "CompletlyHealsOwnersCreatures";
    public const string DamageEqualOpponentsCreaturesCount = "DamageEqualOpponentsCreaturesCount";
    public const string DealDamageToMostHitPointedOpponentsCreature_ = "DealDamageToMostHitPointedOpponentsCreature_";
    public const string DealDamageToNeigboringSlots_ = "DealDamageToNeigboringSlots_";
    public const string DealDamageToOpponentEveryRound_ = "DealDamageToOpponentEveryRound_";
    public const string DealDamageToOpposingSlot_ = "DealDamageToOpposingSlot_";
    public const string DealDamageToOwnerEveryRound_ = "DealDamageToOwnerEveryRound_";
    public const string DealsDamageToAllOpponentsCreaturesEachTurn_ = "DealsDamageToAllOpponentsCreaturesEachTurn_";
    public const string DealsDamageToNewcomers_ = "DealsDamageToNewcomers_";
    public const string DealsDamageToOpponentAndHisCreaturesEqualOwnerElementPower_ = "DealsDamageToOpponentAndHisCreaturesEqualOwnerElementPower_";
    public const string DealsDamageToOpponentEqualsSpecialElementManaEachTurn = "DealsDamageToOpponentEqualsSpecialElementManaEachTurn";
    public const string DealsDamageToOpponentFrom1To6 = "DealsDamageToOpponentFrom1To6";
    public const string DealsDamageToOpponentsCreatureByTheirsCostsAtSummoning = "DealsDamageToOpponentsCreatureByTheirsCostsAtSummoning";
    public const string DealsDamageToOppositeSlotHalflife = "DealsDamageToOppositeSlotHalflife";
    public const string DealsDamageToRandomOpponentsCreature_ = "DealsDamageToRandomOpponentsCreature_";
    public const string DealsDamageToSkipper_ = "DealsDamageToSkipper_";
    public const string DecreaseOpponentPowersEachTurn_ = "DecreaseOpponentPowersEachTurn_";
    public const string DecreasesAllOpponentsPowersWhenKilled_ = "DecreasesAllOpponentsPowersWhenKilled_";
    public const string DecreasesOpponentsElementPowerBy1EachTurn_ = "DecreasesOpponentsElementPowerBy1EachTurn_";
    public const string DestroysWeakOpponentsCreaturesEachTurnEnd_ = "DestroysWeakOpponentsCreaturesEachTurnEnd_";
    public const string DoDamageAtSummoningToAll_ = "DoDamageAtSummoningToAll_";
    public const string DoDamageAtSummoningToOpponent_ = "DoDamageAtSummoningToOpponent_";
    public const string DoDamageAtSummoningToOpponentAndHisCards_ = "DoDamageAtSummoningToOpponentAndHisCards_";
    public const string DoDamageAtSummoningToOpponentsCards_ = "DoDamageAtSummoningToOpponentsCards_";
    public const string DoDamageToOpponentAtSummoning_ = "DoDamageToOpponentAtSummoning_";
    public const string EarnDeathPowerOnOpponentsCreatureDeath = "EarnDeathPowerOnOpponentsCreatureDeath";
    public const string Elemental = "Elemental";
    public const string ExplodesWhenDied_ = "ExplodesWhenDied_";
    public const string GiantSpiderSkill = "GiantSpiderSkill";
    public const string GivesRandomPowerWhenAnyCreatureDies_ = "GivesRandomPowerWhenAnyCreatureDies_";
    public const string GotDamageBonusByNeighboring_ = "GotDamageBonusByNeighboring_";
    public const string GriffinSkill = "GriffinSkill";
    public const string HealEveryone_ = "HealEveryone_";
    public const string HealOwnerEachTurn_ = "HealOwnerEachTurn_";
    public const string HealPlayerAtSummoning_ = "HealPlayerAtSummoning_";
    public const string HealsNeighboringOnSummoning_ = "HealsNeighboringOnSummoning_";
    public const string HealsOwnerEachTurnFrom1To6 = "HealsOwnerEachTurnFrom1To6";
    public const string HealsOwnersCreaturesAtSummoning_ = "HealsOwnersCreaturesAtSummoning_";
    public const string Horror = "Horror";
    public const string IgnoredDamageLessThan_ = "IgnoredDamageLessThan_";
    public const string Immune = "Immune";
    public const string IncreaseCardsCostForEveryone_ = "IncreaseCardsCostForEveryone_";
    public const string IncreaseCardsCostForEveryone2xTimes = "IncreaseCardsCostForEveryone2xTimes";
    public const string IncreaseElementPowerBy1EachTurn_ = "IncreaseElementPowerBy1EachTurn_";
    public const string IncreaseElementPowerBy2EachTurn_ = "IncreaseElementPowerBy2EachTurn_";
    public const string IncreaseNeighborAttackBy_ = "IncreaseNeighborAttackBy_";
    public const string IncreaseOpponentsCardsCost_ = "IncreaseOpponentsCardsCost_";
    public const string IncreaseOpponentsCardsCostPermanently_ = "IncreaseOpponentsCardsCostPermanently_";
    public const string IncreaseOwnersCreaturesAttack_ = "IncreaseOwnersCreaturesAttack_";
    public const string IncreaseOwnersPowers_ = "IncreaseOwnersPowers_";
    public const string IncreasePowerOnSummoningBy2_ = "IncreasePowerOnSummoningBy2_";
    public const string IncreasePowerOnSummoningBy3_ = "IncreasePowerOnSummoningBy3_";
    public const string IncreasesElementPowerBy1EachTurn_ = "IncreasesElementPowerBy1EachTurn_";
    public const string IncreasesOpponentsSpellsCosts_ = "IncreasesOpponentsSpellsCosts_";
    public const string IncreaseSpellsDamage_ = "IncreaseSpellsDamage_";
    public const string IncreasesPowerBy2WhenDying_ = "IncreasesPowerBy2WhenDying_";
    public const string IncreasesRandomOwnersPowerEachTurn_ = "IncreasesRandomOwnersPowerEachTurn_";
    public const string KillsCheapestOpponentsCardTypeWhenAttacksEmptySlot = "KillsCheapestOpponentsCardTypeWhenAttacksEmptySlot";
    public const string MagicImmune = "MagicImmune";
    public const string Marching = "Marching";
    public const string MaxRecievedDamageIs_ = "MaxRecievedDamageIs_";
    public const string Mindstealer = "Mindstealer";
    public const string MovesToEmptySlotIfOppositingSlotIsNotEmpty = "MovesToEmptySlotIfOppositingSlotIsNotEmpty";
    public const string MovesToRandomSlotEachTurn = "MovesToRandomSlotEachTurn";
    public const string MustBeSummonedIntoBusySlot = "MustBeSummonedIntoBusySlot";
    public const string NeighborsAttacksOnSummoning = "NeighborsAttacksOnSummoning";
    public const string OpponentReceivesHPwhenDied_ = "OpponentReceivesHPwhenDied_";
    public const string Phoenix = "Phoenix";
    public const string PhoenixHydra = "PhoenixHydra";
    public const string ProtectsNeighboringBy_ = "ProtectsNeighboringBy_";
    public const string ReduceDamageToOwnerByHalf = "ReduceDamageToOwnerByHalf";
    public const string ReducedOpponentsPowers_ = "ReducedOpponentsPowers_";
    public const string ReducedRandomOpponentsPowerEachTurn_ = "ReducedRandomOpponentsPowerEachTurn_";
    public const string ReducesAttackOfOpponentsCreatures_ = "ReducesAttackOfOpponentsCreatures_";
    public const string ReducesRandomOwnersPowerEachTurn_ = "ReducesRandomOwnersPowerEachTurn_";
    public const string ReflectsDamageToOpponent = "ReflectsDamageToOpponent";
    public const string Regeneration_ = "Regeneration_";
    public const string Revolutionary = "Revolutionary";
    public const string SpellsDamageIncreaseBy50Percentage = "SpellsDamageIncreaseBy50Percentage";
    public const string Stun = "Stun";
    public const string StunsOppositingSlotEachTurn = "StunsOppositingSlotEachTurn";
    public const string StunsOppositSlotOnSummoning = "StunsOppositSlotOnSummoning";
    public const string SummonedSoldierToRandomSlotEachTurn = "SummonedSoldierToRandomSlotEachTurn";
    public const string SummonsClonesToNeighboringSlots = "SummonsClonesToNeighboringSlots";
    public const string TakesDamageToOwnerToItselfInstead = "TakesDamageToOwnerToItselfInstead";
    public const string TakesToOwnerPowersOfAllKindAtSummoning_ = "TakesToOwnerPowersOfAllKindAtSummoning_";
    public const string Vampire = "Vampire";

    public static List<Skill> ParseSkills(string skills)
    {
      List<Skill> skills1 = new List<Skill>();
      if (string.IsNullOrWhiteSpace(skills))
        return skills1;
      string str1 = skills;
      string[] separator = new string[1]{ ";" };
      foreach (string str2 in str1.Split(separator, StringSplitOptions.RemoveEmptyEntries))
      {
        if (str2.Contains("_"))
        {
          string[] strArray = str2.Split('_');
          skills1.Add(new Skill()
          {
            Argument = strArray[1],
            Name = strArray[0] + "_"
          });
        }
        else
          skills1.Add(new Skill() { Name = str2 });
      }
      return skills1;
    }
  }
}
