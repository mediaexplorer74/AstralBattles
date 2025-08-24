// Decompiled with JetBrains decompiler
// Type: AstralBattles.Controls.SmallCardsBook
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Model;
using AstralBattles.ViewModels;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace AstralBattles.Controls
{
public partial class SmallCardsBook : UserControl
  {
    public static readonly DependencyProperty PlayerProperty = DependencyProperty.Register(nameof (Player), typeof (Player), typeof (SmallCardsBook), new PropertyMetadata((object) null, new PropertyChangedCallback(SmallCardsBook.PlayerChangedStatic)));
    public static readonly DependencyProperty BattlefieldViewModelProperty = DependencyProperty.Register(nameof (BattlefieldViewModel), typeof (BattlefieldViewModel), typeof (SmallCardsBook), new PropertyMetadata((object) null, new PropertyChangedCallback(SmallCardsBook.BattlefieldViewModelChangedStatic)));
   

    public SmallCardsBook() => this.InitializeComponent();

    public BattlefieldViewModel BattlefieldViewModel
    {
      get => (BattlefieldViewModel) this.GetValue(SmallCardsBook.BattlefieldViewModelProperty);
      set => this.SetValue(SmallCardsBook.BattlefieldViewModelProperty, (object) value);
    }

    public Player Player
    {
      get => (Player) this.GetValue(SmallCardsBook.PlayerProperty);
      set => this.SetValue(SmallCardsBook.PlayerProperty, (object) value);
    }

    private static void PlayerChangedStatic(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is SmallCardsBook))
        return;
      ((SmallCardsBook) d).PlayerChanged();
    }

    private void PlayerChanged()
    {
    }

    private static void BattlefieldViewModelChangedStatic(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is SmallCardsBook smallCardsBook))
        return;
      smallCardsBook.BattlefieldViewModelChanged(e.OldValue as BattlefieldViewModel);
    }

    private void BattlefieldViewModelChanged(BattlefieldViewModel oldValue)
    {
    }

   
  }
}

