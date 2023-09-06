// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.Coord
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Protocols.CommonProtocol;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public struct Coord : IEquatable<Coord>, IEquatable<Vector2Int>
  {
    public readonly int x;
    public readonly int y;

    public Coord(int x, int y)
    {
      this.x = x;
      this.y = y;
    }

    public Coord(Coord copy)
    {
      this.x = copy.x;
      this.y = copy.y;
    }

    public Coord(Vector2Int vector)
    {
      this.x = vector.x;
      this.y = vector.y;
    }

    public override bool Equals(object obj)
    {
      switch (obj)
      {
        case null:
          return false;
        case Coord other1 when this.Equals(other1):
          return true;
        case Vector2Int other2:
          return this.Equals(other2);
        default:
          return false;
      }
    }

    public override int GetHashCode() => this.x * 397 ^ this.y;

    public bool Equals(Coord other) => this.x == other.x && this.y == other.y;

    public bool IsAlignedWith(Coord other) => this.x == other.x || this.y == other.y;

    public IEnumerable<Coord> StraightPathUntil(Coord other)
    {
      int increment;
      int newY;
      if (!this.Equals(other))
      {
        if (this.x == other.x)
        {
          increment = Math.Sign(other.y - this.y);
          for (newY = this.y + increment; newY != other.y; newY += increment)
            yield return new Coord(this.x, newY);
        }
        else if (this.y == other.y)
        {
          increment = Math.Sign(other.x - this.x);
          for (newY = this.x + increment; newY != other.x; newY += increment)
            yield return new Coord(newY, this.y);
        }
      }
    }

    public static bool operator ==(Coord value, Coord other) => value.x == other.x && value.y == other.y;

    public static bool operator !=(Coord value, Coord other) => value.x != other.x || value.y != other.y;

    public bool Equals(Vector2Int other) => this.x == other.x && this.y == other.y;

    public static explicit operator Vector2Int(Coord value) => new Vector2Int(value.x, value.y);

    public static bool operator ==(Coord value, Vector2Int vector) => value.x == vector.x && value.y == vector.y;

    public static bool operator !=(Coord value, Vector2Int vector) => value.x != vector.x || value.y != vector.y;

    [Pure]
    public CellCoord ToCellCoord() => new CellCoord()
    {
      X = this.x,
      Y = this.y
    };
  }
}
