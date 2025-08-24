// Decompiled with JetBrains decompiler
// Type: AstralBattles.Controls.OpponentSummoningNotification
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Model;
using AstralBattles.ViewModels;
using GalaSoft.MvvmLight;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

#nullable disable
namespace AstralBattles.Controls
{
public partial class OpponentSummoningNotification : UserControl
  {
    internal UserControl thisControl;
    internal Grid LayoutRoot;
    internal VisualStateGroup VisualStateGroup;
    internal Storyboard risingAnimation;
    internal Storyboard hidingAnimation;
    internal VisualState Default;
    internal VisualState Rising;
    internal VisualState Hiding;
    internal Border border;
    private bool _contentLoaded;
    public static readonly DependencyProperty CardDescriptionProperty = DependencyProperty.Register("CardDescription", typeof (string), typeof (OpponentSummoningNotification), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof (Title), typeof (string), typeof (OpponentSummoningNotification), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty CardProperty = DependencyProperty.Register(nameof (Card), typeof (Card), typeof (OpponentSummoningNotification), new PropertyMetadata((object) null, new PropertyChangedCallback(OpponentSummoningNotification.CardPropertyChangedStatic)));
    private bool isHiding;

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/AstralBattles;component/Controls/OpponentSummoningNotification.xaml", UriKind.Relative));
      this.thisControl = (UserControl) this.FindName("thisControl");
      this.LayoutRoot = (Grid) this.FindName("LayoutRoot");
      this.VisualStateGroup = (VisualStateGroup) this.FindName("VisualStateGroup");
      this.risingAnimation = (Storyboard) this.FindName("risingAnimation");
      this.hidingAnimation = (Storyboard) this.FindName("hidingAnimation");
      this.Default = (VisualState) this.FindName("Default");
      this.Rising = (VisualState) this.FindName("Rising");
      this.Hiding = (VisualState) this.FindName("Hiding");
      this.border = (Border) this.FindName("border");
    }

    public OpponentSummoningNotification()
    {
      this.InitializeComponent();
      if (!ViewModelBase.IsInDesignModeStatic)
      {
        this.border.Height = 0.0;
        this.Visibility = Visibility.Collapsed;
        this.hidingAnimation.Completed += new EventHandler(this.HidingAnimationCompleted);
      }
      else
      {
        this.Card = DesignTimeDataContext.Instance.Card;
        this.Title = "Player is summoning Bla bla bla bla";
      }
    }

    public event EventHandler PanelHiding = delegate { };

    public Card Card
    {
      get => (Card) this.GetValue(OpponentSummoningNotification.CardProperty);
      set => this.SetValue(OpponentSummoningNotification.CardProperty, (object) value);
    }

    public string Title
    {
      get => (string) this.GetValue(OpponentSummoningNotification.TitleProperty);
      set => this.SetValue(OpponentSummoningNotification.TitleProperty, (object) value);
    }

    private static void CardPropertyChangedStatic(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is OpponentSummoningNotification))
        return;
      ((OpponentSummoningNotification) d).CardPropertyChanged();
    }

    public void CardPropertyChanged()
    {
      if (ViewModelBase.IsInDesignModeStatic || this.Card == null || this.Visibility == Visibility.Visible)
        return;
      this.Visibility = Visibility.Visible;
      this.risingAnimation.Begin();
    }

    protected override void OnTap(GestureEventArgs e)
    {
      if (this.isHiding || this.Visibility == Visibility.Collapsed)
        return;
      this.isHiding = true;
      this.hidingAnimation.Begin();
      base.OnTap(e);
    }

    private void HidingAnimationCompleted(object sender, EventArgs e)
    {
      this.Visibility = Visibility.Collapsed;
      this.PanelHiding((object) this, EventArgs.Empty);
      this.isHiding = false;
    }

    private void BorderTap(object sender, GestureEventArgs e)
    {
    }
  }
}

