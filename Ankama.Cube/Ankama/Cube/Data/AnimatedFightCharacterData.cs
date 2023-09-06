// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.AnimatedFightCharacterData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public sealed class AnimatedFightCharacterData : AnimatedBoardCharacterData
  {
    [SerializeField]
    private AnimatedFightCharacterType m_characterType;
    [SerializeField]
    private AnimatedFightCharacterActionRange m_actionRange;
    [Header("Animation Properties")]
    [SerializeField]
    private bool m_hasRangedAttackAnimations;
    [SerializeField]
    private bool m_hasDashAnimations = true;
    [SerializeField]
    private AnimatedFightCharacterData.IdleToRunTransitionMode m_idleToRunTransitionMode;
    [Header("Character Effects")]
    [SerializeField]
    private CharacterEffect m_actionEffect;

    public AnimatedFightCharacterActionRange actionRange => this.m_actionRange;

    public AnimatedFightCharacterType characterType => this.m_characterType;

    public bool hasRangedAttackAnimations => this.m_hasRangedAttackAnimations;

    public bool hasDashAnimations => this.m_hasDashAnimations;

    public AnimatedFightCharacterData.IdleToRunTransitionMode idleToRunTransitionMode => this.m_idleToRunTransitionMode;

    public CharacterEffect actionEffect => this.m_actionEffect;

    protected override void GatherAdditionalResourcesLoadingRoutines(List<IEnumerator> routines)
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_actionEffect))
        return;
      routines.Add(this.m_actionEffect.Load());
    }

    protected override void UnloadAdditionalResources()
    {
      if (!((UnityEngine.Object) null != (UnityEngine.Object) this.m_actionEffect))
        return;
      this.m_actionEffect.Unload();
    }

    [Flags]
    public enum IdleToRunTransitionMode
    {
      None = 0,
      IdleToRun = 1,
      RunToIdle = 2,
      Both = RunToIdle | IdleToRun, // 0x00000003
    }
  }
}
