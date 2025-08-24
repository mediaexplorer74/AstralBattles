// Decompiled with JetBrains decompiler
// Type: AstralBattles.Controls.DeckFieldBorder
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Model;
using System;
using System.Diagnostics;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Shapes;

#nullable disable

namespace AstralBattles.Controls
{
  [Windows.UI.Xaml.Markup.ContentProperty("DeckCard")]
public partial class DeckFieldBorder : UserControl
  {
    public static readonly DependencyProperty DeckCardProperty = DependencyProperty.Register(nameof (DeckCard), typeof (DeckField), typeof (DeckFieldBorder), new PropertyMetadata(new PropertyChangedCallback(DeckFieldBorder.DeckCardChangedStatic)));
   

    public DeckFieldBorder() => this.InitializeComponent();

    public DeckField DeckCard
    {
      get => (DeckField) this.GetValue(DeckFieldBorder.DeckCardProperty);
      set => this.SetValue(DeckFieldBorder.DeckCardProperty, (object) value);
    }

    private static void DeckCardChangedStatic(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is DeckFieldBorder))
        return;
      ((DeckFieldBorder) d).DeckCardChanged();
    }

    protected override void OnTap(GestureEventArgs e)
    {
      if (this.DeckCard != null && this.DeckCard.IsWaitingForChoise)
      {
        this.DeckCard.RequestMove();
        base.OnTap(e);
      }
      else
      {
        if (this.DeckCard != null && this.DeckCard.Card != null)
          this.DeckCard.IsSelected = true;
        base.OnTap(e);
      }
    }

    private void DeckCardChanged()
    {
    }

   
  }
}

