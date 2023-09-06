// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.VisualEffects.ParticleAnimationEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Maps.VisualEffects
{
  public class ParticleAnimationEvent : MonoBehaviour
  {
    [SerializeField]
    private VisualEffect[] m_visualEffects;

    public void Play(int index) => this.m_visualEffects[index].Play();

    public void Stop(int index) => this.m_visualEffects[index].Stop();
  }
}
