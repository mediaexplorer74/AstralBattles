// Decompiled with JetBrains decompiler
// Type: AstralBattles.Controls.CampaignFoeFace
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Model;
using System;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;


namespace AstralBattles.Controls
{
public partial class CampaignFoeFace : UserControl
  {
    public static readonly DependencyProperty FoeProperty = DependencyProperty.Register(nameof (Foe), typeof (CampaignOpponent), typeof (CampaignFoeFace), new PropertyMetadata(new PropertyChangedCallback(CampaignFoeFace.FoeChangedStatic)));
    public static readonly DependencyProperty FaceYindexProperty = DependencyProperty.Register(nameof (FaceYindex), typeof (int), typeof (CampaignFoeFace), new PropertyMetadata((object) 0));
    public static readonly DependencyProperty FaceXindexProperty = DependencyProperty.Register(nameof (FaceXindex), typeof (int), typeof (CampaignFoeFace), new PropertyMetadata((object) 0));
    public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof (IsSelected), typeof (bool), typeof (CampaignFoeFace), new PropertyMetadata((object) false));
    public static readonly DependencyProperty BorderImageProperty = DependencyProperty.Register(nameof (BorderImage), typeof (string), typeof (CampaignFoeFace), new PropertyMetadata((object) ""));

    

    public CampaignFoeFace()
    {
      this.InitializeComponent();
      this.BorderImage = "/Resources/Campaign/circle.png";
      this.Tapped += OnTapped;
    }

    public bool IsSelected
    {
      get => (bool) this.GetValue(CampaignFoeFace.IsSelectedProperty);
      set => this.SetValue(CampaignFoeFace.IsSelectedProperty, (object) value);
    }

    public int FaceXindex
    {
      get => (int) this.GetValue(CampaignFoeFace.FaceXindexProperty);
      set => this.SetValue(CampaignFoeFace.FaceXindexProperty, (object) value);
    }

    public int FaceYindex
    {
      get => (int) this.GetValue(CampaignFoeFace.FaceYindexProperty);
      set => this.SetValue(CampaignFoeFace.FaceYindexProperty, (object) value);
    }

    public CampaignOpponent Foe
    {
      get => (CampaignOpponent) this.GetValue(CampaignFoeFace.FoeProperty);
      set => this.SetValue(CampaignFoeFace.FoeProperty, (object) value);
    }

    private static void FoeChangedStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (!(d is CampaignFoeFace))
        return;
      ((CampaignFoeFace) d).FoeChanged();
    }

    private void FoeChanged()
    {
      if (this.Foe == null)
        return;
      this.faceImage.Margin = new Thickness((double) (10 - 67 * this.Foe.ImageXindex), (double) (9 - 67 * this.Foe.ImageYindex), 0.0, 0.0);
      this.Foe.PropertyChanged += new PropertyChangedEventHandler(this.FoePropertyChanged);
      this.BorderImage = this.Foe.IsSelected ? "/Resources/Campaign/circle2.png" : "/Resources/Campaign/circle.png";
    }

    private void OnTapped(object sender, TappedRoutedEventArgs e)
    {
      this.Foe.IsSelected = !this.Foe.IsSelected;
    }

    private void FoePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "IsSelected"))
        return;
      this.IsSelected = this.Foe.IsSelected;
      this.BorderImage = this.Foe.IsSelected ? "/Resources/Campaign/circle2.png" : "/Resources/Campaign/circle.png";
    }

    public string BorderImage
    {
      get => (string) this.GetValue(CampaignFoeFace.BorderImageProperty);
      set => this.SetValue(CampaignFoeFace.BorderImageProperty, (object) value);
    }
  }
}

