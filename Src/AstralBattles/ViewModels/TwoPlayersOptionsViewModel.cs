using AstralBattles.Converters;
using AstralBattles.Core.Infrastructure;
using AstralBattles.Core.Model;
using AstralBattles.Core.Services;
using AstralBattles.Localizations;
using AstralBattles.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace AstralBattles.ViewModels
{
  public class TwoPlayersOptionsViewModel : ViewModelBaseEx
  {
    private string firstPlayer;
    private Specialization firstPlayerSpecialization;
    private string secondPlayer;
    private Specialization secondPlayerSpecialization;
    private bool selectingSecondPlayerinfo;
    private bool selectingFirstPlayerinfo;
    private string[] specializations;
    private string[] specialElements;
    private Deck firstPlayerDeck;
    private Deck secondPlayerDeck;
    private string[] photos;
    private string firstPlayerPhoto;
    private string secondPlayerPhoto;
    private ElementTypeEnum firstPlayerSpecialElement;
    private ElementTypeEnum secondPlayerSpecialElement;
    private bool canContinue;
    private bool isSecondPlayerComputer;

    public TwoPlayersOptionsViewModel()
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
      Photos = Enumerable.Range(1, 79).Select<int, string>((Func<int, string>) (i => "face" + (object) i)).ToArray<string>();
      ChangeFirstPlayer = (ICommand) new RelayCommand(ChangeFirstPlayerAction);
      ChangeSecondPlayer = (ICommand) new RelayCommand(ChangeSecondPlayerAction);

      if (!App.IsInDesignMode)
        return;
      
      FirstPlayerPhoto = ((IEnumerable<string>) Photos).GetRandomElement<string>();
      SecondPlayerPhoto = ((IEnumerable<string>) Photos).GetRandomElement<string>();
      FirstPlayer = "Player3303032";
      SecondPlayer = "Nagg";
    }

    private void ChangeSecondPlayerAction()
    {
      CreatePlayerViewModel.CreatePlayerInfo = new CreatePlayerInfo()
      {
        Element = SecondPlayerSpecialElement,
        Name = SecondPlayer,
        Face = SecondPlayerPhoto,
        Deck = SecondPlayerDeck
      };
      selectingFirstPlayerinfo = false;
      selectingSecondPlayerinfo = true;
      PageNavigationService.OpenCreateNewPlayer(true);
    }

    private void ChangeFirstPlayerAction()
    {
      CreatePlayerViewModel.CreatePlayerInfo = new CreatePlayerInfo()
      {
        Element = FirstPlayerSpecialElement,
        Name = FirstPlayer,
        Face = FirstPlayerPhoto,
        Deck = FirstPlayerDeck
      };
      selectingFirstPlayerinfo = true;
      selectingSecondPlayerinfo = false;
      PageNavigationService.OpenCreateNewPlayer(true);
    }

    private async Task RefreshData()
    {
      // UWP uses ApplicationData.Current.LocalSettings instead of IsolatedStorageSettings
      var applicationSettings = Windows.Storage.ApplicationData.Current.LocalSettings.Values;
      FirstPlayer = (applicationSettings.ContainsKey("FirstPlayer") ? applicationSettings["FirstPlayer"] : "Player 1").ToString();
      SecondPlayer = (applicationSettings.ContainsKey("SecondPlayer") ? applicationSettings["SecondPlayer"] : "Player 2").ToString();
      var a = await Serializer.Exists("CurrentTwoPlayerDuelGame__1_452.xml");
      var b = await Serializer.Exists("DuelWithAiBattlefieldViewModel__1_452.xml");
      CanContinue = a || b;
      FirstPlayerSpecialization = (Specialization) (applicationSettings.ContainsKey("FirstPlayerSpecialization") ? applicationSettings["FirstPlayerSpecialization"] : Specialization.Elementalist);
      SecondPlayerSpecialization = (Specialization) (applicationSettings.ContainsKey("SecondPlayerSpecialization") ? applicationSettings["SecondPlayerSpecialization"] : Specialization.Elementalist);
      FirstPlayerSpecialElement = (ElementTypeEnum) (applicationSettings.ContainsKey("FirstPlayerSpecialElement") ? applicationSettings["FirstPlayerSpecialElement"] : ((IEnumerable<ElementTypeEnum>) SpecialElementsContainer.Elements).GetRandomElement<ElementTypeEnum>());
      SecondPlayerSpecialElement = (ElementTypeEnum) (applicationSettings.ContainsKey("SecondPlayerSpecialElement") ? applicationSettings["SecondPlayerSpecialElement"] : ((IEnumerable<ElementTypeEnum>) SpecialElementsContainer.Elements).GetRandomElement<ElementTypeEnum>());
      FirstPlayerPhoto = (applicationSettings.ContainsKey("FirstPlayerPhoto") ? applicationSettings["FirstPlayerPhoto"] : ((IEnumerable<string>) Photos).GetRandomElement<string>()).ToString();
      SecondPlayerPhoto = (applicationSettings.ContainsKey("SecondPlayerPhoto") ? applicationSettings["SecondPlayerPhoto"] : ((IEnumerable<string>) Photos).GetRandomElement<string>()).ToString();
      FirstPlayerDeck = Deck.Deserialize((applicationSettings.ContainsKey("FirstPlayerDeck") ? applicationSettings["FirstPlayerDeck"] : "").ToStringIfNotNull(), PlayersFactory.CreateRandomDeck(FirstPlayerSpecialElement));
      SecondPlayerDeck = Deck.Deserialize((applicationSettings.ContainsKey("SecondPlayerDeck") ? applicationSettings["SecondPlayerDeck"] : "").ToStringIfNotNull(), PlayersFactory.CreateRandomDeck(SecondPlayerSpecialElement));
      IsSecondPlayerComputer = (bool) (applicationSettings.ContainsKey("IsSecondPlayerComputer") ? applicationSettings["IsSecondPlayerComputer"] : false);
    }

    private void Save()
    {
      // UWP uses ApplicationData.Current.LocalSettings instead of IsolatedStorageSettings
      var applicationSettings = Windows.Storage.ApplicationData.Current.LocalSettings.Values;
      applicationSettings["FirstPlayer"] = FirstPlayer;
      applicationSettings["SecondPlayer"] = SecondPlayer;
      applicationSettings["FirstPlayerSpecialization"] = FirstPlayerSpecialization;
      applicationSettings["SecondPlayerSpecialization"] = SecondPlayerSpecialization;
      applicationSettings["FirstPlayerSpecialElement"] = FirstPlayerSpecialElement;
      applicationSettings["SecondPlayerSpecialElement"] = SecondPlayerSpecialElement;
      applicationSettings["FirstPlayerPhoto"] = FirstPlayerPhoto;
      applicationSettings["SecondPlayerPhoto"] = SecondPlayerPhoto;
      applicationSettings["IsSecondPlayerComputer"] = IsSecondPlayerComputer;
      applicationSettings["FirstPlayerDeck"] = Deck.Serialize(FirstPlayerDeck);
      applicationSettings["SecondPlayerDeck"] = Deck.Serialize(SecondPlayerDeck);
      // UWP automatically saves LocalSettings
    }

    private async void NewGameAction()
    {
        if (CanContinue &&
        await new ContentDialog
        {
            Title = CommonResources.Confirmation,
            Content = CommonResources.GameWillBeOverwritten,
            CloseButtonText = "OK",
            SecondaryButtonText = "Cancel"
        }.ShowAsync() == ContentDialogResult.Secondary)
          return;
        
      if (string.IsNullOrWhiteSpace(FirstPlayer) || string.IsNullOrWhiteSpace(SecondPlayer))
      {
            // Replace MessageBox with UWP ContentDialog for MVP
            int num1 = (int)await new ContentDialog
            {
                Title = "Error",
                Content = CommonResources.NamesShouldBeNotEmptyMessage,
                CloseButtonText = "OK"
            }.ShowAsync();
      }
      else if (FirstPlayer == SecondPlayer)
      {
            // Replace MessageBox with UWP ContentDialog for MVP
            int num2 = (int)await new ContentDialog
            {
                Title = "Error",
                Content = CommonResources.NamesShouldBeNotEmptyMessage,
                CloseButtonText = "OK"
            }.ShowAsync();
        }
      else
      {
        IsBusy = true;
        Save();
        PageNavigationService.OpenBattlefield(false, true, IsSecondPlayerComputer);
      }
    }

    public ICommand ChangeFirstPlayer { get; private set; }

    public ICommand ChangeSecondPlayer { get; private set; }

    private void ContinueAction()
    {
      IsBusy = true;
      PageNavigationService.OpenBattlefield(true, true, (GameModes) (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("LastPlayedMode__1_452") ? Windows.Storage.ApplicationData.Current.LocalSettings.Values["LastPlayedMode__1_452"] : GameModes.NotDefined) == GameModes.DuelWithAi);
    }

    public async Task OnNavigatedTo(NavigationMode mode, Uri uri, Core.Infrastructure.NavigationService navigationService)
    {
      IsBusy = false;
      if (mode == NavigationMode.Back)
      {
        CreatePlayerInfo createPlayerInfo = CreatePlayerViewModel.CreatePlayerInfo;
        if (selectingFirstPlayerinfo && createPlayerInfo != null)
        {
          FirstPlayer = createPlayerInfo.Name;
          FirstPlayerPhoto = createPlayerInfo.Face;
          FirstPlayerSpecialElement = createPlayerInfo.Element;
          FirstPlayerDeck = createPlayerInfo.Deck;
        }
        if (selectingSecondPlayerinfo && createPlayerInfo != null)
        {
          SecondPlayer = createPlayerInfo.Name;
          SecondPlayerPhoto = createPlayerInfo.Face;
          SecondPlayerSpecialElement = createPlayerInfo.Element;
          SecondPlayerDeck = createPlayerInfo.Deck;
        }
        selectingFirstPlayerinfo = false;
        selectingSecondPlayerinfo = false;
      }
      var a = await Serializer.Exists("CurrentTwoPlayerDuelGame__1_452.xml");
      var b = await Serializer.Exists("DuelWithAiBattlefieldViewModel__1_452.xml");
      CanContinue = a || b;
      if (mode == NavigationMode.Back)
        return;
      RefreshData();
    }

    public void OnNavigatedFrom() => Save();

    public string FirstPlayer
    {
      get => firstPlayer;
      set
      {
        firstPlayer = value;
        RaisePropertyChanged(nameof (FirstPlayer));
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

    public string[] SpecialElements
    {
      get => specialElements;
      set
      {
        specialElements = value;
        RaisePropertyChanged(nameof (SpecialElements));
      }
    }

    public Deck FirstPlayerDeck
    {
      get => firstPlayerDeck;
      set
      {
        firstPlayerDeck = value;
        RaisePropertyChanged(nameof (FirstPlayerDeck));
      }
    }

    public Deck SecondPlayerDeck
    {
      get => secondPlayerDeck;
      set
      {
        secondPlayerDeck = value;
        RaisePropertyChanged(nameof (SecondPlayerDeck));
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

    public Specialization FirstPlayerSpecialization
    {
      get => firstPlayerSpecialization;
      set
      {
        firstPlayerSpecialization = value;
        RaisePropertyChanged(nameof (FirstPlayerSpecialization));
      }
    }

    public string SecondPlayer
    {
      get => secondPlayer;
      set
      {
        secondPlayer = value;
        RaisePropertyChanged(nameof (SecondPlayer));
      }
    }

    public string FirstPlayerPhoto
    {
      get => firstPlayerPhoto;
      set
      {
        firstPlayerPhoto = value;
        RaisePropertyChanged(nameof (FirstPlayerPhoto));
      }
    }

    public string SecondPlayerPhoto
    {
      get => secondPlayerPhoto;
      set
      {
        secondPlayerPhoto = value;
        RaisePropertyChanged(nameof (SecondPlayerPhoto));
      }
    }

    public Specialization SecondPlayerSpecialization
    {
      get => secondPlayerSpecialization;
      set
      {
        secondPlayerSpecialization = value;
        RaisePropertyChanged(nameof (SecondPlayerSpecialization));
      }
    }

    public ElementTypeEnum FirstPlayerSpecialElement
    {
      get => firstPlayerSpecialElement;
      set
      {
        firstPlayerSpecialElement = value;
        RaisePropertyChanged(nameof (FirstPlayerSpecialElement));
      }
    }

    public ElementTypeEnum SecondPlayerSpecialElement
    {
      get => secondPlayerSpecialElement;
      set
      {
        secondPlayerSpecialElement = value;
        RaisePropertyChanged(nameof (SecondPlayerSpecialElement));
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

    public bool IsSecondPlayerComputer
    {
      get => isSecondPlayerComputer;
      set
      {
        if (isSecondPlayerComputer == value)
          return;
        isSecondPlayerComputer = value;
        RaisePropertyChanged(nameof (IsSecondPlayerComputer));
        if (value && SecondPlayer == "Player 2")
        {
          SecondPlayer = "Computer";
        }
        else
        {
          if (value || !(SecondPlayer == "Computer"))
            return;
          SecondPlayer = "Player 2";
        }
      }
    }
  }
}
