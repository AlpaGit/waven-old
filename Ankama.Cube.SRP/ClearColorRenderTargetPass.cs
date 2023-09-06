// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.ClearColorRenderTargetPass
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public class ClearColorRenderTargetPass : AbstractPass
  {
    private const string ClearTag = "Clear Color target";
    private RenderTargetIdentifier m_colorRTI;
    private RenderTargetIdentifier m_depthRTI;

    public void Setup(RenderTargetIdentifier colorRTI, RenderTargetIdentifier depthRTI)
    {
      this.m_colorRTI = colorRTI;
      this.m_depthRTI = depthRTI;
    }

    public override void Execute(
      ref ScriptableRenderContext context,
      ref RenderingData renderingData,
      ForwardRenderer renderer)
    {
      CubeSRPQualitySettings qualitySettings = CubeSRP.qualitySettings;
      CommandBuffer commandBuffer = CommandBufferPool.Get("Clear Color target");
      using (new ProfilingSample(commandBuffer, "Clear Color target", (CustomSampler) null))
      {
        ClearFlag cameraClearFlag = this.GetCameraClearFlag(ref renderingData);
        Camera camera = renderingData.camera;
        SRPUtility.SetRenderTarget(commandBuffer, this.m_colorRTI, this.m_depthRTI);
        SRPUtility.ClearRenderTarget(commandBuffer, cameraClearFlag, CoreUtils.ConvertSRGBToActiveColorSpace(camera.backgroundColor));
      }
      context.ExecuteCommandBuffer(commandBuffer);
      CommandBufferPool.Release(commandBuffer);
    }

    public override void ReleaseResources(CommandBuffer cmd)
    {
    }

    private ClearFlag GetCameraClearFlag(ref RenderingData renderingData)
    {
      CameraClearFlags clearFlags = renderingData.camera.clearFlags;
      ClearFlag cameraClearFlag = ClearFlag.None;
      if (clearFlags != CameraClearFlags.Nothing && this.m_depthRTI == (RenderTargetIdentifier) -1 && !renderingData.renderDepth)
        cameraClearFlag |= ClearFlag.Depth;
      if (clearFlags == CameraClearFlags.Color || clearFlags == CameraClearFlags.Skybox)
        cameraClearFlag |= ClearFlag.Color;
      return cameraClearFlag;
    }
  }
}
