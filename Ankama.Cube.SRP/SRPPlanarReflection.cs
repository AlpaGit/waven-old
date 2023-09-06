// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.SRPPlanarReflection
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Ankama.Cube.SRP
{
  [ExecuteInEditMode]
  public class SRPPlanarReflection : MonoBehaviour, IBeforeCameraCulling, IAfterCameraRender
  {
    public static List<SRPPlanarReflection> s_renderedPeflectionPlanes = new List<SRPPlanarReflection>();
    [SerializeField]
    private Renderer m_targetRenderer;
    [SerializeField]
    private SRPPlanarReflection.FarClipMode m_farClipMode;
    [SerializeField]
    [Min(0.1f)]
    private float m_farClip = 100f;
    [SerializeField]
    [Min(0.1f)]
    private float m_fade = 2f;
    [SerializeField]
    private float m_clipPlaneOffset;
    [SerializeField]
    private LayerMask m_cullingMask = (LayerMask) 1;
    [SerializeField]
    [Range(1f, 10f)]
    private int m_downsample = 2;
    private bool m_isActive;
    private bool m_componentEnable;
    private Dictionary<Camera, RenderTexture> m_reflexionRtts = new Dictionary<Camera, RenderTexture>();
    private Dictionary<Camera, Vector2> m_lastCameraSizes = new Dictionary<Camera, Vector2>();
    private RenderTexture m_currentRtt;
    private MaterialPropertyBlock m_propertyBlock;

    public LayerMask cullingMask => this.m_cullingMask;

    public SRPPlanarReflection.FarClipMode farClipMode => this.m_farClipMode;

    public float farClip => this.m_farClip;

    public float clipPlaneOffset => this.m_clipPlaneOffset;

    public float fade => this.m_fade;

    public RenderTexture rt => this.m_currentRtt;

    private void OnEnable()
    {
      this.m_componentEnable = true;
      this.UpdateState();
      QualityManager.onChanged += new Action<QualityAsset>(this.OnQualityChanged);
    }

    private void OnDisable()
    {
      this.m_componentEnable = false;
      this.UpdateState();
      QualityManager.onChanged -= new Action<QualityAsset>(this.OnQualityChanged);
    }

    private void OnQualityChanged(QualityAsset asset) => this.UpdateState();

    private void UpdateState()
    {
      bool activeState = this.GetActiveState();
      if (this.m_isActive == activeState)
        return;
      this.m_isActive = activeState;
      if (this.m_isActive)
      {
        if (this.m_propertyBlock == null)
          this.m_propertyBlock = new MaterialPropertyBlock();
        CubeSRP.s_beforeCameraCulling.Add((IBeforeCameraCulling) this);
        CubeSRP.s_afterCameraRender.Add((IAfterCameraRender) this);
      }
      else
      {
        foreach (KeyValuePair<Camera, RenderTexture> reflexionRtt in this.m_reflexionRtts)
          SRPUtility.DestroyRtt(reflexionRtt.Value);
        this.m_reflexionRtts.Clear();
        this.m_lastCameraSizes.Clear();
        CubeSRP.s_beforeCameraCulling.Remove((IBeforeCameraCulling) this);
        CubeSRP.s_afterCameraRender.Remove((IAfterCameraRender) this);
      }
    }

    private bool GetActiveState() => this.isActiveAndEnabled && this.m_componentEnable && QualityManager.current.reflection && !((UnityEngine.Object) this.m_targetRenderer == (UnityEngine.Object) null);

    private RenderTexture GetRttFor(Camera cam)
    {
      Vector2 vector2;
      if (this.m_lastCameraSizes.TryGetValue(cam, out vector2) && ((double) vector2.x != (double) cam.pixelWidth || (double) vector2.y != (double) cam.pixelHeight))
      {
        SRPUtility.DestroyRtt(this.m_reflexionRtts[cam]);
        this.m_reflexionRtts.Remove(cam);
      }
      RenderTexture textureFor;
      if (!this.m_reflexionRtts.TryGetValue(cam, out textureFor))
      {
        textureFor = this.CreateTextureFor(cam);
        this.m_reflexionRtts[cam] = textureFor;
        this.m_lastCameraSizes[cam] = new Vector2((float) cam.pixelWidth, (float) cam.pixelHeight);
      }
      return textureFor;
    }

    private RenderTexture CreateTextureFor(Camera cam)
    {
      RenderTexture textureFor = new RenderTexture(Mathf.FloorToInt((float) (cam.pixelWidth / this.m_downsample)), Mathf.FloorToInt((float) (cam.pixelHeight / this.m_downsample)), 32, RenderTextureFormat.ARGB32);
      textureFor.Create();
      return textureFor;
    }

    private void OnWillRenderObject()
    {
      if (!this.m_isActive)
        return;
      Camera currentRenderingCamera = CubeSRP.currentRenderingCamera;
      if ((UnityEngine.Object) currentRenderingCamera == (UnityEngine.Object) null)
        return;
      this.m_currentRtt = this.GetRttFor(currentRenderingCamera);
      this.m_propertyBlock.SetTexture(RenderTargetIdentifiers._PlanarReflection, (Texture) this.m_currentRtt);
      this.m_targetRenderer.SetPropertyBlock(this.m_propertyBlock);
      if (SRPPlanarReflection.s_renderedPeflectionPlanes.Contains(this))
        return;
      SRPPlanarReflection.s_renderedPeflectionPlanes.Add(this);
    }

    public void ExecuteBeforeCameraCulling(Camera camera, ref ScriptableRenderContext context)
    {
    }

    public void ExecuteAfterCameraRender(Camera camera, ref ScriptableRenderContext context)
    {
    }

    public enum FarClipMode
    {
      EqualToCamera,
      ManualRelativeToCamera,
      ManualRelativeToPlane,
    }
  }
}
