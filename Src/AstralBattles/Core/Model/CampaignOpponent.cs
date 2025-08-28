// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Model.CampaignOpponent
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using System.Xml.Serialization;


namespace AstralBattles.Core.Model
{
  public class CampaignOpponent : Player
  {
    private bool isSelected;

    public int ImageXindex { get; set; }

    public int ImageYindex { get; set; }

    public int LocationOnMapX { get; set; }

    public int LocationOnMapY { get; set; }

    public string ConditionalFoe { get; set; }

    public int SurviveXTurns { get; set; }

    public string RewardCards { get; set; }

    [XmlIgnore]
    public bool IsSelected
    {
      get => this.isSelected;
      set
      {
        this.isSelected = value;
        this.RaisePropertyChanged(nameof (IsSelected));
      }
    }

    public string VictoryCondition { get; set; }

    public string LoseCondition { get; set; }

    public string OpponentsWillStartWith { get; set; }

    public string WinIfSummon { get; set; }

    public string RequiredWinOf { get; set; }

    public int DefeatDuringTurns { get; set; }

    public CampaignOpponent()
    {
      this.VictoryCondition = "Defeat opponent.%br%";
      this.LoseCondition = "Lose your life.%br%";
      this.Health = 60;
    }
  }
}
