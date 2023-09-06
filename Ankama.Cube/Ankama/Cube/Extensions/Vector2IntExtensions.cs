// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Extensions.Vector2IntExtensions
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Protocols.CommonProtocol;
using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Cube.Extensions
{
  public static class Vector2IntExtensions
  {
    [Pure]
    public static int DistanceTo(this Vector2Int from, Vector2Int to) => Mathf.Abs(to.x - from.x) + Mathf.Abs(to.y - from.y);

    [Pure]
    public static bool IsAdjacentTo(this Vector2Int from, Vector2Int to) => from.DistanceTo(to) == 1;

    [Pure]
    public static Ankama.Cube.Data.Direction? GetStrictDirection4To(
      this Vector2Int from,
      Vector2Int to)
    {
      Vector2Int vector2Int = to - from;
      if (vector2Int.x > 0 && vector2Int.y == 0)
        return new Ankama.Cube.Data.Direction?(Ankama.Cube.Data.Direction.NorthEast);
      if (vector2Int.x < 0 && vector2Int.y == 0)
        return new Ankama.Cube.Data.Direction?(Ankama.Cube.Data.Direction.SouthWest);
      if (vector2Int.x == 0 && vector2Int.y > 0)
        return new Ankama.Cube.Data.Direction?(Ankama.Cube.Data.Direction.NorthWest);
      return vector2Int.x == 0 && vector2Int.y < 0 ? new Ankama.Cube.Data.Direction?(Ankama.Cube.Data.Direction.SouthEast) : new Ankama.Cube.Data.Direction?();
    }

    [Pure]
    public static Ankama.Cube.Data.Direction GetDirectionTo(this Vector2Int from, Vector2Int to)
    {
      Vector2Int vector2Int = to - from;
      return vector2Int == Vector2Int.zero ? Ankama.Cube.Data.Direction.None : (Ankama.Cube.Data.Direction) (7 - Mathf.RoundToInt(Mathf.Atan2((float) vector2Int.y, (float) vector2Int.x) / 0.7853982f) & 7);
    }

    [Pure]
    public static CellCoord ToCellCoord(this Vector2Int value) => new CellCoord()
    {
      X = value.x,
      Y = value.y
    };

    [Pure]
    public static Vector2Int Rotate(this Vector2Int value, Quaternion rotation)
    {
      Vector3 vector3_1 = new Vector3((float) value.x, 0.0f, (float) value.y);
      Vector3 vector3_2 = rotation * vector3_1;
      return new Vector2Int(Mathf.RoundToInt(vector3_2.x), Mathf.RoundToInt(vector3_2.z));
    }
  }
}
