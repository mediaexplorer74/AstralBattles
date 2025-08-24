// Decompiled with JetBrains decompiler
// Type: AstralBattles.Controls.LogListbox
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using System.Collections.Specialized;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace AstralBattles.Controls
{
public partial class LogListbox : ListBox
  {
    protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
    {
      if (this.Items.Count > 0)
      {
        this.SelectedIndex = this.Items.Count - 1;
        this.ScrollIntoView(this.SelectedItem);
      }
      base.OnItemsChanged(e);
    }
  }
}

