// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Services.CardRegistry
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using AstralBattles.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

#nullable disable
namespace AstralBattles.Core.Services
{
  public static class CardRegistry
  {
    private const string CardsResourceName = "AstralBattles.Core.CardDefinitions.xml";
    private static readonly XmlSerializer Serializer = new XmlSerializer(typeof (Card[]));
    private static IEnumerable<Card> cards = (IEnumerable<Card>) null;

    public static void Reload()
    {
      using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AstralBattles.Core.CardDefinitions.xml"))
        CardRegistry.cards = (IEnumerable<Card>) (CardRegistry.Serializer.Deserialize(manifestResourceStream) as Card[]);
    }

    public static IEnumerable<Card> Cards
    {
      get
      {
        if (CardRegistry.cards == null)
          CardRegistry.Reload();
        return CardRegistry.cards;
      }
    }

    public static Card GetCardByName(string name)
    {
      if (CardRegistry.cards == null)
        CardRegistry.Reload();
      return CardRegistry.cards.First<Card>((Func<Card, bool>) (i => i.Name == name));
    }

    public static IEnumerable<Card> GetCardsByElement(ElementTypeEnum type)
    {
      return (IEnumerable<Card>) CardRegistry.Cards.Where<Card>((Func<Card, bool>) (i => i.ElementType == type && !i.IsHidden)).OrderBy<Card, int>((Func<Card, int>) (i => i.Cost)).ThenBy<Card, int>((Func<Card, int>) (i => i.Damage));
    }
  }
}
