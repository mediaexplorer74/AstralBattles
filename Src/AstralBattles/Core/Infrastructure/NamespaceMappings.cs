using System.Collections.Generic;

namespace AstralBattles.Core.Infrastructure
{
    public static class NamespaceMappings
    {
        // Windows Phone 7 → UWP Mappings
        public static readonly Dictionary<string, string> Mappings = new Dictionary<string, string>
        {
            // Controls
            {"Microsoft.Phone.Controls", "Windows.UI.Xaml.Controls"},
            {"System.Windows.Controls", "Windows.UI.Xaml.Controls"},
            
            // Shell
            {"Microsoft.Phone.Shell", "Windows.UI.Xaml"},
            
            // Tasks
            {"Microsoft.Phone.Tasks", "Windows.System"},
            
            // Navigation
            {"System.Windows.Navigation", "Windows.UI.Xaml.Navigation"},
            
            // Media
            {"Microsoft.Xna.Framework.Audio", "Windows.Media.Playback"},
            
            // Input
            {"Microsoft.Devices.Sensors", "Windows.Devices.Sensors"},
            
            // Storage
            {"System.IO.IsolatedStorage", "Windows.Storage"},
            
            // Threading
            {"System.Threading", "Windows.System.Threading"},
            
            // Networking
            {"Microsoft.Phone.Net.NetworkInformation", "Windows.Networking.Connectivity"},
            
            // Notifications
            {"Microsoft.Phone.Notification", "Windows.UI.Notifications"},
            
            // Camera
            {"Microsoft.Devices", "Windows.Media.Capture"}
        };

        // Классы, требующие специального маппинга
        public static readonly Dictionary<string, string> ClassMappings = new Dictionary<string, string>
        {
            // Controls
            {"PhoneApplicationPage", "Page"},
            {"PhoneApplicationFrame", "Frame"},
            
            // Shell
            {"PhoneApplicationService", "Application"},
            
            // Tasks
            {"MarketplaceDetailTask", "Launcher"},
            {"MarketplaceReviewTask", "Launcher"},
            
            // Storage
            {"IsolatedStorageSettings", "ApplicationData.Current.LocalSettings"},
            {"IsolatedStorageFile", "ApplicationData.Current.LocalFolder"},
            
            // Media
            {"SoundEffect", "MediaPlayer"},
            
            // Navigation
            {"NavigationService", "Frame"}
        };
    }
}
