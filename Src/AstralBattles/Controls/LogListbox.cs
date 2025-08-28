// Decompiled with JetBrains decompiler
// Type: AstralBattles.Controls.LogListbox
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using System.Collections.Specialized;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;


namespace AstralBattles.Controls
{
  public partial class LogListbox : ListBox
  {
    public LogListbox()
    {
      this.Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
      // UWP approach - listen to ItemsSource changes
      if (Items.Count > 0)
      {
        SelectedIndex = Items.Count - 1;
        ScrollIntoView(SelectedItem);
      }
    }
    
    // UWP doesn't have OnItemsChanged - stub for MVP
    // Would need to implement INotifyCollectionChanged handling for full functionality
  }
}

