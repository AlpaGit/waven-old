// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Extensions.Vector2Extensions
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Ankama.Cube.Extensions
{
  public static class Vector2Extensions
  {
    [Pure]
    public static float DistanceTo(this Vector2 from, Vector2 to) => Mathf.Abs(to.x - from.x) + Mathf.Abs(to.y - from.y);

    [Pure]
    public static Vector2Int ToInt(this Vector2 value) => new Vector2Int((int) value.x, (int) value.y);

    [Pure]
    public static Vector2Int RoundToInt(this Vector2 value) => new Vector2Int(Mathf.RoundToInt(value.x), Mathf.RoundToInt(value.y));

    [Pure]
    public static Vector2Int FloorToInt(this Vector2 value) => new Vector2Int(Mathf.FloorToInt(value.x), Mathf.FloorToInt(value.y));

    [Pure]
    public static Vector2Int CeilToInt(this Vector2 value) => new Vector2Int(Mathf.CeilToInt(value.x), Mathf.CeilToInt(value.y));

    [Pure]
    public static Vector2 Clamp(this Vector2 value, float min, float max) => new Vector2(Mathf.Clamp(value.x, min, max), Mathf.Clamp(value.y, min, max));

    [Pure]
    public static Vector2 Clamp(this Vector2 value, Vector2 min, Vector2 max) => new Vector2(Mathf.Clamp(value.x, min.x, max.x), Mathf.Clamp(value.y, min.y, max.y));

    [Pure]
    public static Vector2 Rotate(this Vector2 v, float degrees)
    {
      double f = (double) degrees * (Math.PI / 180.0);
      float num1 = Mathf.Sin((float) f);
      float num2 = Mathf.Cos((float) f);
      float x = v.x;
      float y = v.y;
      return new Vector2((float) ((double) num2 * (double) x - (double) num1 * (double) y), (float) ((double) num1 * (double) x + (double) num2 * (double) y));
    }

    [Pure]
    public static Vector2 Inverse(this Vector2 v) => new Vector2(1f / v.x, 1f / v.y);

    [Pure]
    public static Vector2 Abs(this Vector2 v) => new Vector2(Mathf.Abs(v.x), Mathf.Abs(v.y));
  }
}
