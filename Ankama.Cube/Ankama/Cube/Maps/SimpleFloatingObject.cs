// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.SimpleFloatingObject
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using UnityEngine;

namespace Ankama.Cube.Maps
{
  public class SimpleFloatingObject : MonoBehaviour
  {
    [SerializeField]
    private SimpleFloatingObjectData m_data;

    private void Update()
    {
      Transform parent = this.transform.parent;
      Vector3 vector3_1 = parent.InverseTransformDirection(Vector3.up);
      Vector3 vector3_2 = parent.InverseTransformDirection(Vector3.right);
      Vector3 vector3_3 = parent.InverseTransformDirection(Vector3.forward);
      SimpleFloatingObjectData data = this.m_data;
      Vector3 position = this.transform.position;
      float time = Time.time;
      float num1 = (float) (((double) Mathf.PerlinNoise(position.x + time * data.verticalSpeed, position.y) - 0.5) * 2.0) * data.verticalNoise;
      float num2 = (float) (((double) Mathf.PerlinNoise(position.x + time * data.rotationSpeed, position.y) - 0.5) * 2.0) * data.rotationNoise;
      float num3 = (float) (((double) Mathf.PerlinNoise(position.x, position.y + time * data.rotationSpeed) - 0.5) * 2.0) * data.rotationNoise;
      this.transform.localPosition = vector3_1 * num1;
      this.transform.localRotation = Quaternion.Euler(vector3_2 * num2 + vector3_3 * num3);
    }
  }
}
