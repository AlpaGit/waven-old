// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.SetupShaderConstantPass
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public class SetupShaderConstantPass : AbstractPass
  {
    public override void Execute(
      ref ScriptableRenderContext context,
      ref RenderingData renderingData,
      ForwardRenderer renderer)
    {
      CommandBuffer commandBuffer = CommandBufferPool.Get("Setup Shader Constants");
      renderingData.lightHandler.SetShaderConstants(commandBuffer, ref renderingData);
      CoreUtils.SetKeyword(commandBuffer, "_DIR_SHADOWS", renderer.hasMainShadow);
      CoreUtils.SetKeyword(commandBuffer, "_CLOUDS_SHADOWS", renderer.hasCloudShadow);
      CoreUtils.SetKeyword(commandBuffer, "_LOCAL_SHADOWS", renderer.hasAdditionalShadow);
      bool flag = renderer.hasMainShadow || renderer.hasAdditionalShadow;
      ShadowQuality shadowQuality = renderingData.shadowQuality;
      CoreUtils.SetKeyword(commandBuffer, "_SHADOW_PCF", flag && shadowQuality == ShadowQuality.PCF);
      CoreUtils.SetKeyword(commandBuffer, "_SHADOW_PCSS", flag && shadowQuality == ShadowQuality.PCSS);
      PCSSData pcssData = CubeSRP.resources.PCSSData;
      if (shadowQuality == ShadowQuality.PCSS && (Object) pcssData != (Object) null)
      {
        commandBuffer.SetGlobalInt(PCSSConstantBuffer._BlockerSamples, pcssData.blockerSampleCount);
        commandBuffer.SetGlobalInt(PCSSConstantBuffer._PcfSamples, pcssData.pcfSampleCount);
        commandBuffer.SetGlobalVector(PCSSConstantBuffer._Softness, (Vector4) (new Vector2(pcssData.softnessMin, pcssData.softness) / 100f));
        commandBuffer.SetGlobalFloat(PCSSConstantBuffer._BlockerGradientBias, pcssData.blockerGradientBias);
        commandBuffer.SetGlobalFloat(PCSSConstantBuffer._PcfGradientBias, pcssData.pcfGradientBias);
      }
      CoreUtils.SetKeyword(commandBuffer, "_CAMERA_DEPTH_TEXTURE", renderingData.renderDepth && renderingData.needReadableDepthTexture);
      CoreUtils.SetKeyword(commandBuffer, "_CAMERA_DEPTH_NORMAL_TEXTURE", renderer.hasDepthNormal);
      CoreUtils.SetKeyword(commandBuffer, "_CAMERA_COLOR_OPAQUE_TEXTURE", renderingData.needReadableColorOpaqueTexture);
      context.ExecuteCommandBuffer(commandBuffer);
      CommandBufferPool.Release(commandBuffer);
    }

    public override void ReleaseResources(CommandBuffer cmd)
    {
    }
  }
}
