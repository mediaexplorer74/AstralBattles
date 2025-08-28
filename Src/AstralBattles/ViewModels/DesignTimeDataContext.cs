// Decompiled with JetBrains decompiler
// Type: AstralBattles.ViewModels.DesignTimeDataContext
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Model;
using AstralBattles.Model;
using System;
using System.Collections.ObjectModel;


namespace AstralBattles.ViewModels
{
  public class DesignTimeDataContext
  {
    private static readonly Random Random = new Random(Environment.TickCount);
    private static readonly DesignTimeDataContext instance = new DesignTimeDataContext();

    public static DesignTimeDataContext Instance => DesignTimeDataContext.instance;

    public string LastLogItem => "Player has summoned a Water element";

    public bool ShowLogMode => false;

    public Player Player
    {
      get
      {
        Player player1 = new Player();
        player1.DisplayName = "Name" + (object) DesignTimeDataContext.Random.Next(1, 10);
        player1.Description = "Some description bla bla bla";
        player1.Name = "0";
        player1.Health = 50;
        player1.Specialization = Specialization.Necromancer;
        Player player2 = player1;
        ObservableCollection<Field> observableCollection1 = new ObservableCollection<Field>();
        observableCollection1.Add(this.Field);
        observableCollection1.Add(this.Field);
        observableCollection1.Add(this.Field);
        ObservableCollection<Field> observableCollection2 = observableCollection1;
        player2.Fields = observableCollection2;
        Player player3 = player1;
        ObservableCollection<Element> observableCollection3 = new ObservableCollection<Element>();
        ObservableCollection<Element> observableCollection4 = observableCollection3;
        Element element1 = new Element();
        element1.ElementType = ElementTypeEnum.Fire;
        element1.Mana = 6;
        Element element2 = element1;
        ObservableCollection<Card> observableCollection5 = new ObservableCollection<Card>();
        observableCollection5.Add(this.Card);
        observableCollection5.Add(this.Card);
        observableCollection5.Add(this.Card);
        observableCollection5.Add(this.Card);
        observableCollection5.Add(this.Card);
        observableCollection5.Add(this.Card);
        observableCollection5.Add(this.Card);
        observableCollection5.Add(this.Card);
        observableCollection5.Add(this.Card);
        observableCollection5.Add(this.Card);
        observableCollection5.Add(this.Card);
        ObservableCollection<Card> observableCollection6 = observableCollection5;
        element2.Cards = observableCollection6;
        Element element3 = element1;
        observableCollection4.Add(element3);
        observableCollection3.Add(new Element()
        {
          ElementType = ElementTypeEnum.Water,
          Mana = 7
        });
        observableCollection3.Add(new Element()
        {
          ElementType = ElementTypeEnum.Air,
          Mana = 12
        });
        observableCollection3.Add(new Element()
        {
          ElementType = ElementTypeEnum.Earth,
          Mana = 3
        });
        observableCollection3.Add(new Element()
        {
          ElementType = ElementTypeEnum.Mechanical,
          Mana = 5
        });
        ObservableCollection<Element> observableCollection7 = observableCollection3;
        player3.Elements = observableCollection7;
        return player1;
      }
    }

    public ObservableCollection<DeckField> PlayerFields
    {
      get
      {
        ObservableCollection<DeckField> playerFields = new ObservableCollection<DeckField>();
        playerFields.Add(new DeckField()
        {
          Card = this.Card
        });
        playerFields.Add(new DeckField()
        {
          Card = this.Card
        });
        playerFields.Add(new DeckField()
        {
          Card = this.Card
        });
        playerFields.Add(new DeckField()
        {
          Card = (Card) null
        });
        return playerFields;
      }
    }

    public GameLogItem[] GameLog
    {
      get
      {
        return new GameLogItem[3]
        {
          new GameLogItem("Player has skiped his turn 1", new object[0]),
          new GameLogItem("Player has skiped his turn 2", new object[0]),
          new GameLogItem("Player has skiped his turn 3", new object[0])
        };
      }
    }

    public ElementTypeEnum[] Elements
    {
      get
      {
        return new ElementTypeEnum[5]
        {
          ElementTypeEnum.Fire,
          ElementTypeEnum.Water,
          ElementTypeEnum.Air,
          ElementTypeEnum.Earth,
          ElementTypeEnum.Holy
        };
      }
    }

    public string CardDescription
    {
      get
      {
        return "When Fire Elemental is summoned it deals 3 damage to opponent and his creatures. Increases by 1 the growth of owner's Fire power";
      }
    }

    public string CardName => "Minotaur commander";

    public Card SelectedCardOrCardType => this.Card;

    public Player FirstPlayer => this.Player;

    public Player SecondPlayer => this.Player;

    public Element FirstPlayerSelectedElement => this.Player.Elements[0];

    public Field Field
    {
      get
      {
        Field field = new Field(new Player());
        field.AssignCard((CreatureCard) this.Card, (Action) null);
        return field;
      }
    }

    public ElementTypeEnum ElementType => ElementTypeEnum.Water;

    public int Mana => 10;

    public Card Card
    {
      get
      {
        CreatureCard card = new CreatureCard();
        card.Cost = 7;
        card.Damage = 6;
        card.Description = "Some description";
        card.DisplayName = "Water element";
        card.ElementType = ElementTypeEnum.Water;
        card.Health = 40;
        card.Level = 5;
        card.IsActive = DesignTimeDataContext.Random.Next(0, 2) == 1;
        card.IsSelected = DesignTimeDataContext.Random.Next(0, 2) == 1;
        card.Name = "WaterElemental";
        card.Localization = new Localization()
        {
          English = new LocalizationItem()
          {
            Description = "www",
            DisplayName = "dd"
          },
          Russian = new LocalizationItem()
          {
            Description = "eee",
            DisplayName = "dd"
          }
        };
        return (Card) card;
      }
    }

    public object SelectedCardShortDescription => throw new NotImplementedException();
  }
}
