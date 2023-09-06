// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.QualityAsset
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace Ankama.Cube.SRP
{
  public class QualityAsset : ScriptableObject
  {
    [SerializeField]
    private bool m_postProcess = true;
    [SerializeField]
    private AmbientOcclusionQuality m_aoQuality = AmbientOcclusionQuality.Low;
    [SerializeField]
    [Range(0.0f, 16f)]
    private int m_maxPixelLights = 4;
    [SerializeField]
    private bool m_supportsVertexLight = true;
    [SerializeField]
    private AAType m_aaType;
    [SerializeField]
    private MSAASamples m_mSAA = MSAASamples.MSAA4x;
    [SerializeField]
    private SubpixelMorphologicalAntialiasing.Quality m_subpixelMorphologicalAntialiasingQuality;
    [SerializeField]
    private FXAAQuality m_fastApproximateAntialiasingQuality = FXAAQuality.Standard;
    [SerializeField]
    private bool m_hdr;
    [SerializeField]
    [Range(0.1f, 4f)]
    private float m_renderScale = 1f;
    [SerializeField]
    private bool m_dynamicBatching = true;
    [SerializeField]
    private FogQuality m_fogQuality = FogQuality.Pixel;
    [SerializeField]
    private WindQuality m_windQuality = WindQuality.Enable;
    [SerializeField]
    private bool m_directionalShadows = true;
    [SerializeField]
    private ShadowResolution m_shadowAtlasResolution = ShadowResolution._2048;
    [SerializeField]
    [Range(0.0f, 2f)]
    private float m_directionalShadowBias = 0.05f;
    [SerializeField]
    [Range(0.0f, 3f)]
    private float m_directionalShadowNormalBias = 0.01f;
    [SerializeField]
    private bool m_cloudsShadows = true;
    [SerializeField]
    private ShadowResolution m_cloudsShadowResolution = ShadowResolution._256;
    [SerializeField]
    private bool m_localShadows = true;
    [SerializeField]
    private ShadowResolution m_localShadowsAtlasResolution = ShadowResolution._512;
    [SerializeField]
    private float m_shadowDistance = 50f;
    [SerializeField]
    private ShadowQuality m_shadowQuality = ShadowQuality.PCF;
    [SerializeField]
    private bool m_reflection = true;
    [SerializeField]
    private ShadowQuality m_reflectionShadowQuality = ShadowQuality.PCF;
    [SerializeField]
    private bool m_reflectionClampRenderScale;
    [SerializeField]
    private bool m_depthPrepass = true;
    [SerializeField]
    private bool m_readableDepthTexture = true;
    [SerializeField]
    private bool m_readableOpaqueTexture = true;
    [SerializeField]
    private UIBlurQuality m_uiBlurQuality = UIBlurQuality.Medium;

    public bool postProcess => this.m_postProcess;

    public AAType antiAliasing => this.m_aaType;

    public MSAASamples msaaQuality => this.m_mSAA;

    public FogQuality fogQuality => this.m_fogQuality;

    public WindQuality windQuality => this.m_windQuality;

    public bool hdr => this.m_hdr;

    public float renderScale => this.m_renderScale;

    public bool directionalShadows => this.m_directionalShadows;

    public int directionalShadowAtlasResolution => (int) this.m_shadowAtlasResolution;

    public float directionalShadowBias => this.m_directionalShadowBias;

    public float directionalShadowNormalBias => this.m_directionalShadowNormalBias;

    public bool cloudsShadows => this.m_cloudsShadows;

    public int cloudsShadowResolution => (int) this.m_cloudsShadowResolution;

    public float shadowDistance
    {
      get => this.m_shadowDistance;
      set => this.m_shadowDistance = value;
    }

    public bool localShadows => this.m_localShadows;

    public int localShadowAtlasResolution => (int) this.m_localShadowsAtlasResolution;

    public int maxPixelLights => this.m_maxPixelLights;

    public bool vertexLight => this.m_supportsVertexLight;

    public bool dynamicBatching => this.m_dynamicBatching;

    public ShadowQuality shadowQuality => this.m_shadowQuality;

    public bool reflection => this.m_reflection;

    public ShadowQuality reflectionShadowQuality => this.m_reflectionShadowQuality;

    public bool reflectionClampRenderScale => this.m_reflectionClampRenderScale;

    public bool readableOpaqueTexture => this.m_readableOpaqueTexture;

    public bool readableDepthTexture => this.m_readableDepthTexture;

    public bool depthPrepass => this.m_depthPrepass;

    public UIBlurQuality uiBlurQuality
    {
      get => this.m_uiBlurQuality;
      set
      {
        this.m_uiBlurQuality = value;
        QualityManager.NotifyQualityChanged(this);
      }
    }

    public FXAAQuality fxaaQuality => this.m_fastApproximateAntialiasingQuality;

    public SubpixelMorphologicalAntialiasing.Quality smaaQuality => this.m_subpixelMorphologicalAntialiasingQuality;

    public AmbientOcclusionQuality aoQuality => this.m_aoQuality;
  }
}
