// Decompiled with JetBrains decompiler
// Type: AstralBattles.Controls.CardsBook
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Model;
using AstralBattles.ViewModels;
using Microsoft.Phone.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace AstralBattles.Controls
{
public partial class CardsBook : UserControl
  {
    internal UserControl thisControl;
    internal Grid LayoutRoot;
    internal Grid CardsPanelLayoutRoot;
    internal ListBox listBox;
    internal ListBox rootCardList;
    private bool _contentLoaded;
    public static readonly DependencyProperty PlayerProperty = DependencyProperty.Register(nameof (Player), typeof (Player), typeof (CardsBook), new PropertyMetadata((object) null, new PropertyChangedCallback(CardsBook.PlayerChangedStatic)));
    public static readonly DependencyProperty SixCardsModeProperty = DependencyProperty.Register(nameof (SixCardsMode), typeof (bool), typeof (CardsBook), new PropertyMetadata((object) false, new PropertyChangedCallback(CardsBook.SixCardsModeStaticChange)));
    public static readonly DependencyProperty BattlefieldViewModelProperty = DependencyProperty.Register(nameof (BattlefieldViewModel), typeof (BattlefieldViewModel), typeof (CardsBook), new PropertyMetadata((object) null, new PropertyChangedCallback(CardsBook.BattlefieldViewModelChangedStatic)));

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/AstralBattles;component/Controls/CardsBook.xaml", UriKind.Relative));
      this.thisControl = (UserControl) this.FindName("thisControl");
      this.LayoutRoot = (Grid) this.FindName("LayoutRoot");
      this.CardsPanelLayoutRoot = (Grid) this.FindName("CardsPanelLayoutRoot");
      this.listBox = (ListBox) this.FindName("listBox");
      this.rootCardList = (ListBox) this.FindName("rootCardList");
    }

    public CardsBook() => this.InitializeComponent();

    public BattlefieldViewModel BattlefieldViewModel
    {
      get => (BattlefieldViewModel) this.GetValue(CardsBook.BattlefieldViewModelProperty);
      set => this.SetValue(CardsBook.BattlefieldViewModelProperty, (object) value);
    }

    public Player Player
    {
      get => (Player) this.GetValue(CardsBook.PlayerProperty);
      set => this.SetValue(CardsBook.PlayerProperty, (object) value);
    }

    private static void PlayerChangedStatic(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is CardsBook))
        return;
      ((CardsBook) d).PlayerChanged();
    }

    private void PlayerChanged()
    {
    }

    public bool SixCardsMode
    {
      get => (bool) this.GetValue(CardsBook.SixCardsModeProperty);
      set => this.SetValue(CardsBook.SixCardsModeProperty, (object) value);
    }

    private static void SixCardsModeStaticChange(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is CardsBook cardsBook))
        return;
      cardsBook.SixCardsModeChanged();
    }

    private void SixCardsModeChanged()
    {
      if (this.SixCardsMode)
        this.CardsPanelLayoutRoot.Width = 408.0;
      else
        this.CardsPanelLayoutRoot.Width = 491.0;
    }

    private static void BattlefieldViewModelChangedStatic(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is CardsBook cardsBook))
        return;
      cardsBook.BattlefieldViewModelChanged(e.OldValue as BattlefieldViewModel);
    }

    private void BattlefieldViewModelChanged(BattlefieldViewModel oldValue)
    {
    }

    private void GestureListenerFlick(object sender, FlickGestureEventArgs e)
    {
      if (e.Direction != Orientation.Vertical || this.BattlefieldViewModel == null)
        return;
      this.BattlefieldViewModel.SetNextOrPreviousElement(e.VerticalVelocity < 0.0);
    }

    private void ListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }
  }
}

