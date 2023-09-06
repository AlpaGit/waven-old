// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.CubeSRPQualitySettings
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace Ankama.Cube.SRP
{
  public struct CubeSRPQualitySettings
  {
    private const float kRenderScaleThreshold = 0.05f;

    public bool postProcess { get; private set; }

    public bool hdr { get; private set; }

    public bool dynamicBatching { get; private set; }

    public float renderScale { get; private set; }

    public AAType antiAliasing { get; private set; }

    public MSAASamples msaaQuality { get; private set; }

    public FXAAQuality fxaaQuality { get; private set; }

    public SubpixelMorphologicalAntialiasing.Quality smaaQuality { get; private set; }

    public int maxPixelLights { get; private set; }

    public bool vertexLights { get; private set; }

    public ShadowQuality shadowQuality { get; private set; }

    public float shadowDistance { get; private set; }

    public bool mainShadow { get; private set; }

    public int mainShadowResolution { get; private set; }

    public float mainShadowBias { get; private set; }

    public float mainShadowNormalBias { get; private set; }

    public bool additionalShadow { get; private set; }

    public int additionalShadowResolution { get; private set; }

    public bool cloudShadow { get; private set; }

    public int cloudShadowResolution { get; private set; }

    public bool depthPrePass { get; private set; }

    public bool readableDepth { get; private set; }

    public bool readableOpaqueColor { get; private set; }

    public ShadowQuality reflectionShadowQuality { get; private set; }

    public UIBlurQuality blurQuality { get; private set; }

    public static CubeSRPQualitySettings Create(QualityAsset asset) => new CubeSRPQualitySettings()
    {
      postProcess = asset.postProcess,
      hdr = asset.hdr,
      dynamicBatching = asset.dynamicBatching,
      renderScale = (double) Mathf.Abs(1f - asset.renderScale) < 0.05000000074505806 ? 1f : asset.renderScale,
      antiAliasing = asset.antiAliasing,
      msaaQuality = asset.antiAliasing == AAType.MSAA ? asset.msaaQuality : MSAASamples.None,
      fxaaQuality = asset.fxaaQuality,
      smaaQuality = asset.smaaQuality,
      maxPixelLights = asset.maxPixelLights,
      vertexLights = asset.vertexLight,
      shadowQuality = asset.shadowQuality,
      shadowDistance = asset.shadowDistance,
      mainShadow = asset.directionalShadows,
      mainShadowResolution = asset.directionalShadowAtlasResolution,
      mainShadowBias = asset.directionalShadowBias,
      mainShadowNormalBias = asset.directionalShadowNormalBias,
      additionalShadow = asset.localShadows,
      additionalShadowResolution = asset.localShadowAtlasResolution,
      cloudShadow = asset.cloudsShadows,
      cloudShadowResolution = asset.cloudsShadowResolution,
      depthPrePass = asset.depthPrepass,
      readableDepth = asset.readableDepthTexture,
      readableOpaqueColor = asset.readableOpaqueTexture,
      reflectionShadowQuality = asset.reflectionShadowQuality,
      blurQuality = asset.uiBlurQuality
    };
  }
}
