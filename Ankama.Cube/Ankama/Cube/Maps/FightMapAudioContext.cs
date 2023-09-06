// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.FightMapAudioContext
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Audio;
using Ankama.Cube.Fight;
using Ankama.Cube.Fight.Entities;
using FMOD.Studio;
using System;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  public sealed class FightMapAudioContext : AudioContext
  {
    private const string TurnIndex = "turnIndex";
    private const string LocalPlayerHeroLife = "Music_Health";
    private const string BossEvolutionStep = "Music_BossState";
    private int m_turnIndex;
    private float m_localPlayerHeroLife = 1f;
    private int m_bossEvolutionStep;

    public int turnIndex
    {
      get => this.m_turnIndex;
      set
      {
        if (this.m_turnIndex == value)
          return;
        this.SetParameterValue(nameof (turnIndex), (float) value);
        this.m_turnIndex = value;
      }
    }

    public float localPlayerHeroLife
    {
      get => this.m_localPlayerHeroLife;
      set
      {
        this.SetParameterValue("Music_Health", value);
        this.m_localPlayerHeroLife = value;
      }
    }

    public int bossEvolutionStep
    {
      get => this.m_bossEvolutionStep;
      set
      {
        this.SetParameterValue("Music_BossState", (float) value);
        this.m_bossEvolutionStep = value;
      }
    }

    public override void Initialize()
    {
      base.Initialize();
      FightStatus local = FightStatus.local;
      if (local == null)
        return;
      this.m_turnIndex = local.turnIndex;
      int localPlayerId = local.localPlayerId;
      HeroStatus entityStatus;
      if (!local.TryGetEntity<HeroStatus>((Predicate<HeroStatus>) (e => e.ownerId == localPlayerId), out entityStatus))
        return;
      this.m_localPlayerHeroLife = Mathf.Clamp01((float) entityStatus.life / (float) entityStatus.baseLife);
    }

    protected override void InitializeEventInstance(EventInstance eventInstance)
    {
      int num = (int) eventInstance.setParameterValue("turnIndex", (float) this.m_turnIndex);
    }
  }
}
