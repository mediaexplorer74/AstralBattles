// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Model.Deck
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using AstralBattles.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

#nullable disable
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
          // ISSUE: variable of a compiler-generated type
          Deck.\u003C\u003Ec__DisplayClass1 cDisplayClass1 = this;
          // ISSUE: reference to a compiler-generated field
          string cards1 = cDisplayClass1.cards;
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
          // ISSUE: reference to a compiler-generated field
          cDisplayClass1.cards = cards1 + str2 + ";";
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
