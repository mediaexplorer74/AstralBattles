// Decompiled with JetBrains decompiler
// Type: AstralBattles.ViewModels.AboutViewModel
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Localizations;
using GalaSoft.MvvmLight.Command;
using Microsoft.Phone.Tasks;
using System;
using System.Windows.Input;

#nullable disable
namespace AstralBattles.ViewModels
{
  public class AboutViewModel : ViewModelBaseEx
  {
    private string applicationName;
    private string email;
    private string version;
    private string copyrights;

    public AboutViewModel()
    {
      this.ApplicationName = "Astral Battles";
      this.Version = "1.4.5";
      this.Copyrights = CommonResources.Copyrights;
      this.Email = "astralbattles@live.com";
      this.Review = (ICommand) new RelayCommand((Action) (() => new MarketplaceReviewTask().Show()));
      this.Mailme = (ICommand) new RelayCommand(new Action(this.ComposeLetterAction));
      this.SubmitIdea = (ICommand) new RelayCommand(new Action(this.SubmitIdeaAction));
    }

    private void SubmitIdeaAction()
    {
    }

    private void ComposeLetterAction()
    {
      new EmailComposeTask()
      {
        To = this.Email,
        Subject = string.Format("{0} ({1}) support", (object) this.ApplicationName, (object) this.Version)
      }.Show();
    }

    public string ApplicationName
    {
      get => this.applicationName;
      set
      {
        this.applicationName = value;
        this.RaisePropertyChanged(nameof (ApplicationName));
      }
    }

    public string Email
    {
      get => this.email;
      set
      {
        this.email = value;
        this.RaisePropertyChanged(nameof (Email));
      }
    }

    public string Version
    {
      get => this.version;
      set
      {
        this.version = value;
        this.RaisePropertyChanged(nameof (Version));
      }
    }

    public string Copyrights
    {
      get => this.copyrights;
      set
      {
        this.copyrights = value;
        this.RaisePropertyChanged(nameof (Copyrights));
      }
    }

    public ICommand Review { get; set; }

    public ICommand Mailme { get; set; }

    public ICommand SubmitIdea { get; set; }
  }
}
