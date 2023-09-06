// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.DiceThrownEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Fight.Events
{
  public class DiceThrownEvent : FightEvent, IRelatedToEntity
  {
    public int concernedEntity { get; private set; }

    public int diceValue { get; private set; }

    public DiceThrownEvent(int eventId, int? parentEventId, int concernedEntity, int diceValue)
      : base(FightEventData.Types.EventType.DiceThrown, eventId, parentEventId)
    {
      this.concernedEntity = concernedEntity;
      this.diceValue = diceValue;
    }

    public DiceThrownEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.DiceThrown, proto)
    {
      this.concernedEntity = proto.Int1;
      this.diceValue = proto.Int2;
    }
  }
}
