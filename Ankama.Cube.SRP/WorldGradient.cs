// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.WorldGradient
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Ankama.Cube.SRP
{
  [ExecuteInEditMode]
  public class WorldGradient : MonoBehaviour
  {
    private static List<WorldGradient> m_instances = new List<WorldGradient>();
    private static bool m_isActive;
    [SerializeField]
    public Color m_color = Color.white;
    [SerializeField]
    [Min(0.01f)]
    public float m_fade;
    [SerializeField]
    public float m_worldOffset;

    protected void OnEnable()
    {
      WorldGradient.m_instances.Add(this);
      WorldGradient.UpdateState();
    }

    protected void OnDisable()
    {
      WorldGradient.m_instances.Remove(this);
      WorldGradient.UpdateState();
    }

    protected void Update()
    {
      if ((Object) this != (Object) WorldGradient.m_instances[0] || !WorldGradient.m_isActive)
        return;
      float y = this.transform.position.y + this.m_worldOffset;
      Shader.SetGlobalColor(WorldGradient.Uniforms._GradientColor, this.m_color);
      Shader.SetGlobalVector(WorldGradient.Uniforms._GradientParams, new Vector4(y - this.m_fade, y, this.m_fade, 0.0f));
    }

    private static void UpdateState()
    {
      bool flag = WorldGradient.m_instances != null && WorldGradient.m_instances.Count > 0;
      if (WorldGradient.m_isActive == flag)
        return;
      WorldGradient.m_isActive = flag;
      if (WorldGradient.m_isActive)
        Shader.EnableKeyword(WorldGradient.Uniforms._GradientKeyword);
      else
        Shader.DisableKeyword(WorldGradient.Uniforms._GradientKeyword);
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct Uniforms
    {
      internal static readonly string _GradientKeyword = "_WORLD_GRADIENT";
      internal static readonly int _GradientColor = Shader.PropertyToID("_WorldGradientColor");
      internal static readonly int _GradientParams = Shader.PropertyToID("_WorldGradientParams");
    }
  }
}
