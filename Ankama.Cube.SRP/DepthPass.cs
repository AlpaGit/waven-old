// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.DepthPass
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public class DepthPass : AbstractPass
  {
    private const string DepthPassTag = "Render Depth";
    private RenderTargetIdentifier m_depthRTI;

    public DepthPass() => this.RegisterShaderPassName("Depth");

    public void Setup(RenderTargetIdentifier depthRTI) => this.m_depthRTI = depthRTI;

    public override void Execute(
      ref ScriptableRenderContext context,
      ref RenderingData renderingData,
      ForwardRenderer renderer)
    {
      CubeSRPQualitySettings qualitySettings = CubeSRP.qualitySettings;
      CommandBuffer commandBuffer = CommandBufferPool.Get("Render Depth");
      using (new ProfilingSample(commandBuffer, "Render Depth", (CustomSampler) null))
      {
        ClearFlag clearFlag = ClearFlag.All;
        if (this.m_depthRTI != (RenderTargetIdentifier) -1)
          CoreUtils.SetRenderTarget(commandBuffer, this.m_depthRTI, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, clearFlag, Color.black);
        else
          CoreUtils.SetRenderTarget(commandBuffer, renderingData.finalBackBuffer, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, clearFlag, Color.black);
        context.ExecuteCommandBuffer(commandBuffer);
        commandBuffer.Clear();
        DrawRendererSettings rendererSettings = this.CreateDrawRendererSettings(renderingData.camera, SortFlags.CommonOpaque, RendererConfiguration.None, qualitySettings.dynamicBatching);
        context.DrawRenderers(renderingData.cullResult.visibleRenderers, ref rendererSettings, Filters.opaque);
      }
      context.ExecuteCommandBuffer(commandBuffer);
      CommandBufferPool.Release(commandBuffer);
    }

    public override void ReleaseResources(CommandBuffer cmd)
    {
    }
  }
}
