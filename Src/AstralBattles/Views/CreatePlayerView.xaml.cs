// Decompiled with JetBrains decompiler
// Type: AstralBattles.Views.CreatePlayerView
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.ViewModels;
using Windows.UI.Xaml.Controls;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace AstralBattles.Views
{
public partial class CreatePlayerView : Page
  {
    
    public CreatePlayerView() => this.InitializeComponent();

    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      bool enableDeckEditor = bool.Parse(((Page) this).Frame.Navigate.QueryString.GetValueOrDefault<string, string>("enableDeckEditor", "false"));
      this.editDeckControl.Visibility = enableDeckEditor ? Visibility.Visible : Visibility.Collapsed;
      ((CreatePlayerViewModel) ((FrameworkElement) this).DataContext).OnNavigatedTo(e, ((Page) this).Frame.Navigate, ((Page) this).Frame, enableDeckEditor);
      ((Page) this).OnNavigatedTo(e);
    }

    protected virtual void OnNavigatedFrom(NavigationEventArgs e)
    {
      ((CreatePlayerViewModel) ((FrameworkElement) this).DataContext).OnNavigatedFrom(e, ((Page) this).Frame.Navigate, ((Page) this).Frame);
      ((Page) this).OnNavigatedFrom(e);
    }

    protected virtual void OnBackKeyPress(CancelEventArgs e)
    {
      ((CreatePlayerViewModel) ((FrameworkElement) this).DataContext).OnBackKeyPress();
      base.OnBackKeyPress(e);
    }
  }
}

