// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Render.WaterSpecularDirection
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System;
using UnityEngine;

namespace Ankama.Cube.Render
{
  [Obsolete]
  [ExecuteInEditMode]
  public class WaterSpecularDirection : MonoBehaviour
  {
    internal static readonly int _SpecularDir = Shader.PropertyToID(nameof (_SpecularDir));
    [SerializeField]
    private Transform m_specularDirection;

    private void Update() => Shader.SetGlobalVector(WaterSpecularDirection._SpecularDir, (Vector4) -this.m_specularDirection.forward);
  }
}
