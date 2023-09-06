// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.TimelineAssetSpellEffect
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Animations;
using Ankama.Cube.Fight;
using Ankama.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Ankama.Cube.Data
{
  [CreateAssetMenu(menuName = "Waven/Data/Spell Effects/Timeline Effect")]
  public class TimelineAssetSpellEffect : SpellEffect, ISpellEffectWithTimeline
  {
    [SerializeField]
    private TimelineAsset m_timelineAsset;

    protected override IEnumerator LoadInternal()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      TimelineAssetSpellEffect assetSpellEffect = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        assetSpellEffect.m_initializationState = ScriptableEffect.InitializationState.Loaded;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) TimelineUtility.LoadTimelineResources(assetSpellEffect.m_timelineAsset);
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
      Quaternion rotation,
      Vector3 scale,
      FightContext fightContext,
      ITimelineContextProvider contextProvider)
    {
      if (!((Object) null == (Object) this.m_timelineAsset))
        return (Component) FightObjectFactory.CreateTimelineAssetEffectInstance(this.m_timelineAsset, parent, rotation, scale, fightContext, contextProvider);
      Log.Warning("Tried to instantiate timeline asset spell effect named '" + this.name + "' without a timeline asset setup.", 42, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Data\\Animations\\TimelineAssetSpellEffect.cs");
      return (Component) null;
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
      FightObjectFactory.DestroyTimelineAssetEffectInstance(playableDirector, true);
    }
  }
}
