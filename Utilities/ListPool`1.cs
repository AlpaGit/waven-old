// Decompiled with JetBrains decompiler
// Type: Ankama.Utilities.ListPool`1
// Assembly: Utilities, Version=1.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 572CCA9D-04B9-4AD1-AD09-BD378D62A9F4
// Assembly location: E:\WAVEN\Waven_Data\Managed\Utilities.dll

using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Ankama.Utilities
{
  [PublicAPI]
  public static class ListPool<T>
  {
    private static readonly Stack<List<T>> s_stack = new Stack<List<T>>(2);
    private static int s_maxSize;

    public static int maxSize
    {
      get => ListPool<T>.s_maxSize;
      set
      {
        value = Math.Max(0, value);
        if (value == ListPool<T>.s_maxSize)
          return;
        Stack<List<T>> stack = ListPool<T>.s_stack;
        while (stack.Count > value)
          stack.Pop();
        ListPool<T>.s_maxSize = value;
      }
    }

    [PublicAPI]
    public static List<T> Get() => ListPool<T>.s_stack.Count <= 0 ? new List<T>() : ListPool<T>.s_stack.Pop();

    [PublicAPI]
    public static List<T> Get(int capacity)
    {
      List<T> objList;
      if (ListPool<T>.s_stack.Count > 0)
      {
        objList = ListPool<T>.s_stack.Pop();
        if (objList.Capacity < capacity)
          objList.Capacity = capacity;
      }
      else
        objList = new List<T>(capacity);
      return objList;
    }

    [PublicAPI]
    public static List<T> Get(int capacity, int maxPoolSize)
    {
      int capacity1 = Math.Min(capacity, maxPoolSize);
      List<T> objList;
      if (ListPool<T>.s_stack.Count > 0)
      {
        objList = ListPool<T>.s_stack.Pop();
        if (objList.Capacity < capacity1)
          objList.Capacity = capacity1;
      }
      else
        objList = new List<T>(capacity1);
      ListPool<T>.s_maxSize = maxPoolSize;
      return objList;
    }

    [PublicAPI]
    public static bool Release(List<T> list)
    {
      list.Clear();
      if (ListPool<T>.s_maxSize != 0 && ListPool<T>.s_stack.Count >= ListPool<T>.s_maxSize)
        return false;
      ListPool<T>.s_stack.Push(list);
      return true;
    }

    [PublicAPI]
    public static int Count() => ListPool<T>.s_stack.Count;

    [PublicAPI]
    public static bool Contains(List<T> list) => ListPool<T>.s_stack.Contains(list);

    [PublicAPI]
    public static void Clear() => ListPool<T>.s_stack.Clear();
  }
}
