using AstralBattles.Core.Infrastructure;
using AstralBattles.Core.Model;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel;


namespace AstralBattles.Options
{
  // Using Windows.ApplicationModel.DesignMode for design-time detection

  public class OptionsManager
  {
    public static OptionsManager Current { get; set; }

    public static async Task ReloadAsync()
    {
        if (DesignMode.DesignModeEnabled)
            return;
        try
        {
            OptionsManager.Current = await Serializer.Read<OptionsManager>("Options__1_452.xml");
            if (OptionsManager.Current != null)
                return;
            OptionsManager.Current = new OptionsManager()
            {
                EnableSounds = true,
                EnableVibration = true,
                GameDifficulty = GameDifficulty.Normal
            };
        }
        catch
        {
            OptionsManager.Current = new OptionsManager()
            {
                EnableSounds = true,
                EnableVibration = true,
                GameDifficulty = GameDifficulty.Normal
            };
        }
    }

    public static void Save()
    {
      if (DesignMode.DesignModeEnabled || OptionsManager.Current == null)
        return;
      Serializer.Write<OptionsManager>(OptionsManager.Current, "Options__1_452.xml");
    }

    public string Language { get; set; }

    public bool EnableSounds { get; set; }

    public bool EnableVibration { get; set; }

    public GameDifficulty GameDifficulty { get; set; }
  }
}
