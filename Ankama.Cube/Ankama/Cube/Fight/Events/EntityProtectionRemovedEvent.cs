// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.EntityProtectionRemovedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Fight.Events
{
  public class EntityProtectionRemovedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int protectorId { get; private set; }

    public EntityProtectionRemovedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int protectorId)
      : base(FightEventData.Types.EventType.EntityProtectionRemoved, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.protectorId = protectorId;
    }

    public EntityProtectionRemovedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.EntityProtectionRemoved, proto)
    {
      this.concernedEntity = proto.Int1;
      this.protectorId = proto.Int2;
    }
  }
}
