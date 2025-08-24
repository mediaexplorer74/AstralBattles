// Decompiled with JetBrains decompiler
// Type: AstralBattles.Converters.BindToElementConverter
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace AstralBattles.Converters
{
  public class BindToElementConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return ((IList) value)[int.Parse(parameter.ToString())];
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
