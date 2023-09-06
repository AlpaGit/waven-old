// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.TeamTurnEndedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.UI.Fight;
using System.Collections;
using UnityEngine;

namespace Ankama.Cube.Fight.Events
{
  public class TeamTurnEndedEvent : FightEvent
  {
    public int turnIndex { get; private set; }

    public int teamIndex { get; private set; }

    public TeamTurnEndedEvent(int eventId, int? parentEventId, int turnIndex, int teamIndex)
      : base(FightEventData.Types.EventType.TeamTurnEnded, eventId, parentEventId)
    {
      this.turnIndex = turnIndex;
      this.teamIndex = teamIndex;
    }

    public TeamTurnEndedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.TeamTurnEnded, proto)
    {
      this.turnIndex = proto.Int1;
      this.teamIndex = proto.Int2;
    }

    public override void UpdateStatus(FightStatus fightStatus)
    {
      FightUIRework instance = FightUIRework.instance;
      if (!((Object) null != (Object) instance))
        return;
      instance.EndTurn();
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      FightUIRework instance = FightUIRework.instance;
      if ((Object) null != (Object) instance)
      {
        instance.ShowEndOfTurn();
        yield break;
      }
    }

    public override bool CanBeGroupedWith(FightEvent other) => false;

    public override bool SynchronizeExecution() => true;
  }
}
