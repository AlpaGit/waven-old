// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.TimelineAssetCharacterEffect
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Animations;
using Ankama.Cube.Fight;
using Ankama.Cube.Maps.VisualEffects;
using Ankama.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Ankama.Cube.Data
{
  [CreateAssetMenu(menuName = "Waven/Data/Character Effects/Timeline Asset")]
  public sealed class TimelineAssetCharacterEffect : CharacterEffect, ICharacterEffectWithTimeline
  {
    [SerializeField]
    private TimelineAsset m_timelineAsset;

    protected override IEnumerator LoadInternal()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      TimelineAssetCharacterEffect assetCharacterEffect = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        assetCharacterEffect.m_initializationState = ScriptableEffect.InitializationState.Loaded;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) TimelineUtility.LoadTimelineResources(assetCharacterEffect.m_timelineAsset);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    protected override void UnloadInternal()
    {
      TimelineUtility.UnloadTimelineResources(this.m_timelineAsset);
      this.m_initializationState = ScriptableEffect.InitializationState.None;
    }

    public override Component Instantiate(
      Transform parent,
      ITimelineContextProvider contextProvider)
    {
      if ((Object) null == (Object) this.m_timelineAsset)
      {
        Log.Warning("Tried to instantiate timeline asset character effect named '" + this.name + "' without a timeline asset setup.", 44, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Animations\\TimelineAssetCharacterEffect.cs");
        return (Component) null;
      }
      Quaternion rotation = Quaternion.identity;
      Vector3 scale = Vector3.one;
      if (contextProvider != null && contextProvider.GetTimelineContext() is VisualEffectContext timelineContext)
        timelineContext.GetVisualEffectTransformation(out rotation, out scale);
      return (Component) FightObjectFactory.CreateTimelineAssetEffectInstance(this.m_timelineAsset, parent, rotation, scale, (FightContext) null, contextProvider);
    }

    public override IEnumerator DestroyWhenFinished(Component instance)
    {
      PlayableDirector playableDirector = (PlayableDirector) instance;
      PlayableGraph playableGraph;
      do
      {
        yield return (object) null;
        if ((Object) null == (Object) playableDirector)
          yield break;
        else
          playableGraph = playableDirector.playableGraph;
      }
      while (playableGraph.IsValid() && !playableGraph.IsDone());
      FightObjectFactory.DestroyTimelineAssetEffectInstance(playableDirector, false);
    }
  }
}
