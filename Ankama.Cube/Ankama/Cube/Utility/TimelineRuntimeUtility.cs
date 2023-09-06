// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Utility.TimelineRuntimeUtility
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections.Generic;
using UnityEngine.Playables;

namespace Ankama.Cube.Utility
{
  public static class TimelineRuntimeUtility
  {
    public static IEnumerable<T> EnumerateBehaviours<T>(PlayableGraph playableGraph) where T : class, IPlayableBehaviour, new()
    {
      if (playableGraph.IsValid())
      {
        int rootPlayableCount = playableGraph.GetRootPlayableCount();
        for (int rootPlayableIndex = 0; rootPlayableIndex < rootPlayableCount; ++rootPlayableIndex)
        {
          foreach (T enumerateBehaviour in TimelineRuntimeUtility.EnumerateBehaviours<T>(playableGraph.GetRootPlayable(rootPlayableIndex)))
            yield return enumerateBehaviour;
        }
      }
    }

    public static IEnumerable<T> EnumerateBehaviours<T>(Playable playable) where T : class, IPlayableBehaviour, new()
    {
      if (playable.IsValid<Playable>())
      {
        int inputCount = playable.GetInputCount<Playable>();
        for (int inputIndex = 0; inputIndex < inputCount; ++inputIndex)
        {
          foreach (T enumerateBehaviour in TimelineRuntimeUtility.EnumerateBehaviours<T>(playable.GetInput<Playable>(inputIndex)))
            yield return enumerateBehaviour;
        }
        if (playable.GetPlayableType() == typeof (T))
          yield return ((ScriptPlayable<T>) playable).GetBehaviour();
      }
    }
  }
}
