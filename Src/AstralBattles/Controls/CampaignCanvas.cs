
using AstralBattles.Core.Model;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;


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
        foreach (CampaignOpponent opponent in (System.Collections.Generic.IEnumerable<CampaignOpponent>) this.Opponents)
        {
          CampaignFoeFace element = new CampaignFoeFace();
          element.SetBinding(CampaignFoeFace.FoeProperty, new Binding()
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

