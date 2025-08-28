// Decompiled with JetBrains decompiler
// Type: AstralBattles.Converters.SpecializationConverter
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
  public class SpecializationConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      return (object) this.Convert((Specialization) value);
    }

    public string Convert(Specialization spec)
    {
      switch (spec)
      {
        case Specialization.Pyromancer:
          return CommonResources.Spec_Pyromancer;
        case Specialization.Stormbringer:
          return CommonResources.Spec_Stormbringer;
        case Specialization.IceLord:
          return CommonResources.Spec_IceLord;
        case Specialization.Druid:
          return CommonResources.Spec_Druid;
        case Specialization.Necromancer:
          return CommonResources.Spec_Necromancer;
        case Specialization.Elementalist:
          return CommonResources.Spec_Elementalist;
        default:
          return "unknown";
      }
    }

    public Specialization ConvertBack(string str)
    {
      if (str == CommonResources.Spec_Pyromancer)
        return Specialization.Pyromancer;
      if (str == CommonResources.Spec_Stormbringer)
        return Specialization.Stormbringer;
      if (str == CommonResources.Spec_IceLord)
        return Specialization.IceLord;
      if (str == CommonResources.Spec_Druid)
        return Specialization.Druid;
      if (str == CommonResources.Spec_Necromancer)
        return Specialization.Necromancer;
      return str == CommonResources.Spec_Elementalist ? Specialization.Elementalist : Specialization.Elementalist;
    }

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      string language)
    {
      return (object) this.ConvertBack(value.ToString());
    }
  }
}
