// Decompiled with JetBrains decompiler
// Type: AstralBattles.Converters.TrueToColorConverter
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Helpers;
using System;
using System.Globalization;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;


namespace AstralBattles.Converters
{
  public class TrueToColorConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      return (bool) value ? (object) new SolidColorBrush(("#" + parameter).ToColor()) : (object) new SolidColorBrush();
    }

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      string language)
    {
      throw new NotImplementedException();
    }
  }
}
