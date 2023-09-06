// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.Wind
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using JetBrains.Annotations;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Ankama.Cube.SRP
{
  [ExecuteInEditMode]
  public class Wind : MonoBehaviour
  {
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_intensity = 0.5f;
    [SerializeField]
    private AnimationCurve m_intensityCurve = AnimationCurve.Constant(0.0f, 10f, 1f);
    private float m_time;
    private float m_intensityFactor;

    public Vector3 direction => this.transform.forward;

    public float intensity => this.m_intensityFactor;

    public Vector4 shaderParams => new Vector4(this.m_time, this.m_intensityFactor, 1f, 1f);

    protected void OnEnable() => WindManager.instance.Add(this);

    protected void OnDisable() => WindManager.instance.Remove(this);

    protected void Update()
    {
      this.m_intensityFactor = this.m_intensity * Mathf.Clamp01(this.m_intensityCurve.Evaluate(Time.time));
      this.m_time += Time.deltaTime * this.m_intensityFactor;
    }

    [UsedImplicitly]
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct Uniforms
    {
      internal static readonly string _WindKeyword = "_WIND";
      [UsedImplicitly]
      internal static readonly int _WindDirection = Shader.PropertyToID(nameof (_WindDirection));
      [UsedImplicitly]
      internal static readonly int _WindParams = Shader.PropertyToID(nameof (_WindParams));
    }
  }
}
