// Decompiled with JetBrains decompiler
// Type: AstralBattles.Controls.PlayersTurnNotification
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Localizations;
using GalaSoft.MvvmLight;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

#nullable disable
namespace AstralBattles.Controls
{
public partial class PlayersTurnNotification : UserControl
  {
    public static readonly DependencyProperty PlayersNameProperty = DependencyProperty.Register(nameof (PlayersName), typeof (string), typeof (PlayersTurnNotification), new PropertyMetadata((object) string.Empty, new PropertyChangedCallback(PlayersTurnNotification.PlayersNameChangedStatic)));
    public static readonly DependencyProperty WaitingNextPlayersTurnProperty = DependencyProperty.Register(nameof (WaitingNextPlayersTurn), typeof (bool), typeof (PlayersTurnNotification), new PropertyMetadata((object) false, new PropertyChangedCallback(PlayersTurnNotification.WaitingNextPlayersTurnPropertyChangedStatic)));
    public static readonly DependencyProperty PhotoProperty = DependencyProperty.Register(nameof (Photo), typeof (string), typeof (PlayersTurnNotification), new PropertyMetadata((object) string.Empty));
    public static readonly DependencyProperty MessageBodyProperty = DependencyProperty.Register(nameof (MessageBody), typeof (string), typeof (PlayersTurnNotification), new PropertyMetadata((object) string.Empty));
    private bool isHiding;
    internal UserControl thisControl;
    internal Grid LayoutRoot;
    internal Border border;
    private bool _contentLoaded;

    public PlayersTurnNotification()
    {
      this.InitializeComponent();
      if (ViewModelBase.IsInDesignModeStatic)
        return;
      this.Visibility = Visibility.Collapsed;
    }

    public event EventHandler PanelHiding = delegate { };

    public string MessageBody
    {
      get => (string) this.GetValue(PlayersTurnNotification.MessageBodyProperty);
      set => this.SetValue(PlayersTurnNotification.MessageBodyProperty, (object) value);
    }

    public string Photo
    {
      get => (string) this.GetValue(PlayersTurnNotification.PhotoProperty);
      set => this.SetValue(PlayersTurnNotification.PhotoProperty, (object) value);
    }

    public bool WaitingNextPlayersTurn
    {
      get => (bool) this.GetValue(PlayersTurnNotification.WaitingNextPlayersTurnProperty);
      set => this.SetValue(PlayersTurnNotification.WaitingNextPlayersTurnProperty, (object) value);
    }

    public string PlayersName
    {
      get => (string) this.GetValue(PlayersTurnNotification.PlayersNameProperty);
      set => this.SetValue(PlayersTurnNotification.PlayersNameProperty, (object) value);
    }

    private static void PlayersNameChangedStatic(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is PlayersTurnNotification))
        return;
      ((PlayersTurnNotification) d).PlayersNameChanged();
    }

    private void PlayersNameChanged()
    {
      this.MessageBody = string.Format(CommonResources.NextTurnMessage, (object) this.PlayersName);
    }

    private static void WaitingNextPlayersTurnPropertyChangedStatic(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is PlayersTurnNotification))
        return;
      ((PlayersTurnNotification) d).WaitingNextPlayersTurnPropertyChanged();
    }

    public void WaitingNextPlayersTurnPropertyChanged()
    {
      if (ViewModelBase.IsInDesignModeStatic || !this.WaitingNextPlayersTurn || this.Visibility == Visibility.Visible)
        return;
      this.Visibility = Visibility.Visible;
    }

    protected override void OnTap(GestureEventArgs e)
    {
      if (this.isHiding || this.Visibility == Visibility.Collapsed)
        return;
      this.isHiding = true;
      this.Visibility = Visibility.Collapsed;
      this.PanelHiding((object) this, EventArgs.Empty);
      this.isHiding = false;
      base.OnTap(e);
    }

    private void HidingAnimationCompleted(object sender, EventArgs e)
    {
    }

    private void BorderTap(object sender, GestureEventArgs e)
    {
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/AstralBattles;component/Controls/PlayersTurnNotification.xaml", UriKind.Relative));
      this.thisControl = (UserControl) this.FindName("thisControl");
      this.LayoutRoot = (Grid) this.FindName("LayoutRoot");
      this.border = (Border) this.FindName("border");
    }
  }
}

