// Decompiled with JetBrains decompiler
// Type: AstralBattles.Utils
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;

#nullable disable
namespace AstralBattles
{
  public static class Utils
  {
    public static T FindFirstElementInVisualTree<T>(this DependencyObject parentElement) where T : DependencyObject
    {
      int childrenCount = VisualTreeHelper.GetChildrenCount(parentElement);
      if (childrenCount == 0)
        return default (T);
      for (int childIndex = 0; childIndex < childrenCount; ++childIndex)
      {
        DependencyObject child = VisualTreeHelper.GetChild(parentElement, childIndex);
        if (child != null && child is T elementInVisualTree1)
          return elementInVisualTree1;
        T elementInVisualTree2 = child.FindFirstElementInVisualTree<T>();
        if ((object) elementInVisualTree2 != null)
          return elementInVisualTree2;
      }
      return default (T);
    }

    public static T FindVisualAncestorOfType<T>(this DependencyObject depo) where T : DependencyObject
    {
      for (DependencyObject parent = VisualTreeHelper.GetParent(depo); parent != null; parent = VisualTreeHelper.GetParent(parent))
      {
        if (parent is T visualAncestorOfType)
          return visualAncestorOfType;
      }
      return default (T);
    }

    public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
    {
      if (parent == null)
        return default (T);
      T child1 = default (T);
      int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
      for (int childIndex = 0; childIndex < childrenCount; ++childIndex)
      {
        DependencyObject child2 = VisualTreeHelper.GetChild(parent, childIndex);
        if ((object) (child2 as T) == null)
        {
          child1 = Utils.FindChild<T>(child2, childName);
          if ((object) child1 != null)
            break;
        }
        else if (!string.IsNullOrEmpty(childName))
        {
          if (child2 is FrameworkElement frameworkElement && frameworkElement.Name == childName)
          {
            child1 = (T) child2;
            break;
          }
        }
        else
        {
          child1 = (T) child2;
          break;
        }
      }
      return child1;
    }

    public static Dispatcher CurrentDispatcher { get; set; }

    public static void ClearBackStack(this NavigationService service)
    {
      bool flag = false;
      int num = 0;
      while (service.BackStack.Any<JournalEntry>())
      {
        if (flag)
          break;
        try
        {
          ++num;
          if (num > 100)
            break;
          service.RemoveBackEntry();
        }
        catch
        {
          flag = true;
        }
      }
    }

    public static bool GetBoolValue(this IDictionary<string, string> dictionary, string key)
    {
      return dictionary.ContainsKey(key) && string.Equals(dictionary[key] ?? string.Empty, "true", StringComparison.InvariantCultureIgnoreCase);
    }

    public static bool IsNavigatedFrom(this NavigationService servie, string url)
    {
      try
      {
        JournalEntry journalEntry = servie.BackStack.LastOrDefault<JournalEntry>();
        return journalEntry != null && journalEntry.Source == new Uri(url);
      }
      catch
      {
        return false;
      }
    }
  }
}
