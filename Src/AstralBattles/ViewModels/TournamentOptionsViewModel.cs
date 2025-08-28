using AstralBattles.Converters;
using AstralBattles.Core.Infrastructure;
using AstralBattles.Core.Model;
using AstralBattles.Core.Services;
using AstralBattles.Localizations;
using AstralBattles.Options;
using AstralBattles.Views;
using System;
using System.Collections.Generic;
using Windows.Storage;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using System.Windows.Input;


namespace AstralBattles.ViewModels
{
  public class TournamentOptionsViewModel : ViewModelBaseEx
  {
    private string player;
    private Specialization playerSpecialization;
    private bool selectingPlayerPhoto;
    private GameDifficulty gameDifficulty;
    private string[] specialElements;
    private ElementTypeEnum playerSpecialElement;
    private GameDifficulty[] difficultyLevels;
    private string[] specializations;
    private string[] photos;
    private string playerPhoto;
    private bool canContinue;

    public TournamentOptionsViewModel()
    {
      SpecializationConverter specializationConverter = new SpecializationConverter();
      SpecialElementConverter elementConverter = new SpecialElementConverter();
      Specializations = new string[6]
      {
        specializationConverter.Convert(Specialization.Pyromancer),
        specializationConverter.Convert(Specialization.IceLord),
        specializationConverter.Convert(Specialization.Stormbringer),
        specializationConverter.Convert(Specialization.Druid),
        specializationConverter.Convert(Specialization.Necromancer),
        specializationConverter.Convert(Specialization.Elementalist)
      };
      SpecialElements = ((IEnumerable<ElementTypeEnum>) SpecialElementsContainer.Elements).Select<ElementTypeEnum, string>(new Func<ElementTypeEnum, string>(elementConverter.Convert)).ToArray<string>();
      Continue = new RelayCommand(ContinueAction, (Func<bool>) (() => CanContinue));
      NewGame = new RelayCommand(NewGameAction);
      ChangePlayer = (ICommand) new RelayCommand(ChangePlayerAction);
      Photos = Enumerable.Range(1, 79).Select<int, string>((Func<int, string>) (i => "face" + (object) i)).ToArray<string>();
      PlayerPhotoSelect = (ICommand) new RelayCommand(PlayerPhotoSelectAction);
      if (App.IsInDesignMode)
      {
        PlayerPhoto = ((IEnumerable<string>) Photos).GetRandomElement<string>();
        Player = "Nagga";
        PlayerSpecialElement = ElementTypeEnum.Sorcery;
      }
      else
        RefreshDifficulty();
    }

    private void ChangePlayerAction()
    {
      CreatePlayerViewModel.CreatePlayerInfo = new CreatePlayerInfo()
      {
        Element = PlayerSpecialElement,
        Name = Player,
        Face = PlayerPhoto
      };
      PageNavigationService.OpenCreateNewPlayer();
    }

    private void RefreshDifficulty()
    {
      DifficultyLevels = new GameDifficulty[3]
      {
        GameDifficulty.Easy,
        GameDifficulty.Normal,
        GameDifficulty.Hard
      };
      GameDifficulty = OptionsManager.Current.GameDifficulty;
    }

    private void PlayerPhotoSelectAction()
    {
      selectingPlayerPhoto = true;
      PageNavigationService.FaceSelectionView();
    }

    private async void RefreshData()
    {
      // UWP uses ApplicationData.Current.LocalSettings instead of IsolatedStorageSettings
      var applicationSettings = Windows.Storage.ApplicationData.Current.LocalSettings.Values;
      Player = (applicationSettings.ContainsKey("Player") ? applicationSettings["Player"] : "Player12345").ToString();
      CanContinue = await Serializer.Exists("CurrentTournamentGame__1_452.xml");
      PlayerSpecialization = (Specialization) (applicationSettings.ContainsKey("PlayerSpecialization") ? applicationSettings["PlayerSpecialization"] : Specialization.Elementalist);
      PlayerSpecialElement = (ElementTypeEnum) (applicationSettings.ContainsKey("PlayerSpecialElement") ? applicationSettings["PlayerSpecialElement"] : ((IEnumerable<ElementTypeEnum>) SpecialElementsContainer.Elements).GetRandomElement<ElementTypeEnum>());
      PlayerPhoto = (applicationSettings.ContainsKey("PlayerPhoto") ? applicationSettings["PlayerPhoto"] : ((IEnumerable<string>) Photos).GetRandomElement<string>()).ToString();
    }

    private void Save()
    {
      // UWP uses ApplicationData.Current.LocalSettings instead of IsolatedStorageSettings
      var applicationSettings = Windows.Storage.ApplicationData.Current.LocalSettings.Values;
      applicationSettings["Player"] = Player;
      applicationSettings["PlayerSpecialization"] = PlayerSpecialization;
      applicationSettings["PlayerPhoto"] = PlayerPhoto;
      applicationSettings["PlayerSpecialElement"] = PlayerSpecialElement;
      // UWP automatically saves LocalSettings
    }

    private void NewGameAction()
    {
      // Replace MessageBox with UWP ContentDialog for MVP
      // if (CanContinue && MessageBox.Show(CommonResources.GameWillBeOverwritten, CommonResources.Confirmation, MessageBoxButton.OKCancel) != MessageBoxResult.OK)
      //   return;
      if (string.IsNullOrWhiteSpace(Player))
      {
        // Replace MessageBox with UWP ContentDialog for MVP
        // int num = (int) MessageBox.Show(CommonResources.NamesShouldBeNotEmptyMessage);
      }
      else
      {
        IsBusy = true;
        Save();
        TournamentService.Instance.StartTournament(new AstralBattles.Core.Model.Player()
        {
          Photo = PlayerPhoto,
          DisplayName = Player,
          SpecialElement = PlayerSpecialElement,
          Name = Player + string.Format(" ({0})", (object) CommonResources.You),
          Specialization = Specialization.Elementalist
        });
        PageNavigationService.OpenBattlefield(false);
      }
    }

    public GameDifficulty GameDifficulty
    {
      get => gameDifficulty;
      set
      {
        gameDifficulty = value;
        RaisePropertyChanged(nameof (GameDifficulty));
        OptionsManager.Current.GameDifficulty = value;
        OptionsManager.Save();
      }
    }

    public string[] SpecialElements
    {
      get => specialElements;
      set
      {
        specialElements = value;
        RaisePropertyChanged(nameof (SpecialElements));
      }
    }

    public ElementTypeEnum PlayerSpecialElement
    {
      get => playerSpecialElement;
      set
      {
        playerSpecialElement = value;
        RaisePropertyChanged(nameof (PlayerSpecialElement));
      }
    }

    public GameDifficulty[] DifficultyLevels
    {
      get => difficultyLevels;
      set
      {
        difficultyLevels = value;
        RaisePropertyChanged(nameof (DifficultyLevels));
      }
    }

    public ICommand PlayerPhotoSelect { get; private set; }

    public ICommand ChangePlayer { get; private set; }

    private void ContinueAction()
    {
      IsBusy = true;
      PageNavigationService.OpenBattlefield(true);
    }

    public async void OnNavigatedTo(NavigationMode mode, Uri uri)
    {
      if (mode == NavigationMode.Back)
      {
        CreatePlayerInfo createPlayerInfo = CreatePlayerViewModel.CreatePlayerInfo;
        if (createPlayerInfo != null)
        {
          Player = createPlayerInfo.Name;
          PlayerPhoto = createPlayerInfo.Face;
          PlayerSpecialElement = createPlayerInfo.Element;
        }
      }
      CanContinue = await Serializer.Exists("CurrentTournamentGame__1_452.xml");
      RefreshDifficulty();
      IsBusy = false;
      if (selectingPlayerPhoto && FaceSelectionView.LastSetPhoto != null)
        PlayerPhoto = FaceSelectionView.LastSetPhoto;
      FaceSelectionView.LastSetPhoto = (string) null;
      if (mode == NavigationMode.Back)
        return;
      RefreshData();
    }

    public void OnNavigatedFrom() => Save();

    public string Player
    {
      get => player;
      set
      {
        player = value;
        RaisePropertyChanged(nameof (Player));
      }
    }

    public string[] Specializations
    {
      get => specializations;
      set
      {
        specializations = value;
        RaisePropertyChanged(nameof (Specializations));
      }
    }

    public string[] Photos
    {
      get => photos;
      set
      {
        photos = value;
        RaisePropertyChanged(nameof (Photos));
      }
    }

    public Specialization PlayerSpecialization
    {
      get => playerSpecialization;
      set
      {
        playerSpecialization = value;
        RaisePropertyChanged(nameof (PlayerSpecialization));
      }
    }

    public string PlayerPhoto
    {
      get => playerPhoto;
      set
      {
        playerPhoto = value;
        RaisePropertyChanged(nameof (PlayerPhoto));
      }
    }

    public bool CanContinue
    {
      get => canContinue;
      set
      {
        canContinue = value;
        Continue.RaiseCanExecuteChanged();
      }
    }

    public RelayCommand Continue { get; private set; }

    public RelayCommand NewGame { get; private set; }
  }
}
