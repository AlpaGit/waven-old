// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.TimelineAssetDictionary
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Animations;
using Ankama.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Timeline;

namespace Ankama.Cube.Data
{
  [Serializable]
  public sealed class TimelineAssetDictionary : SerializableDictionary<string, TimelineAsset>
  {
    public TimelineAssetDictionary()
      : base((IEqualityComparer<string>) StringComparer.Ordinal)
    {
    }

    public void GatherLoadRoutines(List<IEnumerator> list)
    {
      foreach (TimelineAsset timelineAsset in this.Values)
        TimelineUtility.GatherTimelineResourcesLoadRoutines(timelineAsset, list);
    }

    public void Unload()
    {
      foreach (TimelineAsset timelineAsset in this.Values)
        TimelineUtility.UnloadTimelineResources(timelineAsset);
    }
  }
}
