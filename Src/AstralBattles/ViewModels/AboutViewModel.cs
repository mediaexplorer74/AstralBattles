using AstralBattles.Core.Infrastructure;
using AstralBattles.Localizations;
using System;
using System.Windows.Input;

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
            ApplicationName = "Astral Battles";
            Version = "1.4.5";
            Copyrights = CommonResources.Copyrights;
            Email = "astralbattles@live.com";
            Review = new RelayCommand(_ => ReviewAction());
            Mailme = new RelayCommand(_ => ComposeLetterAction());
            SubmitIdea = new RelayCommand(_ => SubmitIdeaAction());
        }

        private async void ReviewAction()
        {
            // UWP store review - stub for MVP
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?ProductId=9WZDNCRFJ3Q2"));
        }

        private void SubmitIdeaAction()
        {
            // Implementation for submitting ideas
        }

        private async void ComposeLetterAction()
        {
            // UWP email launcher - stub for MVP  
            // await Windows.System.Launcher.LaunchUriAsync(new Uri($"mailto:{Email}?subject={ApplicationName} ({Version}) support"));
        }

        public string ApplicationName
        {
            get => applicationName;
            set => SetProperty(ref applicationName, value);
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public string Version
        {
            get => version;
            set => SetProperty(ref version, value);
        }

        public string Copyrights
        {
            get => copyrights;
            set => SetProperty(ref copyrights, value);
        }

        public ICommand Review { get; }
        public ICommand Mailme { get; }
        public ICommand SubmitIdea { get; }
    }
}
