﻿// Decompiled with JetBrains decompiler
// Type: AstralBattles.Views.MainPage
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Services;
using AstralBattles.ViewModels;
using AstralBattles.Localizations.Cyclops.MainApplication.Localization;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System;
using System.Diagnostics;


namespace AstralBattles.Views
{
public partial class MainPage : Page
  {
    private ResourceWrapper ResourceWrapper => (ResourceWrapper)Resources["ResourceWrapper"];
       
    public MainPage()
    {
      Utils.CurrentDispatcher = this.Dispatcher;
      this.InitializeComponent();
      CardRegistry.Reload();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      base.OnNavigatedTo(e);
      if (DataContext is MainViewModel mainViewModel)
      {
        mainViewModel.OnNavigatedTo();
      }
    }
  }
}

