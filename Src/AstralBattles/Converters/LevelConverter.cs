// Decompiled with JetBrains decompiler
// Type: AstralBattles.Converters.LevelConverter
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace AstralBattles.Converters
{
  public class LevelConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      switch ((int) value)
      {
        case 1:
          return (object) "I";
        case 2:
          return (object) "II";
        case 3:
          return (object) "III";
        case 4:
          return (object) "IV";
        case 5:
          return (object) "V";
        case 6:
          return (object) "VI";
        default:
          throw new NotSupportedException();
      }
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
