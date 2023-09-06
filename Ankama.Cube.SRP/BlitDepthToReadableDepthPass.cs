// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.BlitDepthToReadableDepthPass
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public class BlitDepthToReadableDepthPass : AbstractPass
  {
    protected RenderTargetIdentifier m_fromRTI;
    private int m_readableDepthRT = -1;

    public void Setup(RenderTargetIdentifier from) => this.m_fromRTI = from;

    public override void Execute(
      ref ScriptableRenderContext context,
      ref RenderingData renderingData,
      ForwardRenderer renderer)
    {
      this.m_readableDepthRT = RenderTargetIdentifiers._DepthReadable;
      this.CopyDepth(ref context, ref renderingData, this.m_fromRTI, this.m_readableDepthRT, CubeSRP.resources.copyDepthMaterial);
    }

    public override void ReleaseResources(CommandBuffer cmd) => SRPUtility.ReleaseTemporaryRT(cmd, ref this.m_readableDepthRT);

    private void CopyDepth(
      ref ScriptableRenderContext context,
      ref RenderingData renderingData,
      RenderTargetIdentifier fromRT,
      int toRT,
      Material material)
    {
      CommandBuffer commandBuffer = CommandBufferPool.Get("Copy Depth");
      RenderTextureDescriptor baseRtDescriptor = renderingData.baseRTDescriptor with
      {
        colorFormat = RenderTextureFormat.Depth,
        depthBufferBits = 32,
        msaaSamples = 1,
        bindMS = false
      };
      commandBuffer.GetTemporaryRT(toRT, baseRtDescriptor, FilterMode.Point);
      commandBuffer.SetGlobalTexture(ShaderProperties._CameraDepthAttachment, fromRT);
      if (renderingData.msaaSamples > 1)
      {
        commandBuffer.DisableShaderKeyword("_DEPTH_NO_MSAA");
        if (renderingData.msaaSamples == 4)
        {
          commandBuffer.DisableShaderKeyword("_DEPTH_MSAA_2");
          commandBuffer.EnableShaderKeyword("_DEPTH_MSAA_4");
        }
        else
        {
          commandBuffer.EnableShaderKeyword("_DEPTH_MSAA_2");
          commandBuffer.DisableShaderKeyword("_DEPTH_MSAA_4");
        }
      }
      else
      {
        commandBuffer.EnableShaderKeyword("_DEPTH_NO_MSAA");
        commandBuffer.DisableShaderKeyword("_DEPTH_MSAA_2");
        commandBuffer.DisableShaderKeyword("_DEPTH_MSAA_4");
      }
      commandBuffer.Blit(fromRT, (RenderTargetIdentifier) toRT, material);
      context.ExecuteCommandBuffer(commandBuffer);
      CommandBufferPool.Release(commandBuffer);
    }
  }
}
