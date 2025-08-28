
using System;
using System.Globalization;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;


namespace AstralBattles.Converters
{
  public class TrueToGreenColorConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      return (bool) value ? (object) new SolidColorBrush(Colors.Green) : (object) new SolidColorBrush(Colors.White);
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
