// Decompiled with JetBrains decompiler
// Type: AstralBattles.Views.MainPage
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Services;
using AstralBattles.ViewModels;
using Microsoft.Phone.Controls;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

#nullable disable
namespace AstralBattles.Views
{
public partial class MainPage : PhoneApplicationPage
  {
    private bool _contentLoaded;

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/AstralBattles;component/Views/MainPage.xaml", UriKind.Relative));
    }

    public MainPage()
    {
      Utils.CurrentDispatcher = ((DependencyObject) this).Dispatcher;
      this.InitializeComponent();
      CardRegistry.Reload();
    }

    private void PhoneApplicationPageLoaded(object sender, RoutedEventArgs e)
    {
    }

    protected virtual void OnBackKeyPress(CancelEventArgs e)
    {
    }

    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      if (e.Uri.ToString().EndsWith("?back=true"))
        ((Page) this).NavigationService.ClearBackStack();
      ((MainViewModel) ((FrameworkElement) this).DataContext).OnNavigatedTo();
      ((Page) this).OnNavigatedFrom(e);
    }
  }
}

