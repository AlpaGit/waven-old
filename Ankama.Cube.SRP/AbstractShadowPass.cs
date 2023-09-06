// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.AbstractShadowPass
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public abstract class AbstractShadowPass : AbstractPass
  {
    protected RenderTexture m_shadowmapRT;

    public override void ReleaseResources(CommandBuffer cmd)
    {
      if (!((Object) this.m_shadowmapRT != (Object) null))
        return;
      RenderTexture.ReleaseTemporary(this.m_shadowmapRT);
      this.m_shadowmapRT = (RenderTexture) null;
    }

    protected RenderTexture GetShadowRT(CommandBuffer cmd, int width, int height)
    {
      RenderTexture temporary = RenderTexture.GetTemporary(width, height, 16, CubeSRP.shadowmapFormat);
      temporary.filterMode = FilterMode.Bilinear;
      temporary.wrapMode = TextureWrapMode.Clamp;
      return temporary;
    }
  }
}
