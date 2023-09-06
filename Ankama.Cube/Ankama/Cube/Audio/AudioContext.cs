// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Audio.AudioContext
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Animations;
using FMOD.Studio;
using System.Collections.Generic;

namespace Ankama.Cube.Audio
{
  public abstract class AudioContext : ITimelineContext
  {
    private readonly List<EventInstance> m_eventInstances = new List<EventInstance>();

    protected abstract void InitializeEventInstance(EventInstance eventInstance);

    public virtual void Initialize() => AudioManager.AddAudioContext(this);

    public void AddEventInstance(EventInstance eventInstance)
    {
      if (!eventInstance.isValid())
        return;
      this.InitializeEventInstance(eventInstance);
      this.m_eventInstances.Add(eventInstance);
    }

    public void SetParameterValue(string name, float value)
    {
      List<EventInstance> eventInstances = this.m_eventInstances;
      int count = eventInstances.Count;
      for (int index = 0; index < count; ++index)
      {
        int num = (int) eventInstances[index].setParameterValue(name, value);
      }
    }

    public void Cleanup()
    {
      List<EventInstance> eventInstances = this.m_eventInstances;
      for (int index = eventInstances.Count - 1; index >= 0; --index)
      {
        if (!eventInstances[index].isValid())
          eventInstances.RemoveAt(index);
      }
    }

    public void Release()
    {
      AudioManager.RemoveAudioContext(this);
      this.m_eventInstances.Clear();
    }
  }
}
