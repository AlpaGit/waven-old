// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.DamageReducedEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Protocols.CommonProtocol;

namespace Ankama.Cube.Fight.Events
{
  public class DamageReducedEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int reductionValue { get; private set; }

    public DamageReductionType reason { get; private set; }

    public DamageReducedEvent(
      int eventId,
      int? parentEventId,
      int concernedEntity,
      int reductionValue,
      DamageReductionType reason)
      : base(FightEventData.Types.EventType.DamageReduced, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.reductionValue = reductionValue;
      this.reason = reason;
    }

    public DamageReducedEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.DamageReduced, proto)
    {
      this.concernedEntity = proto.Int1;
      this.reductionValue = proto.Int2;
      this.reason = proto.DamageReductionType1;
    }
  }
}
