// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Utility.ParticleSystemTrailTextureFixer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Utility
{
  [ExecuteInEditMode]
  [RequireComponent(typeof (ParticleSystemRenderer))]
  public sealed class ParticleSystemTrailTextureFixer : MonoBehaviour
  {
    private static readonly int s_mainTexId = Shader.PropertyToID("_MainTex");

    private void OnEnable() => this.Apply();

    private void Apply()
    {
      ParticleSystemRenderer component = this.GetComponent<ParticleSystemRenderer>();
      Material trailMaterial = component.trailMaterial;
      if ((Object) null == (Object) trailMaterial)
        return;
      MaterialPropertyBlock properties = new MaterialPropertyBlock();
      component.GetPropertyBlock(properties, 1);
      properties.SetTexture(ParticleSystemTrailTextureFixer.s_mainTexId, trailMaterial.mainTexture);
      component.SetPropertyBlock(properties, 1);
    }
  }
}
