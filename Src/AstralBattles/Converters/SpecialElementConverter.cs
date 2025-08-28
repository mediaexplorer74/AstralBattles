// Decompiled with JetBrains decompiler
// Type: AstralBattles.Converters.SpecialElementConverter
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Model;
using AstralBattles.Localizations;
using System;
using System.Collections.Generic;
using System.Globalization;
using Windows.UI.Xaml.Data;


namespace AstralBattles.Converters
{
  public class SpecialElementConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      return !(value is ElementTypeEnum spec) ? (object) "unknown" : (object) this.Convert(spec);
    }

    public string Convert(ElementTypeEnum spec)
    {
      switch (spec)
      {
        case ElementTypeEnum.Death:
          return CommonResources.Death;
        case ElementTypeEnum.Sorcery:
          return CommonResources.Sorcery;
        case ElementTypeEnum.Illusion:
          return CommonResources.Illusion;
        case ElementTypeEnum.Holy:
          return CommonResources.Holy;
        case ElementTypeEnum.Mechanical:
          return CommonResources.Mechanical;
        case ElementTypeEnum.Chaos:
          return CommonResources.Chaos;
        case ElementTypeEnum.Control:
          return CommonResources.Control;
        case ElementTypeEnum.Beast:
          return CommonResources.Beast;
        case ElementTypeEnum.Demonic:
          return CommonResources.Demonic;
        case ElementTypeEnum.Goblins:
          return CommonResources.Goblins;
        default:
          return "unknown";
      }
    }

    public ElementTypeEnum ConvertBack(string str)
    {
      if (str == CommonResources.Death)
        return ElementTypeEnum.Death;
      if (str == CommonResources.Sorcery)
        return ElementTypeEnum.Sorcery;
      if (str == CommonResources.Holy)
        return ElementTypeEnum.Holy;
      if (str == CommonResources.Illusion)
        return ElementTypeEnum.Illusion;
      if (str == CommonResources.Mechanical)
        return ElementTypeEnum.Mechanical;
      if (str == CommonResources.Control)
        return ElementTypeEnum.Control;
      if (str == CommonResources.Chaos)
        return ElementTypeEnum.Chaos;
      if (str == CommonResources.Demonic)
        return ElementTypeEnum.Demonic;
      if (str == CommonResources.Beast)
        return ElementTypeEnum.Beast;
      return str == CommonResources.Goblins ? ElementTypeEnum.Goblins : ((IEnumerable<ElementTypeEnum>) SpecialElementsContainer.Elements).GetRandomElement<ElementTypeEnum>();
    }

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      string language)
    {
      if (value == null)
        value = (object) string.Empty;
      return (object) this.ConvertBack(value.ToString());
    }
  }
}
