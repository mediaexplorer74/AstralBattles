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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;

#nullable disable
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

