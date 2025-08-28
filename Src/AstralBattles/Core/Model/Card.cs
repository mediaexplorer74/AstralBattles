// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Model.Card
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using AstralBattles.Core.Infrastructure;
using AstralBattles.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;


namespace AstralBattles.Core.Model
{
  [XmlInclude(typeof (FakeCard))]
  [XmlInclude(typeof (CreatureCard))]
  [XmlInclude(typeof (SpellCard))]
  public class Card : NotifyPropertyChangedBase
  {
    private int cost;
    private int damage;
    private string description;
    private string displayName;
    private ElementTypeEnum elementType;
    private int health;
    private int level;
    private bool isActive;
    private bool isSelected;
    private string skills;
    private string name;
    private Localization localization;
    private Dictionary<string, Skill> skillsArray = new Dictionary<string, Skill>();
    private bool isHidden;

    public string DisplayName
    {
      get => this.displayName;
      set
      {
        this.displayName = value;
        this.RaisePropertyChanged(nameof (DisplayName));
      }
    }

    public bool IsHidden
    {
      get => this.isHidden;
      set
      {
        this.isHidden = value;
        this.RaisePropertyChanged(nameof (IsHidden));
      }
    }

    public string Name
    {
      get => this.name;
      set
      {
        this.name = value;
        this.RaisePropertyChanged(nameof (Name));
      }
    }

    public Localization Localization
    {
      get => this.localization;
      set
      {
        this.localization = value;
        this.RaisePropertyChanged(nameof (Localization));
      }
    }

    public string Description
    {
      get => this.description;
      set
      {
        this.description = value;
        this.RaisePropertyChanged(nameof (Description));
      }
    }

    public bool IsActive
    {
      get => this.isActive;
      set
      {
        if (this.isActive == value)
          return;
        this.isActive = value;
        this.RaisePropertyChanged(nameof (IsActive));
      }
    }

    public bool IsSelected
    {
      get => this.isSelected;
      set
      {
        this.isSelected = value;
        this.RaisePropertyChanged(nameof (IsSelected));
      }
    }

    public ElementTypeEnum ElementType
    {
      get => this.elementType;
      set
      {
        this.elementType = value;
        this.RaisePropertyChanged(nameof (ElementType));
      }
    }

    public int Cost
    {
      get => this.cost;
      set
      {
        this.cost = value;
        this.RaisePropertyChanged(nameof (Cost));
      }
    }

    public int Damage
    {
      get => this.damage;
      set
      {
        this.damage = value;
        this.RaisePropertyChanged(nameof (Damage));
        this.RaisePropertyChanged("DamageString");
      }
    }

    [XmlIgnore]
    public string DamageString => !this.IsDamageIndeterminated ? this.Damage.ToString() : "?";

    public int Health
    {
      get => this.health;
      set
      {
        this.health = value;
        this.RaisePropertyChanged(nameof (Health));
      }
    }

    public bool IsDamageIndeterminated { get; set; }

    public int Level
    {
      get => this.level;
      set
      {
        this.level = value;
        this.RaisePropertyChanged(nameof (Level));
      }
    }

    public virtual Card Clone() => new Card();

    public string Skills
    {
      get => this.skills;
      set
      {
        this.skills = value;
        this.skillsArray = SkillsRegistry.ParseSkills(value).ToDictionary<Skill, string, Skill>((Func<Skill, string>) (i => i.Name), (Func<Skill, Skill>) (i => i));
        this.RaisePropertyChanged(nameof (Skills));
        this.RaisePropertyChanged("SkillsArray");
      }
    }

    [XmlIgnore]
    public Skill this[string skillName]
    {
      get
      {
        Skill skill = (Skill) null;
        this.SkillsTable.TryGetValue(skillName, out skill);
        return skill;
      }
    }

    [XmlIgnore]
    public Dictionary<string, Skill> SkillsTable => this.skillsArray;

    public override string ToString()
    {
      return string.Format("Card {0} ({1})", (object) this.Name, (object) this.ElementType);
    }
  }
}
