// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.CubeSRP
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Profiling;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace Ankama.Cube.SRP
{
  public class CubeSRP : RenderPipeline
  {
    public const int DepthBufferBits = 32;
    public const int ShadowmapBufferBits = 16;
    private static Dictionary<object, float> s_lightIntensityFactors = new Dictionary<object, float>();
    public static List<IBeforeCameraCulling> s_beforeCameraCulling = new List<IBeforeCameraCulling>();
    public static List<IBeforeCameraRender> s_beforeCameraRender = new List<IBeforeCameraRender>();
    public static List<IAfterCameraRender> s_afterCameraRender = new List<IAfterCameraRender>();
    public static Dictionary<Camera, IBlurCamera> s_blurCamera = new Dictionary<Camera, IBlurCamera>();
    private CameraComparer m_cameraComparer = new CameraComparer();
    private CullResults m_cullResults;
    private RenderingData m_renderingData;
    private ForwardRenderer m_forwardRenderer;
    private LightsHandler m_lightsHandler;
    private int m_tempBackBuffer = -1;
    private const string RenderCameraTag = "Render Camera";

    public static RenderTextureFormat shadowmapFormat { get; private set; }

    public static bool supportShadowTexture { get; private set; }

    public static bool supportDepthTexture { get; private set; }

    public static float characterFocusFactor { get; set; }

    public static Camera currentRenderingCamera { get; private set; }

    public static CubeSRPResources resources { get; private set; }

    public static PostProcessResources postProcessResources { get; private set; }

    public static CubeSRPQualitySettings qualitySettings { get; private set; }

    public static CubeSRPAsset asset { get; private set; }

    public CubeSRP(CubeSRPAsset pipelineAsset)
    {
      CubeSRP.asset = pipelineAsset;
      CubeSRP.postProcessResources = pipelineAsset.postProcessResources;
      CubeSRP.resources = pipelineAsset.resources;
      CubeSRP.resources.Init(pipelineAsset);
      QualityManager.onChanged += new Action<QualityAsset>(this.UpdateQualitySettings);
      this.UpdateQualitySettings(QualityManager.current);
      Shader.globalRenderPipeline = "LightweightPipeline";
      CubeSRP.shadowmapFormat = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Shadowmap) ? RenderTextureFormat.Shadowmap : RenderTextureFormat.Depth;
      CubeSRP.supportShadowTexture = SystemInfo.SupportsRenderTextureFormat(CubeSRP.shadowmapFormat);
      CubeSRP.supportDepthTexture = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth);
      this.m_forwardRenderer = new ForwardRenderer(pipelineAsset);
      this.m_lightsHandler = new LightsHandler();
    }

    public override void Dispose()
    {
      base.Dispose();
      Shader.globalRenderPipeline = "";
      this.m_lightsHandler.Dispose();
      this.m_forwardRenderer.Dispose();
      CubeSRP.resources.Dispose();
    }

    public override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
      if (cameras.Length == 0)
        return;
      base.Render(context, cameras);
      RenderPipeline.BeginFrameRendering(cameras);
      GraphicsSettings.lightsUseLinearIntensity = true;
      this.SetupPerFrameShaderConstants();
      Array.Sort<Camera>(cameras, (IComparer<Camera>) this.m_cameraComparer);
      this.m_tempBackBuffer = -1;
      if (CubeSRP.s_blurCamera.Count > 0)
      {
        Camera camera = cameras[0];
        this.m_tempBackBuffer = RenderTargetIdentifiers._TempBackBuffer;
        CommandBuffer commandBuffer = CommandBufferPool.Get("Get tempBackBuffer");
        commandBuffer.GetTemporaryRT(this.m_tempBackBuffer, new RenderTextureDescriptor(camera.pixelWidth, camera.pixelHeight)
        {
          colorFormat = RenderTextureFormat.Default,
          enableRandomWrite = false,
          depthBufferBits = 32,
          sRGB = true,
          msaaSamples = 1
        }, FilterMode.Bilinear);
        CoreUtils.SetRenderTarget(commandBuffer, (RenderTargetIdentifier) this.m_tempBackBuffer, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, ClearFlag.All, Color.black);
        context.ExecuteCommandBuffer(commandBuffer);
        CommandBufferPool.Release(commandBuffer);
        context.Submit();
      }
      int length = cameras.Length;
      for (int index = 0; index < length; ++index)
      {
        Camera camera = cameras[index];
        RenderPipeline.BeginCameraRendering(camera);
        this.RenderCamera(ref context, camera);
      }
      if (this.m_tempBackBuffer == -1)
        return;
      CommandBuffer commandBuffer1 = CommandBufferPool.Get("Blit tempBackBuffer to backBuffer");
      commandBuffer1.SetGlobalTexture(ShaderProperties._BlitTex, (RenderTargetIdentifier) this.m_tempBackBuffer);
      commandBuffer1.Blit((RenderTargetIdentifier) this.m_tempBackBuffer, (RenderTargetIdentifier) BuiltinRenderTextureType.CameraTarget, CubeSRP.resources.blitMaterial);
      commandBuffer1.ReleaseTemporaryRT(this.m_tempBackBuffer);
      context.ExecuteCommandBuffer(commandBuffer1);
      CommandBufferPool.Release(commandBuffer1);
      context.Submit();
    }

    private void RenderCamera(ref ScriptableRenderContext context, Camera camera)
    {
      CubeSRP.currentRenderingCamera = camera;
      string name = "Render Camera";
      CommandBuffer commandBuffer = CommandBufferPool.Get(name);
      using (new ProfilingSample(commandBuffer, name, (CustomSampler) null))
      {
        ScriptableCullingParameters cullingParameters;
        if (!CullResults.GetCullingParameters(camera, false, out cullingParameters))
        {
          CommandBufferPool.Release(commandBuffer);
          return;
        }
        cullingParameters.shadowDistance = Mathf.Min(CubeSRP.qualitySettings.shadowDistance, camera.farClipPlane);
        context.ExecuteCommandBuffer(commandBuffer);
        commandBuffer.Clear();
        Camera.CameraCallback onPreCull = Camera.onPreCull;
        if (onPreCull != null)
          onPreCull(camera);
        for (int index = 0; index < CubeSRP.s_beforeCameraCulling.Count; ++index)
          CubeSRP.s_beforeCameraCulling[index].ExecuteBeforeCameraCulling(camera, ref context);
        CullResults.Cull(ref cullingParameters, context, ref this.m_cullResults);
        this.m_lightsHandler.Setup(ref this.m_cullResults);
        RenderingData.GetRenderingData(camera, CubeSRP.qualitySettings, this.m_lightsHandler, ref this.m_cullResults, out this.m_renderingData, this.m_tempBackBuffer);
        this.SetupPerCameraShaderConstants(ref this.m_renderingData);
        Camera.CameraCallback onPreRender = Camera.onPreRender;
        if (onPreRender != null)
          onPreRender(camera);
        for (int index = 0; index < CubeSRP.s_beforeCameraRender.Count; ++index)
          CubeSRP.s_beforeCameraRender[index].ExecuteBeforeCameraRender(camera, ref context);
        this.m_forwardRenderer.Render(ref context, ref this.m_renderingData);
      }
      context.ExecuteCommandBuffer(commandBuffer);
      CommandBufferPool.Release(commandBuffer);
      context.Submit();
      Camera.CameraCallback onPostRender = Camera.onPostRender;
      if (onPostRender != null)
        onPostRender(camera);
      for (int index = 0; index < CubeSRP.s_afterCameraRender.Count; ++index)
        CubeSRP.s_afterCameraRender[index].ExecuteAfterCameraRender(camera, ref context);
      CubeSRP.currentRenderingCamera = (Camera) null;
      SRPPlanarReflection.s_renderedPeflectionPlanes.Clear();
    }

    private void SetupPerFrameShaderConstants() => Shader.SetGlobalVector(PerFrameBuffer._ShadowColor, (Vector4) CoreUtils.ConvertSRGBToActiveColorSpace(RenderSettings.ambientLight * CubeSRP.GetLightIntensityFactor()));

    private void SetupPerCameraShaderConstants(ref RenderingData renderingData)
    {
      Camera camera = renderingData.camera;
      float renderScale = renderingData.renderScale;
      float x = (float) camera.pixelWidth * renderScale;
      float y = (float) camera.pixelHeight * renderScale;
      Shader.SetGlobalVector(PerCameraBuffer._ScaledScreenParams, new Vector4(x, y, (float) (1.0 + 1.0 / (double) x), (float) (1.0 + 1.0 / (double) y)));
    }

    private void UpdateQualitySettings(QualityAsset asset)
    {
      CubeSRP.qualitySettings = CubeSRPQualitySettings.Create(asset);
      if ((MSAASamples) QualitySettings.antiAliasing == CubeSRP.qualitySettings.msaaQuality)
        return;
      QualitySettings.antiAliasing = (int) CubeSRP.qualitySettings.msaaQuality;
    }

    public static void SetLightIntensityFactor(object from, float factor) => CubeSRP.s_lightIntensityFactors[from] = factor;

    public static void RemoveLightIntensityFactor(object from) => CubeSRP.s_lightIntensityFactors.Remove(from);

    public static float GetLightIntensityFactor()
    {
      float lightIntensityFactor1 = 1f;
      foreach (KeyValuePair<object, float> lightIntensityFactor2 in CubeSRP.s_lightIntensityFactors)
        lightIntensityFactor1 *= lightIntensityFactor2.Value;
      return lightIntensityFactor1;
    }
  }
}
