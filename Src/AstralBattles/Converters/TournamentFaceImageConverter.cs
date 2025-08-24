// Decompiled with JetBrains decompiler
// Type: AstralBattles.Converters.TournamentFaceImageConverter
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Model;
using AstralBattles.Core.Services;
using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace AstralBattles.Converters
{
  public class TournamentFaceImageConverter : IValueConverter
  {
    private static readonly FaceImageConverter imageConverter = new FaceImageConverter();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value == null)
        return (object) null;
      string str = value.ToString();
      Player currentPlayer = TournamentService.Instance.Tournament.CurrentPlayer;
      return str == currentPlayer.Name ? TournamentFaceImageConverter.imageConverter.Convert((object) currentPlayer.Photo, targetType, parameter, culture) : TournamentFaceImageConverter.imageConverter.Convert((object) str, targetType, parameter, culture);
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
