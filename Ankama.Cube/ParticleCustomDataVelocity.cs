// Decompiled with JetBrains decompiler
// Type: ParticleCustomDataVelocity
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParticleCustomDataVelocity : MonoBehaviour
{
  [SerializeField]
  private ParticleSystem m_particleSystem;
  [SerializeField]
  private ParticleSystemCustomData customData;
  private List<Vector4> m_customData = new List<Vector4>();
  private ParticleSystem.Particle[] m_particles;
  private int m_maxParticlesCount;

  private void Awake()
  {
    this.m_maxParticlesCount = this.m_particleSystem.main.maxParticles;
    this.m_particles = new ParticleSystem.Particle[this.m_maxParticlesCount];
  }

  private void Reset()
  {
    this.m_particleSystem = this.GetComponent<ParticleSystem>();
    this.m_maxParticlesCount = this.m_particleSystem.main.maxParticles;
    this.m_particles = new ParticleSystem.Particle[this.m_maxParticlesCount];
  }

  private void Update()
  {
    this.m_particleSystem.GetCustomParticleData(this.m_customData, this.customData);
    if (this.m_particles == null)
      return;
    int particles = this.m_particleSystem.GetParticles(this.m_particles);
    for (int index = 0; index < particles; ++index)
      this.m_customData[index] = (Vector4) this.m_particles[index].totalVelocity;
    this.m_particleSystem.SetCustomParticleData(this.m_customData, this.customData);
  }
}
