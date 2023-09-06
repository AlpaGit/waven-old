// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.EntityPassiveFxEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Fight.Events
{
  public class EntityPassiveFxEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public EntityPassiveFxEvent(int eventId, int? parentEventId, int concernedEntity)
      : base(FightEventData.Types.EventType.EntityPassiveFx, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
    }

    public EntityPassiveFxEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.EntityPassiveFx, proto)
    {
      this.concernedEntity = proto.Int1;
    }
  }
}
