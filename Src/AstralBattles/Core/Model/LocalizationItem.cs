// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Model.LocalizationItem
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll


namespace AstralBattles.Core.Model
{
  public class LocalizationItem
  {
    public string DisplayName { get; set; }

    public string Description { get; set; }

    public bool IsEmpty
    {
      get
      {
        return string.IsNullOrWhiteSpace(this.DisplayName) && string.IsNullOrWhiteSpace(this.Description);
      }
    }
  }
}
