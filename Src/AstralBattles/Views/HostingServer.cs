// Decompiled with JetBrains decompiler
// Type: AstralBattles.Views.HostingServer
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable

namespace AstralBattles.Views
{
public partial class HostingServer : Page
  {
    internal StackPanel LayoutRoot;
    internal StackPanel TitlePanel;
    internal TextBlock PageTitle;
    private bool _contentLoaded;

    public HostingServer() => this.InitializeComponent();

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/AstralBattles;component/Views/HostingServer.xaml", UriKind.Relative));
      this.LayoutRoot = (StackPanel) ((FrameworkElement) this).FindName("LayoutRoot");
      this.TitlePanel = (StackPanel) ((FrameworkElement) this).FindName("TitlePanel");
      this.PageTitle = (TextBlock) ((FrameworkElement) this).FindName("PageTitle");
    }
  }
}

