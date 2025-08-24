// Decompiled with JetBrains decompiler
// Type: AstralBattles.Controls.DeckFieldControl
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Model;
using GalaSoft.MvvmLight;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

#nullable disable
namespace AstralBattles.Controls
{
public partial class DeckFieldControl : UserControl
  {
    internal UserControl control;
    internal Grid LayoutRoot;
    internal Border cardInBookBorder;
    private bool _contentLoaded;
    private bool isDragging;
    public static readonly DependencyProperty CardProperty = DependencyProperty.Register(nameof (Card), typeof (Card), typeof (DeckFieldControl), new PropertyMetadata((PropertyChangedCallback) null));

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/AstralBattles;component/Controls/DeckField.xaml", UriKind.Relative));
      this.control = (UserControl) this.FindName("control");
      this.LayoutRoot = (Grid) this.FindName("LayoutRoot");
      this.cardInBookBorder = (Border) this.FindName("cardInBookBorder");
    }

    public DeckFieldControl()
    {
      this.InitializeComponent();
      if (!ViewModelBase.IsInDesignModeStatic)
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

    private void DeckField_MouseMove(object sender, MouseEventArgs e)
    {
      if (this == null || !this.isDragging)
        return;
      Point point = e.GetPosition((UIElement) null);
      point = new Point(point.Y, 480.0 - point.X);
      if (!(this.RenderTransform is TranslateTransform translateTransform))
      {
        translateTransform = new TranslateTransform();
        this.RenderTransform = (Transform) translateTransform;
      }
      translateTransform.X = point.X - this.Width / 2.0;
      translateTransform.Y = point.Y - this.Height / 2.0;
    }

    private void DeckField_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      this.isDragging = false;
      this.ReleaseMouseCapture();
    }

    private void DeckField_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      this.isDragging = true;
      this.CaptureMouse();
    }

    public Card Card
    {
      get => (Card) this.GetValue(DeckFieldControl.CardProperty);
      set => this.SetValue(DeckFieldControl.CardProperty, (object) value);
    }

    private void MouseDragElementBehaviorDragBegun(object sender, MouseEventArgs e)
    {
      this.DragBegun((object) this, e);
    }

    private void MouseDragElementBehaviorDragFinished(object sender, MouseEventArgs e)
    {
      this.DragFinished(sender, e);
    }

    private void MouseDragElementBehaviorDragging(object sender, MouseEventArgs e)
    {
      this.Dragging(sender, e);
    }

    public event EventHandler<MouseEventArgs> DragBegun = delegate { };

    public event EventHandler<MouseEventArgs> DragFinished = delegate { };

    public event EventHandler<MouseEventArgs> Dragging = delegate { };
  }
}

