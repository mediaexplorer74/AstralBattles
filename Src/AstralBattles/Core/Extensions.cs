// Decompiled with JetBrains decompiler
// Type: AstralBattles.Extensions
// Assembly: AstralBattles.Core, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 6DDFE75F-AA71-406D-841A-1AF1DF23E1FF
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.Core.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Core;


namespace AstralBattles
{
  public static class Extensions
  {
    public static readonly Random Random = new Random(Environment.TickCount);

    public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
    {
      foreach (T obj in items)
        action(obj);
    }

    public static string ToStringIfNotNull(this object obj) => obj?.ToString();

    public static void InvokeOnFirstOrDefault<T>(
      this IEnumerable<T> items,
      Func<T, bool> predicate,
      Action<T> action)
      where T : class
    {
      T obj = items.FirstOrDefault<T>(predicate);
      if ((object) obj == null)
        return;
      action(obj);
    }

    public static bool TryExecute(Action action)
    {
      try
      {
        action();
        return true;
      }
      catch
      {
        return false;
      }
    }

    public static T GetRandomElement<T>(this IEnumerable<T> source)
    {
      List<T> list = source.ToList<T>();
      if (list.Count == 0)
        return default (T);
      try
      {
        return list[Extensions.Random.Next(0, list.Count)];
      }
      catch
      {
        return list[Extensions.Random.Next(0, list.Count - 1)];
      }
    }

    public static T GetValueOrDefault<T, TKey>(this IDictionary<TKey, T> dictionary, TKey key, T defaultValue = default(T))
    {
      T obj;
      return dictionary.TryGetValue(key, out obj) ? obj : defaultValue;
    }

    public static T PopValueOrDefault<T, TKey>(this IDictionary<TKey, T> dictionary, TKey key, T defaultValue = default(T))
    {
      T valueOrDefault = dictionary.GetValueOrDefault<T, TKey>(key, defaultValue);
      dictionary[key] = defaultValue;
      return valueOrDefault;
    }

    public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int parts)
    {
      int i = 0;
      return list.GroupBy<T, int>((Func<T, int>) (name => i++ % parts)).Select<IGrouping<int, T>, IEnumerable<T>>((Func<IGrouping<int, T>, IEnumerable<T>>) (part => part.AsEnumerable<T>()));
    }

    public static void ExecuteWithDelay(this Action action, int ms)
    {
      CoreDispatcher dispatcher = Window.Current.Dispatcher;
      Task.Run(async () =>
      {
        await Task.Delay(ms);
        await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action());
      });
    }

    public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> source, int limit)
    {
      return source.OrderBy<T, int>((Func<T, int>) (i => Extensions.Random.Next(1, 100))).Take<T>(limit);
    }
  }
}
