// Decompiled with JetBrains decompiler
// Type: AstralBattles.Controls.CardsBook
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Model;
using AstralBattles.ViewModels;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;


namespace AstralBattles.Controls
{
public partial class CardsBook : UserControl
  {
    public static readonly DependencyProperty PlayerProperty = DependencyProperty.Register(nameof (Player), typeof (Player), typeof (CardsBook), new PropertyMetadata((object) null, new PropertyChangedCallback(CardsBook.PlayerChangedStatic)));
    public static readonly DependencyProperty SixCardsModeProperty = DependencyProperty.Register(nameof (SixCardsMode), typeof (bool), typeof (CardsBook), new PropertyMetadata((object) false, new PropertyChangedCallback(CardsBook.SixCardsModeStaticChange)));
    public static readonly DependencyProperty BattlefieldViewModelProperty = DependencyProperty.Register(nameof (BattlefieldViewModel), typeof (BattlefieldViewModel), typeof (CardsBook), new PropertyMetadata((object) null, new PropertyChangedCallback(CardsBook.BattlefieldViewModelChangedStatic)));

   
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

    private void GestureListenerFlick(object sender, ManipulationDeltaRoutedEventArgs e)
    {
      if (this.BattlefieldViewModel == null)
        return;
      this.BattlefieldViewModel.SetNextOrPreviousElement(e.Delta.Translation.Y < 0.0);
    }

    private void ListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }
  }
}

