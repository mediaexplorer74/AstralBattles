// Decompiled with JetBrains decompiler
// Type: AstralBattles.Converters.ElementTextConverter
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
  public class ElementTextConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      switch ((ElementTypeEnum) value)
      {
        case ElementTypeEnum.Fire:
          return (object) CommonResources.Fire;
        case ElementTypeEnum.Water:
          return (object) CommonResources.Water;
        case ElementTypeEnum.Air:
          return (object) CommonResources.Air;
        case ElementTypeEnum.Earth:
          return (object) CommonResources.Earth;
        case ElementTypeEnum.Death:
          return (object) CommonResources.Death;
        case ElementTypeEnum.Sorcery:
          return (object) CommonResources.Sorcery;
        case ElementTypeEnum.Illusion:
          return (object) CommonResources.Illusion;
        case ElementTypeEnum.Holy:
          return (object) CommonResources.Holy;
        case ElementTypeEnum.Mechanical:
          return (object) CommonResources.Mechanical;
        case ElementTypeEnum.Chaos:
          return (object) CommonResources.Chaos;
        case ElementTypeEnum.Control:
          return (object) CommonResources.Control;
        case ElementTypeEnum.Beast:
          return (object) CommonResources.Beast;
        case ElementTypeEnum.Demonic:
          return (object) CommonResources.Demonic;
        case ElementTypeEnum.Goblins:
          return (object) CommonResources.Goblins;
        default:
          throw new ArgumentOutOfRangeException();
      }
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
