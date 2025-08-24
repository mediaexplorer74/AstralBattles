// Decompiled with JetBrains decompiler
// Type: AstralBattles.Controls.CampaignCanvas
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

#nullable disable
namespace AstralBattles.Controls
{
public partial class CampaignCanvas : Canvas
  {
    public static readonly DependencyProperty OpponentsProperty = DependencyProperty.Register(nameof (Opponents), typeof (ObservableCollection<CampaignOpponent>), typeof (CampaignCanvas), new PropertyMetadata(new PropertyChangedCallback(CampaignCanvas.OpponentsChangedStatic)));

    public ObservableCollection<CampaignOpponent> Opponents
    {
      get
      {
        return (ObservableCollection<CampaignOpponent>) this.GetValue(CampaignCanvas.OpponentsProperty);
      }
      set => this.SetValue(CampaignCanvas.OpponentsProperty, (object) value);
    }

    private static void OpponentsChangedStatic(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is CampaignCanvas))
        return;
      ((CampaignCanvas) d).OpponentsChanged();
    }

    private void OpponentsChanged()
    {
      if (this.Opponents == null)
      {
        this.Children.Clear();
      }
      else
      {
        foreach (CampaignOpponent opponent in (Collection<CampaignOpponent>) this.Opponents)
        {
          CampaignFoeFace element = new CampaignFoeFace();
          element.SetBinding(CampaignFoeFace.FoeProperty, new Binding(".")
          {
            Source = (object) opponent
          });
          this.Children.Add((UIElement) element);
          Canvas.SetLeft((UIElement) element, (double) opponent.LocationOnMapX - element.Width + 10.0);
          Canvas.SetTop((UIElement) element, (double) opponent.LocationOnMapY - element.Height + 20.0);
        }
      }
    }
  }
}

