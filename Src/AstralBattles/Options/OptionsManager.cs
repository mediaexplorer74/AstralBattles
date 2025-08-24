// Decompiled with JetBrains decompiler
// Type: AstralBattles.Options.OptionsManager
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Infrastructure;
using AstralBattles.Core.Model;
using GalaSoft.MvvmLight;

#nullable disable
namespace AstralBattles.Options
{
  public class OptionsManager
  {
    public static OptionsManager Current { get; set; }

    public static void Reload()
    {
      if (ViewModelBase.IsInDesignModeStatic)
        return;
      try
      {
        OptionsManager.Current = Serializer.Read<OptionsManager>("Options__1_452.xml");
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
      if (ViewModelBase.IsInDesignModeStatic || OptionsManager.Current == null)
        return;
      Serializer.Write<OptionsManager>(OptionsManager.Current, "Options__1_452.xml");
    }

    public string Language { get; set; }

    public bool EnableSounds { get; set; }

    public bool EnableVibration { get; set; }

    public GameDifficulty GameDifficulty { get; set; }
  }
}
