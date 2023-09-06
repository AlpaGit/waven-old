// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.SRPCamera
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Ankama.Cube.SRP
{
  [RequireComponent(typeof (Camera))]
  [DisallowMultipleComponent]
  [ExecuteInEditMode]
  public class SRPCamera : MonoBehaviour
  {
    [SerializeField]
    private bool m_renderShadows = true;
    [SerializeField]
    private bool m_renderDepthPrePass = true;
    [SerializeField]
    private bool m_needReadableDepthTexture = true;
    [SerializeField]
    private bool m_needReadableColorOpaqueTexture = true;
    [SerializeField]
    private bool m_antiAliasing = true;
    [SerializeField]
    private bool m_characterFocus;
    [SerializeField]
    private PostProcessLayer m_postProcessLayer;

    public PostProcessLayer postProcessLayer => this.m_postProcessLayer;

    public bool renderShadows => this.m_renderShadows;

    public bool renderDepthPrePass => this.m_renderDepthPrePass;

    public bool needReadableDepthTexture => this.m_needReadableDepthTexture;

    public bool needReadableColorOpaqueTexture => this.m_needReadableColorOpaqueTexture;

    public bool antiAliasing => this.m_antiAliasing;

    public bool characterFocus => this.m_characterFocus;
  }
}
