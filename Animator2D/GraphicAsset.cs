// Decompiled with JetBrains decompiler
// Type: Ankama.Animations.GraphicAsset
// Assembly: Animator2D, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B1AA31E-B85F-49F3-86B3-33EE02F7513D
// Assembly location: E:\WAVEN\Waven_Data\Managed\Animator2D.dll

using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Animations
{
  [UsedImplicitly]
  public sealed class GraphicAsset : ScriptableObject
  {
    [SerializeField]
    internal Texture2D atlas;
    [SerializeField]
    internal Vector2[] vertices;
    [SerializeField]
    internal Vector2[] uvs;
    [SerializeField]
    internal int[] triangles;
  }
}
