using System;
using Windows.UI.Xaml.Navigation;

namespace AstralBattles.Core.Infrastructure
{
    // Stub classes for legacy Windows Phone navigation compatibility
    // These will be replaced with proper UWP navigation in future versions
    
    public class NavigationContext
    {
        // Stub for legacy navigation context
        public object State { get; set; }
    }

    public class NavigationService
    {
        public bool CanGoBack => false; // Stub implementation
        public void GoBack() { /* Stub implementation */ }
        public void Navigate(string url) { /* Stub implementation */ }
    }
}