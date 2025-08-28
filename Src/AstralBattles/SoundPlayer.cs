// Decompiled with JetBrains decompiler
// Type: AstralBattles.SoundPlayer
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.IO;


namespace AstralBattles
{
  public static class SoundPlayer
  {
    public static void PlaySound(string soundFile)
    {
      try
      {
        if (!OptionsManager.Current.EnableSounds)
          return;
        using (Stream stream = TitleContainer.OpenStream("Resources\\Sounds\\" + soundFile + ".wav"))
        {
          SoundEffect soundEffect = SoundEffect.FromStream(stream);
          FrameworkDispatcher.Update();
          soundEffect.Play();
        }
      }
      catch (Exception ex)
      {
      }
    }
  }
}
