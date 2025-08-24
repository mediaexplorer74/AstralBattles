// Decompiled with JetBrains decompiler
// Type: AstralBattles.Controls.CreatureControl
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using AstralBattles.Converters;
using AstralBattles.Core.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

#nullable disable
namespace AstralBattles.Controls
{
public partial class CreatureControl : UserControl
  {
    private CardImageConverter imageConverter = new CardImageConverter();
    private readonly Queue<string> overflowTextGotHealthChangesStack = new Queue<string>();
    private readonly Queue<string> overflowTextGotDamageChangesStack = new Queue<string>();
    private bool isPlayerGotHealthAnimated;
    private bool isPlayerGotDamageAnimated;
    public static readonly DependencyProperty FieldNameProperty = DependencyProperty.Register(nameof (FieldName), typeof (string), typeof (CreatureControl), new PropertyMetadata((object) ""));
    public static readonly DependencyProperty ImageUriProperty = DependencyProperty.Register(nameof (ImageUri), typeof (string), typeof (CreatureControl), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty FieldProperty = DependencyProperty.Register(nameof (Field), typeof (Field), typeof (CreatureControl), new PropertyMetadata((object) null, new PropertyChangedCallback(CreatureControl.FieldPropertyChangedStatic)));
    public static readonly DependencyProperty OverlayTextProperty = DependencyProperty.Register(nameof (OverlayText), typeof (string), typeof (CreatureControl), new PropertyMetadata((PropertyChangedCallback) null));
    private Action dieStateCompleted;
    private Action summonCompleted;
    private Action fieldAttackingCompleted;
    private Action skipCompleted;
    internal UserControl mainControl;
    internal Grid grid;
    internal VisualStateGroup VisualStateGroup;
    internal Storyboard dieStateStoryboard;
    internal Storyboard attackBottomStateStoryboard;
    internal Storyboard attackTopStateStoryboard;
    internal Storyboard gotDamageStateStoryboard;
    internal Storyboard gotHealthStateStoryboard;
    internal Storyboard skipTurnStateStoryboard;
    internal Storyboard creationStateStoryboard;
    internal Storyboard doActStoryboard;
    internal VisualState DefaultState;
    internal VisualState DieState;
    internal VisualState GotHealthState;
    internal VisualState SelectedState;
    internal VisualState CreationState;
    internal VisualState AttackBottomState;
    internal VisualState AttackTopState;
    internal VisualState GotDamageState;
    internal VisualState SkipTurnState;
    internal VisualState BaseState;
    internal VisualState ActState;
    internal Rectangle waitSelectRectangle;
    internal Rectangle rootRectangle;
    internal Grid LayoutRoot;
    internal Image image;
    internal Border border;
    internal TextBlock textBlock;
    internal TextBlock textBlock3;
    internal TextBlock textBlock1;
    internal TextBlock textBlock2;
    internal Border clocksBorder;
    internal Image image1;
    private bool _contentLoaded;

    public CreatureControl()
    {
      if (ViewModelBase.IsInDesignModeStatic)
      {
        this.Field = new Field(new Player())
        {
          IsWaitingForSelect = false
        };
        Field field = this.Field;
        CreatureCard creatureCard = new CreatureCard();
        creatureCard.Cost = 7;
        creatureCard.Damage = 6;
        creatureCard.Description = "Some description";
        creatureCard.DisplayName = "Water element";
        creatureCard.ElementType = ElementTypeEnum.Water;
        creatureCard.Health = 40;
        creatureCard.Level = 5;
        creatureCard.Name = "WaterElemental";
        CreatureCard c = creatureCard;
        field.AssignCard(c, (Action) null);
        this.ImageUri = this.imageConverter.Convert((Card) this.Field.Card);
      }
      this.InitializeComponent();
      if (ViewModelBase.IsInDesignModeStatic)
        return;
      this.attackBottomStateStoryboard.Completed += new EventHandler(this.FieldAttackingCompleted);
      this.attackTopStateStoryboard.Completed += new EventHandler(this.FieldAttackingCompleted);
      this.dieStateStoryboard.Completed += new EventHandler(this.DieStateCompleted);
      this.creationStateStoryboard.Completed += new EventHandler(this.SummonCompleted);
      this.skipTurnStateStoryboard.Completed += new EventHandler(this.SkipCompleted);
      this.gotDamageStateStoryboard.Completed += new EventHandler(this.GotDamageStoryboardCompleted);
      this.gotHealthStateStoryboard.Completed += new EventHandler(this.GotHealthStoryboardCompleted);
    }

    private void GotDamageStoryboardCompleted(object sender, EventArgs e)
    {
      this.isPlayerGotDamageAnimated = false;
      if (this.overflowTextGotDamageChangesStack.Count <= 0)
        return;
      this.OverlayText = this.overflowTextGotDamageChangesStack.Dequeue();
      this.isPlayerGotDamageAnimated = true;
      this.gotDamageStateStoryboard.Begin();
    }

    private void GotHealthStoryboardCompleted(object sender, EventArgs e)
    {
      this.isPlayerGotHealthAnimated = false;
      if (this.overflowTextGotHealthChangesStack.Count <= 0)
        return;
      this.OverlayText = this.overflowTextGotHealthChangesStack.Dequeue();
      this.isPlayerGotHealthAnimated = true;
      this.gotHealthStateStoryboard.Begin();
    }

    public Field Field
    {
      get => (Field) this.GetValue(CreatureControl.FieldProperty);
      set => this.SetValue(CreatureControl.FieldProperty, (object) value);
    }

    public string FieldName
    {
      get => (string) this.GetValue(CreatureControl.FieldNameProperty);
      set => this.SetValue(CreatureControl.FieldNameProperty, (object) value);
    }

    public string ImageUri
    {
      get => (string) this.GetValue(CreatureControl.ImageUriProperty);
      set => this.SetValue(CreatureControl.ImageUriProperty, (object) value);
    }

    private static void FieldPropertyChangedStatic(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is CreatureControl))
        return;
      ((CreatureControl) d).FieldPropertyChanged(e.OldValue as Field);
    }

    private void FieldPropertyChanged(Field oldField)
    {
      this.UpdateImage();
      if (ViewModelBase.IsInDesignModeStatic)
        return;
      if (oldField != null)
      {
        oldField.Skip -= new EventHandler<IntValueChangedEventArgs>(this.FieldSkip);
        oldField.GotDamage -= new EventHandler<IntValueChangedEventArgs>(this.CardGotDamage);
        oldField.GotHealth -= new EventHandler<IntValueChangedEventArgs>(this.CardGotHealth);
        oldField.Summoned -= new EventHandler<IntValueChangedEventArgs>(this.CardSummoned);
        oldField.Died -= new EventHandler<IntValueChangedEventArgs>(this.CardDied);
        oldField.Attacking -= new EventHandler<IntValueChangedEventArgs>(this.FieldAttacking);
        oldField.Acts -= new EventHandler(this.FieldActs);
      }
      if (this.Field == null)
        return;
      this.Field.Skip += new EventHandler<IntValueChangedEventArgs>(this.FieldSkip);
      this.Field.GotDamage += new EventHandler<IntValueChangedEventArgs>(this.CardGotDamage);
      this.Field.GotHealth += new EventHandler<IntValueChangedEventArgs>(this.CardGotHealth);
      this.Field.Summoned += new EventHandler<IntValueChangedEventArgs>(this.CardSummoned);
      this.Field.Died += new EventHandler<IntValueChangedEventArgs>(this.CardDied);
      this.Field.Attacking += new EventHandler<IntValueChangedEventArgs>(this.FieldAttacking);
      this.Field.Acts += new EventHandler(this.FieldActs);
    }

    private void FieldActs(object sender, EventArgs e) => this.doActStoryboard.Begin();

    private void FieldSkip(object sender, IntValueChangedEventArgs e)
    {
      this.skipCompleted = e.Callback;
      this.skipTurnStateStoryboard.Begin();
    }

    private void UpdateImage()
    {
      if (this.Field == null || this.Field.IsEmpty)
        this.ImageUri = (string) null;
      else
        this.ImageUri = this.imageConverter.Convert((Card) this.Field.Card);
    }

    private void FieldAttacking(object sender, IntValueChangedEventArgs e)
    {
      SoundPlayer.PlaySound("attack");
      this.fieldAttackingCompleted = e.Callback;
      if (this.IsOnTheTop)
        this.attackBottomStateStoryboard.Begin();
      else
        this.attackTopStateStoryboard.Begin();
    }

    private void CardDied(object sender, IntValueChangedEventArgs e)
    {
      Vibration.Vibrate();
      this.UpdateImage();
      this.dieStateCompleted = e.Callback;
      this.dieStateStoryboard.Begin();
    }

    private void CardSummoned(object sender, IntValueChangedEventArgs e)
    {
      this.GoToDefaultState();
      this.UpdateImage();
      this.summonCompleted = e.Callback;
      this.creationStateStoryboard.Begin();
    }

    private void GoToDefaultState()
    {
      ((PlaneProjection) this.image1.Projection).RotationZ = 0.0;
      ((PlaneProjection) this.LayoutRoot.Projection).LocalOffsetZ = 0.0;
      ((CompositeTransform) this.LayoutRoot.RenderTransform).Rotation = 0.0;
      ((CompositeTransform) this.LayoutRoot.RenderTransform).ScaleX = 1.0;
    }

    private void CardGotHealth(object sender, IntValueChangedEventArgs e)
    {
      string str = "+" + (object) e.Value;
      if (this.isPlayerGotHealthAnimated)
      {
        this.overflowTextGotHealthChangesStack.Enqueue(str);
      }
      else
      {
        this.OverlayText = str;
        this.isPlayerGotHealthAnimated = true;
        this.gotHealthStateStoryboard.Begin();
      }
    }

    private void CardGotDamage(object sender, IntValueChangedEventArgs e)
    {
      string str = "-" + (object) e.Value;
      if (this.isPlayerGotDamageAnimated)
      {
        this.overflowTextGotDamageChangesStack.Enqueue(str);
      }
      else
      {
        this.OverlayText = str;
        this.isPlayerGotDamageAnimated = true;
        this.gotDamageStateStoryboard.Begin();
      }
    }

    public string OverlayText
    {
      get => (string) this.GetValue(CreatureControl.OverlayTextProperty);
      set => this.SetValue(CreatureControl.OverlayTextProperty, (object) value);
    }

    public bool IsOnTheTop => this.Field.IsOnTheTop;

    public event EventHandler Selecting = delegate { };

    protected override void OnTap(System.Windows.Input.GestureEventArgs e)
    {
      if (this.Field == null)
        return;
      this.Field.IsSelected = true;
      this.Selecting((object) this, EventArgs.Empty);
      base.OnTap(e);
    }

    private void MainControlLoaded(object sender, RoutedEventArgs e)
    {
    }

    private void DieStateCompleted(object sender, EventArgs e)
    {
      if (this.dieStateCompleted != null)
      {
        this.dieStateCompleted();
        this.dieStateCompleted = (Action) null;
      }
      this.GoToDefaultState();
      this.UpdateImage();
    }

    private void SummonCompleted(object sender, EventArgs e)
    {
      if (this.summonCompleted != null)
      {
        this.summonCompleted();
        this.summonCompleted = (Action) null;
      }
      this.UpdateImage();
    }

    private void FieldAttackingCompleted(object sender, EventArgs e)
    {
      if (this.fieldAttackingCompleted == null)
        return;
      this.fieldAttackingCompleted();
      this.fieldAttackingCompleted = (Action) null;
    }

    private void SkipCompleted(object sender, EventArgs e)
    {
      if (this.skipCompleted == null)
        return;
      this.skipCompleted();
      this.skipCompleted = (Action) null;
    }

    private void GestureListenerTap(object sender, Microsoft.Phone.Controls.GestureEventArgs e)
    {
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/AstralBattles;component/Controls/CreatureControl.xaml", UriKind.Relative));
      this.mainControl = (UserControl) this.FindName("mainControl");
      this.grid = (Grid) this.FindName("grid");
      this.VisualStateGroup = (VisualStateGroup) this.FindName("VisualStateGroup");
      this.dieStateStoryboard = (Storyboard) this.FindName("dieStateStoryboard");
      this.attackBottomStateStoryboard = (Storyboard) this.FindName("attackBottomStateStoryboard");
      this.attackTopStateStoryboard = (Storyboard) this.FindName("attackTopStateStoryboard");
      this.gotDamageStateStoryboard = (Storyboard) this.FindName("gotDamageStateStoryboard");
      this.gotHealthStateStoryboard = (Storyboard) this.FindName("gotHealthStateStoryboard");
      this.skipTurnStateStoryboard = (Storyboard) this.FindName("skipTurnStateStoryboard");
      this.creationStateStoryboard = (Storyboard) this.FindName("creationStateStoryboard");
      this.doActStoryboard = (Storyboard) this.FindName("doActStoryboard");
      this.DefaultState = (VisualState) this.FindName("DefaultState");
      this.DieState = (VisualState) this.FindName("DieState");
      this.GotHealthState = (VisualState) this.FindName("GotHealthState");
      this.SelectedState = (VisualState) this.FindName("SelectedState");
      this.CreationState = (VisualState) this.FindName("CreationState");
      this.AttackBottomState = (VisualState) this.FindName("AttackBottomState");
      this.AttackTopState = (VisualState) this.FindName("AttackTopState");
      this.GotDamageState = (VisualState) this.FindName("GotDamageState");
      this.SkipTurnState = (VisualState) this.FindName("SkipTurnState");
      this.BaseState = (VisualState) this.FindName("BaseState");
      this.ActState = (VisualState) this.FindName("ActState");
      this.waitSelectRectangle = (Rectangle) this.FindName("waitSelectRectangle");
      this.rootRectangle = (Rectangle) this.FindName("rootRectangle");
      this.LayoutRoot = (Grid) this.FindName("LayoutRoot");
      this.image = (Image) this.FindName("image");
      this.border = (Border) this.FindName("border");
      this.textBlock = (TextBlock) this.FindName("textBlock");
      this.textBlock3 = (TextBlock) this.FindName("textBlock3");
      this.textBlock1 = (TextBlock) this.FindName("textBlock1");
      this.textBlock2 = (TextBlock) this.FindName("textBlock2");
      this.clocksBorder = (Border) this.FindName("clocksBorder");
      this.image1 = (Image) this.FindName("image1");
    }
  }
}

