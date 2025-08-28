using AstralBattles.Core.Model;
using AstralBattles.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;


using AstralBattles.Core.Infrastructure;

namespace AstralBattles.ViewModels
{
  public class CampaignViewModel : ViewModelBaseEx
  {
    private ObservableCollection<CampaignOpponent> opponents;
    private bool isWaitingUserChoise;
    private CampaignOpponent selectedCampaignOpponent;

    public CampaignViewModel()
    {
      CloseFoeDialog = new RelayCommand(CloseFoeDialogAction);
      FightWithFoe = new RelayCommand(FightWithFoeAction);
      ObservableCollection<CampaignOpponent> observableCollection1 = new ObservableCollection<CampaignOpponent>();
      ObservableCollection<CampaignOpponent> observableCollection2 = observableCollection1;
      CampaignOpponent campaignOpponent1 = new CampaignOpponent();
      campaignOpponent1.ImageXindex = 7;
      campaignOpponent1.ImageYindex = 1;
      campaignOpponent1.LocationOnMapX = 333;
      campaignOpponent1.LocationOnMapY = 494;
      campaignOpponent1.Name = "Ilona";
      campaignOpponent1.SpecialElement = ElementTypeEnum.Illusion;
      campaignOpponent1.Photo = "face18";
      campaignOpponent1.DisplayName = "Ilona, illusionist";
      campaignOpponent1.SurviveXTurns = 8;
      campaignOpponent1.CreaturesAtStart = new string[5]
      {
        "GoblinBerserker",
        null,
        null,
        null,
        "GoblinBerserker"
      };
      CampaignOpponent campaignOpponent2 = campaignOpponent1;
      observableCollection2.Add(campaignOpponent2);
      ObservableCollection<CampaignOpponent> observableCollection3 = observableCollection1;
      CampaignOpponent campaignOpponent3 = new CampaignOpponent();
      campaignOpponent3.ImageXindex = 8;
      campaignOpponent3.ImageYindex = 4;
      campaignOpponent3.LocationOnMapX = 513;
      campaignOpponent3.LocationOnMapY = 623;
      campaignOpponent3.Name = "Chappy";
      campaignOpponent3.Photo = "face42";
      campaignOpponent3.SpecialElement = ElementTypeEnum.Holy;
      campaignOpponent3.DisplayName = "Chappy, cleric";
      campaignOpponent3.OpponentsWillStartWith = "MasterHealer";
      CampaignOpponent campaignOpponent4 = campaignOpponent3;
      observableCollection3.Add(campaignOpponent4);
      ObservableCollection<CampaignOpponent> observableCollection4 = observableCollection1;
      CampaignOpponent campaignOpponent5 = new CampaignOpponent();
      campaignOpponent5.ImageXindex = 1;
      campaignOpponent5.ImageYindex = 4;
      campaignOpponent5.LocationOnMapX = 557;
      campaignOpponent5.LocationOnMapY = 460;
      campaignOpponent5.Name = "Deomir";
      campaignOpponent5.Photo = "face49";
      campaignOpponent5.DisplayName = "Deomir, cleric";
      campaignOpponent5.SpecialElement = ElementTypeEnum.Holy;
      campaignOpponent5.OpponentsWillStartWith = "PriestOfFire";
      CampaignOpponent campaignOpponent6 = campaignOpponent5;
      observableCollection4.Add(campaignOpponent6);
      ObservableCollection<CampaignOpponent> observableCollection5 = observableCollection1;
      CampaignOpponent campaignOpponent7 = new CampaignOpponent();
      campaignOpponent7.ImageXindex = 9;
      campaignOpponent7.ImageYindex = 2;
      campaignOpponent7.LocationOnMapX = 416;
      campaignOpponent7.LocationOnMapY = 362;
      campaignOpponent7.Name = "Niraon";
      campaignOpponent7.Photo = "face30";
      campaignOpponent7.SpecialElement = ElementTypeEnum.Mechanical;
      campaignOpponent7.DisplayName = "Niraon, mechanician";
      campaignOpponent7.WinIfSummon = "MindMaster";
      CampaignOpponent campaignOpponent8 = campaignOpponent7;
      observableCollection5.Add(campaignOpponent8);
      ObservableCollection<CampaignOpponent> observableCollection6 = observableCollection1;
      CampaignOpponent campaignOpponent9 = new CampaignOpponent();
      campaignOpponent9.ImageXindex = 0;
      campaignOpponent9.ImageYindex = 2;
      campaignOpponent9.LocationOnMapX = 122;
      campaignOpponent9.LocationOnMapY = 470;
      campaignOpponent9.Name = "Vseslav";
      campaignOpponent9.Photo = "face21";
      campaignOpponent9.SpecialElement = ElementTypeEnum.Control;
      campaignOpponent9.DisplayName = "Vseslav, dominator";
      CampaignOpponent campaignOpponent10 = campaignOpponent9;
      observableCollection6.Add(campaignOpponent10);
      ObservableCollection<CampaignOpponent> observableCollection7 = observableCollection1;
      CampaignOpponent campaignOpponent11 = new CampaignOpponent();
      campaignOpponent11.ImageXindex = 9;
      campaignOpponent11.ImageYindex = 1;
      campaignOpponent11.LocationOnMapX = 681;
      campaignOpponent11.LocationOnMapY = 717;
      campaignOpponent11.Name = "Fademir";
      campaignOpponent11.RequiredWinOf = "Deomir";
      campaignOpponent11.Photo = "face20";
      campaignOpponent11.SpecialElement = ElementTypeEnum.Control;
      campaignOpponent11.DisplayName = "Fademir, dominator";
      campaignOpponent11.DefeatDuringTurns = 10;
      campaignOpponent11.RewardCards = "MindMaster";
      CampaignOpponent campaignOpponent12 = campaignOpponent11;
      observableCollection7.Add(campaignOpponent12);
      ObservableCollection<CampaignOpponent> observableCollection8 = observableCollection1;
      CampaignOpponent campaignOpponent13 = new CampaignOpponent();
      campaignOpponent13.ImageXindex = 4;
      campaignOpponent13.ImageYindex = 4;
      campaignOpponent13.LocationOnMapX = 746;
      campaignOpponent13.LocationOnMapY = 580;
      campaignOpponent13.Name = "Imelda";
      campaignOpponent13.Photo = "face45";
      campaignOpponent13.SpecialElement = ElementTypeEnum.Mechanical;
      campaignOpponent13.DisplayName = "Imdelda the Beautiful, mechanician";
      CampaignOpponent campaignOpponent14 = campaignOpponent13;
      observableCollection8.Add(campaignOpponent14);
      ObservableCollection<CampaignOpponent> observableCollection9 = observableCollection1;
      CampaignOpponent campaignOpponent15 = new CampaignOpponent();
      campaignOpponent15.ImageXindex = 4;
      campaignOpponent15.ImageYindex = 3;
      campaignOpponent15.LocationOnMapX = 980;
      campaignOpponent15.LocationOnMapY = 600;
      campaignOpponent15.Name = "Zippy";
      campaignOpponent15.Photo = "face35";
      campaignOpponent15.SpecialElement = ElementTypeEnum.Illusion;
      campaignOpponent15.DisplayName = "Zippy, illusionist";
      CampaignOpponent campaignOpponent16 = campaignOpponent15;
      observableCollection9.Add(campaignOpponent16);
      ObservableCollection<CampaignOpponent> observableCollection10 = observableCollection1;
      CampaignOpponent campaignOpponent17 = new CampaignOpponent();
      campaignOpponent17.ImageXindex = 8;
      campaignOpponent17.ImageYindex = 3;
      campaignOpponent17.LocationOnMapX = 250;
      campaignOpponent17.LocationOnMapY = 150;
      campaignOpponent17.Name = "Turion";
      campaignOpponent17.Photo = "face35";
      campaignOpponent17.SpecialElement = ElementTypeEnum.Mechanical;
      campaignOpponent17.DisplayName = "Turion, mechanician";
      CampaignOpponent campaignOpponent18 = campaignOpponent17;
      observableCollection10.Add(campaignOpponent18);
      ObservableCollection<CampaignOpponent> observableCollection11 = observableCollection1;
      CampaignOpponent campaignOpponent19 = new CampaignOpponent();
      campaignOpponent19.ImageXindex = 5;
      campaignOpponent19.ImageYindex = 5;
      campaignOpponent19.LocationOnMapX = 636;
      campaignOpponent19.LocationOnMapY = 155;
      campaignOpponent19.Name = "Helga";
      campaignOpponent19.Health = 150;
      campaignOpponent19.Photo = "face56";
      campaignOpponent19.SpecialElement = ElementTypeEnum.Mechanical;
      campaignOpponent19.DisplayName = "Helga the Whale, mechanician";
      CampaignOpponent campaignOpponent20 = campaignOpponent19;
      observableCollection11.Add(campaignOpponent20);
      ObservableCollection<CampaignOpponent> observableCollection12 = observableCollection1;
      CampaignOpponent campaignOpponent21 = new CampaignOpponent();
      campaignOpponent21.ImageXindex = 8;
      campaignOpponent21.ImageYindex = 3;
      campaignOpponent21.LocationOnMapX = 669;
      campaignOpponent21.LocationOnMapY = 329;
      campaignOpponent21.Name = "Halael";
      campaignOpponent21.Photo = "face5";
      campaignOpponent21.SpecialElement = ElementTypeEnum.Control;
      campaignOpponent21.DisplayName = "Halael, dominator";
      CampaignOpponent campaignOpponent22 = campaignOpponent21;
      observableCollection12.Add(campaignOpponent22);
      ObservableCollection<CampaignOpponent> observableCollection13 = observableCollection1;
      CampaignOpponent campaignOpponent23 = new CampaignOpponent();
      campaignOpponent23.ImageXindex = 5;
      campaignOpponent23.ImageYindex = 1;
      campaignOpponent23.LocationOnMapX = 800;
      campaignOpponent23.LocationOnMapY = 250;
      campaignOpponent23.Name = "YagHorseprince";
      campaignOpponent23.Photo = "face16";
      campaignOpponent23.SpecialElement = ElementTypeEnum.Chaos;
      campaignOpponent23.DisplayName = "Yag Horseprince, chaosmaster";
      CampaignOpponent campaignOpponent24 = campaignOpponent23;
      observableCollection13.Add(campaignOpponent24);
      ObservableCollection<CampaignOpponent> observableCollection14 = observableCollection1;
      CampaignOpponent campaignOpponent25 = new CampaignOpponent();
      campaignOpponent25.ImageXindex = 9;
      campaignOpponent25.ImageYindex = 4;
      campaignOpponent25.LocationOnMapX = 455;
      campaignOpponent25.LocationOnMapY = 142;
      campaignOpponent25.Name = "Ratlin";
      campaignOpponent25.Photo = "face50";
      campaignOpponent25.SpecialElement = ElementTypeEnum.Control;
      campaignOpponent25.DisplayName = "Ratlin, dominator";
      CampaignOpponent campaignOpponent26 = campaignOpponent25;
      observableCollection14.Add(campaignOpponent26);
      ObservableCollection<CampaignOpponent> observableCollection15 = observableCollection1;
      CampaignOpponent campaignOpponent27 = new CampaignOpponent();
      campaignOpponent27.ImageXindex = 8;
      campaignOpponent27.ImageYindex = 1;
      campaignOpponent27.LocationOnMapX = 920;
      campaignOpponent27.LocationOnMapY = 471;
      campaignOpponent27.Name = "Gustav";
      campaignOpponent27.Photo = "face19";
      campaignOpponent27.SpecialElement = ElementTypeEnum.Holy;
      campaignOpponent27.DisplayName = "Gustav, cleric";
      CampaignOpponent campaignOpponent28 = campaignOpponent27;
      observableCollection15.Add(campaignOpponent28);
      ObservableCollection<CampaignOpponent> observableCollection16 = observableCollection1;
      CampaignOpponent campaignOpponent29 = new CampaignOpponent();
      campaignOpponent29.ImageXindex = 7;
      campaignOpponent29.ImageYindex = 0;
      campaignOpponent29.LocationOnMapX = 972;
      campaignOpponent29.LocationOnMapY = 310;
      campaignOpponent29.Name = "Orcia";
      campaignOpponent29.Photo = "face8";
      campaignOpponent29.SpecialElement = ElementTypeEnum.Mechanical;
      campaignOpponent29.DisplayName = "Orcia, mechanician";
      CampaignOpponent campaignOpponent30 = campaignOpponent29;
      observableCollection16.Add(campaignOpponent30);
      ObservableCollection<CampaignOpponent> observableCollection17 = observableCollection1;
      CampaignOpponent campaignOpponent31 = new CampaignOpponent();
      campaignOpponent31.ImageXindex = 4;
      campaignOpponent31.ImageYindex = 1;
      campaignOpponent31.LocationOnMapX = 934;
      campaignOpponent31.LocationOnMapY = 163;
      campaignOpponent31.Name = "Polyrisus";
      campaignOpponent31.Photo = "face15";
      campaignOpponent31.SpecialElement = ElementTypeEnum.Control;
      campaignOpponent31.DisplayName = "Polyrisus, dominator";
      CampaignOpponent campaignOpponent32 = campaignOpponent31;
      observableCollection17.Add(campaignOpponent32);
      ObservableCollection<CampaignOpponent> observableCollection18 = observableCollection1;
      CampaignOpponent campaignOpponent33 = new CampaignOpponent();
      campaignOpponent33.ImageXindex = 4;
      campaignOpponent33.ImageYindex = 5;
      campaignOpponent33.LocationOnMapX = 1141;
      campaignOpponent33.LocationOnMapY = 200;
      campaignOpponent33.Name = "LordOfAir";
      campaignOpponent33.Photo = "face55";
      campaignOpponent33.SpecialElement = ElementTypeEnum.Illusion;
      campaignOpponent33.DisplayName = "Lord of Air, illusionist";
      CampaignOpponent campaignOpponent34 = campaignOpponent33;
      observableCollection18.Add(campaignOpponent34);
      ObservableCollection<CampaignOpponent> observableCollection19 = observableCollection1;
      CampaignOpponent campaignOpponent35 = new CampaignOpponent();
      campaignOpponent35.ImageXindex = 3;
      campaignOpponent35.ImageYindex = 5;
      campaignOpponent35.LocationOnMapX = 1260;
      campaignOpponent35.LocationOnMapY = 373;
      campaignOpponent35.Name = "LordOfFire";
      campaignOpponent35.Photo = "face54";
      campaignOpponent35.SpecialElement = ElementTypeEnum.Chaos;
      campaignOpponent35.DisplayName = "Lord of Fire, chaosmaster";
      CampaignOpponent campaignOpponent36 = campaignOpponent35;
      observableCollection19.Add(campaignOpponent36);
      ObservableCollection<CampaignOpponent> observableCollection20 = observableCollection1;
      CampaignOpponent campaignOpponent37 = new CampaignOpponent();
      campaignOpponent37.ImageXindex = 0;
      campaignOpponent37.ImageYindex = 3;
      campaignOpponent37.LocationOnMapX = 1294;
      campaignOpponent37.LocationOnMapY = 211;
      campaignOpponent37.Name = "Flora";
      campaignOpponent37.Photo = "face31";
      campaignOpponent37.SpecialElement = ElementTypeEnum.Holy;
      campaignOpponent37.DisplayName = "Flora, cleric";
      CampaignOpponent campaignOpponent38 = campaignOpponent37;
      observableCollection20.Add(campaignOpponent38);
      ObservableCollection<CampaignOpponent> items = observableCollection1;
      items.ForEach<CampaignOpponent>((Action<CampaignOpponent>) (i => i.PropertyChanged += new PropertyChangedEventHandler(OpponentPropertyChanged)));
      Opponents = items;
    }

    private void FightWithFoeAction() => CloseFoeDialogAction();

    private void CloseFoeDialogAction()
    {
      if (SelectedCampaignOpponent == null)
        return;
      SelectedCampaignOpponent.IsSelected = false;
      SelectedCampaignOpponent = (CampaignOpponent) null;
    }

    private void OpponentPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      CampaignOpponent opponent = sender as CampaignOpponent;
      if (!(e.PropertyName == "IsSelected") || opponent == null || !opponent.IsSelected)
        return;
      Opponents.Where<CampaignOpponent>((Func<CampaignOpponent, bool>) (i => i != opponent)).ForEach<CampaignOpponent>((Action<CampaignOpponent>) (i => i.IsSelected = false));
      SelectedCampaignOpponent = opponent;
    }

    public ObservableCollection<CampaignOpponent> Opponents
    {
      get => opponents;
      set
      {
        opponents = value;
        RaisePropertyChanged(nameof (Opponents));
      }
    }

    public bool IsWaitingUserChoise
    {
      get => isWaitingUserChoise;
      set
      {
        isWaitingUserChoise = value;
        RaisePropertyChanged(nameof (IsWaitingUserChoise));
      }
    }

    public RelayCommand CloseFoeDialog { get; private set; }

    public RelayCommand FightWithFoe { get; private set; }

    public CampaignOpponent SelectedCampaignOpponent
    {
      get => selectedCampaignOpponent;
      set
      {
        selectedCampaignOpponent = value;
        RaisePropertyChanged(nameof (SelectedCampaignOpponent));
        IsWaitingUserChoise = value != null;
      }
    }

    // Modified for UWP compatibility - removed CancelEventArgs
    public void OnBackKeyPress()
    {
      if (SelectedCampaignOpponent != null)
      {
        SelectedCampaignOpponent.IsSelected = false;
        SelectedCampaignOpponent = (CampaignOpponent) null;
      }
      PageNavigationService.CampaignOptions(true);
    }
  }
}
