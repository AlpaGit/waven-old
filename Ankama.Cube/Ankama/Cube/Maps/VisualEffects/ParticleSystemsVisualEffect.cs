// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.VisualEffects.ParticleSystemsVisualEffect
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Maps.VisualEffects
{
  [SelectionBase]
  [ExecuteInEditMode]
  public sealed class ParticleSystemsVisualEffect : VisualEffect
  {
    [SerializeField]
    private List<ParticleSystem> m_particleSystems = new List<ParticleSystem>();
    [SerializeField]
    private List<ParticleSystemsVisualEffect.StartupParticleSystemData> m_startupData = new List<ParticleSystemsVisualEffect.StartupParticleSystemData>();

    public override bool IsAlive()
    {
      List<ParticleSystem> particleSystems = this.m_particleSystems;
      int count = particleSystems.Count;
      for (int index = 0; index < count; ++index)
      {
        ParticleSystem particleSystem = particleSystems[index];
        if ((UnityEngine.Object) null != (UnityEngine.Object) particleSystem && particleSystem.IsAlive(false))
          return true;
      }
      return false;
    }

    protected override void PlayInternal()
    {
      List<ParticleSystem> particleSystems = this.m_particleSystems;
      List<ParticleSystemsVisualEffect.StartupParticleSystemData> startupData = this.m_startupData;
      int count1 = startupData.Count;
      for (int index = 0; index < count1; ++index)
      {
        ParticleSystemsVisualEffect.StartupParticleSystemData particleSystemData = startupData[index];
        ParticleSystem particleSystem = particleSystems[particleSystemData.particleSystemIndex];
        if ((UnityEngine.Object) null != (UnityEngine.Object) particleSystem)
          particleSystemData.Apply(particleSystem);
      }
      int count2 = particleSystems.Count;
      for (int index = 0; index < count2; ++index)
      {
        ParticleSystem particleSystem = particleSystems[index];
        if ((UnityEngine.Object) null != (UnityEngine.Object) particleSystem)
          particleSystem.Play(false);
      }
    }

    protected override void PauseInternal()
    {
      List<ParticleSystem> particleSystems = this.m_particleSystems;
      int count = particleSystems.Count;
      for (int index = 0; index < count; ++index)
      {
        ParticleSystem particleSystem = particleSystems[index];
        if ((UnityEngine.Object) null != (UnityEngine.Object) particleSystem)
          particleSystem.Pause(false);
      }
    }

    protected override void StopInternal(VisualEffectStopMethod stopMethod)
    {
      List<ParticleSystem> particleSystems = this.m_particleSystems;
      ParticleSystemStopBehavior stopBehavior = (ParticleSystemStopBehavior) stopMethod;
      int count = particleSystems.Count;
      for (int index = 0; index < count; ++index)
      {
        ParticleSystem particleSystem = particleSystems[index];
        if ((UnityEngine.Object) null != (UnityEngine.Object) particleSystem)
        {
          ParticleSystem.MainModule main = particleSystem.main;
          if (main.ringBufferMode != ParticleSystemRingBufferMode.Disabled)
            main.ringBufferMode = ParticleSystemRingBufferMode.Disabled;
          particleSystem.Stop(false, stopBehavior);
        }
      }
    }

    protected override void ClearInternal()
    {
      List<ParticleSystem> particleSystems = this.m_particleSystems;
      int count = particleSystems.Count;
      for (int index = 0; index < count; ++index)
      {
        ParticleSystem particleSystem = particleSystems[index];
        if ((UnityEngine.Object) null != (UnityEngine.Object) particleSystem)
          particleSystem.Clear();
      }
    }

    [Serializable]
    private struct StartupParticleSystemData : 
      IEquatable<ParticleSystemsVisualEffect.StartupParticleSystemData>
    {
      [SerializeField]
      public int particleSystemIndex;
      [SerializeField]
      private ParticleSystemRingBufferMode m_ringBufferMode;

      public static bool Handles(ParticleSystem particleSystem) => particleSystem.main.ringBufferMode != 0;

      public StartupParticleSystemData(int index, ParticleSystem particleSystem)
      {
        ParticleSystem.MainModule main = particleSystem.main;
        this.particleSystemIndex = index;
        this.m_ringBufferMode = main.ringBufferMode;
      }

      public void Apply([NotNull] ParticleSystem particleSystem) => particleSystem.main.ringBufferMode = this.m_ringBufferMode;

      public static bool operator ==(
        ParticleSystemsVisualEffect.StartupParticleSystemData lhs,
        ParticleSystemsVisualEffect.StartupParticleSystemData rhs)
      {
        return lhs.particleSystemIndex == rhs.particleSystemIndex && lhs.m_ringBufferMode == rhs.m_ringBufferMode;
      }

      public static bool operator !=(
        ParticleSystemsVisualEffect.StartupParticleSystemData lhs,
        ParticleSystemsVisualEffect.StartupParticleSystemData rhs)
      {
        return lhs.particleSystemIndex != rhs.particleSystemIndex || lhs.m_ringBufferMode != rhs.m_ringBufferMode;
      }

      public bool Equals(
        ParticleSystemsVisualEffect.StartupParticleSystemData other)
      {
        return this.particleSystemIndex == other.particleSystemIndex && this.m_ringBufferMode == other.m_ringBufferMode;
      }

      public override bool Equals(object obj) => obj != null && obj is ParticleSystemsVisualEffect.StartupParticleSystemData particleSystemData && this.particleSystemIndex == particleSystemData.particleSystemIndex && this.m_ringBufferMode == particleSystemData.m_ringBufferMode;

      public override int GetHashCode() => this.particleSystemIndex;
    }
  }
}
