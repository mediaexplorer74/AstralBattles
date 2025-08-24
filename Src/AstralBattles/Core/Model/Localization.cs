// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Model.Localization
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using System;
using System.Xml.Serialization;

#nullable disable
namespace AstralBattles.Core.Model
{
  public class Localization
  {
    [XmlElement(ElementName = "ru-RU")]
    public LocalizationItem Russian { get; set; }

    [XmlElement(ElementName = "en-US")]
    public LocalizationItem English { get; set; }

    [XmlElement(ElementName = "fr-FR")]
    public LocalizationItem French { get; set; }

    [XmlElement(ElementName = "de-DE")]
    public LocalizationItem German { get; set; }

    [XmlIgnore]
    public LocalizationItem DefaultLanguage
    {
      get
      {
        if (AstralBattles.Core.AppContext.Locale.Equals("ru-RU", StringComparison.InvariantCultureIgnoreCase) && this.Russian != null && !this.Russian.IsEmpty)
          return this.Russian;
        if (AstralBattles.Core.AppContext.Locale.Equals("fr-FR", StringComparison.InvariantCultureIgnoreCase) && this.French != null && !this.French.IsEmpty)
          return this.French;
        return AstralBattles.Core.AppContext.Locale.Equals("de-DE", StringComparison.InvariantCultureIgnoreCase) && this.German != null && !this.German.IsEmpty ? this.German : this.English;
      }
    }
  }
}
