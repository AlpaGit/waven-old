// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.RenderingData
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace Ankama.Cube.SRP
{
  public struct RenderingData
  {
    public Camera camera;
    public CullResults cullResult;
    public RenderTextureDescriptor baseRTDescriptor;
    public LightsHandler lightHandler;
    public bool isRenderScaled;
    public float renderScale;
    public bool isSceneViewCamera;
    public bool isDefaultViewport;
    public bool isRenderingToTexture;
    public bool isHdrEnabled;
    public bool renderDirectionalShadow;
    public bool renderCloudShadow;
    public bool renderLocalShadow;
    public ShadowQuality shadowQuality;
    public bool renderDepth;
    public bool needReadableDepthTexture;
    public bool needReadableColorOpaqueTexture;
    public bool characterFocus;
    public AAType antiAliasing;
    public int msaaSamples;
    public IBlurCamera blur;
    public bool postProcessEnabled;
    public PostProcessLayer postProcessLayer;
    public RenderTargetIdentifier finalBackBuffer;
    public RendererConfiguration renderConfiguration;

    public static void GetRenderingData(
      Camera camera,
      CubeSRPQualitySettings settings,
      LightsHandler lightHandler,
      ref CullResults cullResult,
      out RenderingData renderingData,
      int tempBackBuffer = -1)
    {
      renderingData.camera = camera;
      renderingData.cullResult = cullResult;
      renderingData.isSceneViewCamera = camera.cameraType == CameraType.SceneView;
      renderingData.isRenderingToTexture = (UnityEngine.Object) camera.targetTexture != (UnityEngine.Object) null && !renderingData.isSceneViewCamera;
      renderingData.isHdrEnabled = camera.allowHDR && settings.hdr;
      Rect rect = camera.rect;
      renderingData.isDefaultViewport = (double) Math.Abs(rect.x) <= 0.0 && (double) Math.Abs(rect.y) <= 0.0 && (double) Math.Abs(rect.width) >= 1.0 && (double) Math.Abs(rect.height) >= 1.0;
      if (camera.cameraType == CameraType.Game)
      {
        renderingData.renderScale = settings.renderScale;
        renderingData.isRenderScaled = !Mathf.Approximately(renderingData.renderScale, 1f);
      }
      else
      {
        renderingData.renderScale = 1f;
        renderingData.isRenderScaled = false;
      }
      bool flag = (double) settings.shadowDistance > 0.0 && CubeSRP.supportShadowTexture;
      renderingData.renderDirectionalShadow = settings.mainShadow & flag;
      renderingData.renderCloudShadow = settings.cloudShadow && renderingData.renderDirectionalShadow;
      renderingData.renderLocalShadow = settings.additionalShadow & flag;
      renderingData.shadowQuality = settings.shadowQuality;
      renderingData.renderDepth = renderingData.isSceneViewCamera;
      renderingData.needReadableDepthTexture = renderingData.isSceneViewCamera;
      renderingData.needReadableColorOpaqueTexture = renderingData.isSceneViewCamera;
      renderingData.antiAliasing = AAType.Disable;
      renderingData.msaaSamples = 1;
      renderingData.postProcessEnabled = false;
      renderingData.postProcessLayer = (PostProcessLayer) null;
      renderingData.characterFocus = false;
      SRPCamera component = camera.gameObject.GetComponent<SRPCamera>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null && component.isActiveAndEnabled)
      {
        renderingData.renderDirectionalShadow &= component.renderShadows;
        renderingData.renderLocalShadow &= component.renderShadows;
        renderingData.antiAliasing = component.antiAliasing ? settings.antiAliasing : AAType.Disable;
        if (renderingData.antiAliasing == AAType.MSAA)
          renderingData.msaaSamples = (UnityEngine.Object) camera.targetTexture != (UnityEngine.Object) null ? camera.targetTexture.antiAliasing : (int) settings.msaaQuality;
        renderingData.postProcessLayer = component.postProcessLayer;
        renderingData.postProcessEnabled = settings.postProcess && (UnityEngine.Object) renderingData.postProcessLayer != (UnityEngine.Object) null && renderingData.postProcessLayer.isActiveAndEnabled;
        ref bool local1 = ref renderingData.renderDepth;
        local1 = ((local1 ? 1 : 0) | (!component.renderDepthPrePass ? 0 : (settings.depthPrePass ? 1 : 0))) != 0;
        ref bool local2 = ref renderingData.needReadableDepthTexture;
        local2 = ((local2 ? 1 : 0) | (!component.needReadableDepthTexture ? 0 : (settings.readableDepth ? 1 : 0))) != 0;
        ref bool local3 = ref renderingData.needReadableColorOpaqueTexture;
        local3 = ((local3 ? 1 : 0) | (!component.needReadableColorOpaqueTexture ? 0 : (settings.readableOpaqueColor ? 1 : 0))) != 0;
        renderingData.characterFocus = component.characterFocus;
      }
      CubeSRP.s_blurCamera.TryGetValue(camera, out renderingData.blur);
      RenderTextureDescriptor textureDescriptor = new RenderTextureDescriptor(camera.pixelWidth, camera.pixelHeight)
      {
        colorFormat = renderingData.isHdrEnabled ? RenderTextureFormat.DefaultHDR : RenderTextureFormat.Default,
        enableRandomWrite = false
      };
      textureDescriptor.width = (int) ((double) textureDescriptor.width * (double) renderingData.renderScale);
      textureDescriptor.height = (int) ((double) textureDescriptor.height * (double) renderingData.renderScale);
      renderingData.baseRTDescriptor = textureDescriptor;
      renderingData.finalBackBuffer = tempBackBuffer != -1 ? new RenderTargetIdentifier(tempBackBuffer) : (RenderTargetIdentifier) BuiltinRenderTextureType.CameraTarget;
      renderingData.lightHandler = lightHandler;
      renderingData.renderConfiguration = RenderingData.GetRenderConfiguration(lightHandler);
    }

    public bool RequireColorRT()
    {
      if (this.isRenderingToTexture)
        return false;
      return this.isSceneViewCamera || this.isRenderScaled || this.isHdrEnabled || this.msaaSamples > 1 || this.postProcessEnabled || !this.isDefaultViewport || this.antiAliasing != AAType.Disable || this.needReadableColorOpaqueTexture;
    }

    private static RendererConfiguration GetRenderConfiguration(LightsHandler lightHandler)
    {
      RendererConfiguration renderConfiguration = RendererConfiguration.None;
      if (lightHandler.localLightIndex.Count > 0)
      {
        if (lightHandler.useComputeBufferForPerObjectLightIndices)
          renderConfiguration |= RendererConfiguration.ProvideLightIndices;
        else
          renderConfiguration |= RendererConfiguration.PerObjectLightIndices8;
      }
      return renderConfiguration;
    }
  }
}
