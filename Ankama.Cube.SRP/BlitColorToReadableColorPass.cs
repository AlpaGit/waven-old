// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.BlitColorToReadableColorPass
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public class BlitColorToReadableColorPass : AbstractPass
  {
    protected RenderTargetIdentifier m_fromColorRTI;
    protected RenderTargetIdentifier m_depthRTI;
    private int m_readableColorRT = -1;

    public void Setup(RenderTargetIdentifier fromColorRTI, RenderTargetIdentifier depthRTI)
    {
      this.m_fromColorRTI = fromColorRTI;
      this.m_depthRTI = depthRTI;
    }

    public override void Execute(
      ref ScriptableRenderContext context,
      ref RenderingData renderingData,
      ForwardRenderer renderer)
    {
      this.m_readableColorRT = RenderTargetIdentifiers._ColorOpaqueReadable;
      CommandBuffer commandBuffer = CommandBufferPool.Get("Copy Opaque Color");
      RenderTextureDescriptor baseRtDescriptor = renderingData.baseRTDescriptor with
      {
        depthBufferBits = 0
      };
      commandBuffer.GetTemporaryRT(this.m_readableColorRT, baseRtDescriptor, FilterMode.Bilinear);
      commandBuffer.SetGlobalTexture(ShaderProperties._BlitTex, this.m_fromColorRTI);
      commandBuffer.Blit(this.m_fromColorRTI, (RenderTargetIdentifier) this.m_readableColorRT, CubeSRP.resources.blitMaterial);
      SRPUtility.SetRenderTarget(commandBuffer, this.m_fromColorRTI, this.m_depthRTI);
      context.ExecuteCommandBuffer(commandBuffer);
      CommandBufferPool.Release(commandBuffer);
    }

    public override void ReleaseResources(CommandBuffer cmd) => SRPUtility.ReleaseTemporaryRT(cmd, ref this.m_readableColorRT);
  }
}
