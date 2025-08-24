// Decompiled with JetBrains decompiler
// Type: AstralBattles.Localizations.LocalizationManager
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Localizations.Cyclops.MainApplication.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;

#nullable disable
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
      if (Thread.CurrentThread.CurrentUICulture.Name.Equals(name1))
        return;
      Thread.CurrentThread.CurrentUICulture = new CultureInfo(name1);
      Thread.CurrentThread.CurrentCulture = new CultureInfo(name1);
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
