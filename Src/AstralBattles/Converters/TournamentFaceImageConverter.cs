using AstralBattles.Core.Model;
using AstralBattles.Core.Services;
using System;
using System.Globalization;
using Windows.UI.Xaml.Data;


namespace AstralBattles.Converters
{
  public class TournamentFaceImageConverter : IValueConverter
  {
    private static readonly FaceImageConverter imageConverter = new FaceImageConverter();

    public object Convert(object value, Type targetType, object parameter, string language)
    {
      if (value == null)
        return (object) null;
      string str = value.ToString();
      Player currentPlayer = TournamentService.Instance.Tournament.CurrentPlayer;
      return str == currentPlayer.Name ? TournamentFaceImageConverter.imageConverter.Convert((object) currentPlayer.Photo, targetType, parameter, language) : TournamentFaceImageConverter.imageConverter.Convert((object) str, targetType, parameter, language);
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
