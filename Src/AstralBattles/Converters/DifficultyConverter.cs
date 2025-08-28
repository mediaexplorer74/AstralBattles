// Decompiled with JetBrains decompiler
// Type: AstralBattles.Converters.DifficultyConverter
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Model;
using AstralBattles.Localizations;
using System;
using System.Globalization;
using Windows.UI.Xaml.Data;


namespace AstralBattles.Converters
{
  public class DifficultyConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      if (!(value is GameDifficulty gameDifficulty))
        return (object) string.Empty;
      switch (gameDifficulty)
      {
        case GameDifficulty.Normal:
          return (object) CommonResources.Normal;
        case GameDifficulty.Easy:
          return (object) CommonResources.Easy;
        case GameDifficulty.Hard:
          return (object) CommonResources.Hard;
        default:
          return (object) string.Empty;
      }
    }

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      string language)
    {
      if (value == null)
        return (object) GameDifficulty.Normal;
      string str = value.ToString();
      if (str == CommonResources.Normal)
        return (object) GameDifficulty.Normal;
      if (str == CommonResources.Easy)
        return (object) GameDifficulty.Easy;
      return str == CommonResources.Hard ? (object) GameDifficulty.Hard : (object) GameDifficulty.Normal;
    }
  }
}
