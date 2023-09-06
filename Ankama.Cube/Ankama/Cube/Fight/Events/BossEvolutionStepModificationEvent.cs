// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.BossEvolutionStepModificationEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Maps;
using Ankama.Cube.Maps.Objects;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class BossEvolutionStepModificationEvent : FightEvent
  {
    public int valueBefore { get; private set; }

    public int valueAfter { get; private set; }

    public BossEvolutionStepModificationEvent(
      int eventId,
      int? parentEventId,
      int valueBefore,
      int valueAfter)
      : base(FightEventData.Types.EventType.BossEvolutionStepModification, eventId, parentEventId)
    {
      this.valueBefore = valueBefore;
      this.valueAfter = valueAfter;
    }

    public BossEvolutionStepModificationEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.BossEvolutionStepModification, proto)
    {
      this.valueBefore = proto.Int1;
      this.valueAfter = proto.Int2;
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      if (fightStatus == FightStatus.local)
      {
        FightMap current = FightMap.current;
        if ((Object) null != (Object) current)
        {
          current.SetBossEvolutionStep(this.valueAfter);
          if (current.bossObject is IBossEvolution bossObject)
            yield return (object) bossObject.PlayLevelChangeAnim(this.valueBefore, this.valueAfter);
        }
      }
    }

    public override bool CanBeGroupedWith(FightEvent other) => false;

    public override bool SynchronizeExecution() => true;
  }
}
