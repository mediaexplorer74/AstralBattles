// Decompiled with JetBrains decompiler
// Type: AstralBattles.Converters.IsCreatureToVisible
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Model;
using System;
using System.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;


namespace AstralBattles.Converters
{
  public class IsCreatureToVisible : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      return (object) (Visibility) (!(value is CreatureCard) ? 1 : 0);
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
