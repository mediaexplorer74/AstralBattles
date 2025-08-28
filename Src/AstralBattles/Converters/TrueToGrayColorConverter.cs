
using System;
using System.Globalization;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;


namespace AstralBattles.Converters
{
  public class TrueToGrayColorConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      return (bool) value ? (object) new SolidColorBrush(Color.FromArgb(byte.MaxValue, (byte) 58, (byte) 58, (byte) 58)) : (object) new SolidColorBrush(Colors.Black);
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
