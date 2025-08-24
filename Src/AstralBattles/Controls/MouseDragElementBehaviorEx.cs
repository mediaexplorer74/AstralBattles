// Decompiled with JetBrains decompiler
// Type: AstralBattles.Controls.MouseDragElementBehaviorEx
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using Microsoft.Expression.Interactivity.Core;
using System;
using System.Reflection;
using System.Threading;
using System.Windows;
using Windows.UI.Xaml;
using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Input;

#nullable disable
namespace AstralBattles.Controls
{
public partial class MouseDragElementBehaviorEx : Behavior<FrameworkElement>
  {
    public static readonly DependencyProperty XProperty = DependencyProperty.Register(nameof (X), typeof (double), typeof (MouseDragElementBehaviorEx), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(MouseDragElementBehaviorEx.OnXChanged)));
    public static readonly DependencyProperty YProperty = DependencyProperty.Register(nameof (Y), typeof (double), typeof (MouseDragElementBehaviorEx), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(MouseDragElementBehaviorEx.OnYChanged)));
    public static readonly DependencyProperty ConstrainToParentBoundsProperty = DependencyProperty.Register(nameof (ConstrainToParentBounds), typeof (bool), typeof (MouseDragElementBehaviorEx), new PropertyMetadata((object) false, new PropertyChangedCallback(MouseDragElementBehaviorEx.OnConstrainToParentBoundsChanged)));
    private bool settingPosition;
    private Point relativePosition;
    private Transform cachedRenderTransform;
    private MouseEventHandler DragBegunEv;
    private MouseEventHandler DraggingEv;
    private MouseEventHandler DragFinishedEv;

    public double X
    {
      get => (double) this.GetValue(MouseDragElementBehaviorEx.XProperty);
      set => this.SetValue(MouseDragElementBehaviorEx.XProperty, (object) value);
    }

    public double Y
    {
      get => (double) this.GetValue(MouseDragElementBehaviorEx.YProperty);
      set => this.SetValue(MouseDragElementBehaviorEx.YProperty, (object) value);
    }

    public bool ConstrainToParentBounds
    {
      get => (bool) this.GetValue(MouseDragElementBehaviorEx.ConstrainToParentBoundsProperty);
      set
      {
        this.SetValue(MouseDragElementBehaviorEx.ConstrainToParentBoundsProperty, (object) value);
      }
    }

    private Point ActualPosition
    {
      get
      {
        Point transformOffset = MouseDragElementBehaviorEx.GetTransformOffset(this.AssociatedObject.TransformToVisual(this.RootElement));
        return new Point(transformOffset.X, transformOffset.Y);
      }
    }

    private Rect ElementBounds
    {
      get
      {
        Rect rect = (Rect) typeof (ExtendedVisualStateManager).GetMethod("GetLayoutRect", BindingFlags.Static | BindingFlags.NonPublic).Invoke((object) null, (object[]) new FrameworkElement[1]
        {
          this.AssociatedObject
        });
        return new Rect(new Point(0.0, 0.0), new Size(rect.Width, rect.Height));
      }
    }

    private FrameworkElement ParentElement => this.AssociatedObject.Parent as FrameworkElement;

    private UIElement RootElement
    {
      get
      {
        DependencyObject reference = (DependencyObject) this.AssociatedObject;
        for (DependencyObject dependencyObject = reference; dependencyObject != null; dependencyObject = VisualTreeHelper.GetParent(reference))
          reference = dependencyObject;
        return reference as UIElement;
      }
    }

    private Transform RenderTransform
    {
      get
      {
        if (this.cachedRenderTransform == null || !object.ReferenceEquals((object) this.cachedRenderTransform, (object) this.AssociatedObject.RenderTransform))
          this.RenderTransform = MouseDragElementBehaviorEx.CloneTransform(this.AssociatedObject.RenderTransform);
        return this.cachedRenderTransform;
      }
      set
      {
        if (this.cachedRenderTransform == value)
          return;
        this.cachedRenderTransform = value;
        this.AssociatedObject.RenderTransform = value;
      }
    }

    public event MouseEventHandler DragBegun
    {
      add
      {
        MouseEventHandler mouseEventHandler = this.DragBegunEv;
        MouseEventHandler comparand;
        do
        {
          comparand = mouseEventHandler;
          mouseEventHandler = Interlocked.CompareExchange<MouseEventHandler>(ref this.DragBegunEv, comparand + value, comparand);
        }
        while (mouseEventHandler != comparand);
      }
      remove
      {
        MouseEventHandler mouseEventHandler = this.DragBegunEv;
        MouseEventHandler comparand;
        do
        {
          comparand = mouseEventHandler;
          mouseEventHandler = Interlocked.CompareExchange<MouseEventHandler>(ref this.DragBegunEv, comparand - value, comparand);
        }
        while (mouseEventHandler != comparand);
      }
    }

    public event MouseEventHandler Dragging
    {
      add
      {
        MouseEventHandler mouseEventHandler = this.DraggingEv;
        MouseEventHandler comparand;
        do
        {
          comparand = mouseEventHandler;
          mouseEventHandler = Interlocked.CompareExchange<MouseEventHandler>(ref this.DraggingEv, comparand + value, comparand);
        }
        while (mouseEventHandler != comparand);
      }
      remove
      {
        MouseEventHandler mouseEventHandler = this.DraggingEv;
        MouseEventHandler comparand;
        do
        {
          comparand = mouseEventHandler;
          mouseEventHandler = Interlocked.CompareExchange<MouseEventHandler>(ref this.DraggingEv, comparand - value, comparand);
        }
        while (mouseEventHandler != comparand);
      }
    }

    public event MouseEventHandler DragFinished
    {
      add
      {
        MouseEventHandler mouseEventHandler = this.DragFinishedEv;
        MouseEventHandler comparand;
        do
        {
          comparand = mouseEventHandler;
          mouseEventHandler = Interlocked.CompareExchange<MouseEventHandler>(ref this.DragFinishedEv, comparand + value, comparand);
        }
        while (mouseEventHandler != comparand);
      }
      remove
      {
        MouseEventHandler mouseEventHandler = this.DragFinishedEv;
        MouseEventHandler comparand;
        do
        {
          comparand = mouseEventHandler;
          mouseEventHandler = Interlocked.CompareExchange<MouseEventHandler>(ref this.DragFinishedEv, comparand - value, comparand);
        }
        while (mouseEventHandler != comparand);
      }
    }

    private static void OnXChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
      MouseDragElementBehaviorEx elementBehaviorEx = (MouseDragElementBehaviorEx) sender;
      elementBehaviorEx.UpdatePosition(new Point((double) args.NewValue, elementBehaviorEx.Y));
    }

    private static void OnYChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
      MouseDragElementBehaviorEx elementBehaviorEx = (MouseDragElementBehaviorEx) sender;
      elementBehaviorEx.UpdatePosition(new Point(elementBehaviorEx.X, (double) args.NewValue));
    }

    private static void OnConstrainToParentBoundsChanged(
      object sender,
      DependencyPropertyChangedEventArgs args)
    {
      MouseDragElementBehaviorEx elementBehaviorEx = (MouseDragElementBehaviorEx) sender;
      elementBehaviorEx.UpdatePosition(new Point(elementBehaviorEx.X, elementBehaviorEx.Y));
    }

    private void UpdatePosition(Point point)
    {
      if (this.settingPosition || this.AssociatedObject == null)
        return;
      Point transformOffset = MouseDragElementBehaviorEx.GetTransformOffset(this.AssociatedObject.TransformToVisual(this.RootElement));
      this.ApplyTranslation(double.IsNaN(point.X) ? 0.0 : point.X - transformOffset.X, double.IsNaN(point.Y) ? 0.0 : point.Y - transformOffset.Y);
    }

    private void ApplyTranslation(double x, double y)
    {
      if (this.ParentElement == null)
        return;
      Point point = MouseDragElementBehaviorEx.TransformAsVector(this.RootElement.TransformToVisual((UIElement) this.ParentElement), x, y);
      x = point.X;
      y = point.Y;
      if (this.ConstrainToParentBounds)
      {
        FrameworkElement parentElement = this.ParentElement;
        Rect rect1 = new Rect(0.0, 0.0, parentElement.ActualWidth, parentElement.ActualHeight);
        Rect rect2 = this.AssociatedObject.TransformToVisual((UIElement) parentElement).TransformBounds(this.ElementBounds);
        rect2.X += x;
        rect2.Y += y;
        if (!MouseDragElementBehaviorEx.RectContainsRect(rect1, rect2))
        {
          if (rect2.X < rect1.Left)
          {
            double num = rect2.X - rect1.Left;
            x -= num;
          }
          else if (rect2.Right > rect1.Right)
          {
            double num = rect2.Right - rect1.Right;
            x -= num;
          }
          if (rect2.Y < rect1.Top)
          {
            double num = rect2.Y - rect1.Top;
            y -= num;
          }
          else if (rect2.Bottom > rect1.Bottom)
          {
            double num = rect2.Bottom - rect1.Bottom;
            y -= num;
          }
        }
      }
      this.ApplyTranslationTransform(x, y);
    }

    internal void ApplyTranslationTransform(double x, double y)
    {
      Transform renderTransform = this.RenderTransform;
      if (!(renderTransform is TranslateTransform translateTransform))
      {
        TransformGroup transformGroup1 = renderTransform as TransformGroup;
        MatrixTransform matrixTransform = renderTransform as MatrixTransform;
        if (renderTransform is CompositeTransform compositeTransform)
        {
          compositeTransform.TranslateX += x;
          compositeTransform.TranslateY += y;
          return;
        }
        if (transformGroup1 != null)
        {
          if (transformGroup1.Children.Count > 0)
            translateTransform = transformGroup1.Children[transformGroup1.Children.Count - 1] as TranslateTransform;
          if (translateTransform == null)
          {
            translateTransform = new TranslateTransform();
            transformGroup1.Children.Add((Transform) translateTransform);
          }
        }
        else
        {
          if (matrixTransform != null)
          {
            Matrix matrix = matrixTransform.Matrix;
            matrix.OffsetX += x;
            matrix.OffsetY += y;
            this.RenderTransform = (Transform) new MatrixTransform()
            {
              Matrix = matrix
            };
            return;
          }
          TransformGroup transformGroup2 = new TransformGroup();
          translateTransform = new TranslateTransform();
          if (renderTransform != null)
            transformGroup2.Children.Add(renderTransform);
          transformGroup2.Children.Add((Transform) translateTransform);
          this.RenderTransform = (Transform) transformGroup2;
        }
      }
      translateTransform.X += x;
      translateTransform.Y += y;
    }

    internal static Transform CloneTransform(Transform transform)
    {
      if (transform == null)
        return (Transform) null;
      transform.GetType();
      if (transform is ScaleTransform scaleTransform)
        return (Transform) new ScaleTransform()
        {
          CenterX = scaleTransform.CenterX,
          CenterY = scaleTransform.CenterY,
          ScaleX = scaleTransform.ScaleX,
          ScaleY = scaleTransform.ScaleY
        };
      if (transform is RotateTransform rotateTransform)
        return (Transform) new RotateTransform()
        {
          Angle = rotateTransform.Angle,
          CenterX = rotateTransform.CenterX,
          CenterY = rotateTransform.CenterY
        };
      if (transform is SkewTransform skewTransform)
        return (Transform) new SkewTransform()
        {
          AngleX = skewTransform.AngleX,
          AngleY = skewTransform.AngleY,
          CenterX = skewTransform.CenterX,
          CenterY = skewTransform.CenterY
        };
      if (transform is TranslateTransform translateTransform)
        return (Transform) new TranslateTransform()
        {
          X = translateTransform.X,
          Y = translateTransform.Y
        };
      if (transform is MatrixTransform matrixTransform)
        return (Transform) new MatrixTransform()
        {
          Matrix = matrixTransform.Matrix
        };
      if (transform is TransformGroup transformGroup1)
      {
        TransformGroup transformGroup = new TransformGroup();
        foreach (Transform child in (PresentationFrameworkCollection<Transform>) transformGroup1.Children)
          transformGroup.Children.Add(MouseDragElementBehaviorEx.CloneTransform(child));
        return (Transform) transformGroup;
      }
      if (!(transform is CompositeTransform compositeTransform))
        return (Transform) null;
      return (Transform) new CompositeTransform()
      {
        CenterX = compositeTransform.CenterX,
        CenterY = compositeTransform.CenterY,
        Rotation = compositeTransform.Rotation,
        ScaleX = compositeTransform.ScaleX,
        ScaleY = compositeTransform.ScaleY,
        SkewX = compositeTransform.SkewX,
        SkewY = compositeTransform.SkewY,
        TranslateX = compositeTransform.TranslateX,
        TranslateY = compositeTransform.TranslateY
      };
    }

    private void UpdatePosition()
    {
      Point transformOffset = MouseDragElementBehaviorEx.GetTransformOffset(this.AssociatedObject.TransformToVisual(this.RootElement));
      this.X = transformOffset.X;
      this.Y = transformOffset.Y;
    }

    internal void StartDrag(Point positionInElementCoordinates)
    {
      this.relativePosition = positionInElementCoordinates;
      this.AssociatedObject.CaptureMouse();
      this.AssociatedObject.MouseMove += new MouseEventHandler(this.OnMouseMove);
      this.AssociatedObject.LostMouseCapture += new MouseEventHandler(this.OnLostMouseCapture);
      this.AssociatedObject.AddHandler(UIElement.MouseLeftButtonUpEvent, (Delegate) new MouseButtonEventHandler(this.OnMouseLeftButtonUp), false);
    }

    internal void HandleDrag(Point newPositionInElementCoordinates)
    {
      Point point = MouseDragElementBehaviorEx.TransformAsVector(this.AssociatedObject.TransformToVisual(this.RootElement), newPositionInElementCoordinates.X - this.relativePosition.X, newPositionInElementCoordinates.Y - this.relativePosition.Y);
      this.settingPosition = true;
      this.ApplyTranslation(point.X, point.Y);
      this.UpdatePosition();
      this.settingPosition = false;
    }

    internal void EndDrag()
    {
      this.AssociatedObject.MouseMove -= new MouseEventHandler(this.OnMouseMove);
      this.AssociatedObject.LostMouseCapture -= new MouseEventHandler(this.OnLostMouseCapture);
      this.AssociatedObject.RemoveHandler(UIElement.MouseLeftButtonUpEvent, (Delegate) new MouseButtonEventHandler(this.OnMouseLeftButtonUp));
    }

    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      this.StartDrag(e.GetPosition((UIElement) this.AssociatedObject));
      if (this.DragBegunEv == null)
        return;
      this.DragBegunEv((object) this, (MouseEventArgs) e);
    }

    private void OnLostMouseCapture(object sender, MouseEventArgs e)
    {
      this.EndDrag();
      if (this.DragFinishedEv == null)
        return;
      this.DragFinishedEv((object) this, e);
    }

    private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      this.AssociatedObject.ReleaseMouseCapture();
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
      this.HandleDrag(e.GetPosition((UIElement) this.AssociatedObject));
      if (this.DraggingEv == null)
        return;
      this.DraggingEv((object) this, e);
    }

    private static bool RectContainsRect(Rect rect1, Rect rect2)
    {
      return !rect1.IsEmpty && !rect2.IsEmpty && rect1.X <= rect2.X && rect1.Y <= rect2.Y && rect1.X + rect1.Width >= rect2.X + rect2.Width && rect1.Y + rect1.Height >= rect2.Y + rect2.Height;
    }

    private static Point TransformAsVector(GeneralTransform transform, double x, double y)
    {
      Point point1 = transform.Transform(new Point(0.0, 0.0));
      Point point2 = transform.Transform(new Point(x, y));
      return new Point(point2.X - point1.X, point2.Y - point1.Y);
    }

    private static Point GetTransformOffset(GeneralTransform transform)
    {
      return transform.Transform(new Point(0.0, 0.0));
    }

    protected override void OnAttached()
    {
      this.AssociatedObject.AddHandler(UIElement.MouseLeftButtonDownEvent, (Delegate) new MouseButtonEventHandler(this.OnMouseLeftButtonDown), false);
    }

    protected override void OnDetaching()
    {
      this.AssociatedObject.RemoveHandler(UIElement.MouseLeftButtonDownEvent, (Delegate) new MouseButtonEventHandler(this.OnMouseLeftButtonDown));
    }
  }
}

