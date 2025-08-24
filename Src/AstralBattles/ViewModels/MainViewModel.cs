// Decompiled with JetBrains decompiler
// Type: AstralBattles.ViewModels.MainViewModel
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Infrastructure;
using AstralBattles.Core.Model;
using AstralBattles.Localizations;
using AstralBattles.Options;
using AstralBattles.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;

#nullable disable
namespace AstralBattles.ViewModels
{
  public class MainViewModel : ViewModelBaseEx
  {
    private static readonly ILogger Logger = LogFactory.GetLogger<MainViewModel>();
    private bool canContinue;
    private bool isSoundsEnabled;
    private bool isVibrationEnabled;
    private string language;
    private string[] languages;

    public MainViewModel()
    {
      this.Continue = new RelayCommand(new Action(this.ContinueAction), new Func<bool>(this.ContinueCanExecute));
      this.Tournament = new RelayCommand(new Action(this.TournamentAction), new Func<bool>(this.TournamentCanExecute));
      this.ShowTutorial = new RelayCommand(new Action(this.ShowTutorialAction));
      if (ViewModelBase.IsInDesignModeStatic)
        return;
      this.Languages = ((IEnumerable<AstralBattles.Localizations.Language>) LocalizationManager.Instance.Languages).Select<AstralBattles.Localizations.Language, string>((Func<AstralBattles.Localizations.Language, string>) (i => i.Name)).ToArray<string>();
      OptionsManager.Reload();
      this.IsSoundsEnabled = OptionsManager.Current.EnableSounds;
      this.IsVibrationEnabled = OptionsManager.Current.EnableVibration;
      this.Language = OptionsManager.Current.Language;
      this.TwoPlayers = new RelayCommand((Action) (() => PageNavigationService.TwoPlayersOptions()));
      this.Campaign = new RelayCommand((Action) (() => PageNavigationService.CampaignOptions()));
      this.OnNavigatedTo();
    }

    private void ShowTutorialAction()
    {
      new MediaPlayerLauncher()
      {
        Media = new Uri("Resources/Videos/tutorial.mp4", UriKind.Relative),
        Location = ((MediaLocationType) 1),
        Controls = ((MediaPlaybackControls) 0),
        Orientation = ((MediaPlayerOrientation) 0)
      }.Show();
    }

    public void OnNavigatedTo()
    {
      this.IsBusy = false;
      this.CanContinue = Serializer.Exists("CurrentTournamentGame__1_452.xml") || Serializer.Exists("CurrentTwoPlayerDuelGame__1_452.xml") || Serializer.Exists("DuelWithAiBattlefieldViewModel__1_452.xml");
    }

    private void TournamentAction() => PageNavigationService.OpenTournamentOptions();

    public bool CanContinue
    {
      get => this.canContinue;
      set
      {
        this.canContinue = value;
        this.RaisePropertyChanged(nameof (CanContinue));
        this.Continue.RaiseCanExecuteChanged();
      }
    }

    private bool TournamentCanExecute() => true;

    private void ContinueAction()
    {
      try
      {
        this.IsBusy = true;
        bool flag1 = Serializer.Exists("CurrentTwoPlayerDuelGame__1_452.xml");
        bool flag2 = Serializer.Exists("CurrentTournamentGame__1_452.xml");
        bool flag3 = Serializer.Exists("DuelWithAiBattlefieldViewModel__1_452.xml");
        GameModes valueOrDefault = (GameModes) IsolatedStorageSettings.ApplicationSettings.GetValueOrDefault<object, string>("LastPlayedMode__1_452", (object) GameModes.NotDefined);
        bool is2PlayersDuel = valueOrDefault == GameModes.HotsitDuel || valueOrDefault == GameModes.DuelWithAi;
        bool isAiDuel = valueOrDefault == GameModes.DuelWithAi;
        if ((valueOrDefault != GameModes.DuelWithAi || !flag3) && (valueOrDefault != GameModes.HotsitDuel || !flag1) && (valueOrDefault != GameModes.Tournament || !flag2) || valueOrDefault == GameModes.NotDefined)
        {
          is2PlayersDuel = flag1;
          isAiDuel = flag3;
        }
        if (valueOrDefault == GameModes.NotDefined)
          this.IsBusy = false;
        else
          PageNavigationService.OpenBattlefield(true, is2PlayersDuel, isAiDuel);
      }
      catch (Exception ex)
      {
        MainViewModel.Logger.LogError(ex);
        throw;
      }
    }

    private bool ContinueCanExecute() => this.CanContinue;

    public RelayCommand ShowTutorial { get; set; }

    public RelayCommand Continue { get; set; }

    public RelayCommand Tournament { get; set; }

    public RelayCommand OpenUserVoicePage { get; set; }

    public RelayCommand RateMyApplicationPage { get; set; }

    public RelayCommand TwoPlayers { get; set; }

    public RelayCommand Campaign { get; set; }

    public bool IsSoundsEnabled
    {
      get => this.isSoundsEnabled;
      set
      {
        this.isSoundsEnabled = value;
        this.RaisePropertyChanged(nameof (IsSoundsEnabled));
        OptionsManager.Current.EnableSounds = value;
        OptionsManager.Save();
      }
    }

    public bool IsVibrationEnabled
    {
      get => this.isVibrationEnabled;
      set
      {
        this.isVibrationEnabled = value;
        this.RaisePropertyChanged(nameof (IsVibrationEnabled));
        OptionsManager.Current.EnableVibration = value;
        OptionsManager.Save();
      }
    }

    public string Language
    {
      get => this.language;
      set
      {
        this.language = value;
        this.RaisePropertyChanged(nameof (Language));
        OptionsManager.Current.Language = value;
        LocalizationManager.ChangeLanguage(value);
        OptionsManager.Save();
      }
    }

    public string[] Languages
    {
      get => this.languages;
      set => this.languages = value;
    }
  }
}
