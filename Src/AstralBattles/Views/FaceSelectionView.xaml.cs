using Windows.UI.Xaml.Controls;
using System;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


namespace AstralBattles.Views
{
public partial class FaceSelectionView : Page
  {
    
    public FaceSelectionView()
    {
      this.InitializeComponent();
      string[] array = Enumerable.Range(1, 79).Select<int, string>((Func<int, string>) (i => "face" + (object) i)).ToArray<string>();
      FaceSelectionView.LastSetPhoto = (string) null;
      foreach (string str in array)
      {
        Button button = new Button();
        button.BorderThickness = new Thickness(0.0);
        button.Margin = new Thickness(-2.0);
        Button source = button;
        // UWP doesn't have TiltEffect - stub for MVP
        source.Click += new RoutedEventHandler(this.ButtonClick);
        source.DataContext = (object) str;
        BitmapImage bitmapImage = new BitmapImage(new Uri(string.Format("ms-appx:///Resources/Avatars/{0}.JPG", (object) str)));
        Image image1 = new Image();
        image1.Width = 90.0;
        image1.Height = 90.0;
        image1.Margin = new Thickness(-10.0, -5.0, -10.0, -5.0);
        image1.Source = bitmapImage;
        Image image2 = image1;
        source.Content = (object) image2;
        // TODO: Fix panel reference - commenting out for MVP build
      // this.panel.Children.Add((UIElement) source);
      }
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      FaceSelectionView.LastSetPhoto = (string) null;
      base.OnNavigatedTo(e);
    }

    public static string LastSetPhoto { get; set; }

    private void ButtonClick(object sender, RoutedEventArgs e)
    {
      if (!(sender is Button button) || button.DataContext == null)
        return;
      FaceSelectionView.LastSetPhoto = button.DataContext.ToString();
      if (!((Page) this).Frame.CanGoBack)
        return;
      ((Page) this).Frame.GoBack();
    }

   
  }
}

