// Decompiled with JetBrains decompiler
// Type: AstralBattles.Core.Model.FakeCard
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using System.Xml.Serialization;

#nullable disable
namespace AstralBattles.Core.Model
{
  public class FakeCard : Card
  {
    public FakeCard()
    {
      this.Name = "Locked";
      this.Cost = 999;
      this.IsActive = false;
      this.Localization = new Localization()
      {
        English = new LocalizationItem()
        {
          DisplayName = "Locked",
          Description = "Locked"
        },
        Russian = new LocalizationItem()
        {
          DisplayName = "Недоступно",
          Description = "В данной дуэли это поле недоступно"
        }
      };
    }

    public override Card Clone()
    {
      FakeCard fakeCard = new FakeCard();
      fakeCard.Name = this.Name;
      fakeCard.Cost = this.Cost;
      fakeCard.IsActive = this.IsActive;
      fakeCard.Localization = this.Localization;
      return (Card) fakeCard;
    }

    [XmlIgnore]
    public bool IsFake => true;
  }
}
