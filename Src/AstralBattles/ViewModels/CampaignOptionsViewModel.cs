// Decompiled with JetBrains decompiler
// Type: AstralBattles.ViewModels.CampaignOptionsViewModel
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Converters;
using AstralBattles.Core.Infrastructure;
using AstralBattles.Core.Model;
using AstralBattles.Core.Services;
using AstralBattles.Localizations;
using AstralBattles.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

#nullable disable
namespace AstralBattles.ViewModels
{
  public class CampaignOptionsViewModel : ViewModelBaseEx
  {
    private string player;
    private Specialization playerSpecialization;
    private bool selectingPlayerPhoto;
    private string[] specialElements;
    private ElementTypeEnum playerSpecialElement;
    private GameDifficulty[] difficultyLevels;
    private string[] specializations;
    private string[] photos;
    private string playerPhoto;
    private bool canContinue;

    public CampaignOptionsViewModel()
    {
      SpecializationConverter specializationConverter = new SpecializationConverter();
      SpecialElementConverter elementConverter = new SpecialElementConverter();
      this.Specializations = new string[6]
      {
        specializationConverter.Convert(Specialization.Pyromancer),
        specializationConverter.Convert(Specialization.IceLord),
        specializationConverter.Convert(Specialization.Stormbringer),
        specializationConverter.Convert(Specialization.Druid),
        specializationConverter.Convert(Specialization.Necromancer),
        specializationConverter.Convert(Specialization.Elementalist)
      };
      this.SpecialElements = ((IEnumerable<ElementTypeEnum>) SpecialElementsContainer.Elements).Select<ElementTypeEnum, string>(new Func<ElementTypeEnum, string>(elementConverter.Convert)).ToArray<string>();
      this.Continue = new RelayCommand(new Action(this.ContinueAction), (Func<bool>) (() => this.CanContinue));
      this.NewCampaign = (ICommand) new RelayCommand((Action) (() => PageNavigationService.OpenCampaignMap()));
      this.NewGame = new RelayCommand(new Action(this.NewGameAction));
      this.ChangePlayer = (ICommand) new RelayCommand(new Action(this.ChangePlayerAction));
      this.Photos = Enumerable.Range(1, 79).Select<int, string>((Func<int, string>) (i => "face" + (object) i)).ToArray<string>();
      this.PlayerPhotoSelect = (ICommand) new RelayCommand(new Action(this.PlayerPhotoSelectAction));
      if (this.IsInDesignMode)
      {
        this.PlayerPhoto = ((IEnumerable<string>) this.Photos).GetRandomElement<string>();
        this.Player = "Nagga";
        this.PlayerSpecialElement = ElementTypeEnum.Sorcery;
      }
      else
        this.RefreshDifficulty();
    }

    private void ChangePlayerAction()
    {
      CreatePlayerViewModel.CreatePlayerInfo = new CreatePlayerInfo()
      {
        Element = this.PlayerSpecialElement,
        Name = this.Player,
        Face = this.PlayerPhoto
      };
      PageNavigationService.OpenCreateNewPlayer();
    }

    private void RefreshDifficulty()
    {
    }

    private void PlayerPhotoSelectAction()
    {
      this.selectingPlayerPhoto = true;
      PageNavigationService.FaceSelectionView();
    }

    private void RefreshData()
    {
      IsolatedStorageSettings applicationSettings = IsolatedStorageSettings.ApplicationSettings;
      this.Player = applicationSettings.GetValueOrDefault<object, string>("CampaignPlayer", (object) "Player12345").ToString();
      this.CanContinue = Serializer.Exists("CampaignBattlefieldViewModel__1_452.xml");
      this.PlayerSpecialization = (Specialization) applicationSettings.GetValueOrDefault<object, string>("CampaignPlayerSpecialization", (object) Specialization.Elementalist);
      this.PlayerSpecialElement = (ElementTypeEnum) applicationSettings.GetValueOrDefault<object, string>("CampaignPlayerSpecialElement", (object) ((IEnumerable<ElementTypeEnum>) SpecialElementsContainer.Elements).GetRandomElement<ElementTypeEnum>());
      this.PlayerPhoto = applicationSettings.GetValueOrDefault<object, string>("CampaignPlayerPhoto", (object) ((IEnumerable<string>) this.Photos).GetRandomElement<string>()).ToString();
    }

    private void Save()
    {
      IsolatedStorageSettings applicationSettings = IsolatedStorageSettings.ApplicationSettings;
      applicationSettings["CampaignPlayer"] = (object) this.Player;
      applicationSettings["CampaignPlayerSpecialization"] = (object) this.PlayerSpecialization;
      applicationSettings["CampaignPlayerPhoto"] = (object) this.PlayerPhoto;
      applicationSettings["CampaignPlayerSpecialElement"] = (object) this.PlayerSpecialElement;
      applicationSettings.Save();
    }

    private void NewGameAction()
    {
      if (this.CanContinue && MessageBox.Show(CommonResources.GameWillBeOverwritten, CommonResources.Confirmation, MessageBoxButton.OKCancel) != MessageBoxResult.OK)
        return;
      if (string.IsNullOrWhiteSpace(this.Player))
      {
        int num = (int) MessageBox.Show(CommonResources.NamesShouldBeNotEmptyMessage);
      }
      else
      {
        this.IsBusy = true;
        this.Save();
        CampaignService.Instance.StartCampaign(new AstralBattles.Core.Model.Player()
        {
          Photo = this.PlayerPhoto,
          DisplayName = this.Player,
          SpecialElement = this.PlayerSpecialElement,
          Name = this.Player + string.Format(" ({0})", (object) CommonResources.You),
          Specialization = Specialization.Elementalist
        });
        PageNavigationService.OpenBattlefield(false);
      }
    }

    public string[] SpecialElements
    {
      get => this.specialElements;
      set
      {
        this.specialElements = value;
        this.RaisePropertyChanged(nameof (SpecialElements));
      }
    }

    public ElementTypeEnum PlayerSpecialElement
    {
      get => this.playerSpecialElement;
      set
      {
        this.playerSpecialElement = value;
        this.RaisePropertyChanged(nameof (PlayerSpecialElement));
      }
    }

    public GameDifficulty[] DifficultyLevels
    {
      get => this.difficultyLevels;
      set
      {
        this.difficultyLevels = value;
        this.RaisePropertyChanged(nameof (DifficultyLevels));
      }
    }

    public ICommand PlayerPhotoSelect { get; private set; }

    public ICommand ChangePlayer { get; private set; }

    public ICommand NewCampaign { get; private set; }

    private void ContinueAction()
    {
      this.IsBusy = true;
      PageNavigationService.OpenBattlefield(true);
    }

    public void OnNavigatedTo(NavigationMode mode, Uri uri)
    {
      if (mode == NavigationMode.Back)
      {
        CreatePlayerInfo createPlayerInfo = CreatePlayerViewModel.CreatePlayerInfo;
        if (createPlayerInfo != null)
        {
          this.Player = createPlayerInfo.Name;
          this.PlayerPhoto = createPlayerInfo.Face;
          this.PlayerSpecialElement = createPlayerInfo.Element;
        }
      }
      this.CanContinue = Serializer.Exists("CampaignBattlefieldViewModel__1_452.xml");
      this.RefreshDifficulty();
      this.IsBusy = false;
      if (this.selectingPlayerPhoto && FaceSelectionView.LastSetPhoto != null)
        this.PlayerPhoto = FaceSelectionView.LastSetPhoto;
      FaceSelectionView.LastSetPhoto = (string) null;
      if (mode == NavigationMode.Back)
        return;
      this.RefreshData();
    }

    public void OnNavigatedFrom() => this.Save();

    public string Player
    {
      get => this.player;
      set
      {
        this.player = value;
        this.RaisePropertyChanged(nameof (Player));
      }
    }

    public string[] Specializations
    {
      get => this.specializations;
      set
      {
        this.specializations = value;
        this.RaisePropertyChanged(nameof (Specializations));
      }
    }

    public string[] Photos
    {
      get => this.photos;
      set
      {
        this.photos = value;
        this.RaisePropertyChanged(nameof (Photos));
      }
    }

    public Specialization PlayerSpecialization
    {
      get => this.playerSpecialization;
      set
      {
        this.playerSpecialization = value;
        this.RaisePropertyChanged(nameof (PlayerSpecialization));
      }
    }

    public string PlayerPhoto
    {
      get => this.playerPhoto;
      set
      {
        this.playerPhoto = value;
        this.RaisePropertyChanged(nameof (PlayerPhoto));
      }
    }

    public bool CanContinue
    {
      get => this.canContinue;
      set
      {
        this.canContinue = value;
        this.Continue.RaiseCanExecuteChanged();
      }
    }

    public RelayCommand Continue { get; private set; }

    public RelayCommand NewGame { get; private set; }
  }
}
