// Decompiled with JetBrains decompiler
// Type: Ankama.Utilities.CollectionsExtensions
// Assembly: Utilities, Version=1.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 572CCA9D-04B9-4AD1-AD09-BD378D62A9F4
// Assembly location: E:\WAVEN\Waven_Data\Managed\Utilities.dll

using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Utilities
{
  [PublicAPI]
  public static class CollectionsExtensions
  {
    [PublicAPI]
    public static bool MoveNextSafe(
      [NotNull] this IEnumerator enumerator,
      CollectionsExtensions.ExceptionHandler exceptionHandler = null)
    {
      try
      {
        return enumerator.MoveNext();
      }
      catch (Exception ex)
      {
        if (exceptionHandler == null)
          Debug.LogException(ex);
        else
          exceptionHandler(ex);
        return false;
      }
    }

    [PublicAPI]
    public static bool MoveNextRecursive([NotNull] this IEnumerator enumerator)
    {
      if (enumerator.Current is IEnumerator current1 && current1.MoveNextRecursive())
        return true;
      if (!enumerator.MoveNext())
        return false;
      if (enumerator.Current is IEnumerator current2)
        current2.MoveNextRecursive();
      return true;
    }

    [PublicAPI]
    public static bool MoveNextRecursiveSafe(
      [NotNull] this IEnumerator enumerator,
      CollectionsExtensions.ExceptionHandler exceptionHandler = null)
    {
      if (enumerator.Current is IEnumerator current1 && current1.MoveNextRecursiveSafe(exceptionHandler))
        return true;
      try
      {
        if (!enumerator.MoveNext())
          return false;
      }
      catch (Exception ex)
      {
        if (exceptionHandler == null)
          Debug.LogException(ex);
        else
          exceptionHandler(ex);
        return false;
      }
      if (enumerator.Current is IEnumerator current2)
        current2.MoveNextRecursiveSafe(exceptionHandler);
      return true;
    }

    [PublicAPI]
    public static bool MoveNextRecursiveImmediate([NotNull] this IEnumerator enumerator)
    {
      while (!(enumerator.Current is IEnumerator current) || !current.MoveNextRecursiveImmediate())
      {
        if (!enumerator.MoveNext())
          return false;
        if (enumerator.Current == null)
          return true;
      }
      return true;
    }

    [PublicAPI]
    public static bool MoveNextRecursiveImmediateSafe(
      [NotNull] this IEnumerator enumerator,
      CollectionsExtensions.ExceptionHandler exceptionHandler = null)
    {
      while (!(enumerator.Current is IEnumerator current) || !current.MoveNextRecursiveImmediateSafe(exceptionHandler))
      {
        try
        {
          if (!enumerator.MoveNext())
            return false;
        }
        catch (Exception ex)
        {
          if (exceptionHandler == null)
            Debug.LogException(ex);
          else
            exceptionHandler(ex);
          return false;
        }
        if (enumerator.Current == null)
          return true;
      }
      return true;
    }

    [PublicAPI]
    public static int IndexOf<T>([NotNull] this IList<T> list, [NotNull] Predicate<T> predicate)
    {
      int count = list.Count;
      for (int index = 0; index < count; ++index)
      {
        if (predicate(list[index]))
          return index;
      }
      return -1;
    }

    [PublicAPI]
    public static int LastIndexOf<T>([NotNull] this IList<T> list, [NotNull] Predicate<T> predicate)
    {
      for (int index = list.Count - 1; index >= 0; --index)
      {
        if (predicate(list[index]))
          return index;
      }
      return -1;
    }

    [PublicAPI]
    public delegate void ExceptionHandler(Exception e);
  }
}
