// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Extensions.DirectionExtensions
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace Ankama.Cube.Extensions
{
  public static class DirectionExtensions
  {
    [PublicAPI]
    public static bool IsAxisAligned(this Ankama.Cube.Data.Direction value) => (value & Ankama.Cube.Data.Direction.SouthEast) != 0;

    [PublicAPI]
    public static Ankama.Cube.Data.Direction TurnCounterClockwise45(this Ankama.Cube.Data.Direction value) => value + 7 & Ankama.Cube.Data.Direction.NorthEast;

    [PublicAPI]
    public static Ankama.Cube.Data.Direction TurnCounterClockwise90(this Ankama.Cube.Data.Direction value) => value + 6 & Ankama.Cube.Data.Direction.NorthEast;

    [PublicAPI]
    public static Ankama.Cube.Data.Direction TurnClockwise45(this Ankama.Cube.Data.Direction value) => value + 1 & Ankama.Cube.Data.Direction.NorthEast;

    [PublicAPI]
    public static Ankama.Cube.Data.Direction TurnClockwise90(this Ankama.Cube.Data.Direction value) => value + 2 & Ankama.Cube.Data.Direction.NorthEast;

    [PublicAPI]
    public static Ankama.Cube.Data.Direction Inverse(this Ankama.Cube.Data.Direction value) => value + 4 & Ankama.Cube.Data.Direction.NorthEast;

    [PublicAPI]
    public static Ankama.Cube.Data.Direction Rotate(this Ankama.Cube.Data.Direction value, DirectionAngle angle) => value + (int) angle + 8 & Ankama.Cube.Data.Direction.NorthEast;

    [PublicAPI]
    public static Ankama.Cube.Data.Direction RotateInverse(
      this Ankama.Cube.Data.Direction value,
      DirectionAngle angle)
    {
      return (Ankama.Cube.Data.Direction) ((int) value - (int) angle - 8 & 7);
    }

    [PublicAPI]
    public static DirectionAngle DirectionAngleTo(this Ankama.Cube.Data.Direction from, Ankama.Cube.Data.Direction to) => (DirectionAngle) ((to - from + 4 & 7) - 4);

    [PublicAPI]
    public static Ankama.Cube.Data.Direction GetAxisAligned(this Ankama.Cube.Data.Direction value, Ankama.Cube.Data.Direction from)
    {
      int num1 = (int) from;
      int num2 = 2 * ((int) ((value - num1 + 4 & Ankama.Cube.Data.Direction.NorthEast) - 4) / 2);
      return (Ankama.Cube.Data.Direction) (num1 + num2 & 7);
    }

    [PublicAPI]
    public static Quaternion GetRotation(this Ankama.Cube.Data.Direction value) => Quaternion.AngleAxis(-45f * (float) (7 - value), Vector3.up);

    [PublicAPI]
    public static Quaternion GetInverseRotation(this Ankama.Cube.Data.Direction value) => Quaternion.AngleAxis(45f * (float) (7 - value), Vector3.up);
  }
}
