// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.Graphic
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Ankama.Animations
{
  [Serializable]
  public sealed class Graphic
  {
    [SerializeField]
    internal Texture2D atlas;
    [SerializeField]
    internal Vector2[] vertices;
    [SerializeField]
    internal Vector2[] uvs;
    [SerializeField]
    internal int[] triangles;
    [NonSerialized]
    internal int textureId;

    internal Graphic([NotNull] Texture2D atlas, [NotNull] Vector2[] vertices, [NotNull] Vector2[] uvs, [NotNull] int[] triangles)
    {
      this.atlas = atlas;
      this.vertices = vertices;
      this.uvs = uvs;
      this.triangles = triangles;
      this.textureId = atlas.GetInstanceID();
    }
  }
}
