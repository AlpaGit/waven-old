// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.CubeSRPResources
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Serialization;

namespace Ankama.Cube.SRP
{
  public class CubeSRPResources : ScriptableObject
  {
    [FormerlySerializedAs("BlitShader")]
    [SerializeField]
    private Shader m_blitShader;
    [FormerlySerializedAs("CopyDepthShader")]
    [SerializeField]
    private Shader m_copyDepthShader;
    [FormerlySerializedAs("SamplingShader")]
    [SerializeField]
    private Shader m_samplingShader;
    [FormerlySerializedAs("BlurShader")]
    [SerializeField]
    private Shader m_blurShader;
    [FormerlySerializedAs("DesaturateShader")]
    [SerializeField]
    private Shader m_desaturateShader;
    [FormerlySerializedAs("AOShader")]
    [SerializeField]
    private Shader m_aoShader;
    [FormerlySerializedAs("FXAAShader")]
    [SerializeField]
    private Shader m_fxaaShader;
    [FormerlySerializedAs("PCSSData")]
    [SerializeField]
    private PCSSData m_PCSSData;
    [Space]
    [SerializeField]
    private BlurDataDictionary m_blurDatas;
    private bool m_initialized;

    public Shader blitShader => this.m_blitShader;

    public Shader copyDepthShader => this.m_copyDepthShader;

    public Shader samplingShader => this.m_samplingShader;

    public Shader blurShader => this.m_blurShader;

    public Shader desaturateShader => this.m_desaturateShader;

    public Shader aoShader => this.m_aoShader;

    public Shader fxaaShader => this.m_fxaaShader;

    public PCSSData PCSSData => this.m_PCSSData;

    public Material errorMaterial { get; private set; }

    public Material copyDepthMaterial { get; private set; }

    public Material blitMaterial { get; private set; }

    public Material samplingMaterial { get; private set; }

    public Material blurMaterial { get; private set; }

    public Material desaturateMaterial { get; private set; }

    public Material ppFxaaMaterial { get; private set; }

    public Material ppSmaaMaterial { get; private set; }

    public Mesh fullscreenQuad { get; private set; }

    public void Init(CubeSRPAsset srpAsset)
    {
      if (this.m_initialized)
        return;
      this.errorMaterial = CoreUtils.CreateEngineMaterial("Hidden/InternalErrorShader");
      this.copyDepthMaterial = CoreUtils.CreateEngineMaterial(this.copyDepthShader);
      this.blitMaterial = CoreUtils.CreateEngineMaterial(this.blitShader);
      this.samplingMaterial = CoreUtils.CreateEngineMaterial(this.samplingShader);
      this.blurMaterial = CoreUtils.CreateEngineMaterial(this.blurShader);
      this.desaturateMaterial = CoreUtils.CreateEngineMaterial(this.desaturateShader);
      this.ppFxaaMaterial = CoreUtils.CreateEngineMaterial(this.fxaaShader);
      this.ppSmaaMaterial = CoreUtils.CreateEngineMaterial(srpAsset.postProcessResources.shaders.subpixelMorphologicalAntialiasing);
      this.fullscreenQuad = this.CreateFullScreenQuad();
      this.m_initialized = true;
    }

    public void Dispose()
    {
      if (!this.m_initialized)
        return;
      CoreUtils.Destroy((Object) this.errorMaterial);
      CoreUtils.Destroy((Object) this.copyDepthMaterial);
      CoreUtils.Destroy((Object) this.blitMaterial);
      CoreUtils.Destroy((Object) this.samplingMaterial);
      CoreUtils.Destroy((Object) this.blurMaterial);
      CoreUtils.Destroy((Object) this.desaturateMaterial);
      CoreUtils.Destroy((Object) this.ppFxaaMaterial);
      CoreUtils.Destroy((Object) this.ppSmaaMaterial);
      CoreUtils.Destroy((Object) this.fullscreenQuad);
      this.m_initialized = false;
    }

    public UIBlurData GetBlurData(UIBlurQuality quality) => this.m_blurDatas[quality];

    private Mesh CreateFullScreenQuad()
    {
      float y1 = 1f;
      float y2 = 0.0f;
      Mesh mesh = new Mesh();
      mesh.name = "Fullscreen Quad";
      Mesh fullScreenQuad = mesh;
      fullScreenQuad.SetVertices(new List<Vector3>()
      {
        new Vector3(-1f, -1f, 0.0f),
        new Vector3(-1f, 1f, 0.0f),
        new Vector3(1f, -1f, 0.0f),
        new Vector3(1f, 1f, 0.0f)
      });
      fullScreenQuad.SetUVs(0, new List<Vector2>()
      {
        new Vector2(0.0f, y2),
        new Vector2(0.0f, y1),
        new Vector2(1f, y2),
        new Vector2(1f, y1)
      });
      fullScreenQuad.SetIndices(new int[6]
      {
        0,
        1,
        2,
        2,
        1,
        3
      }, MeshTopology.Triangles, 0, false);
      fullScreenQuad.UploadMeshData(true);
      fullScreenQuad.hideFlags = HideFlags.HideAndDontSave;
      return fullScreenQuad;
    }
  }
}
