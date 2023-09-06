// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.BossReserveModificationEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections;

namespace Ankama.Cube.Fight.Events
{
  public class BossReserveModificationEvent : FightEvent
  {
    public int valueBefore { get; private set; }

    public int valueAfter { get; private set; }

    public BossReserveModificationEvent(
      int eventId,
      int? parentEventId,
      int valueBefore,
      int valueAfter)
      : base(FightEventData.Types.EventType.BossReserveModification, eventId, parentEventId)
    {
      this.valueBefore = valueBefore;
      this.valueAfter = valueAfter;
    }

    public BossReserveModificationEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.BossReserveModification, proto)
    {
      this.valueBefore = proto.Int1;
      this.valueAfter = proto.Int2;
    }

    public override IEnumerator UpdateView(FightStatus fightStatus)
    {
      FightStatus local = FightStatus.local;
      yield break;
    }
  }
}
