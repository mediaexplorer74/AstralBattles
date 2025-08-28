// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Services.CampaignService
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using AstralBattles.Core.Model;


namespace AstralBattles.Core.Services
{
  public class CampaignService
  {
    private static readonly CampaignService instance = new CampaignService();

    private CampaignService()
    {
    }

    public static CampaignService Instance => CampaignService.instance;

    public void StartCampaign(Player player)
    {
      this.CampaignInfo = new CampaignInfo()
      {
        CurrentPlayer = player
      };
    }

    public CampaignInfo CampaignInfo { get; set; }
  }
}
