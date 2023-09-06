// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.DesaturatePass
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public class DesaturatePass : AbstractPass
  {
    private float m_factor;
    private RenderTargetIdentifier m_colorRTI;

    public void Setup(float factor, RenderTargetIdentifier colorRTI)
    {
      this.m_factor = factor;
      this.m_colorRTI = colorRTI;
    }

    public override void Execute(
      ref ScriptableRenderContext context,
      ref RenderingData renderingData,
      ForwardRenderer renderer)
    {
      if ((double) this.m_factor <= 0.0)
        return;
      CommandBuffer commandBuffer = CommandBufferPool.Get("Desaturate");
      Material desaturateMaterial = CubeSRP.resources.desaturateMaterial;
      desaturateMaterial.SetFloat(DesaturateShaderProperties._DesaturateFactor, 1f - this.m_factor);
      commandBuffer.BlitFullscreenTriangle((RenderTargetIdentifier) BuiltinRenderTextureType.None, this.m_colorRTI, desaturateMaterial);
      context.ExecuteCommandBuffer(commandBuffer);
      CommandBufferPool.Release(commandBuffer);
    }

    public override void ReleaseResources(CommandBuffer cmd)
    {
    }
  }
}
