// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.SRP.WindMill
// Assembly: Ankama.Cube.SRP, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1953DE0-1D2C-4C74-B3E1-FEE168A089B3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.SRP.dll

using UnityEngine;

namespace Ankama.Cube.SRP
{
  public class WindMill : MonoBehaviour
  {
    [SerializeField]
    private float m_horizontalRotationMaxSpeed = -200f;
    [SerializeField]
    private float m_verticalRotationSpeed = 4f;
    [SerializeField]
    private float m_verticalNoiseAmount = 5f;
    [SerializeField]
    private float m_verticalNoiseSpeed = 1f;
    [SerializeField]
    private Transform m_verticalAxis;
    [SerializeField]
    private Transform m_horizontalAxis;

    private void Update()
    {
      Vector3 position = this.transform.position;
      float intensity = WindManager.instance.intensity;
      Vector3 direction = WindManager.instance.direction with
      {
        y = 0.0f
      };
      this.m_horizontalAxis.Rotate(Vector3.forward, this.m_horizontalRotationMaxSpeed * Time.deltaTime * intensity, Space.Self);
      this.m_verticalAxis.rotation = Quaternion.Lerp(this.m_verticalAxis.rotation, Quaternion.LookRotation(-(Quaternion.Euler(0.0f, (float) (((double) Mathf.PerlinNoise(position.x + Time.time * this.m_verticalNoiseSpeed * intensity, position.y) - 0.5) * 2.0) * this.m_verticalNoiseAmount, 0.0f) * direction), this.m_verticalAxis.up), intensity * this.m_verticalRotationSpeed * Time.deltaTime);
    }
  }
}
