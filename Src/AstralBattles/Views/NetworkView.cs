// Decompiled with JetBrains decompiler
// Type: AstralBattles.Views.NetworkView
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using Microsoft.Phone.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace AstralBattles.Views
{
public partial class NetworkView : PhoneApplicationPage
  {
    internal PhoneApplicationPage mainControl;
    internal Grid ContentPanel;
    private bool _contentLoaded;

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/AstralBattles;component/Views/NetworkView.xaml", UriKind.Relative));
      this.mainControl = (PhoneApplicationPage) ((FrameworkElement) this).FindName("mainControl");
      this.ContentPanel = (Grid) ((FrameworkElement) this).FindName("ContentPanel");
    }

    public NetworkView() => this.InitializeComponent();
  }
}

