// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.PlayerTombstone
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using UnityEngine;

namespace Ankama.Cube.Maps
{
  public sealed class PlayerTombstone : MonoBehaviour
  {
    [SerializeField]
    private float m_maxTiltAngle = 10f;

    private void Awake()
    {
      CameraHandler current = CameraHandler.current;
      float angle1 = (float) (180.0 - 45.0 * ((Object) null != (Object) current ? (double) current.mapRotation : 0.0));
      float angle2 = (float) (-(double) this.m_maxTiltAngle + 2.0 * (double) this.m_maxTiltAngle * (double) Random.value);
      this.transform.rotation = Quaternion.AngleAxis(angle1, Vector3.up) * Quaternion.AngleAxis(angle2, this.transform.right) * this.transform.rotation;
    }
  }
}
