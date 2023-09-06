// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.TransparentPostProcessPass
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public class TransparentPostProcessPass : AbstractPass
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
      CommandBuffer commandBuffer = CommandBufferPool.Get("Transparent PostProcess + FinalBlit");
      RenderTextureFormat colorFormat = renderingData.baseRTDescriptor.colorFormat;
      renderer.RenderPostProcess(commandBuffer, ref renderingData, colorFormat, this.m_colorRTI, this.m_backBufferRTI, false);
      context.ExecuteCommandBuffer(commandBuffer);
      CommandBufferPool.Release(commandBuffer);
    }

    public override void ReleaseResources(CommandBuffer cmd)
    {
    }
  }
}
