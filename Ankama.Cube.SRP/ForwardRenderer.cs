// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.ForwardRenderer
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace Ankama.Cube.SRP
{
  public class ForwardRenderer
  {
    private PostProcessRenderContext m_ppRenderContext;
    private DirectionalShadowPass m_directionalShadowPass;
    private CloudsShadowPass m_cloudPass;
    private LocalShadowPass m_localShadowPass;
    private SetupCameraPropertyPass m_setupCameraPropertyPass;
    private DepthPass m_depthPass;
    private BlitDepthToReadableDepthPass m_blitDepthToReadableDepthPass;
    private DepthNormalPass m_depthNormalPass;
    private SetupShaderConstantPass m_setupShaderConstantPass;
    private ClearColorRenderTargetPass m_clearColorPass;
    private RenderOpaquePass m_opaquePass;
    private OpaquePostProcessPass m_opaquePPPass;
    private RenderSkyboxPass m_skyboxPass;
    private BlitColorToReadableColorPass m_blitColorToReadableColorPass;
    private RenderTransparentPass m_transparentWaterPass;
    private AntiAliasingPass m_aaPass;
    private RenderTransparentPass m_transparentPass;
    private DesaturatePass m_desaturatePass;
    private RenderOpaquePass m_opaqueIsolatedCharacterPass;
    private RenderTransparentPass m_transparentIsolatedCharacterPass;
    private PlanarReflectionPass m_planarReflectionPass;
    private BlurPass m_blurPass;
    private TransparentPostProcessPass m_transparentPostProcessPass;
    private BlitColorBufferToBackBufferPass m_blitColorBufferToBackBufferPass;
    private List<AbstractPass> m_passQueue = new List<AbstractPass>();
    private int m_colorRT = -1;
    private int m_depthRT = -1;

    public bool hasMainShadow { get; private set; }

    public bool hasAdditionalShadow { get; private set; }

    public bool hasCloudShadow { get; private set; }

    public bool hasDepthNormal { get; private set; }

    public ForwardRenderer(CubeSRPAsset pipelineAsset)
    {
      this.m_ppRenderContext = new PostProcessRenderContext();
      this.m_directionalShadowPass = new DirectionalShadowPass();
      this.m_cloudPass = new CloudsShadowPass();
      this.m_localShadowPass = new LocalShadowPass();
      this.m_setupCameraPropertyPass = new SetupCameraPropertyPass();
      this.m_depthPass = new DepthPass();
      this.m_blitDepthToReadableDepthPass = new BlitDepthToReadableDepthPass();
      this.m_depthNormalPass = new DepthNormalPass();
      this.m_setupShaderConstantPass = new SetupShaderConstantPass();
      this.m_clearColorPass = new ClearColorRenderTargetPass();
      this.m_opaquePass = new RenderOpaquePass();
      this.m_opaquePPPass = new OpaquePostProcessPass();
      this.m_skyboxPass = new RenderSkyboxPass();
      this.m_blitColorToReadableColorPass = new BlitColorToReadableColorPass();
      this.m_transparentWaterPass = new RenderTransparentPass();
      this.m_aaPass = new AntiAliasingPass();
      this.m_transparentPass = new RenderTransparentPass();
      this.m_desaturatePass = new DesaturatePass();
      this.m_opaqueIsolatedCharacterPass = new RenderOpaquePass();
      this.m_transparentIsolatedCharacterPass = new RenderTransparentPass();
      this.m_blurPass = new BlurPass();
      this.m_planarReflectionPass = new PlanarReflectionPass();
      this.m_transparentPostProcessPass = new TransparentPostProcessPass();
      this.m_blitColorBufferToBackBufferPass = new BlitColorBufferToBackBufferPass();
    }

    public void Dispose()
    {
      this.m_cloudPass.RemoveResources();
      this.m_planarReflectionPass.RemoveResources();
    }

    private void Setup(ref ScriptableRenderContext context, ref RenderingData renderingData)
    {
      CommandBuffer commandBuffer = CommandBufferPool.Get("SetupRTs");
      if (renderingData.postProcessEnabled)
        renderingData.postProcessLayer.UpdateVolumeSystem(renderingData.camera, commandBuffer);
      this.hasMainShadow = false;
      this.hasAdditionalShadow = false;
      this.hasCloudShadow = false;
      this.hasDepthNormal = false;
      this.m_passQueue.Clear();
      this.m_colorRT = -1;
      this.m_depthRT = -1;
      bool flag = renderingData.RequireColorRT();
      if (renderingData.blur != null)
      {
        this.m_blurPass.Setup(renderingData.blur.factor, renderingData.finalBackBuffer);
        this.m_passQueue.Add((AbstractPass) this.m_blurPass);
      }
      if (renderingData.renderDirectionalShadow)
      {
        this.hasMainShadow = this.m_directionalShadowPass.Setup(ref renderingData, this);
        if (this.hasMainShadow)
        {
          this.m_passQueue.Add((AbstractPass) this.m_directionalShadowPass);
          if (renderingData.renderCloudShadow)
          {
            this.m_cloudPass.Setup(this.m_directionalShadowPass);
            this.m_passQueue.Add((AbstractPass) this.m_cloudPass);
            this.hasCloudShadow = true;
          }
        }
      }
      if (renderingData.renderLocalShadow)
      {
        this.hasAdditionalShadow = this.m_localShadowPass.Setup(ref renderingData, this);
        if (this.hasAdditionalShadow)
          this.m_passQueue.Add((AbstractPass) this.m_localShadowPass);
      }
      if (SRPPlanarReflection.s_renderedPeflectionPlanes.Count > 0)
        this.m_passQueue.Add((AbstractPass) this.m_planarReflectionPass);
      this.m_passQueue.Add((AbstractPass) this.m_setupCameraPropertyPass);
      if (renderingData.renderDepth)
      {
        if ((flag || renderingData.isRenderingToTexture || renderingData.needReadableDepthTexture ? (CubeSRP.supportDepthTexture ? 1 : 0) : 0) != 0)
        {
          this.m_depthRT = RenderTargetIdentifiers._Depth;
          RenderTextureDescriptor baseRtDescriptor = renderingData.baseRTDescriptor with
          {
            colorFormat = RenderTextureFormat.Depth,
            depthBufferBits = 32,
            msaaSamples = renderingData.msaaSamples,
            bindMS = renderingData.msaaSamples > 1
          };
          commandBuffer.GetTemporaryRT(this.m_depthRT, baseRtDescriptor, FilterMode.Point);
        }
        this.m_depthPass.Setup((RenderTargetIdentifier) this.m_depthRT);
        this.m_passQueue.Add((AbstractPass) this.m_depthPass);
        if (this.m_depthRT != -1 && renderingData.needReadableDepthTexture)
        {
          this.m_blitDepthToReadableDepthPass.Setup((RenderTargetIdentifier) this.m_depthRT);
          this.m_passQueue.Add((AbstractPass) this.m_blitDepthToReadableDepthPass);
        }
      }
      this.hasDepthNormal = renderingData.postProcessEnabled && this.RequireDepthNormalRT(renderingData.postProcessLayer, this.m_ppRenderContext);
      if (this.hasDepthNormal)
      {
        this.m_depthNormalPass.Setup((RenderTargetIdentifier) this.m_depthRT);
        this.m_passQueue.Add((AbstractPass) this.m_depthNormalPass);
      }
      if ((flag || this.m_depthRT != -1) && !renderingData.isRenderingToTexture)
      {
        this.m_colorRT = RenderTargetIdentifiers._Color;
        RenderTextureDescriptor baseRtDescriptor = renderingData.baseRTDescriptor with
        {
          depthBufferBits = this.m_depthRT != -1 ? 0 : 32,
          sRGB = true,
          msaaSamples = renderingData.msaaSamples
        };
        commandBuffer.GetTemporaryRT(this.m_colorRT, baseRtDescriptor, FilterMode.Bilinear);
      }
      this.m_passQueue.Add((AbstractPass) this.m_setupShaderConstantPass);
      RenderTargetIdentifier targetIdentifier = this.m_colorRT != -1 ? (RenderTargetIdentifier) this.m_colorRT : renderingData.finalBackBuffer;
      int num = !renderingData.characterFocus ? 0 : ((double) CubeSRP.characterFocusFactor > 1.0 / 1000.0 ? 1 : 0);
      int cullingMask = renderingData.camera.cullingMask;
      if (num != 0)
        cullingMask &= ~(1 << LayerMaskNames.characterFocusLayer);
      this.m_clearColorPass.Setup(targetIdentifier, (RenderTargetIdentifier) this.m_depthRT);
      this.m_passQueue.Add((AbstractPass) this.m_clearColorPass);
      this.m_opaquePass.Setup(targetIdentifier, (RenderTargetIdentifier) this.m_depthRT, cullingMask);
      this.m_passQueue.Add((AbstractPass) this.m_opaquePass);
      if (renderingData.postProcessEnabled)
      {
        this.m_opaquePPPass.Setup(targetIdentifier);
        this.m_passQueue.Add((AbstractPass) this.m_opaquePPPass);
      }
      if (renderingData.camera.clearFlags == CameraClearFlags.Skybox)
        this.m_passQueue.Add((AbstractPass) this.m_skyboxPass);
      if (renderingData.needReadableColorOpaqueTexture)
      {
        this.m_blitColorToReadableColorPass.Setup(targetIdentifier, (RenderTargetIdentifier) this.m_depthRT);
        this.m_passQueue.Add((AbstractPass) this.m_blitColorToReadableColorPass);
      }
      int LayerMask = cullingMask;
      if (renderingData.antiAliasing == AAType.FXAA || renderingData.antiAliasing == AAType.SMAA)
      {
        this.m_transparentWaterPass.Setup(targetIdentifier, (RenderTargetIdentifier) this.m_depthRT, LayerMaskNames.waterMask);
        this.m_passQueue.Add((AbstractPass) this.m_transparentWaterPass);
        this.m_aaPass.Setup(targetIdentifier, (RenderTargetIdentifier) this.m_depthRT);
        this.m_passQueue.Add((AbstractPass) this.m_aaPass);
        LayerMask &= ~(1 << LayerMaskNames.waterLayer);
      }
      this.m_transparentPass.Setup(targetIdentifier, (RenderTargetIdentifier) this.m_depthRT, LayerMask);
      this.m_passQueue.Add((AbstractPass) this.m_transparentPass);
      if (num != 0)
      {
        this.m_desaturatePass.Setup(CubeSRP.characterFocusFactor, (RenderTargetIdentifier) this.m_colorRT);
        this.m_passQueue.Add((AbstractPass) this.m_desaturatePass);
        this.m_opaqueIsolatedCharacterPass.Setup(targetIdentifier, (RenderTargetIdentifier) this.m_depthRT, LayerMaskNames.characterFocusMask);
        this.m_passQueue.Add((AbstractPass) this.m_opaqueIsolatedCharacterPass);
        this.m_transparentIsolatedCharacterPass.Setup(targetIdentifier, (RenderTargetIdentifier) this.m_depthRT, LayerMaskNames.characterFocusMask);
        this.m_passQueue.Add((AbstractPass) this.m_transparentIsolatedCharacterPass);
      }
      if (targetIdentifier != renderingData.finalBackBuffer && !renderingData.isRenderingToTexture)
      {
        if (renderingData.postProcessEnabled)
        {
          this.m_transparentPostProcessPass.Setup(targetIdentifier, renderingData.finalBackBuffer);
          this.m_passQueue.Add((AbstractPass) this.m_transparentPostProcessPass);
        }
        else
        {
          this.m_blitColorBufferToBackBufferPass.Setup(targetIdentifier, renderingData.finalBackBuffer);
          this.m_passQueue.Add((AbstractPass) this.m_blitColorBufferToBackBufferPass);
        }
      }
      context.ExecuteCommandBuffer(commandBuffer);
      CommandBufferPool.Release(commandBuffer);
    }

    public void Render(ref ScriptableRenderContext context, ref RenderingData renderingData)
    {
      this.Setup(ref context, ref renderingData);
      for (int index = 0; index < this.m_passQueue.Count; ++index)
        this.m_passQueue[index].Execute(ref context, ref renderingData, this);
      this.DisposePasses(ref context);
    }

    private void DisposePasses(ref ScriptableRenderContext context)
    {
      CommandBuffer commandBuffer = CommandBufferPool.Get("Release Resources");
      for (int index = 0; index < this.m_passQueue.Count; ++index)
        this.m_passQueue[index].ReleaseResources(commandBuffer);
      SRPUtility.ReleaseTemporaryRT(commandBuffer, ref this.m_colorRT);
      SRPUtility.ReleaseTemporaryRT(commandBuffer, ref this.m_depthRT);
      context.ExecuteCommandBuffer(commandBuffer);
      CommandBufferPool.Release(commandBuffer);
    }

    public void RenderPostProcess(
      CommandBuffer cmd,
      ref RenderingData renderingData,
      RenderTextureFormat colorFormat,
      RenderTargetIdentifier source,
      RenderTargetIdentifier dest,
      bool opaqueOnly)
    {
      Camera camera = renderingData.camera;
      this.m_ppRenderContext.Reset();
      this.m_ppRenderContext.camera = camera;
      this.m_ppRenderContext.source = source;
      this.m_ppRenderContext.sourceFormat = colorFormat;
      this.m_ppRenderContext.destination = dest;
      this.m_ppRenderContext.command = cmd;
      this.m_ppRenderContext.flip = (Object) camera.targetTexture == (Object) null && dest == (RenderTargetIdentifier) BuiltinRenderTextureType.CameraTarget;
      if (opaqueOnly)
        renderingData.postProcessLayer.RenderOpaqueOnly(this.m_ppRenderContext);
      else
        renderingData.postProcessLayer.Render(this.m_ppRenderContext);
    }

    private bool RequireDepthNormalRT(
      PostProcessLayer postProcessLayer,
      PostProcessRenderContext ppRenderContext)
    {
      if ((Object) postProcessLayer == (Object) null)
        return false;
      PostProcessBundle bundle = postProcessLayer.GetBundle<CustomAmbientOcclusion>();
      return bundle != null && bundle.settings.IsEnabledAndSupported(ppRenderContext);
    }
  }
}
