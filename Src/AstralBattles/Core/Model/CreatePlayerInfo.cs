// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Model.CreatePlayerInfo
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll


namespace AstralBattles.Core.Model
{
  public class CreatePlayerInfo
  {
    public string Name { get; set; }

    public string Face { get; set; }

    public ElementTypeEnum Element { get; set; }

    public Deck Deck { get; set; }

    public CreatePlayerInfo Clone()
    {
      return new CreatePlayerInfo()
      {
        Name = this.Name,
        Element = this.Element,
        Face = this.Face,
        Deck = this.Deck
      };
    }
  }
}
