// Decompiled with JetBrains decompiler
// Type: Ankama.Utilities.MonoBehaviourExtensions
// Assembly: Utilities, Version=1.10.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 572CCA9D-04B9-4AD1-AD09-BD378D62A9F4
// Assembly location: E:\WAVEN\Waven_Data\Managed\Utilities.dll

using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

namespace Ankama.Utilities
{
  [PublicAPI]
  public static class MonoBehaviourExtensions
  {
    [PublicAPI]
    public static Coroutine StartCoroutineImmediate(
      this MonoBehaviour monoBehaviour,
      [NotNull] IEnumerator routine)
    {
      return monoBehaviour.StartCoroutine(MonoBehaviourExtensions.ExecuteRoutineImmediate(routine));
    }

    [PublicAPI]
    public static Coroutine StartCoroutineImmediateSafe(
      this MonoBehaviour monoBehaviour,
      [NotNull] IEnumerator routine,
      CollectionsExtensions.ExceptionHandler exceptionHandler = null)
    {
      return monoBehaviour.StartCoroutine(MonoBehaviourExtensions.ExecuteRoutineImmediateSafe(routine, exceptionHandler));
    }

    private static IEnumerator ExecuteRoutineImmediate(IEnumerator routine)
    {
      object previousYieldInstruction = (object) null;
      object yieldInstruction;
      while (MonoBehaviourExtensions.MoveNextRecursiveImmediate(routine, previousYieldInstruction, out yieldInstruction))
      {
        previousYieldInstruction = yieldInstruction;
        yield return yieldInstruction;
      }
    }

    private static IEnumerator ExecuteRoutineImmediateSafe(
      IEnumerator routine,
      CollectionsExtensions.ExceptionHandler exceptionHandler)
    {
      object previousYieldInstruction = (object) null;
      object yieldInstruction;
      while (MonoBehaviourExtensions.MoveNextRecursiveImmediateSafe(routine, previousYieldInstruction, exceptionHandler, out yieldInstruction))
      {
        previousYieldInstruction = yieldInstruction;
        yield return yieldInstruction;
      }
    }

    private static bool MoveNextRecursiveImmediate(
      IEnumerator enumerator,
      object previousYieldInstruction,
      out object yieldInstruction)
    {
      do
      {
        object current = enumerator.Current;
        if (current != null)
        {
          if (!(current is IEnumerator enumerator1))
          {
            if (current is YieldInstruction yieldInstruction1 && yieldInstruction1 != previousYieldInstruction)
            {
              yieldInstruction = (object) yieldInstruction1;
              return true;
            }
          }
          else if (MonoBehaviourExtensions.MoveNextRecursiveImmediate(enumerator1, previousYieldInstruction, out yieldInstruction))
            return true;
        }
        if (!enumerator.MoveNext())
        {
          yieldInstruction = (object) null;
          return false;
        }
      }
      while (enumerator.Current != null);
      yieldInstruction = (object) null;
      return true;
    }

    private static bool MoveNextRecursiveImmediateSafe(
      IEnumerator enumerator,
      object previousYieldInstruction,
      CollectionsExtensions.ExceptionHandler exceptionHandler,
      out object yieldInstruction)
    {
      do
      {
        object current = enumerator.Current;
        if (current != null)
        {
          if (!(current is IEnumerator enumerator1))
          {
            if (current is YieldInstruction yieldInstruction1 && yieldInstruction1 != previousYieldInstruction)
            {
              yieldInstruction = (object) yieldInstruction1;
              return true;
            }
          }
          else if (MonoBehaviourExtensions.MoveNextRecursiveImmediateSafe(enumerator1, previousYieldInstruction, exceptionHandler, out yieldInstruction))
            return true;
        }
        try
        {
          if (!enumerator.MoveNext())
          {
            yieldInstruction = (object) null;
            return false;
          }
        }
        catch (Exception ex)
        {
          if (exceptionHandler == null)
            Debug.LogException(ex);
          else
            exceptionHandler(ex);
          yieldInstruction = (object) null;
          return false;
        }
      }
      while (enumerator.Current != null);
      yieldInstruction = (object) null;
      return true;
    }
  }
}
