// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.FogManager
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Ankama.Cube.SRP
{
  public class FogManager : IBeforeCameraRender
  {
    private List<Fog> m_fogs = new List<Fog>();
    private bool m_isActive;
    private static FogManager m_instance;

    public static FogManager instance
    {
      get
      {
        if (FogManager.m_instance == null)
          FogManager.m_instance = new FogManager();
        return FogManager.m_instance;
      }
    }

    public void Register(Fog fog)
    {
      this.m_fogs.Add(fog);
      this.UpdateState();
    }

    public void Unregister(Fog fog)
    {
      this.m_fogs.Remove(fog);
      this.UpdateState();
    }

    private void UpdateState()
    {
      bool flag = this.m_fogs.Count > 0;
      if (this.m_isActive == flag)
        return;
      this.m_isActive = flag;
      if (this.m_isActive)
      {
        CubeSRP.s_beforeCameraRender.Add((IBeforeCameraRender) this);
      }
      else
      {
        CubeSRP.s_beforeCameraRender.Remove((IBeforeCameraRender) this);
        SRPUtility.SetKeyword("_FOG", false);
        SRPUtility.SetKeyword("_FOG_VERTEX", false);
      }
    }

    public void ExecuteBeforeCameraRender(Camera camera, ref ScriptableRenderContext context)
    {
      if (camera.cameraType == CameraType.Reflection)
        return;
      Fog fog = this.m_fogs[0];
      FogQuality fogQuality = QualityManager.current.fogQuality;
      SRPUtility.SetKeyword("_FOG", fogQuality == FogQuality.Pixel);
      SRPUtility.SetKeyword("_FOG_VERTEX", fogQuality == FogQuality.Vertex);
      Shader.SetGlobalColor(FogManager.Uniforms._FogColor, fog.color);
      Shader.SetGlobalVector(FogManager.Uniforms._FogParams, fog.shaderParams);
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct Uniforms
    {
      internal static readonly int _FogParams = Shader.PropertyToID(nameof (_FogParams));
      internal static readonly int _FogColor = Shader.PropertyToID(nameof (_FogColor));
    }
  }
}
