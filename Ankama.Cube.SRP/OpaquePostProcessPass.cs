// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.OpaquePostProcessPass
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public class OpaquePostProcessPass : AbstractPass
  {
    private RenderTargetIdentifier m_colorRTI;

    public void Setup(RenderTargetIdentifier colorRTI) => this.m_colorRTI = colorRTI;

    public override void Execute(
      ref ScriptableRenderContext context,
      ref RenderingData renderingData,
      ForwardRenderer renderer)
    {
      CommandBuffer commandBuffer = CommandBufferPool.Get("Opaque PostProcess");
      RenderTextureFormat colorFormat = renderingData.baseRTDescriptor.colorFormat;
      renderer.RenderPostProcess(commandBuffer, ref renderingData, colorFormat, this.m_colorRTI, this.m_colorRTI, true);
      context.ExecuteCommandBuffer(commandBuffer);
      CommandBufferPool.Release(commandBuffer);
    }

    public override void ReleaseResources(CommandBuffer cmd)
    {
    }
  }
}
