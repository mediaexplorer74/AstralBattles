// Decompiled with JetBrains decompiler
// Type: AstralBattles.Converters.TrueToGrayColorConverter
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

#nullable disable
namespace AstralBattles.Converters
{
  public class TrueToGrayColorConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (bool) value ? (object) new SolidColorBrush(Color.FromArgb(byte.MaxValue, (byte) 58, (byte) 58, (byte) 58)) : (object) new SolidColorBrush(Colors.Black);
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
