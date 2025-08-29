using AstralBattles.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Diagnostics;
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
         Debug.WriteLine(ex.Message);
      }
    }
  }
}
