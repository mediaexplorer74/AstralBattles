
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Core;


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

    public static CoreDispatcher CurrentDispatcher { get; set; }

    public static void ClearBackStack(object service)
    {
      // UWP navigation uses Frame.BackStack instead of NavigationService
      // This method is commented out for MVP - implement Frame-based navigation later
    }

    public static bool GetBoolValue(this IDictionary<string, string> dictionary, string key)
    {
      return dictionary.ContainsKey(key) && string.Equals(dictionary[key] ?? string.Empty, "true", StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsNavigatedFrom(object service, string url)
    {
      // UWP navigation - stub for MVP
      return false;
    }
  }
}
