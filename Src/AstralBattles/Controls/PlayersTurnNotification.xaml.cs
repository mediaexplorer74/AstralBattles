using AstralBattles.Localizations;
using AstralBattles.Core.Infrastructure;
using Windows.ApplicationModel;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;



namespace AstralBattles.Controls
{
public partial class PlayersTurnNotification : UserControl
  {
    public static readonly DependencyProperty PlayersNameProperty = DependencyProperty.Register(nameof (PlayersName), typeof (string), typeof (PlayersTurnNotification), new PropertyMetadata((object) string.Empty, new PropertyChangedCallback(PlayersTurnNotification.PlayersNameChangedStatic)));
    public static readonly DependencyProperty WaitingNextPlayersTurnProperty = DependencyProperty.Register(nameof (WaitingNextPlayersTurn), typeof (bool), typeof (PlayersTurnNotification), new PropertyMetadata((object) false, new PropertyChangedCallback(PlayersTurnNotification.WaitingNextPlayersTurnPropertyChangedStatic)));
    public static readonly DependencyProperty PhotoProperty = DependencyProperty.Register(nameof (Photo), typeof (string), typeof (PlayersTurnNotification), new PropertyMetadata((object) string.Empty));
    public static readonly DependencyProperty MessageBodyProperty = DependencyProperty.Register(nameof (MessageBody), typeof (string), typeof (PlayersTurnNotification), new PropertyMetadata((object) string.Empty));
    private bool isHiding;
    

    public PlayersTurnNotification()
    {
      this.InitializeComponent();
      if (DesignMode.DesignModeEnabled)
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
      if (DesignMode.DesignModeEnabled || !this.WaitingNextPlayersTurn || this.Visibility == Visibility.Visible)
        return;
      this.Visibility = Visibility.Visible;
    }

    protected override void OnTapped(TappedRoutedEventArgs e)
    {
      if (this.isHiding || this.Visibility == Visibility.Collapsed)
        return;
      this.isHiding = true;
      this.Visibility = Visibility.Collapsed;
      this.PanelHiding((object) this, EventArgs.Empty);
      this.isHiding = false;
      base.OnTapped(e);
    }

    private void HidingAnimationCompleted(object sender, EventArgs e)
    {
    }

    private void BorderTap(object sender, TappedRoutedEventArgs e)
    {
    }
   
  }
}

