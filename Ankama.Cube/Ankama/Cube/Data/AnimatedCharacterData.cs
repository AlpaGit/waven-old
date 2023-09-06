// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.AnimatedCharacterData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Animations;
using Ankama.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

namespace Ankama.Cube.Data
{
  public abstract class AnimatedCharacterData : ScriptableObject, ITimelineAssetProvider
  {
    [SerializeField]
    protected int m_areaSize = 1;
    [HideInInspector]
    [SerializeField]
    protected TimelineAssetDictionary m_timelineAssetDictionary;
    [SerializeField]
    protected CharacterEffect m_spawnEffect;
    [SerializeField]
    protected CharacterEffect m_deathEffect;
    [NonSerialized]
    private int m_referenceCounter;
    [NonSerialized]
    private AnimatedCharacterData.TimelineResourceLoadingState m_timelineResourcesLoadState;

    public int areaSize => this.m_areaSize;

    public CharacterEffect spawnEffect => this.m_spawnEffect;

    public CharacterEffect deathEffect => this.m_deathEffect;

    public IEnumerator LoadTimelineResources()
    {
      ++this.m_referenceCounter;
      switch (this.m_timelineResourcesLoadState)
      {
        case AnimatedCharacterData.TimelineResourceLoadingState.None:
          this.m_timelineResourcesLoadState = AnimatedCharacterData.TimelineResourceLoadingState.Loading;
          List<IEnumerator> loadRoutines = ListPool<IEnumerator>.Get(4);
          try
          {
            this.m_timelineAssetDictionary.GatherLoadRoutines(loadRoutines);
            if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_spawnEffect)
              loadRoutines.Add(this.m_spawnEffect.Load());
            if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_deathEffect)
              loadRoutines.Add(this.m_deathEffect.Load());
            this.GatherAdditionalResourcesLoadingRoutines(loadRoutines);
          }
          catch (Exception ex)
          {
            Debug.LogException(ex);
            this.m_timelineResourcesLoadState = AnimatedCharacterData.TimelineResourceLoadingState.Failed;
          }
          yield return (object) EnumeratorUtility.ParallelRecursiveImmediateSafeExecution(loadRoutines.ToArray());
          ListPool<IEnumerator>.Release(loadRoutines);
          this.m_timelineResourcesLoadState = AnimatedCharacterData.TimelineResourceLoadingState.Loaded;
          break;
        case AnimatedCharacterData.TimelineResourceLoadingState.Loading:
          while (this.m_timelineResourcesLoadState == AnimatedCharacterData.TimelineResourceLoadingState.Loading)
            yield return (object) null;
          break;
        case AnimatedCharacterData.TimelineResourceLoadingState.Loaded:
          break;
        case AnimatedCharacterData.TimelineResourceLoadingState.Failed:
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public void UnloadTimelineResources()
    {
      --this.m_referenceCounter;
      if (this.m_referenceCounter > 0)
        return;
      switch (this.m_timelineResourcesLoadState)
      {
        case AnimatedCharacterData.TimelineResourceLoadingState.None:
          break;
        case AnimatedCharacterData.TimelineResourceLoadingState.Loading:
        case AnimatedCharacterData.TimelineResourceLoadingState.Loaded:
          this.m_timelineAssetDictionary.Unload();
          if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_spawnEffect)
            this.m_spawnEffect.Unload();
          if ((UnityEngine.Object) null != (UnityEngine.Object) this.m_deathEffect)
            this.m_deathEffect.Unload();
          this.UnloadAdditionalResources();
          this.m_timelineResourcesLoadState = AnimatedCharacterData.TimelineResourceLoadingState.None;
          break;
        case AnimatedCharacterData.TimelineResourceLoadingState.Failed:
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public bool HasTimelineAsset(string currentTimelineKey) => this.m_timelineAssetDictionary.ContainsKey(currentTimelineKey);

    public bool TryGetTimelineAsset(string key, out TimelineAsset timelineAsset) => this.m_timelineAssetDictionary.TryGetValue(key, out timelineAsset);

    protected abstract void GatherAdditionalResourcesLoadingRoutines(List<IEnumerator> routines);

    protected abstract void UnloadAdditionalResources();

    private enum TimelineResourceLoadingState
    {
      None,
      Loading,
      Loaded,
      Failed,
    }
  }
}
