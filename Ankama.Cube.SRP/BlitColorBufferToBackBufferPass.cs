// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.BlitColorBufferToBackBufferPass
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public class BlitColorBufferToBackBufferPass : AbstractPass
  {
    private RenderTargetIdentifier m_colorRTI;
    private RenderTargetIdentifier m_backBufferRTI;

    public void Setup(RenderTargetIdentifier colorRTI, RenderTargetIdentifier backBufferRTI)
    {
      this.m_colorRTI = colorRTI;
      this.m_backBufferRTI = backBufferRTI;
    }

    public override void Execute(
      ref ScriptableRenderContext context,
      ref RenderingData renderingData,
      ForwardRenderer renderer)
    {
      CommandBuffer commandBuffer = CommandBufferPool.Get("Blit colorRT to backBuffer");
      commandBuffer.SetGlobalTexture(ShaderProperties._BlitTex, this.m_colorRTI);
      Material blitMaterial = CubeSRP.resources.blitMaterial;
      if (!renderingData.isDefaultViewport)
      {
        CoreUtils.SetRenderTarget(commandBuffer, this.m_backBufferRTI, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, ClearFlag.None, Color.black);
        commandBuffer.SetViewProjectionMatrices(Matrix4x4.identity, Matrix4x4.identity);
        commandBuffer.SetViewport(renderingData.camera.pixelRect);
        commandBuffer.DrawMesh(CubeSRP.resources.fullscreenQuad, Matrix4x4.identity, blitMaterial, 0, 0, (MaterialPropertyBlock) null);
      }
      else
        commandBuffer.Blit(this.m_colorRTI, this.m_backBufferRTI, blitMaterial);
      context.ExecuteCommandBuffer(commandBuffer);
      CommandBufferPool.Release(commandBuffer);
    }

    public override void ReleaseResources(CommandBuffer cmd)
    {
    }
  }
}
