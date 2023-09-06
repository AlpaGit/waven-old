// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Animations.GameObjectRotation
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Cube.Animations
{
  [ExecuteInEditMode]
  public class GameObjectRotation : MonoBehaviour
  {
    [SerializeField]
    [UsedImplicitly]
    private Vector3 m_axis;
    [SerializeField]
    [UsedImplicitly]
    private Space m_space;

    private void Update() => this.transform.Rotate(this.m_axis * Time.deltaTime, this.m_space);
  }
}
