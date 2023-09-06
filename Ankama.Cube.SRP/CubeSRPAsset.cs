// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.CubeSRPAsset
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace Ankama.Cube.SRP
{
  public class CubeSRPAsset : RenderPipelineAsset
  {
    [Header("Resources")]
    [SerializeField]
    private CubeSRPResources m_resourcesAsset;
    [SerializeField]
    private PostProcessResources m_postProcessResources;

    protected override IRenderPipeline InternalCreatePipeline() => (IRenderPipeline) new CubeSRP(this);

    public PostProcessResources postProcessResources => this.m_postProcessResources;

    public CubeSRPResources resources => this.m_resourcesAsset;
  }
}
