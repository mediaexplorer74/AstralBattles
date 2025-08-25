// Decompiled with JetBrains decompiler
// Type: AstralBattles.Views.CampaignOptionsView
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

#nullable disable

namespace AstralBattles.Views
{
public partial class CampaignOptionsView : Page
  {
    
    public CampaignOptionsView() => this.InitializeComponent();

    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      if (e.Uri.ToString().EndsWith("?back=true"))
        ((Page) this).Frame.ClearBackStack();
      IDictionary<string, string> queryString = ((Page) this).Frame.Navigate.QueryString;
      ((CampaignOptionsViewModel) ((FrameworkElement) this).DataContext).OnNavigatedTo(e.NavigationMode, e.Uri);
      ((Page) this).OnNavigatedTo(e);
    }

    protected virtual void OnNavigatedFrom(NavigationEventArgs e)
    {
      ((CampaignOptionsViewModel) ((FrameworkElement) this).DataContext).OnNavigatedFrom();
      ((Page) this).OnNavigatedFrom(e);
    }
   
  }
}

