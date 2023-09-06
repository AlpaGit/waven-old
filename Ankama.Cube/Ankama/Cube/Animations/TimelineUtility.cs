// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.TimelineUtility
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

namespace Ankama.Cube.Animations
{
  public static class TimelineUtility
  {
    public static void GatherTimelineResourcesLoadRoutines(
      TimelineAsset timelineAsset,
      List<IEnumerator> list)
    {
      if ((Object) null == (Object) timelineAsset)
        return;
      foreach (TrackAsset outputTrack in timelineAsset.GetOutputTracks())
      {
        foreach (TimelineClip clip in outputTrack.GetClips())
        {
          if (clip.asset is ITimelineResourcesProvider asset)
          {
            IEnumerator enumerator = asset.LoadResources();
            list.Add(enumerator);
          }
        }
      }
    }

    public static IEnumerator LoadTimelineResources(TimelineAsset timelineAsset)
    {
      if (!((Object) null == (Object) timelineAsset))
      {
        int outputTrackCount = timelineAsset.outputTrackCount;
        if (outputTrackCount != 0)
        {
          List<IEnumerator> loadRoutine = ListPool<IEnumerator>.Get(outputTrackCount);
          foreach (TrackAsset outputTrack in timelineAsset.GetOutputTracks())
          {
            foreach (TimelineClip clip in outputTrack.GetClips())
            {
              if (clip.asset is ITimelineResourcesProvider asset)
                loadRoutine.Add(asset.LoadResources());
            }
          }
          yield return (object) EnumeratorUtility.ParallelRecursiveImmediateSafeExecution(loadRoutine.ToArray());
          ListPool<IEnumerator>.Release(loadRoutine);
        }
      }
    }

    public static void UnloadTimelineResources(TimelineAsset timelineAsset)
    {
      if ((Object) null == (Object) timelineAsset)
        return;
      foreach (TrackAsset outputTrack in timelineAsset.GetOutputTracks())
      {
        foreach (TimelineClip clip in outputTrack.GetClips())
        {
          if (clip.asset is ITimelineResourcesProvider asset)
            asset.UnloadResources();
        }
      }
    }
  }
}
