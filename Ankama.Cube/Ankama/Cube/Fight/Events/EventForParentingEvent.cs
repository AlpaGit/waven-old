// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.EventForParentingEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

namespace Ankama.Cube.Fight.Events
{
  public class EventForParentingEvent : FightEvent
  {
    public EventForParentingEvent(int eventId, int? parentEventId)
      : base(FightEventData.Types.EventType.EventForParenting, eventId, parentEventId)
    {
    }

    public EventForParentingEvent(FightEventData proto)
      : base(FightEventData.Types.EventType.EventForParenting, proto)
    {
    }
  }
}
