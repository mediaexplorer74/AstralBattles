using AstralBattles.Controls;
using AstralBattles.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Input;


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
      this.ViewModel.OnNavigatedTo(e, null);
      if (e.NavigationMode != NavigationMode.Back)
      {
        this.playerPanel.Children.Clear();
        this.libraryPanel.Children.Clear();
        for (int index = 0; index < 6; ++index)
        {
          DeckFieldBorder deckFieldBorder = new DeckFieldBorder();
          deckFieldBorder.Style = ((FrameworkElement) this).Resources[(object) "cardBorderStyle"] as Style;
          deckFieldBorder.SetBinding(DeckFieldBorder.DeckCardProperty, new Binding()
          {
            Path = new PropertyPath("SelectedElement.PlayerFields[" + (object) index + "]"),
            Source = ((FrameworkElement) this).DataContext
          });
          this.playerPanel.Children.Add((UIElement) deckFieldBorder);
          this.playersBorders.Add(deckFieldBorder);
        }
        for (int index = 0; index < 12; ++index)
        {
          DeckFieldBorder deckFieldBorder = new DeckFieldBorder();
          deckFieldBorder.Style = ((FrameworkElement) this).Resources[(object) "cardBorderStyle"] as Style;
          deckFieldBorder.SetBinding(DeckFieldBorder.DeckCardProperty, new Binding()
          {
            Path = new PropertyPath("SelectedElement.Library[" + (object) index + "]"),
            Source = ((FrameworkElement) this).DataContext
          });
          this.libraryPanel.Children.Add((UIElement) deckFieldBorder);
          this.librariesBorders.Add(deckFieldBorder);
        }
      }
      base.OnNavigatedTo(e);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
      this.ViewModel.OnNavigatedFrom(e);
      base.OnNavigatedFrom(e);
    }

    private DeckConfiguratorViewModel ViewModel
    {
      get => ((FrameworkElement) this).DataContext as DeckConfiguratorViewModel;
    }

    private void GestureListener_Flick(object sender, ManipulationDeltaRoutedEventArgs e)
    {
      if (this.ViewModel == null) // Orientation.Vertical is not directly available in ManipulationDeltaRoutedEventArgs, will need to check delta.Translation.Y
        return;
      this.ViewModel.SetNextOrPreviousElement(e.Delta.Translation.Y < 0.0);
    }
  }
}

