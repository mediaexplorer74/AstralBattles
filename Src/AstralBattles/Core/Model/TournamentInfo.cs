// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Model.TournamentInfo
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using AstralBattles.Core.Infrastructure;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace AstralBattles.Core.Model
{
  public class TournamentInfo
  {
    public TournamentInfo()
    {
      this.Stat = new List<PlayerPoint>();
      this.Rounds = new List<List<Tuple<string, string>>>();
    }

    public Player CurrentPlayer { get; set; }

    public List<List<Tuple<string, string>>> Rounds { get; set; }

    public List<PlayerPoint> Stat { get; set; }

    public int CurrentRoundIndex { get; set; }

    [XmlIgnore]
    public List<Tuple<string, string>> CurrentRound
    {
      get
      {
        return this.CurrentRoundIndex >= 0 && this.CurrentRoundIndex < this.Rounds.Count ? this.Rounds[this.CurrentRoundIndex] : new List<Tuple<string, string>>();
      }
      set
      {
        if (this.CurrentRoundIndex < 0 || this.CurrentRoundIndex >= this.Rounds.Count)
          return;
        this.Rounds[this.CurrentRoundIndex] = value;
      }
    }

    [XmlIgnore]
    public List<Tuple<string, string>> PreviousRound
    {
      get
      {
        return this.CurrentRoundIndex >= 1 && this.CurrentRoundIndex <= this.Rounds.Count ? this.Rounds[this.CurrentRoundIndex - 1] : new List<Tuple<string, string>>();
      }
    }

    [XmlIgnore]
    public bool IsFinished => this.CurrentRoundIndex >= this.Rounds.Count - 1;
  }
}
