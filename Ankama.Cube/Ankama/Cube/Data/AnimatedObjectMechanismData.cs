// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.AnimatedObjectMechanismData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public class AnimatedObjectMechanismData : AnimatedBoardCharacterData
  {
    [Header("Animation Properties")]
    [SerializeField]
    private bool m_hasActivationAnimation;
    [Header("Character Effects")]
    [SerializeField]
    private CharacterEffect m_activationEffect;

    public bool hasActivationAnimation => this.m_hasActivationAnimation;

    public CharacterEffect activationEffect => this.m_activationEffect;

    protected override void GatherAdditionalResourcesLoadingRoutines(List<IEnumerator> routines)
    {
      if (!((Object) null != (Object) this.m_activationEffect))
        return;
      routines.Add(this.m_activationEffect.Load());
    }

    protected override void UnloadAdditionalResources()
    {
      if (!((Object) null != (Object) this.m_activationEffect))
        return;
      this.m_activationEffect.Unload();
    }
  }
}
