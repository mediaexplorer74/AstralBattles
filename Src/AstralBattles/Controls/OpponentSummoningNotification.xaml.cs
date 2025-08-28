using AstralBattles.Core.Model;
using AstralBattles.ViewModels;
using AstralBattles.Core.Infrastructure;
using Windows.ApplicationModel;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;


namespace AstralBattles.Controls
{
public partial class OpponentSummoningNotification : UserControl
  {
    
    public static readonly DependencyProperty CardDescriptionProperty = DependencyProperty.Register("CardDescription", typeof (string), typeof (OpponentSummoningNotification), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof (Title), typeof (string), typeof (OpponentSummoningNotification), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty CardProperty = DependencyProperty.Register(nameof (Card), typeof (Card), typeof (OpponentSummoningNotification), new PropertyMetadata((object) null, new PropertyChangedCallback(OpponentSummoningNotification.CardPropertyChangedStatic)));
    private bool isHiding;

    
    public OpponentSummoningNotification()
    {
      this.InitializeComponent();
      if (!DesignMode.DesignModeEnabled)
      {
        this.border.Height = 0.0;
        this.Visibility = Visibility.Collapsed;
        this.hidingAnimation.Completed += new EventHandler<object>(this.HidingAnimationCompleted);
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
      if (DesignMode.DesignModeEnabled || this.Card == null || this.Visibility == Visibility.Visible)
        return;
      this.Visibility = Visibility.Visible;
      this.risingAnimation.Begin();
    }

    protected override void OnTapped(TappedRoutedEventArgs e)
    {
      if (this.isHiding || this.Visibility == Visibility.Collapsed)
        return;
      this.isHiding = true;
      this.hidingAnimation.Begin();
      base.OnTapped(e);
    }

    private void HidingAnimationCompleted(object sender, object e)
    {
      this.Visibility = Visibility.Collapsed;
      this.PanelHiding((object) this, EventArgs.Empty);
      this.isHiding = false;
    }

    private void BorderTap(object sender, TappedRoutedEventArgs e)
    {
    }
  }
}

