// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.Utility.ListsDiff`1
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections.Generic;
using System.Linq;

namespace Ankama.Cube.Data.Utility
{
  public class ListsDiff<T>
  {
    private static readonly IReadOnlyList<T> EmptyList = (IReadOnlyList<T>) new T[0];

    public IReadOnlyList<T> added { get; }

    public IReadOnlyList<T> removed { get; }

    public ListsDiff(IReadOnlyList<T> oldList, IReadOnlyList<T> newList)
    {
      this.removed = ListsDiff<T>.FindOnlyInFirst(oldList, newList);
      this.added = ListsDiff<T>.FindOnlyInFirst(newList, oldList);
    }

    private static IReadOnlyList<T> FindOnlyInFirst(IReadOnlyList<T> first, IReadOnlyList<T> second)
    {
      List<T> objList = (List<T>) null;
      foreach (T obj in (IEnumerable<T>) first)
      {
        if (!((IEnumerable<T>) second).Contains<T>(obj))
        {
          if (objList == null)
            objList = new List<T>();
          objList.Add(obj);
        }
      }
      return (IReadOnlyList<T>) objList ?? ListsDiff<T>.EmptyList;
    }
  }
}
