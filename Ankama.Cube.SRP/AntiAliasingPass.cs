// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.AntiAliasingPass
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace Ankama.Cube.SRP
{
  public class AntiAliasingPass : AbstractPass
  {
    private const string AATag = "Process AA";
    private RenderTargetIdentifier m_colorRTI;
    private RenderTargetIdentifier m_depthRTI;

    public void Setup(RenderTargetIdentifier colorRTI, RenderTargetIdentifier depthRTI)
    {
      this.m_colorRTI = colorRTI;
      this.m_depthRTI = depthRTI;
    }

    public override void Execute(
      ref ScriptableRenderContext srpContext,
      ref RenderingData renderingData,
      ForwardRenderer renderer)
    {
      CommandBuffer commandBuffer = CommandBufferPool.Get("Process AA");
      RenderTextureDescriptor baseRtDescriptor = renderingData.baseRTDescriptor with
      {
        depthBufferBits = 0
      };
      int colorCopy = AntiAliasingPass.ShaderIDs._Color_Copy;
      commandBuffer.GetTemporaryRT(colorCopy, baseRtDescriptor, FilterMode.Bilinear);
      commandBuffer.Blit(this.m_colorRTI, (RenderTargetIdentifier) colorCopy);
      CubeSRPQualitySettings qualitySettings = CubeSRP.qualitySettings;
      if (renderingData.antiAliasing == AAType.FXAA)
      {
        Material ppFxaaMaterial = CubeSRP.resources.ppFxaaMaterial;
        ppFxaaMaterial.shaderKeywords = (string[]) null;
        ppFxaaMaterial.EnableKeyword("FXAA_KEEP_ALPHA");
        ppFxaaMaterial.EnableKeyword(qualitySettings.fxaaQuality == FXAAQuality.Fast ? "FXAA_LOW" : "FXAA");
        ppFxaaMaterial.SetFloat(AntiAliasingPass.ShaderIDs._RenderViewportScaleFactor, 1f);
        ppFxaaMaterial.SetVector(AntiAliasingPass.ShaderIDs._UVTransform, SystemInfo.graphicsUVStartsAtTop ? new Vector4(1f, -1f, 0.0f, 1f) : new Vector4(1f, 1f, 0.0f, 0.0f));
        commandBuffer.BlitFullscreenTriangle((RenderTargetIdentifier) colorCopy, this.m_colorRTI, ppFxaaMaterial);
      }
      else if (renderingData.antiAliasing == AAType.SMAA)
      {
        SubpixelMorphologicalAntialiasing.Quality smaaQuality = qualitySettings.smaaQuality;
        PostProcessResources processResources = CubeSRP.postProcessResources;
        Material ppSmaaMaterial = CubeSRP.resources.ppSmaaMaterial;
        ppSmaaMaterial.shaderKeywords = (string[]) null;
        ppSmaaMaterial.SetTexture("_AreaTex", (Texture) processResources.smaaLuts.area);
        ppSmaaMaterial.SetTexture("_SearchTex", (Texture) processResources.smaaLuts.search);
        commandBuffer.GetTemporaryRT(AntiAliasingPass.ShaderIDs._SMAA_Flip, baseRtDescriptor, FilterMode.Bilinear);
        commandBuffer.GetTemporaryRT(AntiAliasingPass.ShaderIDs._SMAA_Flop, baseRtDescriptor, FilterMode.Bilinear);
        commandBuffer.BlitFullscreenTriangle(this.m_colorRTI, (RenderTargetIdentifier) AntiAliasingPass.ShaderIDs._SMAA_Flip, ppSmaaMaterial, (int) smaaQuality, true);
        commandBuffer.BlitFullscreenTriangle((RenderTargetIdentifier) AntiAliasingPass.ShaderIDs._SMAA_Flip, (RenderTargetIdentifier) AntiAliasingPass.ShaderIDs._SMAA_Flop, ppSmaaMaterial, (int) (3 + smaaQuality));
        commandBuffer.SetGlobalTexture("_BlendTex", (RenderTargetIdentifier) AntiAliasingPass.ShaderIDs._SMAA_Flop);
        commandBuffer.BlitFullscreenTriangle((RenderTargetIdentifier) colorCopy, this.m_colorRTI, ppSmaaMaterial, 6);
        commandBuffer.ReleaseTemporaryRT(AntiAliasingPass.ShaderIDs._SMAA_Flip);
        commandBuffer.ReleaseTemporaryRT(AntiAliasingPass.ShaderIDs._SMAA_Flop);
      }
      commandBuffer.ReleaseTemporaryRT(colorCopy);
      SRPUtility.SetRenderTarget(commandBuffer, this.m_colorRTI, this.m_depthRTI);
      srpContext.ExecuteCommandBuffer(commandBuffer);
      CommandBufferPool.Release(commandBuffer);
    }

    public override void ReleaseResources(CommandBuffer cmd)
    {
    }

    private class ShaderIDs
    {
      internal static readonly int _Color_Copy = Shader.PropertyToID(nameof (_Color_Copy));
      internal static readonly int _RenderViewportScaleFactor = Shader.PropertyToID(nameof (_RenderViewportScaleFactor));
      internal static readonly int _UVTransform = Shader.PropertyToID(nameof (_UVTransform));
      internal static readonly int _SMAA_Flip = Shader.PropertyToID(nameof (_SMAA_Flip));
      internal static readonly int _SMAA_Flop = Shader.PropertyToID(nameof (_SMAA_Flop));
    }
  }
}
