// Decompiled with JetBrains decompiler
// Type: AstralBattles.Views.DeckConfigurator
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Controls;
using AstralBattles.ViewModels;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;

#nullable disable
namespace AstralBattles.Views
{
public partial class DeckConfigurator : PhoneApplicationPage
  {
    internal Grid LayoutRoot;
    internal WrapPanel playerPanel;
    internal WrapPanel libraryPanel;
    private bool _contentLoaded;
    private readonly List<DeckFieldBorder> playersBorders = new List<DeckFieldBorder>();
    private readonly List<DeckFieldBorder> librariesBorders = new List<DeckFieldBorder>();

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/AstralBattles;component/Views/DeckConfigurator.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.playerPanel = (WrapPanel) ((FrameworkElement) this).FindName("playerPanel");
      this.libraryPanel = (WrapPanel) ((FrameworkElement) this).FindName("libraryPanel");
    }

    public DeckConfigurator()
    {
      this.InitializeComponent();
      this.InitializeFields();
    }

    private void InitializeFields()
    {
      ((FrameworkElement) this).DataContext = (object) new DeckConfiguratorViewModel();
    }

    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      this.ViewModel.OnNavigatedTo(e, ((Page) this).NavigationService);
      if (e.NavigationMode != NavigationMode.Back)
      {
        this.playerPanel.Children.Clear();
        this.libraryPanel.Children.Clear();
        for (int index = 0; index < 6; ++index)
        {
          DeckFieldBorder deckFieldBorder = new DeckFieldBorder();
          deckFieldBorder.Style = ((FrameworkElement) this).Resources[(object) "cardBorderStyle"] as Style;
          deckFieldBorder.SetBinding(DeckFieldBorder.DeckCardProperty, new Binding("SelectedElement.PlayerFields[" + (object) index + "]")
          {
            Source = ((FrameworkElement) this).DataContext
          });
          this.playerPanel.Children.Add((UIElement) deckFieldBorder);
          this.playersBorders.Add(deckFieldBorder);
        }
        for (int index = 0; index < 12; ++index)
        {
          DeckFieldBorder deckFieldBorder = new DeckFieldBorder();
          deckFieldBorder.Style = ((FrameworkElement) this).Resources[(object) "cardBorderStyle"] as Style;
          deckFieldBorder.SetBinding(DeckFieldBorder.DeckCardProperty, new Binding("SelectedElement.Library[" + (object) index + "]")
          {
            Source = ((FrameworkElement) this).DataContext
          });
          this.libraryPanel.Children.Add((UIElement) deckFieldBorder);
          this.librariesBorders.Add(deckFieldBorder);
        }
      }
      ((Page) this).OnNavigatedTo(e);
    }

    protected virtual void OnNavigatedFrom(NavigationEventArgs e)
    {
      this.ViewModel.OnNavigatedFrom(e);
      ((Page) this).OnNavigatedFrom(e);
    }

    private DeckConfiguratorViewModel ViewModel
    {
      get => ((FrameworkElement) this).DataContext as DeckConfiguratorViewModel;
    }

    private void GestureListener_Flick(object sender, FlickGestureEventArgs e)
    {
      if (e.Direction != Orientation.Vertical || this.ViewModel == null)
        return;
      this.ViewModel.SetNextOrPreviousElement(e.VerticalVelocity < 0.0);
    }
  }
}

