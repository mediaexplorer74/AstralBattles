// Decompiled with JetBrains decompiler
// Type: AstralBattles.Converters.TrueToOpacityConverter
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace AstralBattles.Converters
{
  public class TrueToOpacityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return value is bool flag ? (object) (flag ? 1.0 : (parameter == null ? 0.4 : double.Parse(parameter.ToString(), (IFormatProvider) CultureInfo.InvariantCulture))) : (object) 0;
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
