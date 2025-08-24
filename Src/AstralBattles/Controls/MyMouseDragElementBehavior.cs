// Decompiled with JetBrains decompiler
// Type: AstralBattles.Controls.MyMouseDragElementBehavior
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

#nullable disable
namespace AstralBattles.Controls
{
public partial class MyMouseDragElementBehavior : Behavior<FrameworkElement>
  {
    private Point relativePosition;
    public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.Register(nameof (IsEnabled), typeof (bool), typeof (MyMouseDragElementBehavior), new PropertyMetadata((object) true));
    private static int zIndex = 0;

    public event MouseEventHandler DragBegun;

    public event MouseEventHandler DragFinished;

    public event MouseEventHandler Dragging;

    public bool IsEnabled
    {
      get => (bool) this.GetValue(MyMouseDragElementBehavior.IsEnabledProperty);
      set => this.SetValue(MyMouseDragElementBehavior.IsEnabledProperty, (object) value);
    }

    protected override void OnAttached()
    {
      this.AssociatedObject.AddHandler(UIElement.MouseLeftButtonDownEvent, (Delegate) new MouseButtonEventHandler(this.OnMouseLeftButtonDown), false);
      base.OnAttached();
    }

    protected override void OnDetaching()
    {
      this.AssociatedObject.RemoveHandler(UIElement.MouseLeftButtonDownEvent, (Delegate) new MouseButtonEventHandler(this.OnMouseLeftButtonDown));
      base.OnDetaching();
    }

    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      if (!this.IsEnabled)
        return;
      ++MyMouseDragElementBehavior.zIndex;
      Canvas.SetZIndex((UIElement) this.AssociatedObject, MyMouseDragElementBehavior.zIndex);
      this.StartDrag(e.GetPosition((UIElement) this.AssociatedObject));
      if (this.DragBegun == null)
        return;
      this.DragBegun((object) this, (MouseEventArgs) e);
    }

    private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      this.AssociatedObject.ReleaseMouseCapture();
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
      this.HandleDrag(e.GetPosition((UIElement) this.AssociatedObject));
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
      this.AssociatedObject.CaptureMouse();
      this.AssociatedObject.MouseMove += new MouseEventHandler(this.OnMouseMove);
      this.AssociatedObject.LostMouseCapture += new MouseEventHandler(this.OnLostMouseCapture);
      this.AssociatedObject.AddHandler(UIElement.MouseLeftButtonUpEvent, (Delegate) new MouseButtonEventHandler(this.OnMouseLeftButtonUp), false);
    }

    internal void EndDrag()
    {
      this.AssociatedObject.MouseMove -= new MouseEventHandler(this.OnMouseMove);
      this.AssociatedObject.LostMouseCapture -= new MouseEventHandler(this.OnLostMouseCapture);
      this.AssociatedObject.RemoveHandler(UIElement.MouseLeftButtonUpEvent, (Delegate) new MouseButtonEventHandler(this.OnMouseLeftButtonUp));
    }

    private void OnLostMouseCapture(object sender, MouseEventArgs e)
    {
      this.EndDrag();
      if (this.DragFinished == null)
        return;
      this.DragFinished((object) this, e);
    }
  }
}

