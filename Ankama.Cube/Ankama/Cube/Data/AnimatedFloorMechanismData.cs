// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.AnimatedFloorMechanismData
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public sealed class AnimatedFloorMechanismData : AnimatedCharacterData
  {
    [SerializeField]
    private GameObject m_prefab;

    public GameObject prefab => this.m_prefab;

    protected override void GatherAdditionalResourcesLoadingRoutines(List<IEnumerator> routines)
    {
    }

    protected override void UnloadAdditionalResources()
    {
    }
  }
}
