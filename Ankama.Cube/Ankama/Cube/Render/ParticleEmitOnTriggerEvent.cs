// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Render.ParticleEmitOnTriggerEvent
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Render
{
  [ExecuteInEditMode]
  public class ParticleEmitOnTriggerEvent : MonoBehaviour
  {
    [SerializeField]
    private ParticleSystem m_particleSystemListener;
    [SerializeField]
    private ParticleEmitOnTriggerEvent.ParticleSystemEmit[] m_particleSystemEmits;
    private int pSystemsCount;
    private List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();

    private void Awake()
    {
      this.pSystemsCount = this.m_particleSystemEmits.Length;
      for (int index = 0; index < this.pSystemsCount; ++index)
        this.m_particleSystemEmits[index].trsf = this.m_particleSystemEmits[index].pSystem.transform;
    }

    private void OnParticleTrigger()
    {
      int triggerParticles = this.m_particleSystemListener.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, this.particles);
      for (int index1 = 0; index1 < triggerParticles; ++index1)
      {
        Vector3 position = this.particles[index1].position;
        for (int index2 = 0; index2 < this.pSystemsCount; ++index2)
        {
          ParticleEmitOnTriggerEvent.ParticleSystemEmit particleSystemEmit = this.m_particleSystemEmits[index2];
          particleSystemEmit.trsf.position = position;
          particleSystemEmit.pSystem.Emit(particleSystemEmit.emitCount);
        }
      }
    }

    [Serializable]
    private struct ParticleSystemEmit
    {
      public ParticleSystem pSystem;
      public int emitCount;
      [HideInInspector]
      public Transform trsf;
    }
  }
}
