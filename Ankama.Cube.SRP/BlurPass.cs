// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.BlurPass
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace Ankama.Cube.SRP
{
  public class BlurPass : AbstractPass
  {
    private float m_factor;
    private RenderTargetIdentifier m_colorRTI;

    public void Setup(float factor, RenderTargetIdentifier colorRTI)
    {
      this.m_factor = factor;
      this.m_colorRTI = colorRTI;
    }

    public override void Execute(
      ref ScriptableRenderContext context,
      ref RenderingData renderingData,
      ForwardRenderer renderer)
    {
      UIBlurQuality blurQuality = CubeSRP.qualitySettings.blurQuality;
      if (blurQuality == UIBlurQuality.None || (double) this.m_factor <= 0.0)
        return;
      CommandBuffer commandBuffer = CommandBufferPool.Get("Blur");
      RenderTextureDescriptor baseRtDescriptor = renderingData.baseRTDescriptor with
      {
        depthBufferBits = 0
      };
      UIBlurData blurData = CubeSRP.resources.GetBlurData(blurQuality);
      int downsample = blurData.downsample;
      bool flag = (double) this.m_factor > 0.5 && downsample > 0;
      float num1 = blurData.blurSize * this.m_factor;
      int blurIterations = blurData.blurIterations;
      Material blurMaterial = CubeSRP.resources.blurMaterial;
      int num2 = blurData.blurType == UIBlurData.BlurType.StandardGauss ? 0 : 2;
      if (flag)
      {
        int blurTempRt = BlurShaderProperties._BlurTempRT;
        int blurTempRt2 = BlurShaderProperties._BlurTempRT2;
        baseRtDescriptor.width >>= downsample;
        baseRtDescriptor.height >>= downsample;
        commandBuffer.GetTemporaryRT(blurTempRt, baseRtDescriptor, FilterMode.Bilinear);
        commandBuffer.Blit(this.m_colorRTI, (RenderTargetIdentifier) blurTempRt, blurMaterial, 0);
        float num3 = (float) (1.0 / (1.0 * (double) (1 << downsample)));
        float num4 = num1 * num3;
        for (int index = 0; index < blurIterations; ++index)
        {
          float num5 = (float) index * 1f * this.m_factor;
          commandBuffer.SetGlobalVector(BlurShaderProperties._BlurParameter, new Vector4(num4 + num5, -num4 - num5, 0.0f, 0.0f));
          commandBuffer.GetTemporaryRT(blurTempRt2, baseRtDescriptor, FilterMode.Bilinear);
          commandBuffer.Blit((RenderTargetIdentifier) blurTempRt, (RenderTargetIdentifier) blurTempRt2, blurMaterial, 1 + num2);
          commandBuffer.ReleaseTemporaryRT(blurTempRt);
          commandBuffer.GetTemporaryRT(blurTempRt, baseRtDescriptor, FilterMode.Bilinear);
          commandBuffer.Blit((RenderTargetIdentifier) blurTempRt2, (RenderTargetIdentifier) blurTempRt, blurMaterial, 2 + num2);
          commandBuffer.ReleaseTemporaryRT(blurTempRt2);
        }
        commandBuffer.Blit((RenderTargetIdentifier) blurTempRt, this.m_colorRTI);
        commandBuffer.ReleaseTemporaryRT(blurTempRt);
      }
      else
      {
        int blurTempRt2 = BlurShaderProperties._BlurTempRT2;
        for (int index = 0; index < blurIterations; ++index)
        {
          float num6 = (float) index * 1f * this.m_factor;
          commandBuffer.SetGlobalVector(BlurShaderProperties._BlurParameter, new Vector4(num1 + num6, -num1 - num6, 0.0f, 0.0f));
          commandBuffer.GetTemporaryRT(blurTempRt2, baseRtDescriptor, FilterMode.Bilinear);
          commandBuffer.Blit(this.m_colorRTI, (RenderTargetIdentifier) blurTempRt2, blurMaterial, 1 + num2);
          commandBuffer.Blit((RenderTargetIdentifier) blurTempRt2, this.m_colorRTI, blurMaterial, 2 + num2);
          commandBuffer.ReleaseTemporaryRT(blurTempRt2);
        }
      }
      context.ExecuteCommandBuffer(commandBuffer);
      CommandBufferPool.Release(commandBuffer);
    }

    public override void ReleaseResources(CommandBuffer cmd)
    {
    }
  }
}
