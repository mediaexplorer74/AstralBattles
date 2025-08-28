using System;
using System.Threading;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Microsoft.Xaml.Interactivity;


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
    private PointerEventHandler DragBegunEv;
    private PointerEventHandler DraggingEv;
    private PointerEventHandler DragFinishedEv;

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
        // Use ActualWidth/ActualHeight to avoid reflection on ExtendedVisualStateManager (not available in UWP)
        return new Rect(new Point(0.0, 0.0), new Size(this.AssociatedObject.ActualWidth, this.AssociatedObject.ActualHeight));
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

    public event PointerEventHandler DragBegun
    {
      add
      {
        PointerEventHandler pointerEventHandler = this.DragBegunEv;
        PointerEventHandler comparand;
        do
        {
          comparand = pointerEventHandler;
          pointerEventHandler = Interlocked.CompareExchange<PointerEventHandler>(ref this.DragBegunEv, comparand + value, comparand);
        }
        while (pointerEventHandler != comparand);
      }
      remove
      {
        PointerEventHandler pointerEventHandler = this.DragBegunEv;
        PointerEventHandler comparand;
        do
        {
          comparand = pointerEventHandler;
          pointerEventHandler = Interlocked.CompareExchange<PointerEventHandler>(ref this.DragBegunEv, comparand - value, comparand);
        }
        while (pointerEventHandler != comparand);
      }
    }

    public event PointerEventHandler Dragging
    {
      add
      {
        PointerEventHandler pointerEventHandler = this.DraggingEv;
        PointerEventHandler comparand;
        do
        {
          comparand = pointerEventHandler;
          pointerEventHandler = Interlocked.CompareExchange<PointerEventHandler>(ref this.DraggingEv, comparand + value, comparand);
        }
        while (pointerEventHandler != comparand);
      }
      remove
      {
        PointerEventHandler pointerEventHandler = this.DraggingEv;
        PointerEventHandler comparand;
        do
        {
          comparand = pointerEventHandler;
          pointerEventHandler = Interlocked.CompareExchange<PointerEventHandler>(ref this.DraggingEv, comparand - value, comparand);
        }
        while (pointerEventHandler != comparand);
      }
    }

    public event PointerEventHandler DragFinished
    {
      add
      {
        PointerEventHandler pointerEventHandler = this.DragFinishedEv;
        PointerEventHandler comparand;
        do
        {
          comparand = pointerEventHandler;
          pointerEventHandler = Interlocked.CompareExchange<PointerEventHandler>(ref this.DragFinishedEv, comparand + value, comparand);
        }
        while (pointerEventHandler != comparand);
      }
      remove
      {
        PointerEventHandler pointerEventHandler = this.DragFinishedEv;
        PointerEventHandler comparand;
        do
        {
          comparand = pointerEventHandler;
          pointerEventHandler = Interlocked.CompareExchange<PointerEventHandler>(ref this.DragFinishedEv, comparand - value, comparand);
        }
        while (pointerEventHandler != comparand);
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
      TranslateTransform translateTransform = null;
      
      if (renderTransform is TranslateTransform directTransform)
      {
        translateTransform = directTransform;
      }
      else
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
      
      // Ensure translateTransform is not null before using it
      if (translateTransform != null)
      {
        translateTransform.X += x;
        translateTransform.Y += y;
      }
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
        foreach (var child in transformGroup1.Children)
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

    internal void StartDrag(Point positionInElementCoordinates, PointerRoutedEventArgs e)
    {
      this.relativePosition = positionInElementCoordinates;
      
      // TODO: For UWP MVP build, handle pointer capture properly
      if (this.AssociatedObject is UIElement uiElement)
      {
        uiElement.CapturePointer(e.Pointer);
        this.AssociatedObject.PointerMoved += new PointerEventHandler(this.OnPointerMoved);
        // LostPointerCapture will be handled automatically by UWP when pointer is released
        this.AssociatedObject.PointerReleased += new PointerEventHandler(this.OnPointerReleased);
      }
    }

    internal void HandleDrag(Point newPositionInElementCoordinates)
    {
      Point point = MouseDragElementBehaviorEx.TransformAsVector(this.AssociatedObject.TransformToVisual(this.RootElement), newPositionInElementCoordinates.X - this.relativePosition.X, newPositionInElementCoordinates.Y - this.relativePosition.Y);
      this.settingPosition = true;
      this.ApplyTranslationTransform(point.X, point.Y);
      this.UpdatePosition();
      this.settingPosition = false;
    }

    internal void EndDrag()
    {
      this.AssociatedObject.PointerMoved -= new PointerEventHandler(this.OnPointerMoved);
      // TODO: For UWP MVP build, handle pointer capture properly
      // LostPointerCapture events are handled in StartDrag, no explicit removal needed
      this.AssociatedObject.PointerReleased -= new PointerEventHandler(this.OnPointerReleased);
    }

    private void OnPointerPressed(object sender, PointerRoutedEventArgs e)
    {
      this.StartDrag(e.GetCurrentPoint(this.AssociatedObject).Position, e);
      if (this.DragBegunEv == null)
        return;
      this.DragBegunEv((object) this, e);
    }

    private void OnLostPointerCapture(object sender, PointerRoutedEventArgs e)
    {
      this.EndDrag();
      if (this.DragFinishedEv == null)
        return;
      this.DragFinishedEv((object) this, e);
    }

    private void OnPointerReleased(object sender, PointerRoutedEventArgs e)
    {
      this.AssociatedObject.ReleasePointerCapture(e.Pointer);
    }

    private void OnPointerMoved(object sender, PointerRoutedEventArgs e)
    {
      this.HandleDrag(e.GetCurrentPoint(this.AssociatedObject).Position);
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
      Point point1 = transform.TransformPoint(new Point(0.0, 0.0));
      Point point2 = transform.TransformPoint(new Point(x, y));
      return new Point(point2.X - point1.X, point2.Y - point1.Y);
    }

    private static Point GetTransformOffset(GeneralTransform transform)
    {
      return transform.TransformPoint(new Point(0.0, 0.0));
    }

    protected override void OnAttached()
    {
      this.AssociatedObject.PointerPressed += new PointerEventHandler(this.OnPointerPressed);
    }

    protected override void OnDetaching()
    {
      this.AssociatedObject.PointerPressed -= new PointerEventHandler(this.OnPointerPressed);
    }
  }
}

