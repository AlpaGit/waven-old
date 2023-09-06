// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.Events.DelayedEvent
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using System;

namespace Ankama.Animations.Events
{
  internal struct DelayedEvent
  {
    public readonly DelayedEvent.EventType type;
    public readonly EventArgs args;

    public DelayedEvent(DelayedEvent.EventType eventType, EventArgs eventArgs)
    {
      this.type = eventType;
      this.args = eventArgs;
    }

    public enum EventType
    {
      Initialised,
      AnimationStarted,
      AnimationLooped,
      LabelChanged,
      AnimationEnded,
    }
  }
}
