// Decompiled with JetBrains decompiler
// Type: AstralBattles.Converters.SpecializationTipConverter
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Localizations;
using System;
using System.Globalization;
using Windows.UI.Xaml.Data;


namespace AstralBattles.Converters
{
  public class SpecializationTipConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      string str = value.ToString();
      if (str == CommonResources.Spec_Pyromancer)
        return (object) CommonResources.Spec_PyromancerTip;
      if (str == CommonResources.Spec_Stormbringer)
        return (object) CommonResources.Spec_StormbringerTip;
      if (str == CommonResources.Spec_IceLord)
        return (object) CommonResources.Spec_IceLordTip;
      if (str == CommonResources.Spec_Druid)
        return (object) CommonResources.Spec_DruidTip;
      if (str == CommonResources.Spec_Necromancer)
        return (object) CommonResources.Spec_NecromancerTip;
      return str == CommonResources.Spec_Elementalist ? (object) CommonResources.Spec_ElementalistTip : (object) string.Empty;
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
