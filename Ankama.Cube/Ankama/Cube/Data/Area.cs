// Decompiled with JetBrains decompiler
// Type: Ankama.Cube.Data.Area
// Assembly: Ankama.Cube, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 60D6A912-05DF-4EF6-831C-8B7F076F35C3
// Assembly location: E:\WAVEN\Waven_Data\Managed\Ankama.Cube.dll

using Ankama.Cube.Extensions;
using System;
using UnityEngine;

namespace Ankama.Cube.Data
{
  public abstract class Area
  {
    public Vector2Int refCoord { get; protected set; }

    public Vector2Int[] occupiedCoords { get; protected set; }

    protected Area(Vector2Int refCoord) => this.refCoord = refCoord;

    public abstract Area GetCopy();

    public abstract Area GetAreaAt(Vector2Int position);

    public abstract Area GetMovedArea(Vector2Int offset);

    protected abstract bool AreaSpecificEquals(Area other);

    protected bool Equals(Area other) => this.refCoord.Equals(other.refCoord) && other.GetType() == this.GetType() && this.AreaSpecificEquals(other);

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if (this == obj)
        return true;
      return obj.GetType() == this.GetType() && this.Equals((Area) obj);
    }

    public override int GetHashCode() => this.refCoord.GetHashCode();

    public virtual void Move(Vector2Int offset)
    {
      this.refCoord += offset;
      Vector2Int[] occupiedCoords = this.occupiedCoords;
      int length = occupiedCoords.Length;
      for (int index = 0; index < length; ++index)
        occupiedCoords[index] += offset;
    }

    public virtual void MoveTo(Vector2Int newRefCoords)
    {
      Vector2Int vector2Int = newRefCoords - this.refCoord;
      this.refCoord = newRefCoords;
      Vector2Int[] occupiedCoords = this.occupiedCoords;
      int length = occupiedCoords.Length;
      for (int index = 0; index < length; ++index)
        occupiedCoords[index] += vector2Int;
    }

    public virtual bool Intersects(Area other)
    {
      Vector2Int[] occupiedCoords1 = this.occupiedCoords;
      int length1 = occupiedCoords1.Length;
      Vector2Int[] occupiedCoords2 = other.occupiedCoords;
      int length2 = occupiedCoords2.Length;
      for (int index1 = 0; index1 < length2; ++index1)
      {
        Vector2Int vector2Int = occupiedCoords2[index1];
        for (int index2 = 0; index2 < length1; ++index2)
        {
          if (occupiedCoords1[index2] == vector2Int)
            return true;
        }
      }
      return false;
    }

    public virtual bool Contains(Coord coord)
    {
      Vector2Int vector2Int = new Vector2Int(coord.x, coord.y);
      Vector2Int[] occupiedCoords = this.occupiedCoords;
      int length = this.occupiedCoords.Length;
      for (int index = 0; index < length; ++index)
      {
        if (occupiedCoords[index] == vector2Int)
          return true;
      }
      return false;
    }

    public virtual bool Contains(Area other)
    {
      Vector2Int[] occupiedCoords1 = this.occupiedCoords;
      int length1 = occupiedCoords1.Length;
      Vector2Int[] occupiedCoords2 = other.occupiedCoords;
      int length2 = occupiedCoords2.Length;
      for (int index1 = 0; index1 < length2; ++index1)
      {
        Vector2Int vector2Int = occupiedCoords2[index1];
        bool flag = false;
        for (int index2 = 0; index2 < length1; ++index2)
        {
          if (vector2Int == occupiedCoords1[index2])
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          return false;
      }
      return true;
    }

    public virtual int MinDistanceWith(Area other)
    {
      Vector2Int[] occupiedCoords1 = this.occupiedCoords;
      int length1 = occupiedCoords1.Length;
      Vector2Int[] occupiedCoords2 = other.occupiedCoords;
      int length2 = occupiedCoords2.Length;
      int num1 = int.MaxValue;
      for (int index1 = 0; index1 < length1; ++index1)
      {
        Vector2Int from = occupiedCoords1[index1];
        for (int index2 = 0; index2 < length2; ++index2)
        {
          int num2 = from.DistanceTo(occupiedCoords2[index2]);
          if (num2 < num1)
            num1 = num2;
        }
      }
      return num1;
    }

    public virtual int MinDistanceWith(Vector2Int other)
    {
      Vector2Int[] occupiedCoords = this.occupiedCoords;
      int length = occupiedCoords.Length;
      int num1 = int.MaxValue;
      for (int index = 0; index < length; ++index)
      {
        int num2 = occupiedCoords[index].DistanceTo(other);
        if (num2 < num1)
          num1 = num2;
      }
      return num1;
    }

    public virtual int MinSquaredDistanceWith(Area other)
    {
      Vector2Int[] occupiedCoords1 = this.occupiedCoords;
      int length1 = occupiedCoords1.Length;
      Vector2Int[] occupiedCoords2 = other.occupiedCoords;
      int length2 = occupiedCoords2.Length;
      int num1 = int.MaxValue;
      for (int index1 = 0; index1 < length1; ++index1)
      {
        Vector2Int vector2Int1 = occupiedCoords1[index1];
        for (int index2 = 0; index2 < length2; ++index2)
        {
          Vector2Int vector2Int2 = occupiedCoords2[index2];
          int num2 = Math.Max(Math.Abs(vector2Int1.x - vector2Int2.x), Math.Abs(vector2Int1.y - vector2Int2.y));
          if (num2 < num1)
            num1 = num2;
        }
      }
      return num1;
    }

    public virtual int MinSquaredDistanceWith(Vector2Int other)
    {
      Vector2Int[] occupiedCoords = this.occupiedCoords;
      int length = occupiedCoords.Length;
      int num1 = int.MaxValue;
      for (int index = 0; index < length; ++index)
      {
        Vector2Int vector2Int = occupiedCoords[index];
        int num2 = Math.Max(Math.Abs(vector2Int.x - other.x), Math.Abs(vector2Int.y - other.y));
        if (num2 < num1)
          num1 = num2;
      }
      return num1;
    }

    public virtual bool IsAlignedWith(Area other)
    {
      Vector2Int[] occupiedCoords1 = this.occupiedCoords;
      int length1 = occupiedCoords1.Length;
      Vector2Int[] occupiedCoords2 = other.occupiedCoords;
      int length2 = occupiedCoords2.Length;
      for (int index1 = 0; index1 < length1; ++index1)
      {
        Vector2Int vector2Int1 = occupiedCoords1[index1];
        for (int index2 = 0; index2 < length2; ++index2)
        {
          Vector2Int vector2Int2 = occupiedCoords2[index2];
          if (vector2Int1.x == vector2Int2.x || vector2Int1.y == vector2Int2.y)
            return true;
        }
      }
      return false;
    }

    public virtual bool IsAlignedWith(Vector2Int other)
    {
      Vector2Int[] occupiedCoords = this.occupiedCoords;
      int length = occupiedCoords.Length;
      for (int index = 0; index < length; ++index)
      {
        Vector2Int vector2Int = occupiedCoords[index];
        if (vector2Int.x == other.x || vector2Int.y == other.y)
          return true;
      }
      return false;
    }

    public virtual bool DistanceValid(Area other, int distance)
    {
      Vector2Int[] occupiedCoords1 = this.occupiedCoords;
      int length1 = occupiedCoords1.Length;
      Vector2Int[] occupiedCoords2 = other.occupiedCoords;
      int length2 = occupiedCoords2.Length;
      for (int index1 = 0; index1 < length1; ++index1)
      {
        Vector2Int from = occupiedCoords1[index1];
        for (int index2 = 0; index2 < length2; ++index2)
        {
          if (from.DistanceTo(occupiedCoords2[index2]) == distance)
            return true;
        }
      }
      return false;
    }

    public virtual bool Intersects(Vector2Int other)
    {
      Vector2Int[] occupiedCoords = this.occupiedCoords;
      int length = occupiedCoords.Length;
      for (int index = 0; index < length; ++index)
      {
        if (occupiedCoords[index] == other)
          return true;
      }
      return false;
    }

    public Direction? GetStrictDirection4To(Area other)
    {
      Vector2Int[] occupiedCoords = other.occupiedCoords;
      int length = occupiedCoords.Length;
      for (int index = 0; index < length; ++index)
      {
        Direction? strictDirection4To = this.GetStrictDirection4To(occupiedCoords[index]);
        if (strictDirection4To.HasValue)
          return strictDirection4To;
      }
      return new Direction?();
    }

    public Direction? GetStrictDirection4To(Vector2Int otherCoord)
    {
      Vector2Int[] occupiedCoords = this.occupiedCoords;
      int length = occupiedCoords.Length;
      for (int index = 0; index < length; ++index)
      {
        Direction? strictDirection4To = occupiedCoords[index].GetStrictDirection4To(otherCoord);
        if (strictDirection4To.HasValue)
          return new Direction?(strictDirection4To.Value);
      }
      return new Direction?();
    }
  }
}
