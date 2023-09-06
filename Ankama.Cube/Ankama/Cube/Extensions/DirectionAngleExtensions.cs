// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Extensions.DirectionAngleExtensions
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Data;
using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Ankama.Cube.Extensions
{
  public static class DirectionAngleExtensions
  {
    [PublicAPI]
    public static DirectionAngle Inverse(this DirectionAngle value) => (DirectionAngle) -(int) value;

    [PublicAPI]
    public static Quaternion GetRotation(this DirectionAngle value) => Quaternion.AngleAxis(45f * (float) value, Vector3.up);

    [PublicAPI]
    public static Quaternion GetInverseRotation(this DirectionAngle value) => Quaternion.AngleAxis(-45f * (float) value, Vector3.up);

    [PublicAPI]
    public static DirectionAngle Add(this DirectionAngle value, DirectionAngle shift)
    {
      int num = (int) (value + (int) shift);
      return (DirectionAngle) (num + num / -5 * 8);
    }

    [PublicAPI]
    public static DirectionAngle Substract(this DirectionAngle value, DirectionAngle shift)
    {
      int num = value - shift;
      return (DirectionAngle) (num + num / -5 * 8);
    }

    [PublicAPI]
    public static DirectionAngle Multiply(this DirectionAngle value, int times)
    {
      int num1 = (int) value * times;
      int num2 = num1 % (8 * Math.Sign(num1));
      return (DirectionAngle) (num2 + num2 / -5 * 8);
    }
  }
}
