// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.BossObjectOssuaire
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Animations;
using Ankama.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;

namespace Ankama.Cube.Maps.Objects
{
  public class BossObjectOssuaire : BossObject, IBossEvolution, IBossSpell
  {
    protected const string IdleAnim0 = "phase_1";
    protected const string IdleAnim1 = "phase_2";
    protected const string IdleAnim2 = "phase_3";
    [Header("FX Anim")]
    [SerializeField]
    private TimelineAsset m_transition01;
    [SerializeField]
    private TimelineAsset m_transition12;
    [SerializeField]
    private TimelineAsset m_transition21;
    [SerializeField]
    private TimelineAsset m_transition10;
    [SerializeField]
    private TimelineAsset m_transition20;
    [SerializeField]
    private TimelineAsset m_launchSpell;
    private int m_level;

    protected override string spawnAnimation => "spawn";

    protected override string deathAnimation => "hit1";

    protected override IEnumerator LoadTimelines()
    {
      yield return (object) base.LoadTimelines();
      yield return (object) TimelineUtility.LoadTimelineResources(this.m_transition01);
      yield return (object) TimelineUtility.LoadTimelineResources(this.m_transition12);
      yield return (object) TimelineUtility.LoadTimelineResources(this.m_transition21);
      yield return (object) TimelineUtility.LoadTimelineResources(this.m_transition10);
      yield return (object) TimelineUtility.LoadTimelineResources(this.m_transition20);
      yield return (object) TimelineUtility.LoadTimelineResources(this.m_launchSpell);
    }

    protected override void UnloadTimelines()
    {
      base.UnloadTimelines();
      TimelineUtility.UnloadTimelineResources(this.m_transition01);
      TimelineUtility.UnloadTimelineResources(this.m_transition12);
      TimelineUtility.UnloadTimelineResources(this.m_transition21);
      TimelineUtility.UnloadTimelineResources(this.m_transition10);
      TimelineUtility.UnloadTimelineResources(this.m_transition20);
      TimelineUtility.UnloadTimelineResources(this.m_launchSpell);
    }

    public override void GoToIdle() => this.PlayAnimation(this.GetIdleAnim(this.m_level), loop: true, restart: false);

    public IEnumerator PlayLevelChangeAnim(int valueBefore, int valueAfter)
    {
      BossObjectOssuaire bossObjectOssuaire = this;
      if (bossObjectOssuaire.m_level != valueBefore)
        Log.Warning(string.Format("Level desynchro !! client : {0} / server : {1} ", (object) bossObjectOssuaire.m_level, (object) valueBefore), 64, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\BossObjectOssuaire.cs");
      bossObjectOssuaire.m_level = valueAfter;
      TimelineAsset levelTimeline = bossObjectOssuaire.GetLevelTimeline(valueBefore, valueAfter);
      yield return (object) bossObjectOssuaire.PlayTimeline(levelTimeline);
      bossObjectOssuaire.GoToIdle();
    }

    public void OnLevelChangeAnimEvent() => this.m_animator2D.SetAnimation(this.GetIdleAnim(this.m_level), true, true, false);

    public IEnumerator PlaySpellAnim(int spellId)
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      BossObjectOssuaire bossObjectOssuaire = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        bossObjectOssuaire.GoToIdle();
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (object) bossObjectOssuaire.PlayTimeline(bossObjectOssuaire.m_launchSpell);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    private string GetIdleAnim(int level)
    {
      switch (level)
      {
        case 0:
          return "phase_1";
        case 1:
          return "phase_2";
        case 2:
          return "phase_3";
        default:
          Log.Warning(string.Format("Cannot find idle anim for level {0}", (object) level), 101, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\BossObjectOssuaire.cs");
          return "phase_1";
      }
    }

    private TimelineAsset GetLevelTimeline(int previous, int next)
    {
      if (next > previous)
      {
        if (next == 1)
          return this.m_transition01;
        if (next == 2)
          return this.m_transition12;
      }
      else
      {
        switch (next)
        {
          case 0:
            return previous != 1 ? this.m_transition20 : this.m_transition10;
          case 1:
            return this.m_transition21;
        }
      }
      Log.Warning(string.Format("Cannot find anim for leveling {0} -> {1}", (object) previous, (object) next), 125, "C:\\BuildAgents\\AgentA\\work\\cub_client_win64_master\\client\\DofusCube.Unity\\Assets\\Core\\Code\\Maps\\Objects\\BossObjectOssuaire.cs");
      return this.m_transition01;
    }
  }
}
