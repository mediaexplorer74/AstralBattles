using AstralBattles.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using Windows.UI.Xaml.Shapes;


namespace AstralBattles.Core.Services
{
  public static class CardRegistry
  {
    private const string CardsResourceName = "AstralBattles.Resources.CardDefinitions.xml";//"AstralBattles.Core.CardDefinitions.xml";
    private static readonly XmlSerializer Serializer = new XmlSerializer(typeof (Card[]));
    private static IEnumerable<Card> cards = (IEnumerable<Card>) null;

       
    public static void Reload()
    {
        Assembly asmb = typeof(CardRegistry).GetTypeInfo().Assembly;
        var m_AssemblyName = asmb.FullName;

        // RnD / TODO / fix it
        using (Stream stream = asmb.GetManifestResourceStream(CardsResourceName))
        { 
           if (stream != null)
            CardRegistry.cards = (IEnumerable<Card>)(CardRegistry.Serializer.Deserialize(stream) as Card[]);
        }
       
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
