// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.BossCastSpellEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Maps;
using Ankama.Cube.Maps.Objects;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class BossCastSpellEvent : FightEvent
  {
    public int spellId { get; private set; }

    public BossCastSpellEvent(int eventId, int? parentEventId, int spellId)
      : base(FightEventData.Types.EventType.BossCastSpell, eventId, parentEventId)
    {
      this.spellId = spellId;
    }

    public BossCastSpellEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.BossCastSpell, proto)
    {
      this.spellId = proto.Int1;
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      if (fightStatus == FightStatus.local)
      {
        FightMap current = FightMap.current;
        if ((Object) null != (Object) current && current.bossObject is IBossSpell bossObject)
          yield return (object) bossObject.PlaySpellAnim(this.spellId);
      }
    }

    public override bool CanBeGroupedWith(FightEvent other) => false;

    public override bool SynchronizeExecution() => true;
  }
}
