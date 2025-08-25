// Decompiled with JetBrains decompiler
// Type: AstralBattles.Views.DeckConfigurator
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Controls;
using AstralBattles.ViewModels;
using Windows.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using Windows.UI.Xaml.Controls;
using System.Windows.Data;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace AstralBattles.Views
{
public partial class DeckConfigurator : Page
  {
    
    private readonly List<DeckFieldBorder> playersBorders = new List<DeckFieldBorder>();
    private readonly List<DeckFieldBorder> librariesBorders = new List<DeckFieldBorder>();

    
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
      this.ViewModel.OnNavigatedTo(e, ((Page) this).Frame);
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

