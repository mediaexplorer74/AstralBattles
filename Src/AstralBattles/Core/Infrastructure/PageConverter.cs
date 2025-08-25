using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AstralBattles.Core.Infrastructure
{
    public static class PageConverter
    {
        public static string ConvertXamlToUwp(string xamlContent)
        {
            if (string.IsNullOrEmpty(xamlContent))
                return xamlContent;

            // Replace root element
            xamlContent = xamlContent.Replace("<phone:PhoneApplicationPage", "<Page")
                                   .Replace("</phone:PhoneApplicationPage>", "</Page>");

            // Replace namespaces
            xamlContent = xamlContent.Replace("xmlns:phone=\"clr-namespace:Microsoft.Phone.Controls;assembly=Microsoft.Phone\"", 
                                            "");
            
            // Remove shell:SystemTray attributes
            xamlContent = Regex.Replace(xamlContent, @"shell:SystemTray\.[^\s\"]+", "");
            
            // Replace ApplicationBar
            xamlContent = xamlContent.Replace("<phone:PhoneApplicationPage.ApplicationBar>", 
                                            "<Page.BottomAppBar>\n    <CommandBar>")
                                   .Replace("</phone:PhoneApplicationPage.ApplicationBar>", 
                                           "    </CommandBar>\n</Page.BottomAppBar>")
                                   .Replace("<shell:ApplicationBar", "<CommandBar")
                                   .Replace("</shell:ApplicationBar>", "</CommandBar>")
                                   .Replace("<shell:ApplicationBarIconButton", "<AppBarButton")
                                   .Replace("</shell:ApplicationBarIconButton>", "</AppBarButton>")
                                   .Replace("<shell:ApplicationBarMenuItem", "<AppBarButton")
                                   .Replace("</shell:ApplicationBarMenuItem>", "</AppBarButton>");

            // Replace common controls
            xamlContent = xamlContent.Replace("<phone:", "<")
                                   .Replace("</phone:", "</");

            // Replace common attributes
            xamlContent = xamlContent.Replace("Foreground=\"White\"", "RequestedTheme=\"Dark\"");
            
            // Add UWP namespaces if not present
            if (!xamlContent.Contains("xmlns:controls"))
            {
                xamlContent = xamlContent.Replace("<Page", 
                    "<Page\n    xmlns:controls=\"using:Windows.UI.Xaml.Controls" +
                    "\n    xmlns:media=\"using:Windows.UI.Xaml.Media" +
                    "\n    xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml" +
                    "\n    xmlns:d=\"http://schemas.microsoft.com/expression/blend/2008" +
                    "\n    xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006"\n    mc:Ignorable=\"d\"");
            }

            return xamlContent;
        }

        public static string ConvertCodeBehindToUwp(string csContent)
        {
            if (string.IsNullOrEmpty(csContent))
                return csContent;

            // Replace base class
            csContent = csContent.Replace("PhoneApplicationPage", "Page");

            // Replace using directives
            csContent = csContent.Replace("using Microsoft.Phone.Controls;", 
                                        "using Windows.UI.Xaml.Controls;")
                               .Replace("using Microsoft.Phone.Shell;", 
                                       "using Windows.UI.Xaml;")
                               .Replace("using System.Windows.Navigation;", 
                                       "using Windows.UI.Xaml.Navigation;");

            // Replace navigation service calls
            csContent = csContent.Replace("NavigationService.Navigate", 
                                        "Frame.Navigate");

            // Replace MessageBox
            csContent = csContent.Replace("MessageBox.Show(", 
                                        "await new MessageDialog(");

            return csContent;
        }
    }
}
