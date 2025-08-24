// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Model.Field
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using AstralBattles.Core.Infrastructure;
using AstralBattles.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

#nullable disable
namespace AstralBattles.Core.Model
{
  public class Field : NotifyPropertyChangedBase, IHarmable
  {
    private CreatureCard card;
    private bool isSelected;
    private int damageBonus;
    private bool isWaitingForSelect;
    private bool isOnTheTop;
    private bool isDying;
    private int singleUseDamageBonus;

    public Field(Player player)
    {
      this.IsOnTheTop = player.IsOnTheTop;
      this.OwnerName = player.Name;
    }

    public Field()
    {
    }

    public CreatureCard Card
    {
      get => this.card;
      set
      {
        this.card = value;
        this.RaisePropertyChanged(nameof (Card));
        this.RaisePropertyChanged("IsEmpty");
      }
    }

    public bool IsOnTheTop
    {
      get => this.isOnTheTop;
      set
      {
        this.isOnTheTop = value;
        this.RaisePropertyChanged(nameof (IsOnTheTop));
      }
    }

    public string OwnerName { get; set; }

    public void AssignCard(CreatureCard c, Action callback)
    {
      this.isDying = false;
      if (c == null)
      {
        this.Card = (CreatureCard) null;
      }
      else
      {
        this.Card = c;
        if (!this.Card.IsNotAttackable)
          this.Card.Damage = this.Card.InitialDamage + this.DamageBonus;
        this.Summoned((object) this, new IntValueChangedEventArgs(0, callback));
      }
    }

    public bool IsEmpty => this.Card == null;

    public bool IsSelected
    {
      get => this.isSelected;
      set
      {
        if (this.isSelected == value)
          return;
        this.isSelected = value;
        this.RaisePropertyChanged(nameof (IsSelected));
        if (!value)
          return;
        this.Selected((object) this, EventArgs.Empty);
      }
    }

    public bool IsWaitingForSelect
    {
      get => this.isWaitingForSelect;
      set
      {
        this.isWaitingForSelect = value;
        this.RaisePropertyChanged(nameof (IsWaitingForSelect));
      }
    }

    public int DamageBonus
    {
      get => this.damageBonus;
      set
      {
        this.damageBonus = value;
        this.RaisePropertyChanged(nameof (DamageBonus));
      }
    }

    public void SetDamageBonus(int value)
    {
      if (!this.IsEmpty && !this.Card.IsNotAttackable)
        this.Card.Damage = this.Card.InitialDamage + value + this.SingleUseDamageBonus;
      this.DamageBonus = value;
    }

    public bool DoHarm(
      int value,
      IBattlefield bf,
      bool kill = false,
      bool byMagic = false,
      bool quickDeath = false,
      bool ignoreReplacedBy = false)
    {
      Player playerByName = bf.GetPlayerByName(this.OwnerName);
      Player player = bf.FirstPlayer.Name == this.OwnerName ? bf.SecondPlayer : bf.FirstPlayer;
      value = Math.Abs(value);
      if (this.isDying)
        return playerByName.DoHarm(value, bf);
      Skill skill1 = this.Card["IgnoredDamageLessThan_"];
      Skill skill2 = this.Card["MagicImmune"];
      Skill skill3 = this.Card["MaxRecievedDamageIs_"];
      Skill skill4 = this.Card["ReflectsDamageToOpponent"];
      if (skill3 != null)
      {
        int argumentAsInt = skill3.GetArgumentAsInt();
        if (value > argumentAsInt)
          value = argumentAsInt;
      }
      if (skill2 != null && byMagic)
        return false;
      if (skill1 != null && !kill)
      {
        int argumentAsInt = skill1.GetArgumentAsInt();
        if (value <= argumentAsInt)
          return false;
        value -= argumentAsInt;
      }
      ObservableCollection<Field> fields = playerByName.Fields;
      int num1 = fields.IndexOf(this);
      Field field1 = (Field) null;
      Field field2 = (Field) null;
      if (num1 < fields.Count - 1)
        field2 = fields[num1 + 1];
      if (num1 > 0)
        field1 = fields[num1 - 1];
      int num2 = 0;
      if (field1 != null && !field1.IsEmpty && field1.Card["ProtectsNeighboringBy_"] != null)
        num2 += field1.Card["ProtectsNeighboringBy_"].GetArgumentAsInt();
      if (field2 != null && !field2.IsEmpty && field2.Card["ProtectsNeighboringBy_"] != null)
        num2 += field2.Card["ProtectsNeighboringBy_"].GetArgumentAsInt();
      value -= num2;
      if (value < 0)
        value = 0;
      if (kill)
        value = this.Card.Health;
      if (skill4 != null)
      {
        if (value > this.Card.InitialHealth)
          value = this.Card.InitialHealth;
        player.DoHarm(value, bf);
      }
      this.Card.Health -= value;
      this.GotDamage((object) this, new IntValueChangedEventArgs(value, (Action) null));
      if (this.Card.Health >= 1)
        return this.Card.Health < 1;
      this.CheckIfThereAreAnyDeathLords(bf);
      this.CheckIfCreatureWithBonusDied(bf);
      foreach (Field field3 in player.BusyFields.Where<Field>((Func<Field, bool>) (i => i.Card["GivesRandomPowerWhenAnyCreatureDies_"] != null)))
      {
        int argumentAsInt = field3.Card["GivesRandomPowerWhenAnyCreatureDies_"].GetArgumentAsInt();
        player.IncreaseMana(player.Elements.GetRandomElement<Element>().ElementType, argumentAsInt);
        field3.DoAct();
      }
      foreach (Field field4 in playerByName.BusyFields.Where<Field>((Func<Field, bool>) (i => i.Card["GivesRandomPowerWhenAnyCreatureDies_"] != null)))
      {
        int argumentAsInt = field4.Card["GivesRandomPowerWhenAnyCreatureDies_"].GetArgumentAsInt();
        playerByName.IncreaseMana(playerByName.Elements.GetRandomElement<Element>().ElementType, argumentAsInt);
        field4.DoAct();
      }
      if (this.card["Phoenix"] != null && playerByName.GetElementByType(ElementTypeEnum.Fire).Mana >= 10)
      {
        this.Card.Health = this.Card.InitialHealth;
        return false;
      }
      this.isDying = true;
      if (bf.FirstPlayer.Specialization == Specialization.Necromancer)
        bf.FirstPlayer.Cure(4);
      if (bf.SecondPlayer.Specialization == Specialization.Necromancer)
        bf.SecondPlayer.Cure(4);
      Skill skill5 = this.card["IncreasesPowerBy2WhenDying_"];
      if (skill5 != null)
        playerByName.IncreaseMana(skill5.GetArgumentAsElement(), 2);
      Skill skill6 = this.card["IncreaseNeighborAttackBy_"];
      Skill skill7 = this.card["IncreaseOwnersCreaturesAttack_"];
      Skill skill8 = this.card["DecreasesAllOpponentsPowersWhenKilled_"];
      Skill skill9 = this.card["ExplodesWhenDied_"];
      Skill skill10 = this.card["OpponentReceivesHPwhenDied_"];
      if (skill10 != null)
      {
        int argumentAsInt = skill10.GetArgumentAsInt();
        player.Cure(argumentAsInt);
      }
      if (skill9 != null)
      {
        int damage = skill9.GetArgumentAsInt();
        player.ForEachBusyFields((Action<Field>) (i => i.DoHarm(damage, bf, 1)));
        playerByName.ForEachBusyFields((Action<Field>) (i => i.DoHarm(damage, bf, 1)));
      }
      if (skill8 != null)
        playerByName.IncreaseAllElementsPower(-skill8.GetArgumentAsInt());
      if (skill6 != null)
      {
        int argumentAsInt = skill6.GetArgumentAsInt();
        field1?.SetDamageBonus(field1.DamageBonus - argumentAsInt);
        field2?.SetDamageBonus(field2.DamageBonus - argumentAsInt);
      }
      if (skill7 != null)
      {
        int by = skill7.GetArgumentAsInt();
        playerByName.Fields.ForEach<Field>((Action<Field>) (i => i.SetDamageBonus(i.DamageBonus - by)));
      }
      if (!string.IsNullOrEmpty(this.card.Ability))
      {
        CreatureCard creatureCard = CardRegistry.GetCardByName(this.card.Name).Clone() as CreatureCard;
        creatureCard.Cost = this.card.Cost;
        Element elementByType = playerByName.GetElementByType(this.card.ElementType);
        AstralBattles.Core.Model.Card card = elementByType.Cards.FirstOrDefault<AstralBattles.Core.Model.Card>((Func<AstralBattles.Core.Model.Card, bool>) (i => i.Name == this.card.Ability));
        if (card != null)
          elementByType.Cards[elementByType.Cards.IndexOf(card)] = (AstralBattles.Core.Model.Card) creatureCard;
      }
      if (!quickDeath)
      {
        this.Died((object) this, new IntValueChangedEventArgs(value, (Action) (() =>
        {
          if (!string.IsNullOrEmpty(this.Card.ReplacedBy))
          {
            AstralBattles.Core.Model.Card cardByName = CardRegistry.GetCardByName(this.Card.ReplacedBy);
            this.Card = (CreatureCard) null;
            this.AssignCard((CreatureCard) cardByName.Clone(), (Action) null);
          }
          else
            this.Card = (CreatureCard) null;
          this.isDying = false;
        })));
      }
      else
      {
        this.Card = (CreatureCard) null;
        this.isDying = false;
      }
      return true;
    }

    private void CheckIfCreatureWithBonusDied(IBattlefield bf)
    {
      Player player = bf.FirstPlayer.Name == this.OwnerName ? bf.SecondPlayer : bf.FirstPlayer;
      if (this.Card["IncreaseOpponentsCardsCost_"] != null)
      {
        int value = this.Card["IncreaseOpponentsCardsCost_"].GetArgumentAsInt();
        player.Elements.SelectMany<Element, AstralBattles.Core.Model.Card>((Func<Element, IEnumerable<AstralBattles.Core.Model.Card>>) (i => (IEnumerable<AstralBattles.Core.Model.Card>) i.Cards)).ForEach<AstralBattles.Core.Model.Card>((Action<AstralBattles.Core.Model.Card>) (i => i.Cost -= value));
      }
      if (this.Card["IncreasesOpponentsSpellsCosts_"] == null)
        return;
      int value1 = this.Card["IncreasesOpponentsSpellsCosts_"].GetArgumentAsInt();
      player.Elements.SelectMany<Element, AstralBattles.Core.Model.Card>((Func<Element, IEnumerable<AstralBattles.Core.Model.Card>>) (i => (IEnumerable<AstralBattles.Core.Model.Card>) i.Cards)).OfType<SpellCard>().ForEach<SpellCard>((Action<SpellCard>) (i => i.Cost -= value1));
    }

    private void CheckIfThereAreAnyDeathLords(IBattlefield bf)
    {
      Player player = bf.FirstPlayer.Name == this.OwnerName ? bf.SecondPlayer : bf.FirstPlayer;
      int num = player.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty && i.Card.SkillsTable.ContainsKey("EarnDeathPowerOnOpponentsCreatureDeath"))).Count<Field>();
      if (num <= 0)
        return;
      player.IncreaseMana(ElementTypeEnum.Death, num);
    }

    public void Cure(int value, Action callback, bool ignoreLimit = false)
    {
      try
      {
        value = value < 0 ? 0 : value;
        int num1 = value;
        if (ignoreLimit)
        {
          this.Card.Health += value;
        }
        else
        {
          int num2 = this.Card.InitialHealth - this.Card.Health;
          if (num2 > 0)
          {
            if (num2 <= value)
            {
              this.Card.Health += num2;
              num1 = num2;
            }
            else
            {
              this.Card.Health += value;
              num1 = value;
            }
          }
          else
            num1 = 0;
        }
        this.GotHealth((object) this, new IntValueChangedEventArgs(num1, callback));
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public void DoAct() => this.Acts((object) this, EventArgs.Empty);

    public event EventHandler Acts = delegate { };

    public event EventHandler<IntValueChangedEventArgs> Skip = delegate { };

    public event EventHandler Selected = delegate { };

    public event EventHandler<IntValueChangedEventArgs> Attacking = delegate { };

    public event EventHandler<IntValueChangedEventArgs> GotDamage = delegate { };

    public event EventHandler<IntValueChangedEventArgs> GotHealth = delegate { };

    public event EventHandler<IntValueChangedEventArgs> Died = delegate { };

    public event EventHandler<IntValueChangedEventArgs> Kills = delegate { };

    public event EventHandler<IntValueChangedEventArgs> Summoned = delegate { };

    public void Attack(Field field, IBattlefield bf, Action callback)
    {
      int index = bf.ActivePlayer.Fields.IndexOf(field);
      if (index == -1)
        return;
      Field field1 = bf.InactivePlayer.Fields[index];
      IHarmable harmable = !field1.IsEmpty ? (IHarmable) field1 : (IHarmable) bf.InactivePlayer;
      if (!field1.IsEmpty && field1.Card["Horror"] != null)
      {
        int mana = bf.InactivePlayer.GetElementByType(bf.InactivePlayer.SpecialElement).Mana;
        if (!field.IsEmpty && field.Card.Cost < mana)
        {
          callback();
          return;
        }
      }
      if (field1.IsEmpty && field.Card["KillsCheapestOpponentsCardTypeWhenAttacksEmptySlot"] != null)
      {
        Element randomElement = bf.InactivePlayer.Elements.GetRandomElement<Element>();
        AstralBattles.Core.Model.Card card = randomElement.Cards.OrderBy<AstralBattles.Core.Model.Card, int>((Func<AstralBattles.Core.Model.Card, int>) (i => i.Cost)).FirstOrDefault<AstralBattles.Core.Model.Card>();
        if (!(card is FakeCard))
          randomElement.Cards[randomElement.Cards.IndexOf(card)] = (AstralBattles.Core.Model.Card) new FakeCard();
      }
      if (!this.IsEmpty && !field1.IsEmpty && field1.Card["Mindstealer"] != null)
      {
        this.Attacking((object) this, new IntValueChangedEventArgs(0, callback));
        this.DoHarm(this.Card.Damage, bf);
      }
      else if (this.Card.IsNotAttackable)
        callback();
      else if (this.Card.IsJustSummoned && this.Card["AttackIn1stRound"] == null)
      {
        this.Card.IsJustSummoned = false;
        this.Skip((object) this, new IntValueChangedEventArgs(0, callback));
      }
      else
      {
        Skill skill1 = this.Card["AttackAllEnimies"];
        Skill skill2 = this.Card["Vampire"];
        this.Card.IsJustSummoned = false;
        this.Attacking((object) this, new IntValueChangedEventArgs(0, callback));
        if (skill1 != null)
        {
          List<bool> harmResults = new List<bool>();
          bf.InactivePlayer.Fields.Where<Field>((Func<Field, bool>) (i => !i.IsEmpty)).ForEach<Field>((Action<Field>) (i => harmResults.Add(i.DoHarm(this.Card.Damage, bf))));
          bf.InactivePlayer.DoHarm(this.Card.Damage, bf);
          harmResults.Where<bool>((Func<bool, bool>) (i => i)).ForEach<bool>((Action<bool>) (i => this.Kills((object) this, new IntValueChangedEventArgs(1, (Action) null))));
        }
        else
        {
          if (harmable.DoHarm(this.Card.Damage, bf))
            this.Kills((object) this, new IntValueChangedEventArgs(1, (Action) null));
          if (skill2 != null)
            this.Cure(this.Card.Damage / 2, (Action) null, true);
        }
        this.SetSingleUseDamageBonus(0);
      }
    }

    public override string ToString()
    {
      return string.Format("Field: IsEmpty={0}, IsSelected={1}, [Card={2}]", (object) this.IsEmpty, (object) this.IsSelected, (object) this.Card);
    }

    public bool DoHarm(int value, IBattlefield bf)
    {
      return this.DoHarm(value, bf, false, false, false, false);
    }

    public bool DoHarm(int value, IBattlefield bf, int byMagic = 0)
    {
      return this.DoHarm(value, bf, byMagic: byMagic == 1);
    }

    public void ClearSubscriptions()
    {
      this.Summoned = (EventHandler<IntValueChangedEventArgs>) delegate { };
      this.Attacking = (EventHandler<IntValueChangedEventArgs>) delegate { };
    }

    public int SingleUseDamageBonus
    {
      get => this.singleUseDamageBonus;
      set
      {
        this.singleUseDamageBonus = value;
        this.RaisePropertyChanged(nameof (SingleUseDamageBonus));
      }
    }

    public void SetSingleUseDamageBonus(int i)
    {
      this.SingleUseDamageBonus = i;
      if (this.IsEmpty || this.Card.IsNotAttackable || this.Card.IsJustSummoned)
        return;
      this.Card.Damage = this.Card.InitialDamage + this.DamageBonus + this.SingleUseDamageBonus;
    }

    public void UpdateDamageBonuses()
    {
      this.SetDamageBonus(this.DamageBonus);
      this.SetSingleUseDamageBonus(this.SingleUseDamageBonus);
    }

    public void RemoveCardSafely()
    {
      this.isDying = false;
      this.Card = (CreatureCard) null;
    }
  }
}
