// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.EntityProtectionAddedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Fight.Events
{
  public class EntityProtectionAddedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int protectorId { get; private set; }

    public int fixedValue { get; private set; }

    public int percentsValues { get; private set; }

    public EntityProtectionAddedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int protectorId,
      int fixedValue,
      int percentsValues)
      : base(FightEventData.Types.EventType.EntityProtectionAdded, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.protectorId = protectorId;
      this.fixedValue = fixedValue;
      this.percentsValues = percentsValues;
    }

    public EntityProtectionAddedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.EntityProtectionAdded, proto)
    {
      this.concernedEntity = proto.Int1;
      this.protectorId = proto.Int2;
      this.fixedValue = proto.Int3;
      this.percentsValues = proto.Int4;
    }
  }
}
