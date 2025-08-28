
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


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
      string lower = CultureInfo.CurrentUICulture.Name.ToLower();
      if (((IEnumerable<string>) AppContext.Locales).Contains<string>(lower))
        AppContext.Locale = lower;
      else
        AppContext.Locale = "en-us";
    }

    public static string Locale { get; set; }
  }
}
