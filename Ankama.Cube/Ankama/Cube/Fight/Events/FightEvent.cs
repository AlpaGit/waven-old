// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Fight.Events.FightEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections;
using System.Collections.Generic;

namespace Ankama.Cube.Fight.Events
{
  public abstract class FightEvent
  {
    public readonly FightEventData.Types.EventType eventType;
    public readonly int eventId;
    public readonly int? parentEventId;

    protected FightEvent(FightEventData.Types.EventType eventType, int eventId, int? parentEventId)
    {
      this.eventType = eventType;
      this.eventId = eventId;
      this.parentEventId = parentEventId;
    }

    protected FightEvent(FightEventData.Types.EventType eventType, FightEventData proto)
    {
      this.eventType = eventType;
      this.eventId = proto.EventId;
      this.parentEventId = proto.ParentEventId;
    }

    public FightEvent parentEvent { get; private set; }

    public FightEvent firstChildEvent { get; private set; }

    public FightEvent nextSiblingEvent { get; private set; }

    public void AddChildEvent(FightEvent fightEvent)
    {
      fightEvent.parentEvent = this;
      if (this.firstChildEvent == null)
      {
        this.firstChildEvent = fightEvent;
      }
      else
      {
        FightEvent fightEvent1 = this.firstChildEvent;
        FightEvent fightEvent2;
        do
        {
          fightEvent2 = fightEvent1;
          fightEvent1 = fightEvent2.nextSiblingEvent;
        }
        while (fightEvent1 != null);
        fightEvent2.nextSiblingEvent = fightEvent;
      }
    }

    public virtual bool IsInvisible() => false;

    public virtual bool CanBeGroupedWith(FightEvent other)
    {
      if (this.eventType != other.eventType)
        return false;
      int? parentEventId1 = this.parentEventId;
      int? parentEventId2 = other.parentEventId;
      return parentEventId1.GetValueOrDefault() == parentEventId2.GetValueOrDefault() & parentEventId1.HasValue == parentEventId2.HasValue;
    }

    public virtual bool SynchronizeExecution() => false;

    public IEnumerable<FightEvent> EnumerateChildren()
    {
      for (FightEvent childEvent = this.firstChildEvent; childEvent != null; childEvent = childEvent.nextSiblingEvent)
        yield return childEvent;
    }

    public virtual void UpdateStatus(FightStatus fightStatus)
    {
    }

    public virtual IEnumerator UpdateView(FightStatus fightStatus)
    {
      yield break;
    }
  }
}
