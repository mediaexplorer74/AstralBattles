// Decompiled with JetBrains decompiler
// Type: AstralBattles.Controls.BusyIndicatorControl
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using Microsoft.Phone.Controls;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace AstralBattles.Controls
{
  [TemplateVisualState(Name = "Visible", GroupName = "VisibilityStates")]
  [TemplateVisualState(Name = "Hidden", GroupName = "VisibilityStates")]
public partial class BusyIndicatorControl : ContentControl
  {
    public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(nameof (IsBusy), typeof (bool), typeof (BusyIndicatorControl), new PropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((BusyIndicatorControl) d).OnIsBusyChanged(e))));
    public static readonly DependencyProperty BusyTextProperty = DependencyProperty.Register(nameof (BusyText), typeof (string), typeof (BusyIndicatorControl), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty HideApplicationBarProperty = DependencyProperty.Register(nameof (HideApplicationBar), typeof (bool), typeof (BusyIndicatorControl), new PropertyMetadata((object) true, (PropertyChangedCallback) null));

    public bool IsBusy
    {
      get => (bool) this.GetValue(BusyIndicatorControl.IsBusyProperty);
      set => this.SetValue(BusyIndicatorControl.IsBusyProperty, (object) value);
    }

    public string BusyText
    {
      get => (string) this.GetValue(BusyIndicatorControl.BusyTextProperty);
      set => this.SetValue(BusyIndicatorControl.BusyTextProperty, (object) value);
    }

    public bool HideApplicationBar
    {
      get => (bool) this.GetValue(BusyIndicatorControl.HideApplicationBarProperty);
      set => this.SetValue(BusyIndicatorControl.HideApplicationBarProperty, (object) value);
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.ChangeVisualState(false);
      this.OnIsBusyChanged(new DependencyPropertyChangedEventArgs());
    }

    protected virtual void ChangeVisualState(bool useTransitions)
    {
      VisualStateManager.GoToState((Control) this, this.IsBusy ? "Visible" : "Hidden", useTransitions);
    }

    protected virtual void OnIsBusyChanged(DependencyPropertyChangedEventArgs e)
    {
      if (this.HideApplicationBar && Application.Current.RootVisual is PhoneApplicationFrame rootVisual && ((ContentControl) rootVisual).Content is PhoneApplicationPage)
      {
        PhoneApplicationPage content = (PhoneApplicationPage) ((ContentControl) rootVisual).Content;
        if (content.ApplicationBar != null)
          content.ApplicationBar.IsVisible = !this.IsBusy;
      }
      this.ChangeVisualState(true);
    }

    private static class VisualStates
    {
      public const string StateVisible = "Visible";
      public const string StateHidden = "Hidden";
      public const string GroupVisibility = "VisibilityStates";
    }
  }
}

