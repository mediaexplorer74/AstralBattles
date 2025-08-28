using AstralBattles.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace AstralBattles.Core.Model
{
  public class Deck : Dictionary<ElementTypeEnum, ObservableCollection<Card>>
  {
    public static string Serialize(Deck deck)
    {
      string str1 = "";
      foreach (KeyValuePair<ElementTypeEnum, ObservableCollection<Card>> keyValuePair in (Dictionary<ElementTypeEnum, ObservableCollection<Card>>) deck)
      {
        string cards = "";
        keyValuePair.Value.ForEach<Card>((Action<Card>) (i =>
        {
          string str2;
          switch (i)
          {
            case null:
            case FakeCard _:
              str2 = "NULL";
              break;
            default:
              str2 = i.Name;
              break;
          }
          cards = cards + str2 + ";";
        }));
        str1 += string.Format("[{0}:{1}]", (object) keyValuePair.Key, (object) cards);
      }
      return str1;
    }

    public static Deck Deserialize(string str, Deck defaultDeck)
    {
      if (string.IsNullOrWhiteSpace(str))
        return defaultDeck;
      try
      {
        Deck deck = new Deck();
        foreach (string str1 in ((IEnumerable<string>) str.Split(new string[1]
        {
          "]["
        }, StringSplitOptions.RemoveEmptyEntries)).Select<string, string>((Func<string, string>) (i => i.Replace("[", "").Replace("]", ""))))
        {
          string[] strArray = str1.Split(':');
          ElementTypeEnum key = (ElementTypeEnum) Enum.Parse(typeof (ElementTypeEnum), strArray[0], true);
          IEnumerable<Card> collection = ((IEnumerable<string>) strArray[1].Split(new string[1]
          {
            ";"
          }, StringSplitOptions.RemoveEmptyEntries)).Select<string, Card>((Func<string, Card>) (i => i == "NULL" ? (Card) null : CardRegistry.GetCardByName(i)));
          deck[key] = new ObservableCollection<Card>(collection);
        }
        return deck;
      }
      catch (Exception ex)
      {
        return defaultDeck;
      }
    }
  }
}
