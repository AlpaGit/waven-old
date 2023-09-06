// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.GameObjectNoiseMovement
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Animations
{
  public class GameObjectNoiseMovement : MonoBehaviour
  {
    [SerializeField]
    private bool m_enable_X;
    [SerializeField]
    private bool m_enable_Y;
    [SerializeField]
    private bool m_enable_Z;
    [SerializeField]
    private Vector3 m_noiseSpeed;
    [SerializeField]
    private Vector3 m_noiseStrength;
    private Transform m_trsf;

    private void Awake() => this.m_trsf = this.transform;

    private void Update()
    {
      Vector3 vector3 = new Vector3(0.0f, 0.0f, 0.0f);
      if (this.m_enable_X)
        vector3.x = Mathf.PerlinNoise(0.0f, Time.time * this.m_noiseSpeed.x) * this.m_noiseStrength.x;
      if (this.m_enable_Y)
        vector3.y = Mathf.PerlinNoise(0.3f, Time.time * this.m_noiseSpeed.y) * this.m_noiseStrength.y;
      if (this.m_enable_Z)
        vector3.z = Mathf.PerlinNoise(0.7f, Time.time * this.m_noiseSpeed.z) * this.m_noiseStrength.z;
      this.m_trsf.localPosition = vector3;
    }
  }
}
