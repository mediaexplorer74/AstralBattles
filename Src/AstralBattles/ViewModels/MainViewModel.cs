using AstralBattles.Core.Infrastructure;
using AstralBattles.Core.Model;
using AstralBattles.Localizations;
using AstralBattles.Options;
using AstralBattles.Views;
using System;
using System.Collections.Generic;
using Windows.Storage;
using System.Linq;
using Windows.Media.Playback;
using Windows.Media.Core;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics;


namespace AstralBattles.ViewModels
{
  public class MainViewModel : ViewModelBaseEx
  {
    private static readonly ILogger Logger = LogFactory.GetLogger<MainViewModel>();
    private bool canContinue = false; //!
    private bool isSoundsEnabled = false; //!
    private bool isVibrationEnabled = false; //! 
    private string language = "Russian"; //!
    private string[] languages;

    public MainViewModel()
    {
      Continue = new RelayCommand(ContinueAction, new Func<bool>(ContinueCanExecute));
      Tournament = new RelayCommand(TournamentAction, new Func<bool>(TournamentCanExecute));
      ShowTutorial = new RelayCommand(ShowTutorialAction);
      
      if (ViewModelBaseEx.IsInDesignModeStatic)
        return;

      Languages = ((IEnumerable<AstralBattles.Localizations.Language>) LocalizationManager.Instance.Languages)
                .Select<AstralBattles.Localizations.Language, string>((Func<AstralBattles.Localizations.Language, string>) 
                (i => i.Name)).ToArray<string>();

      OptionsLoad();

     if (OptionsManager.Current != null)
     {
        IsSoundsEnabled = OptionsManager.Current.EnableSounds;
        IsVibrationEnabled = OptionsManager.Current.EnableVibration;
        Language = OptionsManager.Current.Language;
     }
     else
     {
        //IsSoundsEnabled = false;
        //IsVibrationEnabled = false;
        //Language = Languages[0];
     }

      TwoPlayers = new RelayCommand((Action)(() => PageNavigationService.TwoPlayersOptions()));
      Campaign = new RelayCommand((Action) (() => PageNavigationService.CampaignOptions()));
      OnNavigatedTo();
    }

    private async void OptionsLoad()
    {
        await OptionsManager.ReloadAsync();
    }

    private void ShowTutorialAction()
    {
        var mediaPlayer = new MediaPlayer();
        var uri = new Uri("ms-appx:///Resources/Videos/tutorial.mp4");
        mediaPlayer.Source = MediaSource.CreateFromUri(uri);
        mediaPlayer.Play();
    }

    public async void OnNavigatedTo()
    {
      IsBusy = false;
      var a = await Serializer.Exists("CurrentTournamentGame__1_452.xml");
      var b = await Serializer.Exists("CurrentTwoPlayerDuelGame__1_452.xml");
      var c = await Serializer.Exists("DuelWithAiBattlefieldViewModel__1_452.xml");
      CanContinue = a || b || c;
    }

    private void TournamentAction() => PageNavigationService.OpenTournamentOptions();

    public bool CanContinue
    {
      get => canContinue;
      set
      {
        canContinue = value;
        RaisePropertyChanged(nameof (CanContinue));
        Continue.RaiseCanExecuteChanged();
      }
    }

    private bool TournamentCanExecute() => true;

    private async void ContinueAction()
    {
      try
      {
        IsBusy = true;
        bool flag1 = await Serializer.Exists("CurrentTwoPlayerDuelGame__1_452.xml");
        bool flag2 = await Serializer.Exists("CurrentTournamentGame__1_452.xml");
        bool flag3 = await Serializer.Exists("DuelWithAiBattlefieldViewModel__1_452.xml");
        GameModes valueOrDefault = (GameModes) 
                    (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("LastPlayedMode__1_452")
                    ? Windows.Storage.ApplicationData.Current.LocalSettings.Values["LastPlayedMode__1_452"] : GameModes.NotDefined);
        bool is2PlayersDuel = valueOrDefault == GameModes.HotsitDuel || valueOrDefault == GameModes.DuelWithAi;
        bool isAiDuel = valueOrDefault == GameModes.DuelWithAi;

        if ((valueOrDefault != GameModes.DuelWithAi || !flag3) 
                    && (valueOrDefault != GameModes.HotsitDuel || !flag1)
                    && (valueOrDefault != GameModes.Tournament || !flag2) 
                    || valueOrDefault == GameModes.NotDefined)
        {
          is2PlayersDuel = flag1;
          isAiDuel = flag3;
        }
        if (valueOrDefault == GameModes.NotDefined)
          IsBusy = false;
        else
          PageNavigationService.OpenBattlefield(true, is2PlayersDuel, isAiDuel);
      }
      catch (Exception ex)
      {
        Debug.WriteLine("[ex] MainViewModel - ContinueAction error: " + ex.Message);
        MainViewModel.Logger.LogError(ex);
        //throw;
      }
    }

    private bool ContinueCanExecute() => CanContinue;

    public RelayCommand ShowTutorial { get; set; }

    public RelayCommand Continue { get; set; }

    public RelayCommand Tournament { get; set; }

    public RelayCommand OpenUserVoicePage { get; set; }

    public RelayCommand RateMyApplicationPage { get; set; }

    public RelayCommand TwoPlayers { get; set; }

    public RelayCommand Campaign { get; set; }

    public bool IsSoundsEnabled
    {
      get => isSoundsEnabled;
      set
      {
        isSoundsEnabled = value;
        RaisePropertyChanged(nameof (IsSoundsEnabled));
        OptionsManager.Current.EnableSounds = value;
        OptionsManager.Save();
      }
    }

    public bool IsVibrationEnabled
    {
      get => isVibrationEnabled;
      set
      {
        isVibrationEnabled = value;
        RaisePropertyChanged(nameof (IsVibrationEnabled));
        OptionsManager.Current.EnableVibration = value;
        OptionsManager.Save();
      }
    }

    public string Language
    {
      get => language;
      set
      {
        language = value;
        RaisePropertyChanged(nameof (Language));
        OptionsManager.Current.Language = value;
        LocalizationManager.ChangeLanguage(value);
        OptionsManager.Save();
      }
    }

    public string[] Languages
    {
      get => languages;
      set => languages = value;
    }
  }
}
