// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.WindManager
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Ankama.Cube.SRP
{
  public class WindManager : IBeforeCameraRender
  {
    private static WindManager m_instance;
    private bool m_isActive;
    private List<Wind> m_winds = new List<Wind>();

    public static WindManager instance
    {
      get
      {
        if (WindManager.m_instance == null)
          WindManager.m_instance = new WindManager();
        return WindManager.m_instance;
      }
    }

    public Vector3 direction => this.m_winds.Count == 0 ? Vector3.zero : this.m_winds[0].direction;

    public float intensity => this.m_winds.Count == 0 ? 0.0f : this.m_winds[0].intensity;

    public void Add(Wind wind)
    {
      this.m_winds.Add(wind);
      this.UpdateState();
    }

    public void Remove(Wind wind)
    {
      this.m_winds.Remove(wind);
      this.UpdateState();
    }

    private void UpdateState()
    {
      bool flag = this.m_winds.Count > 0;
      if (flag == this.m_isActive)
        return;
      this.m_isActive = flag;
      if (this.m_isActive)
      {
        CubeSRP.s_beforeCameraRender.Add((IBeforeCameraRender) this);
      }
      else
      {
        CubeSRP.s_beforeCameraRender.Remove((IBeforeCameraRender) this);
        SRPUtility.SetKeyword(WindManager.Uniforms.Wind, false);
      }
    }

    public void ExecuteBeforeCameraRender(Camera camera, ref ScriptableRenderContext context)
    {
      Wind wind = this.m_winds[0];
      WindQuality windQuality = QualityManager.current.windQuality;
      SRPUtility.SetKeyword(WindManager.Uniforms.Wind, windQuality == WindQuality.Enable);
      Shader.SetGlobalVector(WindManager.Uniforms._WindDirection, (Vector4) wind.direction);
      Shader.SetGlobalVector(WindManager.Uniforms._WindParams, wind.shaderParams);
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct Uniforms
    {
      internal static readonly string Wind = "_WIND";
      internal static readonly int _WindDirection = Shader.PropertyToID(nameof (_WindDirection));
      internal static readonly int _WindParams = Shader.PropertyToID(nameof (_WindParams));
    }
  }
}
