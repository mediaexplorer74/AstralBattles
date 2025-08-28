// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Ai.ComputerIntellect
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using AstralBattles.Core.Model;
using AstralBattles.Core.Services;
using System.Xml.Serialization;


namespace AstralBattles.Core.Ai
{
  [XmlInclude(typeof (SmartestComputer))]
  [XmlInclude(typeof (StrategicalIntellect))]
  [XmlInclude(typeof (SmartComputer))]
  [XmlInclude(typeof (StupidComputer))]
  public abstract class ComputerIntellect
  {
    public abstract Card GetCard(out Field field);

    [XmlIgnore]
    public IBattlefield Battlefield => GameService.CurrentGame.Battlefield;
  }
}
