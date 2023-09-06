// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.DepthNormalPass
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public class DepthNormalPass : AbstractPass
  {
    private const string DepthPassTag = "Render DepthNormal";
    private static Color ClearColor = new Color(0.5f, 0.5f, 1f, 1f);
    protected RenderTargetIdentifier m_depthRTI;
    private int m_depthNormalRT = -1;

    public DepthNormalPass() => this.RegisterShaderPassName("DepthNormal");

    public void Setup(RenderTargetIdentifier depthRTI) => this.m_depthRTI = depthRTI;

    public override void Execute(
      ref ScriptableRenderContext context,
      ref RenderingData renderingData,
      ForwardRenderer renderer)
    {
      CubeSRPQualitySettings qualitySettings = CubeSRP.qualitySettings;
      CommandBuffer commandBuffer = CommandBufferPool.Get("Render DepthNormal");
      using (new ProfilingSample(commandBuffer, "Render DepthNormal", (CustomSampler) null))
      {
        this.m_depthNormalRT = RenderTargetIdentifiers._DepthNormals;
        RenderTextureDescriptor baseRtDescriptor = renderingData.baseRTDescriptor with
        {
          colorFormat = RenderTextureFormat.ARGB32,
          depthBufferBits = this.m_depthRTI != (RenderTargetIdentifier) -1 ? 0 : 32,
          msaaSamples = renderingData.msaaSamples
        };
        commandBuffer.GetTemporaryRT(this.m_depthNormalRT, baseRtDescriptor, FilterMode.Bilinear);
        if (this.m_depthRTI != (RenderTargetIdentifier) -1)
        {
          ClearFlag clearFlag = ClearFlag.Color;
          CoreUtils.SetRenderTarget(commandBuffer, (RenderTargetIdentifier) this.m_depthNormalRT, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, this.m_depthRTI, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, clearFlag, DepthNormalPass.ClearColor);
        }
        else
        {
          ClearFlag clearFlag = ClearFlag.All;
          CoreUtils.SetRenderTarget(commandBuffer, (RenderTargetIdentifier) this.m_depthNormalRT, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, clearFlag, DepthNormalPass.ClearColor);
        }
        context.ExecuteCommandBuffer(commandBuffer);
        commandBuffer.Clear();
        DrawRendererSettings rendererSettings = this.CreateDrawRendererSettings(renderingData.camera, SortFlags.CommonOpaque, RendererConfiguration.None, qualitySettings.dynamicBatching);
        context.DrawRenderers(renderingData.cullResult.visibleRenderers, ref rendererSettings, Filters.opaque);
      }
      context.ExecuteCommandBuffer(commandBuffer);
      CommandBufferPool.Release(commandBuffer);
    }

    public override void ReleaseResources(CommandBuffer cmd) => SRPUtility.ReleaseTemporaryRT(cmd, ref this.m_depthNormalRT);
  }
}
