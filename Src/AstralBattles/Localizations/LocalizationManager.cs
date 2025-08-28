using AstralBattles.Localizations.Cyclops.MainApplication.Localization;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;


namespace AstralBattles.Localizations
{
  public class LocalizationManager
  {
    private static LocalizationManager instance;

    public static void ChangeLanguage(string name)
    {
      Language language = ((IEnumerable<Language>) LocalizationManager.Instance.Languages).FirstOrDefault<Language>((Func<Language, bool>) (i => i.Name == name));
      string name1 = language == null ? string.Empty : language.Culture;
      if (string.IsNullOrWhiteSpace(name1))
        name1 = CultureInfo.CurrentCulture.Name;
      AstralBattles.Core.AppContext.Locale = name1;
      if (CultureInfo.CurrentUICulture.Name.Equals(name1))
        return;
      // TODO: UWP doesn't support Thread.CurrentThread culture changes for MVP build
      // CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(name1);
      // CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(name1);
      if (!(Application.Current.Resources[(object) "ResourceWrapper"] is ResourceWrapper resource))
        return;
      resource.Refresh();
    }

    public Language[] Languages { get; set; }

    private LocalizationManager()
    {
      this.Languages = new Language[4]
      {
        new Language() { Culture = "en-US", Name = "English" },
        new Language()
        {
          Culture = "fr-FR",
          Name = "French (by Lerne)"
        },
        new Language()
        {
          Culture = "de-DE",
          Name = "German (beta by Patrick Balzer)"
        },
        new Language() { Culture = "ru-RU", Name = "Russian" }
      };
    }

    public static LocalizationManager Instance
    {
      get
      {
        if (LocalizationManager.instance == null)
          LocalizationManager.instance = new LocalizationManager();
        return LocalizationManager.instance;
      }
    }
  }
}
