
using System;
using System.Xml.Serialization;


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
        if (AstralBattles.Core.AppContext.Locale.Equals("ru-RU", StringComparison.OrdinalIgnoreCase) && this.Russian != null && !this.Russian.IsEmpty)
          return this.Russian;
        if (AstralBattles.Core.AppContext.Locale.Equals("fr-FR", StringComparison.OrdinalIgnoreCase) && this.French != null && !this.French.IsEmpty)
          return this.French;
        return AstralBattles.Core.AppContext.Locale.Equals("de-DE", StringComparison.OrdinalIgnoreCase) && this.German != null && !this.German.IsEmpty ? this.German : this.English;
      }
    }
  }
}
