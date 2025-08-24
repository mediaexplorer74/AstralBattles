// Decompiled with JetBrains decompiler
// Type: AstralBattles.ViewModels.TwoPlayersOptionsViewModel
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
      this.NewGame = new RelayCommand(new Action(this.NewGameAction));
      this.Photos = Enumerable.Range(1, 79).Select<int, string>((Func<int, string>) (i => "face" + (object) i)).ToArray<string>();
      this.ChangeFirstPlayer = (ICommand) new RelayCommand(new Action(this.ChangeFirstPlayerAction));
      this.ChangeSecondPlayer = (ICommand) new RelayCommand(new Action(this.ChangeSecondPlayerAction));
      if (!this.IsInDesignMode)
        return;
      this.FirstPlayerPhoto = ((IEnumerable<string>) this.Photos).GetRandomElement<string>();
      this.SecondPlayerPhoto = ((IEnumerable<string>) this.Photos).GetRandomElement<string>();
      this.FirstPlayer = "Player3303032";
      this.SecondPlayer = "Nagg";
    }

    private void ChangeSecondPlayerAction()
    {
      CreatePlayerViewModel.CreatePlayerInfo = new CreatePlayerInfo()
      {
        Element = this.SecondPlayerSpecialElement,
        Name = this.SecondPlayer,
        Face = this.SecondPlayerPhoto,
        Deck = this.SecondPlayerDeck
      };
      this.selectingFirstPlayerinfo = false;
      this.selectingSecondPlayerinfo = true;
      PageNavigationService.OpenCreateNewPlayer(true);
    }

    private void ChangeFirstPlayerAction()
    {
      CreatePlayerViewModel.CreatePlayerInfo = new CreatePlayerInfo()
      {
        Element = this.FirstPlayerSpecialElement,
        Name = this.FirstPlayer,
        Face = this.FirstPlayerPhoto,
        Deck = this.FirstPlayerDeck
      };
      this.selectingFirstPlayerinfo = true;
      this.selectingSecondPlayerinfo = false;
      PageNavigationService.OpenCreateNewPlayer(true);
    }

    private void RefreshData()
    {
      IsolatedStorageSettings applicationSettings = IsolatedStorageSettings.ApplicationSettings;
      this.FirstPlayer = applicationSettings.GetValueOrDefault<object, string>("FirstPlayer", (object) "Player 1").ToString();
      this.SecondPlayer = applicationSettings.GetValueOrDefault<object, string>("SecondPlayer", (object) "Player 2").ToString();
      this.CanContinue = Serializer.Exists("CurrentTwoPlayerDuelGame__1_452.xml") || Serializer.Exists("DuelWithAiBattlefieldViewModel__1_452.xml");
      this.FirstPlayerSpecialization = (Specialization) applicationSettings.GetValueOrDefault<object, string>("FirstPlayerSpecialization", (object) Specialization.Elementalist);
      this.SecondPlayerSpecialization = (Specialization) applicationSettings.GetValueOrDefault<object, string>("SecondPlayerSpecialization", (object) Specialization.Elementalist);
      this.FirstPlayerSpecialElement = (ElementTypeEnum) applicationSettings.GetValueOrDefault<object, string>("FirstPlayerSpecialElement", (object) ((IEnumerable<ElementTypeEnum>) SpecialElementsContainer.Elements).GetRandomElement<ElementTypeEnum>());
      this.SecondPlayerSpecialElement = (ElementTypeEnum) applicationSettings.GetValueOrDefault<object, string>("SecondPlayerSpecialElement", (object) ((IEnumerable<ElementTypeEnum>) SpecialElementsContainer.Elements).GetRandomElement<ElementTypeEnum>());
      this.FirstPlayerPhoto = applicationSettings.GetValueOrDefault<object, string>("FirstPlayerPhoto", (object) ((IEnumerable<string>) this.Photos).GetRandomElement<string>()).ToString();
      this.SecondPlayerPhoto = applicationSettings.GetValueOrDefault<object, string>("SecondPlayerPhoto", (object) ((IEnumerable<string>) this.Photos).GetRandomElement<string>()).ToString();
      this.FirstPlayerDeck = Deck.Deserialize(applicationSettings.GetValueOrDefault<object, string>("FirstPlayerDeck", (object) "").ToStringIfNotNull(), PlayersFactory.CreateRandomDeck(this.FirstPlayerSpecialElement));
      this.SecondPlayerDeck = Deck.Deserialize(applicationSettings.GetValueOrDefault<object, string>("SecondPlayerDeck", (object) "").ToStringIfNotNull(), PlayersFactory.CreateRandomDeck(this.SecondPlayerSpecialElement));
      this.IsSecondPlayerComputer = (bool) applicationSettings.GetValueOrDefault<object, string>("IsSecondPlayerComputer", (object) false);
    }

    private void Save()
    {
      IsolatedStorageSettings applicationSettings = IsolatedStorageSettings.ApplicationSettings;
      applicationSettings["FirstPlayer"] = (object) this.FirstPlayer;
      applicationSettings["SecondPlayer"] = (object) this.SecondPlayer;
      applicationSettings["FirstPlayerSpecialization"] = (object) this.FirstPlayerSpecialization;
      applicationSettings["SecondPlayerSpecialization"] = (object) this.SecondPlayerSpecialization;
      applicationSettings["FirstPlayerSpecialElement"] = (object) this.FirstPlayerSpecialElement;
      applicationSettings["SecondPlayerSpecialElement"] = (object) this.SecondPlayerSpecialElement;
      applicationSettings["FirstPlayerPhoto"] = (object) this.FirstPlayerPhoto;
      applicationSettings["SecondPlayerPhoto"] = (object) this.SecondPlayerPhoto;
      applicationSettings["IsSecondPlayerComputer"] = (object) this.IsSecondPlayerComputer;
      applicationSettings["FirstPlayerDeck"] = (object) Deck.Serialize(this.FirstPlayerDeck);
      applicationSettings["SecondPlayerDeck"] = (object) Deck.Serialize(this.SecondPlayerDeck);
      applicationSettings.Save();
    }

    private void NewGameAction()
    {
      if (this.CanContinue && MessageBox.Show(CommonResources.GameWillBeOverwritten, CommonResources.Confirmation, MessageBoxButton.OKCancel) != MessageBoxResult.OK)
        return;
      if (string.IsNullOrWhiteSpace(this.FirstPlayer) || string.IsNullOrWhiteSpace(this.SecondPlayer))
      {
        int num1 = (int) MessageBox.Show(CommonResources.NamesShouldBeNotEmptyMessage);
      }
      else if (this.FirstPlayer == this.SecondPlayer)
      {
        int num2 = (int) MessageBox.Show(CommonResources.NamesShouldBeNotEqualMessage);
      }
      else
      {
        this.IsBusy = true;
        this.Save();
        PageNavigationService.OpenBattlefield(false, true, this.IsSecondPlayerComputer);
      }
    }

    public ICommand ChangeFirstPlayer { get; private set; }

    public ICommand ChangeSecondPlayer { get; private set; }

    private void ContinueAction()
    {
      this.IsBusy = true;
      PageNavigationService.OpenBattlefield(true, true, (GameModes) IsolatedStorageSettings.ApplicationSettings.GetValueOrDefault<object, string>("LastPlayedMode__1_452", (object) GameModes.NotDefined) == GameModes.DuelWithAi);
    }

    public void OnNavigatedTo(NavigationMode mode, Uri uri, NavigationService navigationService)
    {
      this.IsBusy = false;
      if (mode == NavigationMode.Back)
      {
        CreatePlayerInfo createPlayerInfo = CreatePlayerViewModel.CreatePlayerInfo;
        if (this.selectingFirstPlayerinfo && createPlayerInfo != null)
        {
          this.FirstPlayer = createPlayerInfo.Name;
          this.FirstPlayerPhoto = createPlayerInfo.Face;
          this.FirstPlayerSpecialElement = createPlayerInfo.Element;
          this.FirstPlayerDeck = createPlayerInfo.Deck;
        }
        if (this.selectingSecondPlayerinfo && createPlayerInfo != null)
        {
          this.SecondPlayer = createPlayerInfo.Name;
          this.SecondPlayerPhoto = createPlayerInfo.Face;
          this.SecondPlayerSpecialElement = createPlayerInfo.Element;
          this.SecondPlayerDeck = createPlayerInfo.Deck;
        }
        this.selectingFirstPlayerinfo = false;
        this.selectingSecondPlayerinfo = false;
      }
      this.CanContinue = Serializer.Exists("CurrentTwoPlayerDuelGame__1_452.xml") || Serializer.Exists("DuelWithAiBattlefieldViewModel__1_452.xml");
      if (mode == NavigationMode.Back)
        return;
      this.RefreshData();
    }

    public void OnNavigatedFrom() => this.Save();

    public string FirstPlayer
    {
      get => this.firstPlayer;
      set
      {
        this.firstPlayer = value;
        this.RaisePropertyChanged(nameof (FirstPlayer));
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

    public string[] SpecialElements
    {
      get => this.specialElements;
      set
      {
        this.specialElements = value;
        this.RaisePropertyChanged(nameof (SpecialElements));
      }
    }

    public Deck FirstPlayerDeck
    {
      get => this.firstPlayerDeck;
      set
      {
        this.firstPlayerDeck = value;
        this.RaisePropertyChanged(nameof (FirstPlayerDeck));
      }
    }

    public Deck SecondPlayerDeck
    {
      get => this.secondPlayerDeck;
      set
      {
        this.secondPlayerDeck = value;
        this.RaisePropertyChanged(nameof (SecondPlayerDeck));
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

    public Specialization FirstPlayerSpecialization
    {
      get => this.firstPlayerSpecialization;
      set
      {
        this.firstPlayerSpecialization = value;
        this.RaisePropertyChanged(nameof (FirstPlayerSpecialization));
      }
    }

    public string SecondPlayer
    {
      get => this.secondPlayer;
      set
      {
        this.secondPlayer = value;
        this.RaisePropertyChanged(nameof (SecondPlayer));
      }
    }

    public string FirstPlayerPhoto
    {
      get => this.firstPlayerPhoto;
      set
      {
        this.firstPlayerPhoto = value;
        this.RaisePropertyChanged(nameof (FirstPlayerPhoto));
      }
    }

    public string SecondPlayerPhoto
    {
      get => this.secondPlayerPhoto;
      set
      {
        this.secondPlayerPhoto = value;
        this.RaisePropertyChanged(nameof (SecondPlayerPhoto));
      }
    }

    public Specialization SecondPlayerSpecialization
    {
      get => this.secondPlayerSpecialization;
      set
      {
        this.secondPlayerSpecialization = value;
        this.RaisePropertyChanged(nameof (SecondPlayerSpecialization));
      }
    }

    public ElementTypeEnum FirstPlayerSpecialElement
    {
      get => this.firstPlayerSpecialElement;
      set
      {
        this.firstPlayerSpecialElement = value;
        this.RaisePropertyChanged(nameof (FirstPlayerSpecialElement));
      }
    }

    public ElementTypeEnum SecondPlayerSpecialElement
    {
      get => this.secondPlayerSpecialElement;
      set
      {
        this.secondPlayerSpecialElement = value;
        this.RaisePropertyChanged(nameof (SecondPlayerSpecialElement));
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

    public bool IsSecondPlayerComputer
    {
      get => this.isSecondPlayerComputer;
      set
      {
        if (this.isSecondPlayerComputer == value)
          return;
        this.isSecondPlayerComputer = value;
        this.RaisePropertyChanged(nameof (IsSecondPlayerComputer));
        if (value && this.SecondPlayer == "Player 2")
        {
          this.SecondPlayer = "Computer";
        }
        else
        {
          if (value || !(this.SecondPlayer == "Computer"))
            return;
          this.SecondPlayer = "Player 2";
        }
      }
    }
  }
}
