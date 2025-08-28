
using System;
using Windows.Foundation;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;



namespace AstralBattles.Controls
{
public partial class MyMouseDragElementBehavior : Behavior<FrameworkElement>
  {
    private Point relativePosition;
    public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.Register(
        nameof (IsEnabled), typeof (bool), typeof (MyMouseDragElementBehavior), new PropertyMetadata((object) true));
    private static int zIndex = 0;

    public event PointerEventHandler DragBegun;

    public event PointerEventHandler DragFinished;

    public event PointerEventHandler Dragging;

    public bool IsEnabled
    {
      get => (bool) this.GetValue(MyMouseDragElementBehavior.IsEnabledProperty);
      set => this.SetValue(MyMouseDragElementBehavior.IsEnabledProperty, (object) value);
    }

    protected override void OnAttached()
    {
      this.AssociatedObject.PointerPressed += new PointerEventHandler(this.OnPointerPressed);
      base.OnAttached();
    }

    protected override void OnDetaching()
    {
      this.AssociatedObject.PointerPressed -= new PointerEventHandler(this.OnPointerPressed);
      base.OnDetaching();
    }

    private void OnPointerPressed(object sender, PointerRoutedEventArgs e)
    {
      if (!this.IsEnabled)
        return;
      ++MyMouseDragElementBehavior.zIndex;
      Canvas.SetZIndex((UIElement) this.AssociatedObject, MyMouseDragElementBehavior.zIndex);
      this.StartDrag(e.GetCurrentPoint(this.AssociatedObject).Position);
      if (this.DragBegun == null)
        return;
      this.DragBegun((object) this, e);
    }

    private void OnPointerReleased(object sender, PointerRoutedEventArgs e)
    {
      this.AssociatedObject.ReleasePointerCapture(e.Pointer);
    }

    private void OnPointerMoved(object sender, PointerRoutedEventArgs e)
    {
      this.HandleDrag(e.GetCurrentPoint(this.AssociatedObject).Position);
      if (this.Dragging == null)
        return;
      this.Dragging((object) this, e);
    }

    internal void HandleDrag(Point newPositionInElementCoordinates)
    {
      double num1 = newPositionInElementCoordinates.X - this.relativePosition.X;
      double num2 = newPositionInElementCoordinates.Y - this.relativePosition.Y;
      if (this.AssociatedObject == null)
        return;
      double left = Canvas.GetLeft((UIElement) this.AssociatedObject);
      double top = Canvas.GetTop((UIElement) this.AssociatedObject);
      Canvas.SetLeft((UIElement) this.AssociatedObject, left + num1);
      Canvas.SetTop((UIElement) this.AssociatedObject, top + num2);
    }

    internal void StartDrag(Point positionInElementCoordinates)
    {
      this.relativePosition = positionInElementCoordinates;
      // TODO: Fix pointer capture - commenting out for MVP build
      // this.AssociatedObject.CapturePointer(e.Pointer);
      this.AssociatedObject.PointerMoved += new PointerEventHandler(this.OnPointerMoved);
      // this.AssociatedObject.LostPointerCapture += new PointerEventHandler(this.OnLostPointerCapture);
      this.AssociatedObject.PointerReleased += new PointerEventHandler(this.OnPointerReleased);
    }

    internal void EndDrag()
    {
      this.AssociatedObject.PointerMoved -= new PointerEventHandler(this.OnPointerMoved);
      // this.AssociatedObject.LostPointerCapture -= new PointerEventHandler(this.OnLostPointerCapture);
      this.AssociatedObject.PointerReleased -= new PointerEventHandler(this.OnPointerReleased);
    }

    private void OnLostPointerCapture(object sender, PointerRoutedEventArgs e)
    {
      this.EndDrag();
      if (this.DragFinished == null)
        return;
      this.DragFinished((object) this, e);
    }
  }
}

