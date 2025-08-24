// Decompiled with JetBrains decompiler
// Type: AstralBattles.Controls.PlayerFaceControl
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Core.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

#nullable disable
namespace AstralBattles.Controls
{
public partial class PlayerFaceControl : UserControl
  {
    internal UserControl mainControl;
    internal Border border1;
    internal VisualStateGroup VisualStateGroup;
    internal Storyboard gotDamageStoryboard;
    internal Storyboard gotHealthStoryboard;
    internal Storyboard skipTurnStateStoryboard;
    internal VisualState Default;
    internal VisualState GotDamageState;
    internal VisualState GotHealthState;
    internal VisualState SkipTurnState;
    internal Grid LayoutRoot;
    internal Border border;
    internal TextBlock textBlock1;
    internal TextBlock textBlock;
    internal Border clocksBorder;
    internal Image image1;
    private bool _contentLoaded;
    public static readonly DependencyProperty OverflowTextProperty = DependencyProperty.Register(nameof (OverflowText), typeof (string), typeof (PlayerFaceControl), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty ImageUriProperty = DependencyProperty.Register(nameof (ImageUri), typeof (string), typeof (PlayerFaceControl), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty PlayerProperty = DependencyProperty.Register(nameof (Player), typeof (Player), typeof (PlayerFaceControl), new PropertyMetadata(new PropertyChangedCallback(PlayerFaceControl.PlayerPropertyChangedStatic)));
    private Action skipCallback;
    private readonly Queue<string> overflowTextGotHealthChangesStack = new Queue<string>();
    private readonly Queue<string> overflowTextGotDamageChangesStack = new Queue<string>();
    private bool isPlayerGotHealthAnimated;
    private bool isPlayerGotDamageAnimated;

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/AstralBattles;component/Controls/PlayerFaceControl.xaml", UriKind.Relative));
      this.mainControl = (UserControl) this.FindName("mainControl");
      this.border1 = (Border) this.FindName("border1");
      this.VisualStateGroup = (VisualStateGroup) this.FindName("VisualStateGroup");
      this.gotDamageStoryboard = (Storyboard) this.FindName("gotDamageStoryboard");
      this.gotHealthStoryboard = (Storyboard) this.FindName("gotHealthStoryboard");
      this.skipTurnStateStoryboard = (Storyboard) this.FindName("skipTurnStateStoryboard");
      this.Default = (VisualState) this.FindName("Default");
      this.GotDamageState = (VisualState) this.FindName("GotDamageState");
      this.GotHealthState = (VisualState) this.FindName("GotHealthState");
      this.SkipTurnState = (VisualState) this.FindName("SkipTurnState");
      this.LayoutRoot = (Grid) this.FindName("LayoutRoot");
      this.border = (Border) this.FindName("border");
      this.textBlock1 = (TextBlock) this.FindName("textBlock1");
      this.textBlock = (TextBlock) this.FindName("textBlock");
      this.clocksBorder = (Border) this.FindName("clocksBorder");
      this.image1 = (Image) this.FindName("image1");
    }

    public PlayerFaceControl()
    {
      if (ViewModelBase.IsInDesignModeStatic)
      {
        this.Player = new Player()
        {
          Name = "Lina",
          DisplayName = "Lina",
          Description = "Description",
          Health = 46
        };
        this.OverflowText = "+5";
        this.ImageUri = string.Format("/AstralBattles;component/Resources/Avatars/{0}.JPG", (object) "face32");
      }
      this.InitializeComponent();
      this.gotHealthStoryboard.Completed += new EventHandler(this.GotHealthStoryboardCompleted);
      this.gotDamageStoryboard.Completed += new EventHandler(this.GotDamageStoryboardCompleted);
      this.skipTurnStateStoryboard.Completed += new EventHandler(this.SkipTurnCompleted);
    }

    private void GoToDefaultState() => ((PlaneProjection) this.image1.Projection).RotationZ = 0.0;

    private void GotDamageStoryboardCompleted(object sender, EventArgs e)
    {
      this.isPlayerGotDamageAnimated = false;
      if (this.overflowTextGotDamageChangesStack.Count <= 0)
        return;
      this.PlayerGotDamage(this.overflowTextGotDamageChangesStack.Dequeue());
    }

    private void GotHealthStoryboardCompleted(object sender, EventArgs e)
    {
      this.isPlayerGotHealthAnimated = false;
      if (this.overflowTextGotHealthChangesStack.Count <= 0)
        return;
      this.PlayerGotHealth(this.overflowTextGotHealthChangesStack.Dequeue());
    }

    public Player Player
    {
      get => (Player) this.GetValue(PlayerFaceControl.PlayerProperty);
      set => this.SetValue(PlayerFaceControl.PlayerProperty, (object) value);
    }

    public string ImageUri
    {
      get => (string) this.GetValue(PlayerFaceControl.ImageUriProperty);
      set => this.SetValue(PlayerFaceControl.ImageUriProperty, (object) value);
    }

    public string OverflowText
    {
      get => (string) this.GetValue(PlayerFaceControl.OverflowTextProperty);
      set => this.SetValue(PlayerFaceControl.OverflowTextProperty, (object) value);
    }

    private static void PlayerPropertyChangedStatic(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is PlayerFaceControl))
        return;
      ((PlayerFaceControl) d).PlayerPropertyChanged(e.OldValue as Player);
    }

    private void PlayerPropertyChanged(Player oldPlayer)
    {
      if (oldPlayer != null)
      {
        oldPlayer.GotDamage -= new EventHandler<IntValueChangedEventArgs>(this.PlayerGotDamage);
        oldPlayer.GotHealth -= new EventHandler<IntValueChangedEventArgs>(this.PlayerGotHealth);
        oldPlayer.Stunned -= new EventHandler<IntValueChangedEventArgs>(this.PlayerStunned);
      }
      if (this.Player != null)
      {
        this.ImageUri = string.Format("/AstralBattles;component/Resources/Avatars/{0}.JPG", (object) this.Player.Photo);
        this.Player.GotDamage += new EventHandler<IntValueChangedEventArgs>(this.PlayerGotDamage);
        this.Player.GotHealth += new EventHandler<IntValueChangedEventArgs>(this.PlayerGotHealth);
        this.Player.Stunned += new EventHandler<IntValueChangedEventArgs>(this.PlayerStunned);
      }
      else
        this.ImageUri = (string) null;
    }

    private void PlayerStunned(object sender, IntValueChangedEventArgs e)
    {
      this.skipCallback = e.Callback;
      this.skipTurnStateStoryboard.Begin();
    }

    private void SkipTurnCompleted(object sender, EventArgs e)
    {
      this.GoToDefaultState();
      if (this.skipCallback == null)
        return;
      this.skipCallback();
    }

    private void PlayerGotHealth(object sender, IntValueChangedEventArgs e)
    {
      this.PlayerGotHealth("+" + (object) e.Value);
    }

    private void PlayerGotHealth(string str)
    {
      if (this.isPlayerGotHealthAnimated)
      {
        this.overflowTextGotHealthChangesStack.Enqueue(str);
      }
      else
      {
        this.OverflowText = str;
        this.isPlayerGotHealthAnimated = true;
        this.gotHealthStoryboard.Begin();
      }
    }

    private void PlayerGotDamage(object sender, IntValueChangedEventArgs e)
    {
      this.PlayerGotDamage("-" + (object) e.Value);
    }

    private void PlayerGotDamage(string str)
    {
      if (this.isPlayerGotDamageAnimated)
      {
        this.overflowTextGotDamageChangesStack.Enqueue(str);
      }
      else
      {
        this.OverflowText = str;
        this.isPlayerGotDamageAnimated = true;
        this.gotDamageStoryboard.Begin();
      }
    }

    private void UserControlLoaded(object sender, RoutedEventArgs e)
    {
    }
  }
}

