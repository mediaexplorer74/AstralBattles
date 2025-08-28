﻿﻿﻿﻿﻿﻿﻿﻿// Decompiled with JetBrains decompiler
// Type: AstralBattles.ViewModels.CreatePlayerViewModel
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Model;
using AstralBattles.Core.Services;
using AstralBattles.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Navigation;
using AstralBattles.Core.Infrastructure;



namespace AstralBattles.ViewModels
{
  public class CreatePlayerViewModel : ViewModelBaseEx
  {
    private string face;
    private string name;
    private ElementTypeEnum playerSpecialElement;
    private ObservableCollection<ElementTypeEnum> specialElements;
    private readonly Random random = new Random();
    private AstralBattles.Core.Infrastructure.NavigationService navigationService;

    public CreatePlayerViewModel()
    {
      SpecialElements = new ObservableCollection<ElementTypeEnum>((IEnumerable<ElementTypeEnum>) SpecialElementsContainer.Elements);
      
      // Initialize RelayCommands regardless of design mode
      EditDeck = new RelayCommand(EditDeckAction);
      ChangeFace = new RelayCommand(ChangeFaceAction);
      Ok = new RelayCommand(OkAction);
      
      if (App.IsInDesignMode)
      {
        Face = "face33";
        Name = "Egorbo";
      }
    }

    private void EditDeckAction()
    {
      IsBusy = true;
      Windows.Storage.ApplicationData.Current.LocalSettings.Values["ConfiguratedDeck"] = CreatePlayerViewModel.CreatePlayerInfo.Deck;
      PageNavigationService.OpenDeckEditor();
    }

    private void OkAction()
    {
      CreatePlayerViewModel.CreatePlayerInfo = new CreatePlayerInfo()
      {
        Element = PlayerSpecialElement,
        Face = Face,
        Name = Name,
        Deck = CreatePlayerViewModel.CreatePlayerInfo.Deck
      };
      if (navigationService == null || !navigationService.CanGoBack)
        return;
      navigationService.GoBack();
    }

    private void ChangeFaceAction()
    {
      IsBusy = true;
      PageNavigationService.FaceSelectionView();
    }

    public RelayCommand ChangeFace { get; private set; }

    public RelayCommand EditDeck { get; private set; }

    public RelayCommand Ok { get; private set; }

    public ElementTypeEnum PlayerSpecialElement
    {
      get => playerSpecialElement;
      set
      {
        playerSpecialElement = value;
        RaisePropertyChanged(nameof (PlayerSpecialElement));
        if (CreatePlayerViewModel.CreatePlayerInfo == null || CreatePlayerViewModel.CreatePlayerInfo.Deck == null || PlayerSpecialElement == CreatePlayerViewModel.CreatePlayerInfo.Deck.Last<KeyValuePair<ElementTypeEnum, ObservableCollection<Card>>>().Key)
          return;
        CreatePlayerViewModel.CreatePlayerInfo.Deck = PlayersFactory.CreateRandomDeck(PlayerSpecialElement);
      }
    }

    public ObservableCollection<ElementTypeEnum> SpecialElements
    {
      get => specialElements;
      set
      {
        specialElements = value;
        RaisePropertyChanged(nameof (SpecialElements));
      }
    }

    public string Face
    {
      get => face;
      set
      {
        face = value;
        RaisePropertyChanged(nameof (Face));
      }
    }

    public string Name
    {
      get => name;
      set
      {
        name = value;
        RaisePropertyChanged(nameof (Name));
      }
    }

    public static CreatePlayerInfo CreatePlayerInfo { get; set; }

    public void OnNavigatedTo(
      NavigationEventArgs navigationEventArgs,
      AstralBattles.Core.Infrastructure.NavigationContext navContext,
      AstralBattles.Core.Infrastructure.NavigationService navService,
      bool enableDeckEditor)
    {
      IsBusy = false;
      if (navigationEventArgs.NavigationMode == NavigationMode.Back)
      {
        if (!string.IsNullOrWhiteSpace(FaceSelectionView.LastSetPhoto))
          Face = FaceSelectionView.LastSetPhoto;

        if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.PopValueOrDefault<object, string>("ConfiguratedDeck") as Deck == null)//!= null)
            return;
        Deck deck = (Windows.Storage.ApplicationData.Current.LocalSettings.Values.PopValueOrDefault<object, string>("ConfiguratedDeck") as Deck);
        CreatePlayerViewModel.CreatePlayerInfo.Deck = deck;
      }
      else
      {
        navigationService = navService;
        if (CreatePlayerViewModel.CreatePlayerInfo == null)
        {
          Face = "face" + (object) random.Next(5, 40);
          Name = "Player" + (object) random.Next(10000, 19999);
          PlayerSpecialElement = ElementTypeEnum.Holy;
        }
        else
        {
          Face = CreatePlayerViewModel.CreatePlayerInfo.Face;
          Name = CreatePlayerViewModel.CreatePlayerInfo.Name;
          PlayerSpecialElement = CreatePlayerViewModel.CreatePlayerInfo.Element;
        }
      }
    }

    public void OnNavigatedFrom(
      NavigationEventArgs navigationEventArgs,
      AstralBattles.Core.Infrastructure.NavigationContext navContext,
      AstralBattles.Core.Infrastructure.NavigationService navService)
    {
    }

    public void OnBackKeyPress()
    {
      CreatePlayerViewModel.CreatePlayerInfo = (CreatePlayerInfo) null;
    }
  }
}
