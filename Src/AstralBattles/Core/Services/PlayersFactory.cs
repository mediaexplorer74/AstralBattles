// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Services.PlayersFactory
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using AstralBattles.Core.Ai;
using AstralBattles.Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Storage;
using System.Linq;


namespace AstralBattles.Core.Services
{
  public static class PlayersFactory
  {
    private static Random random = new Random();
    public static readonly int[] DefaultPlayerPowers = new int[5]
    {
      4,
      4,
      4,
      4,
      1
    };

    public static Player CreateOpponent(string name, GameDifficulty gameDifficulty)
    {
      Player opponent = new Player()
      {
        DisplayName = name,
        Name = TournamentService.Instance.GetCurrentOpponent(),
        IsOnTheTop = true,
        IsAi = true,
        Specialization = Specialization.Elementalist
      };
      int[] source = new int[5]{ 3, 3, 3, 3, 1 };
      int num1 = 4;
      int num2 = 80;
      int num3 = 0;
      bool flag1 = gameDifficulty == GameDifficulty.Easy;
      bool flag2 = gameDifficulty == GameDifficulty.Hard;
      switch (name)
      {
        case "Izza":
          source = new int[5]{ 2, 2, 2, 2, 1 };
          num1 = 1;
          num3 = 0;
          num2 = 60;
          opponent.Photo = "Izza";
          opponent.SpecialElement = ElementTypeEnum.Holy;
          opponent.Specialization = Specialization.Elementalist;
          break;
        case "Vatrax":
          source = new int[5]{ 2, 3, 2, 3, 1 };
          num1 = 3;
          num3 = 1;
          num2 = 70;
          opponent.Photo = "Vatrax";
          opponent.SpecialElement = ElementTypeEnum.Sorcery;
          opponent.Specialization = Specialization.Elementalist;
          break;
        case "Roland":
          source = new int[5]{ 3, 3, 3, 3, 1 };
          num1 = 3;
          num3 = 2;
          num2 = 70;
          opponent.Photo = "Roland";
          opponent.SpecialElement = ElementTypeEnum.Mechanical;
          opponent.Specialization = Specialization.Elementalist;
          break;
        case "Kassandra":
          source = new int[5]{ 3, 3, 3, 3, 1 };
          num3 = 3;
          num2 = 80;
          opponent.Photo = "Kassandra";
          opponent.SpecialElement = ElementTypeEnum.Control;
          opponent.Specialization = Specialization.Elementalist;
          if (!flag1)
          {
            opponent.CreaturesAtStart = new string[1]
            {
              "PriestOfFire"
            };
            break;
          }
          break;
        case "Forwor":
          source = new int[5]{ 4, 3, 3, 4, 1 };
          num1 = 4;
          num3 = 4;
          opponent.Health = 90;
          opponent.Photo = "Forwor";
          opponent.SpecialElement = ElementTypeEnum.Demonic;
          opponent.Specialization = Specialization.Elementalist;
          break;
        case "Frida":
          source = new int[5]{ 4, 3, 3, 3, 1 };
          num1 = 5;
          num3 = 5;
          opponent.Health = 90;
          opponent.Photo = "Frida";
          opponent.SpecialElement = ElementTypeEnum.Holy;
          opponent.Specialization = Specialization.Elementalist;
          if (flag2)
          {
            opponent.CreaturesAtStart = new string[1]
            {
              "seasprite"
            };
            break;
          }
          break;
        case "Starper":
          source = new int[5]{ 3, 3, 3, 4, 1 };
          num1 = 6;
          num3 = 6;
          opponent.Health = 100;
          opponent.Photo = "Starper";
          opponent.SpecialElement = ElementTypeEnum.Holy;
          opponent.Specialization = Specialization.Elementalist;
          if (!flag1)
          {
            opponent.CreaturesAtStart = new string[2]
            {
              "ElvenHealer",
              "ElvenHealer"
            };
            break;
          }
          opponent.CreaturesAtStart = new string[1]
          {
            "ElvenHealer"
          };
          break;
        case "Batalus":
          source = new int[5]{ 1, 1, 1, 1, 2 };
          num1 = 7;
          num2 = 60;
          num3 = 7;
          opponent.Health = 110;
          opponent.Photo = "Batalus";
          opponent.SpecialElement = ElementTypeEnum.Death;
          opponent.Specialization = Specialization.Elementalist;
          if (!flag1)
          {
            opponent.CreaturesAtStart = new string[1]
            {
              "Vampire"
            };
            break;
          }
          opponent.CreaturesAtStart = new string[1]
          {
            "Bargul"
          };
          break;
        case "Lina":
          source = new int[5]{ 0, 1, 1, 1, 1 };
          num2 = 120;
          num1 = 8;
          num3 = 8;
          opponent.Photo = "Lina";
          opponent.SpecialElement = ElementTypeEnum.Chaos;
          opponent.Specialization = Specialization.Elementalist;
          if (!flag1)
          {
            opponent.CreaturesAtStart = new string[1]
            {
              "FireElemental"
            };
            break;
          }
          opponent.CreaturesAtStart = new string[2]
          {
            "PriestOfFire",
            "PriestOfFire"
          };
          break;
      }
      if (flag1)
      {
        source = ((IEnumerable<int>) source).Select<int, int>((Func<int, int>) (i => i - 1)).ToArray<int>();
        num2 -= 20;
      }
      if (flag2)
      {
        num2 += 30;
        source = ((IEnumerable<int>) source).Select<int, int>((Func<int, int>) (i => PlayersFactory.random.Next(0, 3) + i)).ToArray<int>();
      }
      int num4;
      opponent.Elements = PlayersFactory.CreateElementsData(num4 = num3 + 1, opponent.Specialization, opponent.SpecialElement, source);
      opponent.AiDifficult = num1;
      opponent.Health = num2;
      return opponent;
    }

    public static Player CreateSecondPlayerForDuel()
    {
      // UWP uses ApplicationData.Current.LocalSettings instead of IsolatedStorageSettings
      var applicationSettings = Windows.Storage.ApplicationData.Current.LocalSettings.Values;
      string str1 = (applicationSettings.ContainsKey("SecondPlayer") ? applicationSettings["SecondPlayer"] : "Player 2").ToString();
      string str2 = (applicationSettings.ContainsKey("SecondPlayerPhoto") ? applicationSettings["SecondPlayerPhoto"] : "face4").ToString();
      int valueOrDefault1 = (int) (applicationSettings.ContainsKey("SecondPlayerSpecialization") ? applicationSettings["SecondPlayerSpecialization"] : Specialization.Elementalist);
      ElementTypeEnum valueOrDefault2 = (ElementTypeEnum) (applicationSettings.ContainsKey("SecondPlayerSpecialElement") ? applicationSettings["SecondPlayerSpecialElement"] : ((IEnumerable<ElementTypeEnum>) SpecialElementsContainer.Elements).GetRandomElement<ElementTypeEnum>());
      ObservableCollection<Element> elements = PlayersFactory.DeckToElements(Deck.Deserialize((applicationSettings.ContainsKey("SecondPlayerDeck") ? applicationSettings["SecondPlayerDeck"] : "").ToStringIfNotNull(), PlayersFactory.CreateRandomDeck(valueOrDefault2)));
      return new Player()
      {
        DisplayName = str1,
        Photo = str2,
        SpecialElement = valueOrDefault2,
        Health = 90,
        Elements = elements,
        Name = str1,
        Specialization = Specialization.Elementalist
      };
    }

    public static Player CreateFirstPlayerForDuel()
    {
      // UWP uses ApplicationData.Current.LocalSettings instead of IsolatedStorageSettings
      var applicationSettings = Windows.Storage.ApplicationData.Current.LocalSettings.Values;
      string str1 = (applicationSettings.ContainsKey("FirstPlayer") ? applicationSettings["FirstPlayer"] : "Player 1").ToString();
      string str2 = (applicationSettings.ContainsKey("FirstPlayerPhoto") ? applicationSettings["FirstPlayerPhoto"] : "face2").ToString();
      int valueOrDefault1 = (int) (applicationSettings.ContainsKey("FirstPlayerSpecialization") ? applicationSettings["FirstPlayerSpecialization"] : Specialization.Elementalist);
      ElementTypeEnum valueOrDefault2 = (ElementTypeEnum) (applicationSettings.ContainsKey("FirstPlayerSpecialElement") ? applicationSettings["FirstPlayerSpecialElement"] : ((IEnumerable<ElementTypeEnum>) SpecialElementsContainer.Elements).GetRandomElement<ElementTypeEnum>());
      ObservableCollection<Element> elements = PlayersFactory.DeckToElements(Deck.Deserialize((applicationSettings.ContainsKey("FirstPlayerDeck") ? applicationSettings["FirstPlayerDeck"] : "").ToStringIfNotNull(), PlayersFactory.CreateRandomDeck(valueOrDefault2)));
      return new Player()
      {
        DisplayName = str1,
        Photo = str2,
        SpecialElement = valueOrDefault2,
        Health = 90,
        Elements = elements,
        Name = str1,
        Specialization = Specialization.Elementalist
      };
    }

    private static ObservableCollection<Element> DeckToElements(Deck deck)
    {
      ObservableCollection<Element> elements = new ObservableCollection<Element>();
      foreach (KeyValuePair<ElementTypeEnum, ObservableCollection<Card>> keyValuePair in (Dictionary<ElementTypeEnum, ObservableCollection<Card>>) deck)
        elements.Add(new Element()
        {
          ElementType = keyValuePair.Key,
          Cards = new ObservableCollection<Card>(keyValuePair.Value.Select<Card, Card>((Func<Card, Card>) (i => i ?? (Card) new FakeCard()))),
          Mana = PlayersFactory.IsSpecial(keyValuePair.Key) ? 1 : 4
        });
      return elements;
    }

    private static bool IsSpecial(ElementTypeEnum key)
    {
      return key != ElementTypeEnum.Fire && key != ElementTypeEnum.Air && key != ElementTypeEnum.Water && key != ElementTypeEnum.Earth;
    }

    public static Deck CreateRandomDeck(ElementTypeEnum specialElement)
    {
      Deck randomDeck = new Deck();
      foreach (Element element in (Collection<Element>) PlayersFactory.CreateElementsData(10, Specialization.Elementalist, specialElement, 4, 4, 4, 4, 1))
        randomDeck[element.ElementType] = element.Cards;
      return randomDeck;
    }

    public static Player CreatePlayer()
    {
      return new Player()
      {
        DisplayName = TournamentService.Instance.Tournament.CurrentPlayer.DisplayName,
        SpecialElement = TournamentService.Instance.Tournament.CurrentPlayer.SpecialElement,
        Health = 80,
        Elements = PlayersFactory.CreateElementsData(TournamentService.Instance.Tournament.CurrentRoundIndex, TournamentService.Instance.Tournament.CurrentPlayer.Specialization, TournamentService.Instance.Tournament.CurrentPlayer.SpecialElement, PlayersFactory.DefaultPlayerPowers),
        Name = TournamentService.Instance.Tournament.CurrentPlayer.Name,
        Specialization = Specialization.Elementalist,
        Photo = TournamentService.Instance.Tournament.CurrentPlayer.Photo
      };
    }

    public static ComputerIntellect GetComputer(IBattlefield bf)
    {
      return (ComputerIntellect) new SmartestComputer();
    }

    public static int GetAiDifficulty(string name)
    {
      int aiDifficulty = 4;
      switch (name)
      {
        case "Lina":
          return 8;
        case "Batalus":
          return 8;
        case "Starper":
        case "Kassandra":
          return 6;
        case "Izza":
          return 2;
        default:
          return aiDifficulty;
      }
    }

    public static ObservableCollection<Element> CreateElementsData(
      int gameIndex,
      Specialization spec,
      ElementTypeEnum specialElement,
      params int[] powers)
    {
      ObservableCollection<Element> elementsData = new ObservableCollection<Element>();
      elementsData.Add(new Element()
      {
        ElementType = ElementTypeEnum.Fire,
        Mana = powers[0],
        Cards = new ObservableCollection<Card>(PlayersFactory.FilterCardsByLevel(CardRegistry.GetCardsByElement(ElementTypeEnum.Fire), spec == Specialization.Pyromancer, gameIndex))
      });
      elementsData.Add(new Element()
      {
        ElementType = ElementTypeEnum.Water,
        Mana = powers[1],
        Cards = new ObservableCollection<Card>(PlayersFactory.FilterCardsByLevel(CardRegistry.GetCardsByElement(ElementTypeEnum.Water), spec == Specialization.IceLord, gameIndex))
      });
      elementsData.Add(new Element()
      {
        ElementType = ElementTypeEnum.Air,
        Mana = powers[2],
        Cards = new ObservableCollection<Card>(PlayersFactory.FilterCardsByLevel(CardRegistry.GetCardsByElement(ElementTypeEnum.Air), spec == Specialization.Stormbringer, gameIndex))
      });
      elementsData.Add(new Element()
      {
        ElementType = ElementTypeEnum.Earth,
        Mana = powers[3],
        Cards = new ObservableCollection<Card>(PlayersFactory.FilterCardsByLevel(CardRegistry.GetCardsByElement(ElementTypeEnum.Earth), spec == Specialization.Druid, gameIndex))
      });
      elementsData.Add(new Element()
      {
        ElementType = specialElement,
        Mana = powers[4],
        Cards = new ObservableCollection<Card>(PlayersFactory.FilterCardsByLevel(CardRegistry.GetCardsByElement(specialElement), true, gameIndex))
      });
      return elementsData;
    }

    private static IEnumerable<Card> FilterCardsByLevel(
      IEnumerable<Card> source,
      bool isSpecialist,
      int game)
    {
      List<Card> list = source.ToList<Card>();
      List<Card> cards = new List<Card>(6);
      for (int index = 1; index < 6; ++index)
      {
        int level = index;
        Card randomElement = list.Where<Card>((Func<Card, bool>) (j => j.Level == level)).GetRandomElement<Card>();
        if (randomElement != null)
          cards.Add(randomElement);
      }
      while (cards.Count<Card>((Func<Card, bool>) (i => i != null)) < 6)
        cards.Add(list.Where<Card>((Func<Card, bool>) (i => !cards.Contains(i))).GetRandomElement<Card>());
      return (IEnumerable<Card>) cards.Select<Card, Card>((Func<Card, Card>) (i => i.Clone())).OrderBy<Card, int>((Func<Card, int>) (i => i.Cost)).ThenBy<Card, int>((Func<Card, int>) (i => i.Level));
    }
  }
}
