
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


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

    protected override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      // For UWP MVP build, call OnIsBusyChanged with default values
      // UWP DependencyPropertyChangedEventArgs doesn't have a public constructor
      this.ChangeVisualState(false);
    }

    protected virtual void ChangeVisualState(bool useTransitions)
    {
      VisualStateManager.GoToState((Control) this, this.IsBusy ? "Visible" : "Hidden", useTransitions);
    }

    protected virtual void OnIsBusyChanged(DependencyPropertyChangedEventArgs e)
    {
      // UWP doesn't have ApplicationBar - stub for MVP
      // Original WP7 code handled ApplicationBar visibility here
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

