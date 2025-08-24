// Decompiled with JetBrains decompiler
// Type: AstralBattles.ViewModels.CreatePlayerViewModel
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Model;
using AstralBattles.Core.Services;
using AstralBattles.Views;
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Navigation;

#nullable disable
namespace AstralBattles.ViewModels
{
  public class CreatePlayerViewModel : ViewModelBaseEx
  {
    private string face;
    private string name;
    private ElementTypeEnum playerSpecialElement;
    private ObservableCollection<ElementTypeEnum> specialElements;
    private readonly Random random = new Random();
    private NavigationService navigationService;

    public CreatePlayerViewModel()
    {
      this.SpecialElements = new ObservableCollection<ElementTypeEnum>((IEnumerable<ElementTypeEnum>) SpecialElementsContainer.Elements);
      if (this.IsInDesignMode)
      {
        this.Face = "face33";
        this.Name = "Egorbo";
      }
      else
      {
        this.EditDeck = new RelayCommand(new Action(this.EditDeckAction));
        this.ChangeFace = new RelayCommand(new Action(this.ChangeFaceAction));
        this.Ok = new RelayCommand(new Action(this.OkAction));
      }
    }

    private void EditDeckAction()
    {
      this.IsBusy = true;
      PhoneApplicationService.Current.State["ConfiguratedDeck"] = (object) CreatePlayerViewModel.CreatePlayerInfo.Deck;
      PageNavigationService.OpenDeckEditor();
    }

    private void OkAction()
    {
      CreatePlayerViewModel.CreatePlayerInfo = new CreatePlayerInfo()
      {
        Element = this.PlayerSpecialElement,
        Face = this.Face,
        Name = this.Name,
        Deck = CreatePlayerViewModel.CreatePlayerInfo.Deck
      };
      if (this.navigationService == null || !this.navigationService.CanGoBack)
        return;
      this.navigationService.GoBack();
    }

    private void ChangeFaceAction()
    {
      this.IsBusy = true;
      PageNavigationService.FaceSelectionView();
    }

    public RelayCommand ChangeFace { get; private set; }

    public RelayCommand EditDeck { get; private set; }

    public RelayCommand Ok { get; private set; }

    public ElementTypeEnum PlayerSpecialElement
    {
      get => this.playerSpecialElement;
      set
      {
        this.playerSpecialElement = value;
        this.RaisePropertyChanged(nameof (PlayerSpecialElement));
        if (CreatePlayerViewModel.CreatePlayerInfo == null || CreatePlayerViewModel.CreatePlayerInfo.Deck == null || this.PlayerSpecialElement == CreatePlayerViewModel.CreatePlayerInfo.Deck.Last<KeyValuePair<ElementTypeEnum, ObservableCollection<Card>>>().Key)
          return;
        CreatePlayerViewModel.CreatePlayerInfo.Deck = PlayersFactory.CreateRandomDeck(this.PlayerSpecialElement);
      }
    }

    public ObservableCollection<ElementTypeEnum> SpecialElements
    {
      get => this.specialElements;
      set
      {
        this.specialElements = value;
        this.RaisePropertyChanged(nameof (SpecialElements));
      }
    }

    public string Face
    {
      get => this.face;
      set
      {
        this.face = value;
        this.RaisePropertyChanged(nameof (Face));
      }
    }

    public string Name
    {
      get => this.name;
      set
      {
        this.name = value;
        this.RaisePropertyChanged(nameof (Name));
      }
    }

    public static CreatePlayerInfo CreatePlayerInfo { get; set; }

    public void OnNavigatedTo(
      NavigationEventArgs navigationEventArgs,
      NavigationContext navContext,
      NavigationService navService,
      bool enableDeckEditor)
    {
      this.IsBusy = false;
      if (navigationEventArgs.NavigationMode == NavigationMode.Back)
      {
        if (!string.IsNullOrWhiteSpace(FaceSelectionView.LastSetPhoto))
          this.Face = FaceSelectionView.LastSetPhoto;
        if (!(PhoneApplicationService.Current.State.PopValueOrDefault<object, string>("ConfiguratedDeck") is Deck deck))
          return;
        CreatePlayerViewModel.CreatePlayerInfo.Deck = deck;
      }
      else
      {
        this.navigationService = navService;
        if (CreatePlayerViewModel.CreatePlayerInfo == null)
        {
          this.Face = "face" + (object) this.random.Next(5, 40);
          this.Name = "Player" + (object) this.random.Next(10000, 19999);
          this.PlayerSpecialElement = ElementTypeEnum.Holy;
        }
        else
        {
          this.Face = CreatePlayerViewModel.CreatePlayerInfo.Face;
          this.Name = CreatePlayerViewModel.CreatePlayerInfo.Name;
          this.PlayerSpecialElement = CreatePlayerViewModel.CreatePlayerInfo.Element;
        }
      }
    }

    public void OnNavigatedFrom(
      NavigationEventArgs navigationEventArgs,
      NavigationContext navContext,
      NavigationService navService)
    {
    }

    public void OnBackKeyPress()
    {
      CreatePlayerViewModel.CreatePlayerInfo = (CreatePlayerInfo) null;
    }
  }
}
