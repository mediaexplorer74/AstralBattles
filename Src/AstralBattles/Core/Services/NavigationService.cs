using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace AstralBattles.Core.Services
{
    public class NavigationService
    {
        private static NavigationService _instance;
        private Frame _frame;
        private readonly Dictionary<string, Type> _pages = new Dictionary<string, Type>();

        public static NavigationService Instance => _instance ??= new NavigationService();

        public void Initialize(Frame frame)
        {
            _frame = frame ?? throw new ArgumentNullException(nameof(frame));
            _frame.Navigated += OnNavigated;
        }

        public void RegisterPage(string key, Type pageType)
        {
            if (_pages.ContainsKey(key))
                throw new ArgumentException($"Page with key {key} is already registered");

            _pages[key] = pageType;
        }

        public bool Navigate(string pageKey, object parameter = null)
        {
            if (!_pages.TryGetValue(pageKey, out var pageType))
                return false;

            return _frame.Navigate(pageType, parameter);
        }

        public void GoBack()
        {
            if (_frame.CanGoBack)
                _frame.GoBack();
        }

        public void GoForward()
        {
            if (_frame.CanGoForward)
                _frame.GoForward();
        }

        public bool CanGoBack => _frame.CanGoBack;
        public bool CanGoForward => _frame.CanGoForward;

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            // Можно добавить логику при навигации
        }
    }
}
