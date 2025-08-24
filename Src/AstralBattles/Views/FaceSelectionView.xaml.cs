// Decompiled with JetBrains decompiler
// Type: AstralBattles.Views.FaceSelectionView
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using Microsoft.Phone.Controls;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Windows.UI.Xaml.Controls;

#nullable disable
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
        TiltEffect.SetIsTiltEnabled((DependencyObject) source, true);
        source.Click += new RoutedEventHandler(this.ButtonClick);
        source.DataContext = (object) str;
        BitmapImage bitmapImage = new BitmapImage(new Uri(string.Format("/AstralBattles;component/Resources/Avatars/{0}.JPG", (object) str), UriKind.Relative));
        Image image1 = new Image();
        image1.Width = 90.0;
        image1.Height = 90.0;
        image1.Margin = new Thickness(-10.0, -5.0, -10.0, -5.0);
        image1.Source = (ImageSource) bitmapImage;
        Image image2 = image1;
        source.Content = (object) image2;
        this.panel.Children.Add((UIElement) source);
      }
    }

    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      FaceSelectionView.LastSetPhoto = (string) null;
      ((Page) this).OnNavigatedTo(e);
    }

    public static string LastSetPhoto { get; set; }

    private void ButtonClick(object sender, RoutedEventArgs e)
    {
      if (!(sender is Button button) || button.DataContext == null)
        return;
      FaceSelectionView.LastSetPhoto = button.DataContext.ToString();
      if (!((Page) this).NavigationService.CanGoBack)
        return;
      ((Page) this).NavigationService.GoBack();
    }

   
  }
}

