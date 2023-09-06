// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.ReserveUsedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Fight.Events
{
  public class ReserveUsedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int valueBefore { get; private set; }

    public ReserveUsedEvent(int eventId, int? parentEventId, int concernedEntity, int valueBefore)
      : base(FightEventData.Types.EventType.ReserveUsed, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.valueBefore = valueBefore;
    }

    public ReserveUsedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.ReserveUsed, proto)
    {
      this.concernedEntity = proto.Int1;
      this.valueBefore = proto.Int2;
    }
  }
}
