// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.AppContext
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable disable
namespace AstralBattles.Core
{
  public static class AppContext
  {
    private const string DefaultLocale = "en-us";
    private static readonly string[] Locales = new string[4]
    {
      "en-us",
      "ru-ru",
      "fr-fr",
      "de-de"
    };

    static AppContext()
    {
      string lower = Thread.CurrentThread.CurrentUICulture.Name.ToLower();
      if (((IEnumerable<string>) AppContext.Locales).Contains<string>(lower))
        AppContext.Locale = lower;
      else
        AppContext.Locale = "en-us";
    }

    public static string Locale { get; set; }
  }
}
