// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.SRPLight
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.SRP
{
  [RequireComponent(typeof (Light))]
  [DisallowMultipleComponent]
  [ExecuteInEditMode]
  public class SRPLight : MonoBehaviour
  {
    public static Dictionary<Light, SRPLight> s_lights = new Dictionary<Light, SRPLight>();
    [SerializeField]
    private Transform m_overrideDirection;
    [SerializeField]
    private LightZone m_lightZone;
    [SerializeField]
    [Range(0.0f, 1f)]
    private float m_cloudShadowStrength = 1f;

    public Light cachedLight { get; private set; }

    public Vector3 direction => (Object) this.m_overrideDirection != (Object) null ? this.m_overrideDirection.forward : this.transform.forward;

    public Quaternion overrideDirRotation
    {
      set
      {
        if (!((Object) this.m_overrideDirection != (Object) null))
          return;
        this.m_overrideDirection.rotation = value;
      }
      get => (Object) this.m_overrideDirection == (Object) null ? this.transform.rotation : this.m_overrideDirection.rotation;
    }

    public LightZone lightZone => this.m_lightZone;

    public float cloudShadowStrength => this.m_cloudShadowStrength;

    private void OnEnable()
    {
      this.cachedLight = this.GetComponent<Light>();
      SRPLight.s_lights[this.cachedLight] = this;
    }

    private void OnDisable() => SRPLight.s_lights.Remove(this.cachedLight);
  }
}
