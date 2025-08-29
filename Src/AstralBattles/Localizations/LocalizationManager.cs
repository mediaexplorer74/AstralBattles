﻿using AstralBattles.Localizations.Cyclops.MainApplication.Localization;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.Globalization;


namespace AstralBattles.Localizations
{
  public class LocalizationManager
  {
    private static LocalizationManager instance;

    public static void ChangeLanguage(string name)
    {
      Language language = ((IEnumerable<Language>) LocalizationManager.Instance.Languages)
                .FirstOrDefault<Language>((Func<Language, bool>) (i => i.Name == name));

      string name1 = language == null ? string.Empty : language.Culture;
      if (string.IsNullOrWhiteSpace(name1))
        name1 = CultureInfo.CurrentCulture.Name;
      AstralBattles.Core.AppContext.Locale = name1;
      
      // UWP language changes
      try 
      {
        ApplicationLanguages.PrimaryLanguageOverride = name1;
      }
      catch
      {
        // Fallback if language setting fails
      }
      
      if (!(Application.Current.Resources[(object) "ResourceWrapper"] is ResourceWrapper resource))
        return;
      resource.Refresh();
    }

    public Language[] Languages { get; set; }

    private LocalizationManager()
    {
      this.Languages = new Language[2]//[4]
      {
        new Language() { Culture = "en-US", Name = "English" },
       
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
