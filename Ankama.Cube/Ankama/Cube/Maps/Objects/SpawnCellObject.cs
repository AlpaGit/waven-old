// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Maps.Objects.SpawnCellObject
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Extensions;
using UnityEngine;

namespace Ankama.Cube.Maps.Objects
{
  public sealed class SpawnCellObject : MonoBehaviour
  {
    [SerializeField]
    private Transform m_rotationTransform;

    public void SetDirection(Ankama.Cube.Data.Direction direction)
    {
      Vector3 eulerAngles = this.m_rotationTransform.rotation.eulerAngles with
      {
        y = 0.0f
      };
      this.m_rotationTransform.rotation = direction.GetRotation() * Quaternion.Euler(eulerAngles);
    }
  }
}
