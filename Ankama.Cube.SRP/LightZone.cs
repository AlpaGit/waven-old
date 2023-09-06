// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.LightZone
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;

namespace Ankama.Cube.SRP
{
  public class LightZone : MonoBehaviour
  {
    [SerializeField]
    private Vector3 m_center = Vector3.zero;
    [SerializeField]
    private Vector3 m_size = Vector3.one;
    [SerializeField]
    [Min(0.0f)]
    private float m_falloff = 1f;
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_factor = 1f;

    public Vector3 center => this.transform.position + this.m_center;

    public Vector4 params1 => new Vector4(this.center.x, this.center.y, this.center.z, this.m_factor);

    public Vector4 params2 => new Vector4(this.m_size.x, this.m_size.y, this.m_size.z, this.m_falloff);
  }
}
