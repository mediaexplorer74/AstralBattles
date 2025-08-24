// Decompiled with JetBrains decompiler
// Type: AstralBattles.Converters.CardImageConverter
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Model;
using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace AstralBattles.Converters
{
  public class CardImageConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (object) this.Convert(value as Card);
    }

    public string Convert(Card card)
    {
      if (card == null)
        return (string) null;
      string str = "/AstralBattles;component/Resources/Cards/" + card.ElementType.ToString() + "/";
      return card is SpellCard || card is FakeCard ? str + "Spells/" + card.Name + ".png" : str + card.Name + ".jpg";
    }

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
