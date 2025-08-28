// Decompiled with JetBrains decompiler
// Type: AstralBattles.Vibration
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Options;
using System;


namespace AstralBattles
{
  public static class Vibration
  {
    public static void Vibrate()
    {
      try
      {
        if (!OptionsManager.Current.EnableVibration)
          return;
        // UWP vibration would use Windows.Phone.Devices.Notification.VibrationDevice
        // Stubbed for MVP build
      }
      catch
      {
      }
    }
  }
}
