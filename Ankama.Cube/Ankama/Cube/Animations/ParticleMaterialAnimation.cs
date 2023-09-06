// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.ParticleMaterialAnimation
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using UnityEngine;

namespace Ankama.Cube.Animations
{
  [ExecuteInEditMode]
  public class ParticleMaterialAnimation : MonoBehaviour
  {
    [SerializeField]
    private ParticleSystem m_particleSystem;
    [SerializeField]
    private Renderer[] m_renderers;
    [SerializeField]
    private ParticleMaterialAnimation.MaterialParam[] m_materialParams;
    private MaterialPropertyBlock m_materialPropertyBlock;
    private int[] m_ids;

    private void Awake()
    {
      this.m_materialPropertyBlock = new MaterialPropertyBlock();
      this.m_renderers[0].GetPropertyBlock(this.m_materialPropertyBlock);
      this.m_ids = new int[this.m_materialParams.Length];
      for (int index = 0; index < this.m_ids.Length; ++index)
        this.m_ids[index] = Shader.PropertyToID(this.m_materialParams[index].parameterName);
    }

    private void Update()
    {
      for (int index = 0; index < this.m_ids.Length; ++index)
        this.m_materialPropertyBlock.SetFloat(this.m_ids[index], this.m_materialParams[index].animationCurve.Evaluate(this.m_particleSystem.time));
      for (int index = 0; index < this.m_renderers.Length; ++index)
        this.m_renderers[index].SetPropertyBlock(this.m_materialPropertyBlock);
    }

    [Serializable]
    private struct MaterialParam
    {
      public string parameterName;
      public AnimationCurve animationCurve;
    }
  }
}
