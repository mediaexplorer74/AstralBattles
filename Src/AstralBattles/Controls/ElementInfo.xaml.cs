using AstralBattles.Core.Model;
using AstralBattles.Core.Infrastructure;
using Windows.ApplicationModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;


namespace AstralBattles.Controls
{
public partial class ElementInfo : UserControl
  {
    private readonly Queue<string> overflowTextChangesStack = new Queue<string>();
    private bool isAnimated;
    public static readonly DependencyProperty OverflowTextProperty = DependencyProperty.Register(nameof (OverflowText), typeof (string), typeof (ElementInfo), new PropertyMetadata((object) ""));
    public static readonly DependencyProperty HideNameProperty = DependencyProperty.Register(nameof (HideName), typeof (bool), typeof (ElementInfo), new PropertyMetadata((object) false));
    public static readonly DependencyProperty ElementProperty = DependencyProperty.Register(nameof (Element), typeof (Element), typeof (ElementInfo), new PropertyMetadata((object) null, new PropertyChangedCallback(ElementInfo.ElementPropertyChangedStatic)));

       public ElementInfo()
    {
      this.InitializeComponent();
      if (DesignMode.DesignModeEnabled)
        return;
      this.manaIncreasedStoryboard.Completed += new EventHandler<object>(this.ManaIncreasedStoryboardCompleted);
      this.manaDecreasedStoryboard.Completed += new EventHandler<object>(this.ManaDecreasedStoryboardCompleted);
    }

    private void ManaDecreasedStoryboardCompleted(object sender, object e)
    {
      this.isAnimated = false;
      if (this.overflowTextChangesStack.Count <= 0)
        return;
      this.ManaChangesAnimation(this.overflowTextChangesStack.Dequeue());
    }

    private void ManaIncreasedStoryboardCompleted(object sender, object e)
    {
      this.isAnimated = false;
      if (this.overflowTextChangesStack.Count <= 0)
        return;
      this.ManaChangesAnimation(this.overflowTextChangesStack.Dequeue());
    }

    public Element Element
    {
      get => (Element) this.GetValue(ElementInfo.ElementProperty);
      set => this.SetValue(ElementInfo.ElementProperty, (object) value);
    }

    public bool HideName
    {
      get => (bool) this.GetValue(ElementInfo.HideNameProperty);
      set => this.SetValue(ElementInfo.HideNameProperty, (object) value);
    }

    public string OverflowText
    {
      get => (string) this.GetValue(ElementInfo.OverflowTextProperty);
      set => this.SetValue(ElementInfo.OverflowTextProperty, (object) value);
    }

    private static void ElementPropertyChangedStatic(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is ElementInfo))
        return;
      ((ElementInfo) d).ElementPropertyChanged(e.OldValue as Element);
    }

    private void ElementPropertyChanged(Element oldElement)
    {
      if (oldElement != null)
      {
        oldElement.ManaDecreased -= new EventHandler<IntValueChangedEventArgs>(this.ElementManaDecreased);
        oldElement.ManaIncreased -= new EventHandler<IntValueChangedEventArgs>(this.ElementManaIncreased);
      }
      if (this.Element == null)
        return;
      this.Element.ManaDecreased += new EventHandler<IntValueChangedEventArgs>(this.ElementManaDecreased);
      this.Element.ManaIncreased += new EventHandler<IntValueChangedEventArgs>(this.ElementManaIncreased);
    }

    private void ManaChangesAnimation(string str)
    {
      if (string.IsNullOrWhiteSpace(str))
        this.isAnimated = false;
      else if (str.Contains("-"))
        this.DecreaseElementAnimation(str);
      else
        this.IncreaseElementAnimation(str);
    }

    private void IncreaseElementAnimation(string str)
    {
      if (this.isAnimated)
      {
        this.overflowTextChangesStack.Enqueue(str);
      }
      else
      {
        this.overflowTextBox.Foreground = (Brush) new SolidColorBrush(Colors.Green);
        this.OverflowText = str;
        this.isAnimated = true;
        this.manaIncreasedStoryboard.Begin();
      }
    }

    private void ElementManaIncreased(object sender, IntValueChangedEventArgs e)
    {
      this.IncreaseElementAnimation("+" + (object) e.Value);
    }

    private void DecreaseElementAnimation(string str)
    {
      if (this.isAnimated)
      {
        this.overflowTextChangesStack.Enqueue(str);
      }
      else
      {
        this.overflowTextBox.Foreground = (Brush) new SolidColorBrush(Colors.Red);
        this.OverflowText = str;
        this.isAnimated = true;
        this.manaDecreasedStoryboard.Begin();
      }
    }

    private void ElementManaDecreased(object sender, IntValueChangedEventArgs e)
    {
      this.DecreaseElementAnimation("-" + (object) e.Value);
    }

    public event EventHandler Selecting = delegate { };

    protected override void OnTapped(TappedRoutedEventArgs e)
    {
      this.Element.IsSelected = true;
      this.Selecting((object) this, EventArgs.Empty);
      base.OnTapped(e);
    }

    private void UserControlLoaded(object sender, RoutedEventArgs e)
    {
    }
  }
}

