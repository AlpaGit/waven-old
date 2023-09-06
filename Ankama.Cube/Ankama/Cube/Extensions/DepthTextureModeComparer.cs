// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Extensions.DepthTextureModeComparer
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Extensions
{
  public class DepthTextureModeComparer : IEqualityComparer<DepthTextureMode>
  {
    public static readonly DepthTextureModeComparer instance = new DepthTextureModeComparer();

    public bool Equals(DepthTextureMode x, DepthTextureMode y) => x == y;

    public int GetHashCode(DepthTextureMode obj) => (int) obj;
  }
}
