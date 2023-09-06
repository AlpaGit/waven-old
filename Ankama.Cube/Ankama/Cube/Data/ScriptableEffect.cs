// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.ScriptableEffect
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public abstract class ScriptableEffect : ScriptableObject
  {
    [NonSerialized]
    protected ScriptableEffect.InitializationState m_initializationState;
    [NonSerialized]
    private int m_referenceCounter;

    public IEnumerator Load()
    {
      ++this.m_referenceCounter;
      switch (this.m_initializationState)
      {
        case ScriptableEffect.InitializationState.None:
          this.m_initializationState = ScriptableEffect.InitializationState.Loading;
          yield return (object) this.LoadInternal();
          break;
        case ScriptableEffect.InitializationState.Loading:
          do
          {
            yield return (object) null;
          }
          while (this.m_initializationState == ScriptableEffect.InitializationState.Loading);
          break;
        case ScriptableEffect.InitializationState.Loaded:
          break;
        case ScriptableEffect.InitializationState.Failed:
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public void Unload()
    {
      --this.m_referenceCounter;
      if (this.m_referenceCounter > 0)
        return;
      switch (this.m_initializationState)
      {
        case ScriptableEffect.InitializationState.None:
          break;
        case ScriptableEffect.InitializationState.Loading:
        case ScriptableEffect.InitializationState.Loaded:
          this.UnloadInternal();
          this.m_initializationState = ScriptableEffect.InitializationState.None;
          break;
        case ScriptableEffect.InitializationState.Failed:
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    protected abstract IEnumerator LoadInternal();

    protected abstract void UnloadInternal();

    public static IEnumerator LoadAll<T>(ICollection<T> effects) where T : ScriptableEffect
    {
      int count = effects.Count;
      switch (count)
      {
        case 0:
          break;
        case 1:
          foreach (T effect in (IEnumerable<T>) effects)
            yield return (object) effect.Load();
          break;
        default:
          IEnumerator[] enumeratorArray = new IEnumerator[count];
          int index = 0;
          foreach (T effect in (IEnumerable<T>) effects)
          {
            enumeratorArray[index] = effect.Load();
            ++index;
          }
          yield return (object) EnumeratorUtility.ParallelRecursiveImmediateSafeExecution(enumeratorArray);
          break;
      }
    }

    protected enum InitializationState
    {
      None,
      Loading,
      Loaded,
      Failed,
    }
  }
}
