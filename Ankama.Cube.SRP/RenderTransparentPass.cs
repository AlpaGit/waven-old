// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.RenderTransparentPass
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine.Experimental.Rendering;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public class RenderTransparentPass : AbstractPass
  {
    private const string RenderTransparentsTag = "Render Transparents";
    private RenderTargetIdentifier m_colorRTI;
    private RenderTargetIdentifier m_depthRTI;
    private int m_layerMask;
    private int m_renderingLayerMask;

    public RenderTransparentPass()
    {
      this.RegisterShaderPassName("PrePass");
      this.RegisterShaderPassName("LightweightForward");
      this.RegisterShaderPassName("SRPDefaultUnlit");
    }

    public void Setup(
      RenderTargetIdentifier colorRTI,
      RenderTargetIdentifier depthRTI,
      int LayerMask = -1,
      int renderingLayerMask = -1)
    {
      this.m_colorRTI = colorRTI;
      this.m_depthRTI = depthRTI;
      this.m_layerMask = LayerMask;
      this.m_renderingLayerMask = renderingLayerMask;
    }

    public override void Execute(
      ref ScriptableRenderContext context,
      ref RenderingData renderingData,
      ForwardRenderer renderer)
    {
      CubeSRPQualitySettings qualitySettings = CubeSRP.qualitySettings;
      CommandBuffer commandBuffer = CommandBufferPool.Get("Render Transparents");
      using (new ProfilingSample(commandBuffer, "Render Transparents", (CustomSampler) null))
      {
        SRPUtility.SetRenderTarget(commandBuffer, this.m_colorRTI, this.m_depthRTI);
        context.ExecuteCommandBuffer(commandBuffer);
        commandBuffer.Clear();
        DrawRendererSettings rendererSettings = this.CreateDrawRendererSettings(renderingData.camera, SortFlags.CommonTransparent, renderingData.renderConfiguration, qualitySettings.dynamicBatching);
        FilterRenderersSettings filterSettings = new FilterRenderersSettings(true)
        {
          renderQueueRange = RenderQueueRange.transparent
        };
        if (this.m_layerMask != -1)
          filterSettings.layerMask = this.m_layerMask;
        if (this.m_renderingLayerMask != -1)
          filterSettings.renderingLayerMask = (uint) this.m_renderingLayerMask;
        CullResults cullResult = renderingData.cullResult;
        context.DrawRenderers(cullResult.visibleRenderers, ref rendererSettings, filterSettings);
      }
      context.ExecuteCommandBuffer(commandBuffer);
      CommandBufferPool.Release(commandBuffer);
    }

    public override void ReleaseResources(CommandBuffer cmd)
    {
    }
  }
}
