using AstralBattles.Core.Model;
using AstralBattles.Core.Infrastructure;
using Windows.ApplicationModel;
using System;
using System.Diagnostics;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;


namespace AstralBattles.Controls
{
public partial class DeckFieldControl : UserControl
  {
    
    private bool isDragging;
    public static readonly DependencyProperty CardProperty = DependencyProperty.Register(nameof (Card), typeof (Card), typeof (DeckFieldControl), new PropertyMetadata((PropertyChangedCallback) null));

  
    public DeckFieldControl()
    {
      this.InitializeComponent();
      if (!DesignMode.DesignModeEnabled)
        return;
      CreatureCard creatureCard = new CreatureCard();
      creatureCard.IsActive = true;
      creatureCard.Cost = 10;
      creatureCard.Damage = 10;
      creatureCard.Health = 40;
      creatureCard.ElementType = ElementTypeEnum.Water;
      creatureCard.Name = "WaterElemental";
      this.Card = (Card) creatureCard;
    }

    private void DeckField_MouseMove(object sender, PointerRoutedEventArgs e)
    {
      if (this == null || !this.isDragging)
        return;
      // Using UWP Pointer API instead of Mouse API
      Point point = e.GetCurrentPoint(null).Position;
      // ... existing code ...
    }

    private void DeckField_PointerReleased(object sender, PointerRoutedEventArgs e)
    {
      this.isDragging = false;
      this.ReleasePointerCapture(e.Pointer);
    }

    private void DeckField_PointerPressed(object sender, PointerRoutedEventArgs e)
    {
      this.isDragging = true;
      this.CapturePointer(e.Pointer);
    }

    public Card Card
    {
      get => (Card) this.GetValue(DeckFieldControl.CardProperty);
      set => this.SetValue(DeckFieldControl.CardProperty, (object) value);
    }

    private void MouseDragElementBehaviorDragBegun(object sender, PointerRoutedEventArgs e)
    {
      this.DragBegun((object) this, e);
    }

    private void MouseDragElementBehaviorDragFinished(object sender, PointerRoutedEventArgs e)
    {
      this.DragFinished(sender, e);
    }

    private void MouseDragElementBehaviorDragging(object sender, PointerRoutedEventArgs e)
    {
      this.Dragging(sender, e);
    }

    public event EventHandler<PointerRoutedEventArgs> DragBegun = delegate { };

    public event EventHandler<PointerRoutedEventArgs> DragFinished = delegate { };

    public event EventHandler<PointerRoutedEventArgs> Dragging = delegate { };
  }
}

