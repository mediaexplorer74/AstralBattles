// Decompiled with JetBrains decompiler
// Type: AstralBattles.Views.CampaignMap
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Controls;
using AstralBattles.ViewModels;
using Windows.UI.Xaml.Controls;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace AstralBattles.Views
{
public partial class CampaignMap : Page
  {
    

    public CampaignMap() => this.InitializeComponent();

    protected virtual void OnBackKeyPress(CancelEventArgs e)
    {
      ((CampaignViewModel) ((FrameworkElement) this).DataContext).OnBackKeyPress(e);
      base.OnBackKeyPress(e);
    }

    
  }
}

