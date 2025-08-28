
using System;
using System.Collections.Generic;
using System.Globalization;
using Windows.UI;
using Windows.UI.Xaml.Media;


namespace AstralBattles.Helpers
{
  public static class ColorFromString
  {
    private static Dictionary<string, Color> namedColors = new Dictionary<string, Color>();

    public static Color ToColor(this string value)
    {
      if (value == null)
        return Colors.Red;
      string lower = value.ToLower();
      if (ColorFromString.namedColors.ContainsKey(lower))
        return ColorFromString.namedColors[lower];
      if (value[0] == '#')
        value = value.Remove(0, 1);
      int length = value.Length;
      switch (length)
      {
        case 6:
        case 8:
          if (ColorFromString.IsHexColor(value))
          {
            if (length == 8)
              return Color.FromArgb(byte.Parse(value.Substring(0, 2), NumberStyles.HexNumber), byte.Parse(value.Substring(2, 2), NumberStyles.HexNumber), byte.Parse(value.Substring(4, 2), NumberStyles.HexNumber), byte.Parse(value.Substring(6, 2), NumberStyles.HexNumber));
            if (length == 6)
              return Color.FromArgb(byte.MaxValue, byte.Parse(value.Substring(0, 2), NumberStyles.HexNumber), byte.Parse(value.Substring(2, 2), NumberStyles.HexNumber), byte.Parse(value.Substring(4, 2), NumberStyles.HexNumber));
            break;
          }
          break;
      }
      string[] strArray = value.Split(new char[2]
      {
        ',',
        ' '
      }, StringSplitOptions.RemoveEmptyEntries);
      if (strArray != null)
      {
        if (strArray.Length == 4)
          return Color.FromArgb(byte.Parse(strArray[0]), byte.Parse(strArray[1]), byte.Parse(strArray[2]), byte.Parse(strArray[3]));
        if (strArray.Length == 3)
          return Color.FromArgb(byte.MaxValue, byte.Parse(strArray[0]), byte.Parse(strArray[1]), byte.Parse(strArray[2]));
      }
      return Colors.Red;
    }

    private static bool IsHexColor(string value)
    {
      if (value == null)
        return false;
      foreach (char character in value.ToCharArray())
      {
        if (!((character >= '0' && character <= '9') || (character >= 'a' && character <= 'f') || (character >= 'A' && character <= 'F')))
          return false;
      }
      return true;
    }
  }
}
