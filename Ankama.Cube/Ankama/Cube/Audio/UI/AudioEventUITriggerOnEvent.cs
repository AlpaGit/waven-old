// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Audio.UI.AudioEventUITriggerOnEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;

namespace Ankama.Cube.Audio.UI
{
  public sealed class AudioEventUITriggerOnEvent : AudioEventUITrigger
  {
    [PublicAPI]
    public void Trigger() => this.PlaySound();
  }
}
