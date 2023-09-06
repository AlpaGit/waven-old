// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Extensions.PlayableDirectorExtensions
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine.Playables;

namespace Ankama.Cube.Extensions
{
  public static class PlayableDirectorExtensions
  {
    public static bool HasReachedEndOfAnimation(this PlayableDirector director)
    {
      PlayableGraph playableGraph = director.playableGraph;
      return !playableGraph.IsValid() || playableGraph.IsDone();
    }
  }
}
