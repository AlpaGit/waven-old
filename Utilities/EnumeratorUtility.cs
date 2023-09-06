// Decompiled with JetBrains decompiler
// Type: Ankama.Utilities.EnumeratorUtility
// Assembly: Utilities, Version=1.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 572CCA9D-04B9-4AD1-AD09-BD378D62A9F4
// Assembly location: E:\WAVEN\Waven_Data\Managed\Utilities.dll

using JetBrains.Annotations;
using System.Collections;

namespace Ankama.Utilities
{
  [PublicAPI]
  public class EnumeratorUtility
  {
    [PublicAPI]
    public static IEnumerator Empty()
    {
      yield break;
    }

    [PublicAPI]
    public static IEnumerator ParallelExecution([NotNull] params IEnumerator[] actions)
    {
      while (true)
      {
        bool flag = true;
        int length = actions.Length;
        for (int index = 0; index < length; ++index)
        {
          IEnumerator action = actions[index];
          if (action != null && action.MoveNext())
            flag = false;
        }
        if (!flag)
          yield return (object) null;
        else
          break;
      }
    }

    [PublicAPI]
    public static IEnumerator ParallelSafeExecution([NotNull] params IEnumerator[] actions)
    {
      while (true)
      {
        bool flag = true;
        int length = actions.Length;
        for (int index = 0; index < length; ++index)
        {
          IEnumerator action = actions[index];
          if (action != null && action.MoveNextSafe())
            flag = false;
        }
        if (!flag)
          yield return (object) null;
        else
          break;
      }
    }

    [PublicAPI]
    public static IEnumerator ParallelSafeExecution(
      [NotNull] IEnumerator[] actions,
      [CanBeNull] CollectionsExtensions.ExceptionHandler exceptionHandler)
    {
      while (true)
      {
        bool flag = true;
        int length = actions.Length;
        for (int index = 0; index < length; ++index)
        {
          IEnumerator action = actions[index];
          if (action != null && action.MoveNextSafe(exceptionHandler))
            flag = false;
        }
        if (!flag)
          yield return (object) null;
        else
          break;
      }
    }

    [PublicAPI]
    public static IEnumerator ParallelRecursiveExecution([NotNull] params IEnumerator[] actions)
    {
      while (true)
      {
        bool flag = true;
        int length = actions.Length;
        for (int index = 0; index < length; ++index)
        {
          IEnumerator action = actions[index];
          if (action != null && action.MoveNextRecursive())
            flag = false;
        }
        if (!flag)
          yield return (object) null;
        else
          break;
      }
    }

    [PublicAPI]
    public static IEnumerator ParallelRecursiveSafeExecution([NotNull] params IEnumerator[] actions)
    {
      while (true)
      {
        bool flag = true;
        int length = actions.Length;
        for (int index = 0; index < length; ++index)
        {
          IEnumerator action = actions[index];
          if (action != null && action.MoveNextRecursiveSafe())
            flag = false;
        }
        if (!flag)
          yield return (object) null;
        else
          break;
      }
    }

    [PublicAPI]
    public static IEnumerator ParallelRecursiveSafeExecution(
      [NotNull] IEnumerator[] actions,
      [CanBeNull] CollectionsExtensions.ExceptionHandler exceptionHandler)
    {
      while (true)
      {
        bool flag = true;
        int length = actions.Length;
        for (int index = 0; index < length; ++index)
        {
          IEnumerator action = actions[index];
          if (action != null && action.MoveNextRecursiveSafe(exceptionHandler))
            flag = false;
        }
        if (!flag)
          yield return (object) null;
        else
          break;
      }
    }

    [PublicAPI]
    public static IEnumerator ParallelRecursiveImmediateExecution([NotNull] params IEnumerator[] actions)
    {
      while (true)
      {
        bool flag = true;
        int length = actions.Length;
        for (int index = 0; index < length; ++index)
        {
          IEnumerator action = actions[index];
          if (action != null && action.MoveNextRecursiveImmediate())
            flag = false;
        }
        if (!flag)
          yield return (object) null;
        else
          break;
      }
    }

    [PublicAPI]
    public static IEnumerator ParallelRecursiveImmediateSafeExecution([NotNull] params IEnumerator[] actions)
    {
      while (true)
      {
        bool flag = true;
        int length = actions.Length;
        for (int index = 0; index < length; ++index)
        {
          IEnumerator action = actions[index];
          if (action != null && action.MoveNextRecursiveImmediateSafe())
            flag = false;
        }
        if (!flag)
          yield return (object) null;
        else
          break;
      }
    }

    [PublicAPI]
    public static IEnumerator ParallelRecursiveImmediateSafeExecution(
      [NotNull] IEnumerator[] actions,
      [CanBeNull] CollectionsExtensions.ExceptionHandler exceptionHandler)
    {
      while (true)
      {
        bool flag = true;
        int length = actions.Length;
        for (int index = 0; index < length; ++index)
        {
          IEnumerator action = actions[index];
          if (action != null && action.MoveNextRecursiveImmediateSafe(exceptionHandler))
            flag = false;
        }
        if (!flag)
          yield return (object) null;
        else
          break;
      }
    }
  }
}
