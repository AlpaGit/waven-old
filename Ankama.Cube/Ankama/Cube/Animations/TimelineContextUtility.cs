// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.TimelineContextUtility
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Playables;

namespace Ankama.Cube.Animations
{
  public static class TimelineContextUtility
  {
    public const string FightContextPropertyName = "fight";
    public const string ContextPropertyName = "context";

    public static FightContext GetFightContext(PlayableGraph graph)
    {
      PropertyName id = new PropertyName("fight");
      return graph.GetResolver().GetReferenceValue(id, out bool _) as FightContext;
    }

    public static void SetFightContext([NotNull] PlayableDirector playableDirector, FightContext fightContext)
    {
      PropertyName id = new PropertyName("fight");
      playableDirector.SetReferenceValue(id, (Object) fightContext);
    }

    public static void ClearFightContext([NotNull] PlayableDirector playableDirector)
    {
      PropertyName id = new PropertyName("fight");
      playableDirector.ClearReferenceValue(id);
    }

    public static T GetContext<T>(PlayableGraph graph) where T : class, ITimelineContext
    {
      PropertyName id = new PropertyName("context");
      return !(graph.GetResolver().GetReferenceValue(id, out bool _) is ITimelineContextProvider referenceValue) ? default (T) : referenceValue.GetTimelineContext() as T;
    }

    public static void SetContextProvider(
      [NotNull] PlayableDirector playableDirector,
      [NotNull] ITimelineContextProvider provider)
    {
      PropertyName id = new PropertyName("context");
      Object timelineBinding = provider.GetTimelineBinding();
      playableDirector.SetReferenceValue(id, timelineBinding);
    }

    public static void ClearContextProvider([NotNull] PlayableDirector playableDirector)
    {
      PropertyName id = new PropertyName("context");
      playableDirector.ClearReferenceValue(id);
    }
  }
}
