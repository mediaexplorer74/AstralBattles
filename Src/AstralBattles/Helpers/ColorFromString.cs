// Decompiled with JetBrains decompiler
// Type: AstralBattles.Helpers.ColorFromString
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Media;

#nullable disable
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
        if (!Uri.IsHexDigit(character))
          return false;
      }
      return true;
    }
  }
}
