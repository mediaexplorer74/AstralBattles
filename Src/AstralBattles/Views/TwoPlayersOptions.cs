// Decompiled with JetBrains decompiler
// Type: AstralBattles.Views.TwoPlayersOptions
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.ViewModels;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

#nullable disable
namespace AstralBattles.Views
{
public partial class TwoPlayersOptions : Page
  {
    internal Grid LayoutRoot;
    private bool _contentLoaded;

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/AstralBattles;component/Views/TwoPlayersOptions.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
    }

    public TwoPlayersOptions() => this.InitializeComponent();

    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      if (e.Uri.ToString().EndsWith("?back=true"))
        ((Page) this).NavigationService.ClearBackStack();
      IDictionary<string, string> queryString = ((Page) this).NavigationContext.QueryString;
      ((TwoPlayersOptionsViewModel) ((FrameworkElement) this).DataContext).OnNavigatedTo(e.NavigationMode, e.Uri, ((Page) this).NavigationService);
      ((Page) this).OnNavigatedTo(e);
    }

    protected virtual void OnNavigatedFrom(NavigationEventArgs e)
    {
      ((TwoPlayersOptionsViewModel) ((FrameworkElement) this).DataContext).OnNavigatedFrom();
      ((Page) this).OnNavigatedFrom(e);
    }
  }
}

